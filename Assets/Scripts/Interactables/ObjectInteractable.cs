using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectInteractable : MonoBehaviour, IInteractable
{
    public GameObject[] pickUps;
    public GameObject[] instantiatedObjects;

   // public static event Action interact;


    private void OnTriggerEnter(Collider other)
    {

    }
    public void DisplayUI()
    {
      
    }
    //Use composite interaction
    public void Interact()
    {
        Debug.Log("Composite Object Interact");
        instantiatedObjects = new GameObject[pickUps.Length];
        for (int i = 0; i < pickUps.Length; i++)
        {
            instantiatedObjects[i] = Instantiate(pickUps[i]);
        }
    }
}
