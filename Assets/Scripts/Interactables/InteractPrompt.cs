using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractPrompt : MonoBehaviour
{
    public GameObject interactPrompt;
    public static bool cantInteract;
    private void Start()
    {
        EventManager.instance.DisableDialogue += DisablePrompt;
    }
    private void OnDisable()
    {
        EventManager.instance.DisableDialogue -= DisablePrompt;

    }
    private void OnTriggerEnter(Collider collider)
    {
        if(collider.CompareTag("Player"))
        {
            if(cantInteract == false)
            {
                interactPrompt.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            interactPrompt.SetActive(false); 
        }
    }

    private void DisablePrompt()
    {
        cantInteract = true;
        interactPrompt.SetActive(false);
    }

    private void Update()
    {
        //Check for key press then toggle off
    }
}
