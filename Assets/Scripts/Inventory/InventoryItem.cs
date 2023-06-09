using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class InventoryItem 
{
    public CollectableData collectableData;
    public int stackSize = 1; //Prevents index starting from 0

    //Construct inventory
    public InventoryItem(CollectableData collectable)
    {
        collectableData = collectable;
    }
    public void AddToStack()
    {
        stackSize++;

    }


    public void RemoveFromStack()
    {
        stackSize--;

    }
}
