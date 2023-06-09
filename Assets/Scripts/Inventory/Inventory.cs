using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static event Action<List<InventoryItem>> OnInvChange;
    //Inv only stores CollectableData
    public List<InventoryItem> inventoryItems;
    //Item data, check key (inventory item)
    private Dictionary<CollectableData, InventoryItem> itemDictionary;
    public CollectableData useables;
    public CollectableData dropables;
    public CollectableData consumables;
    private Action<CollectableData> useCollectable;

    public static Inventory instance { get; set; }
    //TODO: CREATE INV UI, ADD A CLEAR INV BUTTON IN CANNVAS, TOGGLE INV WITH KEY PRESS 'I', Make inv interactable, select item with Mouse click

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            instance = this;
        }
    }
    void Start()
    {
        inventoryItems = new List<InventoryItem>();
        itemDictionary = new Dictionary<CollectableData, InventoryItem>();
    }
    private void OnEnable()
    {
        Tool.OnToolCollected += AddToInv; 
    }

    private void OnDisable()
    {
        //Removes event listeners when they dont need to be called
        Tool.OnToolCollected -= RemoveFromInv; 
    }
    
    public void AddConsumable(ConsumableData consumableData)
    {
        //Try add a consumable
    }
    public void AddToInv(CollectableData collectableData)
    {
        if (itemDictionary.TryGetValue(collectableData, out InventoryItem item))
        {
            //If item already exists in inventory, add it to current stack of item
            item.AddToStack();
            //Debug.Log("Item: " + item.collectableData.collectableName + " stack size is: " + item.stackSize);
            //Fire event when stack increased
            OnInvChange?.Invoke(inventoryItems);
        }
        else
        {
            //Create new inv item, store it in list, add item from list to dictionary
            InventoryItem newItem = new InventoryItem(collectableData);
            inventoryItems.Add(newItem);
            itemDictionary.Add(collectableData, newItem);
            //When new item is created and added to inv
            OnInvChange?.Invoke(inventoryItems);

        }
    }

    public void RemoveFromInv(CollectableData collectableData)
    {
        if (itemDictionary.TryGetValue(collectableData, out InventoryItem item))
        {
            //If item already exists in inventory, remove it from current stack of item
            item.RemoveFromStack();
            //Call drop item here
            if (item.stackSize == 0)
            {
                inventoryItems.Remove(item);
                itemDictionary.Remove(collectableData);
            }
            //If item is removed then unsubcribe from the event of changing the list
            OnInvChange?.Invoke(inventoryItems);

        }
    }

    public bool HasItem(CollectableData collectableData)
    {
        if(itemDictionary.TryGetValue((CollectableData)collectableData, out InventoryItem item))
        {
            //Returns true if the collectableData exists in the dictionary
            return true;
        }
        else
        {
            return false;
        }
    }
    public void DropItem()
    {
        //RemoveFromInv();
    }

    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.R))
        //{
            //Do it with right click in UI
           // RemoveFromInv(useables);
           // RemoveFromInv(dropables);
        //}

    }

    public void UseItem()
    {
        //Use equips
    }

    public void ClearInv()
    {
        itemDictionary.Clear();
        inventoryItems.Clear();
    }
}
