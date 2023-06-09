using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompositeInteraction : MonoBehaviour, IInteractable
{
    [SerializeField] private List<GameObject> interactableObjects;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Interact()
    {
        //List of game objects where interaction has same outcome on all. I.e switching light bulb on and off
        foreach (var interactableGameObject in interactableObjects)
        {
            var interactable = interactableGameObject.GetComponent<IInteractable>();
            if (interactable == null) continue;
            interactable.Interact();
            //Spawn();
        }
        //FIX ITS INTERACTING TWICE WITH ONE KEY PRESS
        //MAKES SENSE BECAUSE BOTH SPAWN THE SAME THING, RATHER USE THIS FOR PUTTING OUT ALL FIRES OR USING ALL PIECES OF WOOD TO CREATE A FIRE
    }

    public void DisplayUI()
    {
        return;
    }
}
