using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Timer : MonoBehaviour
{

    public static S_Timer instance;

    private TimeSpan timeSpan;
    private bool timePaused;
    private float elapsedTime;
    public string formattedTime;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        timePaused = true;
    }

    public void StartTimer()
    {
        timePaused = false;
        elapsedTime = 0f;

        StartCoroutine(UpdateTimer());
    }

    public void StopTimer()
    {
        timePaused = true;
    }


    private IEnumerator UpdateTimer()
    { 
        while (!timePaused)
        {
            elapsedTime += Time.deltaTime;
            timeSpan = TimeSpan.FromSeconds(elapsedTime);
            if (timeSpan.Hours > 0) formattedTime = timeSpan.Hours + ":";
            else formattedTime = "";
            formattedTime += timeSpan.ToString("mm':'ss");

            yield return null;
        }
    }
}
