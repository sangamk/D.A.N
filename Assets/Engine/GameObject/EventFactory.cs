using UnityEngine;
using Assets.Engine.DataModels;

public class EventFactory : MonoBehaviour
{
    public GameObject cube;
    public GameObject sphere;

    public GameObject CreateEvent(TimelineEvent timeline)
    {
        if (timeline.type == "cube")
        {
            return Instantiate(cube, CalculateStartPosition(timeline.offset), CalculateRotation());
        }
        else if (timeline.type == "sphere")
        {
            return Instantiate(sphere, CalculateStartPosition(timeline.offset), CalculateRotation());
        }
        return null;
    }

    private Vector3 CalculateStartPosition(Offset offset)
    {
        var startPosition = transform.position;
        startPosition.z += Shared.GetInstance().distance + offset.z;
        startPosition.x += offset.x;
        startPosition.y += offset.y;
        return startPosition;
    }

    private Quaternion CalculateRotation()
    {
        var startRotation = transform.rotation;
        return startRotation;
    }
}
