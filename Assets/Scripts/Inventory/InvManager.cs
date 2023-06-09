using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InvManager : MonoBehaviour, IPointerClickHandler 
{
    public GameObject slotPrefab;
    //Sets size of inv slot list to 5
    public List<InventorySlot> invSlots = new List<InventorySlot>(5);

    private void OnEnable()
    {
        Inventory.OnInvChange += DrawInv;
    }

    private void OnDisable()
    {
        Inventory.OnInvChange -= DrawInv;
    }
    public void ResetInv()
    {
        foreach (Transform transform in transform)
        {
            Destroy(transform.gameObject);

        }
        //Destroy the child transforms in the list then reset it
        invSlots = new List<InventorySlot>(5);
    }

    void DrawInv(List<InventoryItem> inv)
    {
        //Reset inv each time before drawing new list
        ResetInv();

        for (int i = 0; i < invSlots.Capacity; i++)
        {
            //create 5 slots
            InvSlot();
        }

        for (int i = 0; i < inv.Count; i++)
        {
            //For every index position in the array, draw a slot at that position 
            invSlots[i].DrawSlot(inv[i]);
        }
    }

    void InvSlot()
    {
        GameObject newSlot = Instantiate(slotPrefab);
        //Sets parent slot of new transform slot to false
        newSlot.transform.SetParent(transform, false);
        //Get component of new inv slot
        InventorySlot newInvSlotComp = newSlot.GetComponent<InventorySlot>();
        newInvSlotComp.ClearSlot();
        //Clear the slot and add it to the list of new slots
        invSlots.Add(newInvSlotComp);
    }


    private void Update()
    {
        if (Input.GetKey(KeyCode.N))                            
        {
            //for each slot in Slots
            //clearSlot()
            //Resets inventory slots, calls function and then redraws the slots
            Inventory.instance.ClearInv();
            ResetInv();
            for (int i = 0; i < invSlots.Capacity; i++)
            {
                //create 5 slots
                InvSlot();
            }
            //Check for mouse pointer over slot
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right )
        {
            //check if cursor is over a slot
            
            Debug.Log("right click");
        }
    }
}
//Right clicking object drops it out of inventory in direction player is facing
//Left click consumes a consumable