using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using TMPro;
using UnityEngine;

public class DayCycle : MonoBehaviour
{
    [SerializeField] private float dayLength = 0.5f; //Length of day in minutes in inspector
    public float getDayLength
    {
        get
        {
            return dayLength;
        }
    }
    [SerializeField][Range(0f, 1f)] private float timeOfDay; //Time of day from 0-1 as a fraction
    public float getTimeOfDay
    {
        get
        {
            return timeOfDay;
        }
    }
    [SerializeField] private int dayCount = 0; //Number of days cycled through since start
    public int getDayCount
    {
        get
        {
            return dayCount;
        }
    }
    private float timeScale = 100.0f; //Converts realtime to gametime

    [SerializeField] private int yearNumber = 0;
    public int getYearNumber
    {
        get
        {
            return yearNumber;
        }
    }
    [SerializeField] private int yearLength = 100;
    public float getYearLength
    {
        get
        {
            return yearLength;
        }
    }

    public bool pause = false;
    [SerializeField] private Transform sunRotation;
    [SerializeField] private Transform seasonalRotation;
    [SerializeField] private Light sunLight;
    [SerializeField] private AnimationCurve timeCurve;
    private float timeCurveNormalization;
    private float lightIntensity;
    [SerializeField] private float baseSunIntensity = 1f; //Minimum intensity at rise and set
    [SerializeField] private float sunVariation = 1.5f; //Change in intensity over the day to noon or from noon
    [SerializeField] private Gradient sunGradient; //Colour of directional light
    [SerializeField]
    [Range(-45f, 45f)] private float maxSeasonalTilt;
    public bool isSleeping;
    void Start()
    {
        //Start time of day 
        timeOfDay = 0.19f;
        NormalizeTimeCurve();
        EventManager.instance.StartSleep += PlayerSleep;
        EventManager.instance.EndSleep += PlayerWake;
    }

    private void OnEnable()
    {
    }

    private void OnDisable()
    {
        EventManager.instance.StartSleep -= PlayerSleep;
        EventManager.instance.EndSleep -= PlayerWake;
    }
    void Update()
    {
        if (!pause)
        {
            //Freeze time whilst paused
            UpdateTimeScale();
            UpdateTime();
        }
        SunIntensity();
        ChangeSunRotate();
        ChangeSunColour();
        //Sunrise = 0.25f
        //Sunset = 0.7f
        //0.1f = 1 real time second
        
        //Wake player up at 0.22, sleep player at 0.9
        //Check if is night and player as used R to rest then trigger sleep event
        //Display ui to sleep
        if (timeOfDay >= 0.9f )
        {
            //Increase time of day temporarily to speed up transition
            //Start sleeping when meeting condition
            EventManager.instance.StartSleepEvent();
        }
        else if (timeOfDay >= 0.015f && timeOfDay <= 0.0152f)
        {
            //Call event to stop sleep between 2 points
            EventManager.instance.EndSleepEvent();
        }
        if(timeOfDay > 0.25f && timeOfDay < 0.6f)
        {   
            EventManager.instance.StartNPCWalk();
        }
        //Trigger rain at random intervals through the day
        if(timeOfDay >= 0.6f && timeOfDay <=1.0f)
        {
            //Trigger rain 
            EventManager.instance.Raining();
            EventManager.instance.SetNPCIdle();
        }
        if(timeOfDay >= 0.20f && timeOfDay < 0.6f)
        {
            EventManager.instance.HaltRain();
        }
    }

    public void PlayerSleep()
    {
        //Prevents player from moving during sleep
        isSleeping = true;
        PlayerController.instance.FreezePlayer();
        StartCoroutine(CountSleep());
    }
    IEnumerator CountSleep()
    {
        yield return new WaitForSeconds(10.0f);
    }

    public void PlayerWake()
    {
        //Re-enables key presses to move
        isSleeping = false;
        PlayerController.instance.UnFreezePlayer();
    }
    private void UpdateTimeScale()
    {
        //Time passes 24 times faster in game than realtime
        timeScale = 24 / (dayLength / 60);
        //Change timescale based on time curve
        timeScale *= timeCurve.Evaluate(timeOfDay);
        //Track day length to a target value
        timeScale /= timeCurveNormalization;
    }

    private void UpdateTime()
    {
        timeOfDay += Time.deltaTime * timeScale / 86400;
        if (timeOfDay > 1)
        {
            //Reset time of day back to the beginning on a new day
            dayCount++;
            timeOfDay -= 1;
            if (dayCount > yearLength)
            {
                yearNumber++;
                dayCount = 0;
            }
        }
    }

    private void ChangeSunRotate()
    {
        float angle = timeOfDay * 360f;
        sunRotation.transform.localRotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
        float seasonAngle = -maxSeasonalTilt * Mathf.Sin(dayCount / yearLength * Mathf.PI);
        seasonalRotation.localRotation = Quaternion.Euler(new Vector3(seasonAngle, 0f, 0f));
    }

    private void SunIntensity()
    {
        lightIntensity = Vector3.Dot(sunLight.transform.forward, Vector3.down); //Dot product of the forward x and negative y
        lightIntensity = Mathf.Clamp01(lightIntensity); //Clamp intensity between 0 and 1
        sunLight.intensity = lightIntensity * sunVariation + baseSunIntensity; //Sets intensity of Light to the new value
    }

    private void ChangeSunColour()
    {
        sunLight.color = sunGradient.Evaluate(lightIntensity); 
    }

    private void NormalizeTimeCurve()
    {
        float stepSize = 0.01f;
        int numberOfSteps = Mathf.FloorToInt(1f / stepSize);
        float curveTotal = 0;
        for (int i =0; i < numberOfSteps; i++)
        {
            curveTotal += timeCurve.Evaluate(i * stepSize);
        }
        timeCurveNormalization = curveTotal / numberOfSteps;

    }




}
