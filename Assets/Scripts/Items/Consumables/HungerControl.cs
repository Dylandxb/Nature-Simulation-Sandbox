using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HungerControl : MonoBehaviour
{
    public float raycastDistance = 10.0f;
    public GameObject hungerPrefab;
    public LayerMask ground;
    void Start()
    {
        SpawnWithRaycast();
    }

    void Update()
    {
    }

    private void SpawnWithRaycast()
    {
        //Finds nearest position to the ground
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, raycastDistance, ground))
        {
            //Adds rotation to the angle at which the ray hits the ground
            Quaternion spawnRotation = Quaternion.FromToRotation(Vector3.up, hit.normal);
            //Once found, spawn the prefab at point
            Instantiate(hungerPrefab, hit.point, spawnRotation);
        }
    }
}
