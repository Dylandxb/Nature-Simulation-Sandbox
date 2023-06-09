using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIManager : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private GameObject inventoryPanel;
    [SerializeField] private CanvasGroup canvasGroup;

    private InvManager invManager;
    private bool invIsVisible = false;

    void Start()
    {
        invManager = GetComponent<InvManager>();
        canvasGroup.alpha = 1;
    }

    void Update()
    {
        //Toggle the transparency of the inventory panel
        if (Input.GetKeyDown(KeyCode.I))
        {
            invIsVisible = !invIsVisible;
            if (!invIsVisible)
            {
                canvasGroup.alpha = 0.5f;
            }
            if (invIsVisible)
            {
                canvasGroup.alpha = 1;
            }
        }

    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            //check if cursor is over a slot

            Debug.Log("right click");
        }
    }



}
