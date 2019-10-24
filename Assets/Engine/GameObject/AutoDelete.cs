using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDelete : MonoBehaviour
{
    private float timerInSeconds = 5; //counting timer
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        timerInSeconds -= Time.deltaTime;
        if (timerInSeconds < 0.0f)
        {
            DeleteSelf();
        }
    }

    void DeleteSelf()
    {
        Destroy(gameObject);
    }
}
