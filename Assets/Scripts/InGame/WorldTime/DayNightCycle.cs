using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class DayNightCycle : MonoBehaviour
{
    public static event Action<TimeInfo> HourPassedEvent;

    public static DayNightCycle Instance { get; private set; }

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
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        presentTime = DateTime.Now.Hour + DateTime.Now.Minute / 60f;
    }

    private void FixedUpdate()
    {        
        timeStep = Time.fixedDeltaTime / gameHourDuration;

        IncreaceTime();
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

    private void IncreaceTime()
    {
        presentTime += timeStep;
        if (presentTime >= 24f)
        {
            presentTime = 0f;
            HourPassedEvent?.Invoke(new TimeInfo(presentTime));
        }

        // If new hour began.
        if ((int)(presentTime - timeStep) != (int)presentTime)
        {
            HourPassedEvent?.Invoke(new TimeInfo(presentTime));
        }
    }
}
