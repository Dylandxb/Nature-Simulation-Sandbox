using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class InventorySlot : MonoBehaviour
{
    public Image icon;
    public TextMeshProUGUI itemLabelText;
    public TextMeshProUGUI itemStackSizeText;

    public void ClearSlot()
    {
        //Disables all active UI
        icon.enabled = false;
        itemLabelText.enabled = false;
        itemStackSizeText.enabled = false;
    }
    //Draws the inv item data to the UI slot
    public void DrawSlot(InventoryItem invItem)
    {
        if (invItem == null)
        {
            //If invItem is null
            ClearSlot();
            return;
        }

        //Set UI elements back to true
        icon.enabled = true;
        itemLabelText.enabled = true;
        itemStackSizeText.enabled = true;

        itemStackSizeText.text = invItem.stackSize.ToString();
        icon.sprite = invItem.collectableData.collectIcon;
        itemLabelText.text = invItem.collectableData.collectableName;
    }
}
