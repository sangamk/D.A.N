using Assets.Engine.DataModels;
using System;
using System.Collections.Generic;
using UnityEngine;

public class EventSpawner : MonoBehaviour
{
    private float nextSpawnTime;
    private readonly Queue<long> spawnTimes = new Queue<long>();
    private readonly Queue<IList<TimelineEvent>> timelineEvents = new Queue<IList<TimelineEvent>>();
    private int eventsOccured = 0;
    private int totalEvents = 0;
    [SerializeField]
    private EventFactory eventFactory; 

    void Start()
    {
        Planet planet = new JsonReader().LoadPlanet("example.json");
        if (planet == null)
        {
            Debug.LogError("No planet found");
        }
        var timeline = TimelineToDictionary(planet.timeline);
        SetupSpawnQueues(timeline);
        totalEvents = timeline.Count;
        nextSpawnTime = (Time.deltaTime + spawnTimes.Dequeue()) - SpawnTimeCalculator();
        Debug.Log("Start time " + nextSpawnTime);
    }

    void Update()
    {

        if (Time.time > nextSpawnTime && eventsOccured < totalEvents)
        {
            var eventsToOccur = timelineEvents.Dequeue();
            eventsOccured++;
            SpawnEvent(eventsToOccur);
            Debug.Log("+++ Event occured +++");
            Debug.Log("Event number: " + eventsOccured + "/" + totalEvents);
            Debug.Log("Spawn time: " + nextSpawnTime);
            Debug.Log("Spawn time adjustment: " + SpawnTimeCalculator());
            if (eventsOccured < totalEvents)
            {
                nextSpawnTime += spawnTimes.Dequeue() - nextSpawnTime - SpawnTimeCalculator();
                Debug.Log("Next Spawn time: " + nextSpawnTime);
            }
        }
    }

    public void SpawnEvent(IList<TimelineEvent> eventsToOccur)
    {
        for (int i = 0; i < eventsToOccur.Count; i++)
        {
            var prefab = eventFactory.CreateEvent(eventsToOccur[i]);
        }
    }

    private float SpawnTimeCalculator()
    {
        var relativePosition = transform.position;
        relativePosition.z += 50;
        var distance = Vector3.Distance(transform.position, relativePosition);
        return distance / Shared.GetInstance().speed;
    }

    private void SetupSpawnQueues(IDictionary<long, IList<TimelineEvent>> timeline)
    {
        foreach (KeyValuePair<long, IList<TimelineEvent>> entry in timeline)
        {
            spawnTimes.Enqueue(entry.Key);
            timelineEvents.Enqueue(entry.Value);
        }
    }

    private Dictionary<long, IList<TimelineEvent>> TimelineToDictionary(TimelineEvent[] timelineEvents)
    {
        Array.Sort(timelineEvents, new Comparison<TimelineEvent>((a, b) => a.time.CompareTo(b.time)));

        var result = new Dictionary<long, IList<TimelineEvent>>();
        for(int i = 0; i < timelineEvents.Length; i++)
        {
            if(result.ContainsKey(timelineEvents[i].time))
            {
                result[timelineEvents[i].time].Add(timelineEvents[i]);
            }
            else
            {
                result[timelineEvents[i].time] = new List<TimelineEvent>() { timelineEvents[i] };
            }

        }
        return result;
    }


}
