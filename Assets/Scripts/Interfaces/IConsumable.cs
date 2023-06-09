using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IConsumable 
{
    //Method for all objects in scene which are consumed immediately upon collision
    public void Consume();
    //Pickup holds the item in hand and waits for a key press to consume it
    //public void PickUp();
    //public void Consume(PlayerStats stats);
}
