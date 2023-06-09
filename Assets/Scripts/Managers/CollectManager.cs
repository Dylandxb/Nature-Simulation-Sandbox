using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectManager : MonoBehaviour
{
    //Further extend to a pickup interaction
    private void OnTriggerEnter(Collider coll)
    {
        ICollectable collectable = coll.GetComponent<ICollectable>();
        IConsumable consumable = coll.GetComponent<IConsumable>();
        if (collectable != null)
        {
            //Gets ICollectable component and calls collect method on all objects inhering the interface
            collectable.Collect();
        }
        else if (consumable != null)
        {
            //Gets IConsumable component and calls consume method on all objects inhering the interface
            consumable.Consume();
            //return;
        }
        else
        {
            return;
        }
    }

}
