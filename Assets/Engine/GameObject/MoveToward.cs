
using UnityEngine;

public class MoveToward : MonoBehaviour
{
    private float speed;
    private GameObject spawner;
    void Start()
    {
        speed = Shared.GetInstance().speed;
        spawner = GameObject.Find("Spawner");
    }

    // Update is called once per frame
    void Update()
    {
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, spawner.transform.position, step);
    }
}
