using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Campfire : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject fire;
    [SerializeField] private CollectableData flint;
    void Start()
    {
        fire.SetActive(false);
    }

    void Update()
    {
        
    }
    private void CheckForFlint()
    {
        //Return true if flint exists
        if (Inventory.instance.HasItem(flint))
        {
            //Enable particle system
            fire.SetActive(true);
            //Remove 1 flint from inv
            Inventory.instance.RemoveFromInv(flint);
        }
    }

    public void Interact()
    {
        //Call method from Interface
        CheckForFlint();
    }

    public void DisplayUI()
    {
        return;
    }
}

