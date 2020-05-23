using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitPause : MonoBehaviour
{
    public float duration;
    void Start()
    {
        float pauseEndTime = Time.realtimeSinceStartup + duration;
        Time.timeScale = 0;
        while(Time.realtimeSinceStartup < pauseEndTime) {
        }
        Time.timeScale = 1;
    }
}
