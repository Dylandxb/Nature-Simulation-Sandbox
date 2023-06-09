using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ConsumableController : MonoBehaviour
{
    private PlayerStats stats;
    public GameObject[] consumablePrefabs;
    public GameObject[] instantiatedPrefabs;
    public float spawnProbability;

    public float distanceBetweenCheck;
    //Raycast checks
    public float heightOfCheck = 10.0f, rangeOfCheck = 20.0f;
    //130, 100
    //-106, -60
    public LayerMask ground;
    //Area of cover
    public Vector2 positivePos, negativePos;
    bool objDestroyed;
    void Start()
    {
        stats = GetComponent<PlayerStats>();
        SpawnConsumables();
    }

    void Update()
    {
        CheckForRespawn();
    }

    public void ConsumeItem(ConsumableData consumableData)
    {
        consumableData.GetComponent<IConsumable>().Consume();
    }

    private void SpawnConsumables()
    {
        for(float x  = negativePos.x; x < positivePos.x; x += distanceBetweenCheck)
        {
            for (float z = negativePos.y; z < positivePos.y; z += distanceBetweenCheck)
            {
                RaycastHit hit;
                if(Physics.Raycast(new Vector3(x, heightOfCheck, z), Vector3.down, out hit, rangeOfCheck, ground))
                {
                    if(spawnProbability > Random.Range(0, 101))
                    {
                        instantiatedPrefabs = new GameObject[consumablePrefabs.Length];
                        for (int i = 0; i < consumablePrefabs.Length; i++)
                        {
                            instantiatedPrefabs[i] = Instantiate(consumablePrefabs[i], hit.point, Quaternion.Euler(new Vector3(0, Random.Range(0, 360), 0)), transform);
                        }
                    }
                    
                }
            }
        }
    }

    private bool CheckForRespawn()
    {
        //Check if all childs in the transform have been destroyed
        //return true
        //if true then call SpawnConsumbales
        foreach(Transform child in transform)
        {
            Destroy(child.gameObject);
            objDestroyed = true;
        }
        if (objDestroyed)
        {
            SpawnConsumables();
            return true;
        }
        else
        {
            return false;
        }
    }
}
