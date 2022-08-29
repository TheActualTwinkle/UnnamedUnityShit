using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class DayNightCycle : MonoBehaviour
{
    public EventHandler<TimeInfo> HourPassed;

    [SerializeField] private Light2D globalLight;

    // Time when sun is brightest.
    [Range(0, 23.99f)]
    [SerializeField] private float sunTimePeek;
    public float SunTimePeek { get => sunTimePeek; }

    // Hour in game equals the gameHourDuration (in seconds).
    [Range(1, 240f)]
    [SerializeField] private float gameHourDuration;

    [ReadOnly]
    [SerializeField] private float presentTime;
    public float PresentTime { get => presentTime; }

    private float timeStep;
    private float lightIntensity;

    private void Start()
    {
        presentTime = DateTime.Now.Hour + DateTime.Now.Minute / 60f;
    }

    private void FixedUpdate()
    {        
        timeStep = Time.fixedDeltaTime / gameHourDuration;

        if (timeStep > 0)
        {
            presentTime += timeStep;
            if (presentTime >= 24f)
            {
                presentTime = 0f;
                HourPassed?.Invoke(this, new TimeInfo(presentTime));
            }

            // If new hour began.
            if ((int)(presentTime - timeStep) != (int)presentTime)
            {
                HourPassed?.Invoke(this, new TimeInfo(presentTime));
            }
        }

        SetupBrightness();
    }

    private void SetupBrightness()
    {
        if (presentTime <= sunTimePeek)
        {
            lightIntensity = Mathf.Clamp(presentTime / sunTimePeek, 0.1f, 1f);
        }
        else
        {
            lightIntensity = Mathf.Clamp((24f - presentTime) / (24f - sunTimePeek), 0.1f, 1f);
        }

        globalLight.intensity = lightIntensity;
    }
}
