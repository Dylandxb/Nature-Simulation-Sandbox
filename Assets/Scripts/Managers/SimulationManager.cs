using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimulationManager : MonoBehaviour
{
    //Used to control VFX's
    [SerializeField] private ParticleSystem rainfall;

    void Start()
    {
        rainfall = gameObject.GetComponent<ParticleSystem>();
        //Stops rain from playing before listening to stop rain event
        rainfall.Stop();
        EventManager.instance.StartRain += PlayRain;
        EventManager.instance.StopRain += HaltRain;
    }

    private void OnDisable()
    {
        EventManager.instance.StartRain -= PlayRain;
        EventManager.instance.StopRain -= HaltRain;
    }
    void Update()
    {
        
    }
    //Subscribe the functions of playing the particle effect to the event
    void PlayRain()
    {
        rainfall.Play();
    }

    void HaltRain()
    {
        rainfall.Stop();
    }
}
