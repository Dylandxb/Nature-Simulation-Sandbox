using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.ComponentModel;

public class NPCInteract : MonoBehaviour, IInteractable
{
    [SerializeField] private KeyCode interactKey;
    public GameObject helloMsg;
    private InteractPrompt prompt;
    public static bool isInDialogue;
    [SerializeField] private GameObject guideButton;
    [SerializeField] private GameObject wanderButton;
    [SerializeField] private GameObject wander;
    [SerializeField] private GameObject guide;
    private bool hasInteracted;
    private bool canInteract;
    void Start()
    {
        guideButton.SetActive(false);
        wanderButton.SetActive(false);
        wander.SetActive(false);
        guide.SetActive(false);
        prompt = GetComponent<InteractPrompt>();
        canInteract = true;
        //Listen to dialogue event
        EventManager.instance.DisableDialogue += CanInteract;
    }
    private void OnDisable()
    {
        EventManager.instance.DisableDialogue += CanInteract;

    }

    void Update()
    {
        if(isInDialogue == true)
        {
            prompt.interactPrompt.SetActive(false);
        }
    }


    private void OnTriggerExit(Collider coll)
    {
        if (coll.CompareTag("Player"))
        {
            //Upon leaving NPC range, set cam back to regular view
            PlayerController.instance.SwitchCamView(PlayerController.CamView.Regular);
            isInDialogue = false;
            //Un interact when dialogue finishes or key is pressed again
            helloMsg.SetActive(false);
        }
    }

    void DisablePrompt()
    {
    }
    public void DisplayUI()
    {
        //Display dialogue
        helloMsg.SetActive(true);
        StartCoroutine(ButtonDisplay());
        //Check if guided has been clicked then disable button for future interactions
        //Same with wander
        //Disable buttons once interacted with
        //Add a Button script to the buttons, check for an on click event then disable them

    }
    private IEnumerator ButtonDisplay()
    {
        //Check if buttons have already been displayed then wait until display
        yield return new WaitForSeconds(6.0f);
        guideButton.SetActive(true);
        wanderButton.SetActive(true);
        guide.SetActive(true);
        wander.SetActive(true); 
    }

    public void CanInteract()
    {
        //Determines whether or not player can interact with NPC depending on its state
        canInteract = false;
        prompt.interactPrompt.SetActive(false);
    }
    public void Interact()
    {
        if (canInteract)
        {
            //Interact enabled in: Idle, Dialogue, Guide states
            isInDialogue = true;
            //Calls the dialogue event when in interaction
            //Event has listeners
            EventManager.instance.DialogueEvent();
            //Sets cinemachine to dialogue cam
            PlayerController.instance.SwitchCamView(PlayerController.CamView.NPCView);
        }

    }

}
