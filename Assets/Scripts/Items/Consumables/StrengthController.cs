using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrengthController : MonoBehaviour
{
    public float raycastDistance = 100.0f;
    public GameObject strengthPrefab;
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
        RaycastHit hit;
        if(Physics.Raycast(transform.position, Vector3.down, out hit, raycastDistance, ground))
        {

            Quaternion spawnRotation = Quaternion.FromToRotation(Vector3.up, hit.normal);
            Instantiate(strengthPrefab, hit.point, spawnRotation);
        }

            
        
    }
}
