using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tool : MonoBehaviour, ICollectable
{
    public static event ToolCollected OnToolCollected;
    public delegate void ToolCollected(CollectableData collectableData);

    public CollectableData collectableData;

    [SerializeField] private float rotateSpeed;

    private void Start()
    {
        //Listens to collectable events and picks up
        EventManager.instance.PickupCollectable += Collect;

    }
    private void OnDisable()
    {
        EventManager.instance.PickupCollectable -= Collect;
    }
    public void Collect()
    {
        //On collect, add the tool to inv and destroy it in world
        OnToolCollected?.Invoke(collectableData);
        Destroy(gameObject);
    }

    void Update()
    {
        //Rotates the tools
        transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime);
    }
}
