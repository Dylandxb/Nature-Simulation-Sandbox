using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    AudioSource audioSrc;
    [SerializeField] AudioClip toolPickup;

    private void OnEnable()
    {
        Tool.OnToolCollected += PlayToolCollect;
    }

    private void OnDisable()
    {
        //Unsubscribe from the event when the component is disabled
        Tool.OnToolCollected -= PlayToolCollect;         
    }


    public void PlayToolCollect(CollectableData collectableData)
    {
        AudioSource.PlayClipAtPoint(toolPickup, transform.position);
    }
}
