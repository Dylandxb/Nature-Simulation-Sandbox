using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class EventManager : MonoBehaviour
{
    //List of all events taking place throughout scripts with Singleton
    public static EventManager instance;
    public event Action EnterDialogue;
    public event Action DisableDialogue;
    public event Action StartSleep;
    public event Action EndSleep;
    public event Action PickupCollectable;
    public event Action StartRain;
    public event Action StopRain;
    public event Action NPCWalk;
    public event Action NPCIdle;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }
    public void DialogueEvent()
    {
        EnterDialogue?.Invoke();
    }

    public void StopListeningDialogue()
    {
        DisableDialogue?.Invoke();
    }

    public void StartSleepEvent()
    {
        StartSleep?.Invoke();
        //In sleep event, trigger player pref save, transition UI lasting 10 seconds of screen darkening, wake up in same pos and reset time of day back to dawn, freeze player movement
    }

    public void EndSleepEvent()
    {
        //End of sleep, player wakes up. Transition is over and stats are loaded. Player can move again
        EndSleep?.Invoke();
    }
    public void CollectablePickupEvent()
    {
        PickupCollectable?.Invoke();
    }

    public void Raining()
    {
        StartRain?.Invoke();
    }

    public void HaltRain()
    {
        StopRain?.Invoke();
    }

    public void StartNPCWalk()
    {
        NPCWalk?.Invoke();
    }

    public void SetNPCIdle()
    {
        NPCIdle?.Invoke();
    }
}
