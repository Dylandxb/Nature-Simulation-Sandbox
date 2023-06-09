using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HungerController : MonoBehaviour
{
    [SerializeField] private GameObject hungerPrefab;
    public float spawnProbability;

    public float distanceBetweenCheck;
    private int count = 10;
    //Raycast checks
    public float heightOfCheck = 10.0f, rangeOfCheck = 20.0f;
    //130, 100
    //-106, -60
    public LayerMask ground;
    //Area of cover
    public Vector2 positivePos, negativePos;
    void Start()
    {
        
    }

    void Update()
    {
        SpawnHunger();
    }

    private void SpawnHunger()
    {

        for (float x = negativePos.x; x < positivePos.x; x += distanceBetweenCheck)
        {
            for (float z = negativePos.y; z < positivePos.y; z += distanceBetweenCheck)
            {
                RaycastHit hit;
                if (Physics.Raycast(new Vector3(x, heightOfCheck, z), Vector3.down, out hit, rangeOfCheck, ground))
                {
                    //if (spawnProbability > Random.Range(0, 101))
                    //{
                        //for (int i = 0; i < count; i++)
                        //{
                            Instantiate(hungerPrefab, hit.point, Quaternion.Euler(new Vector3(0, Random.Range(0, 360), 0)), transform);
                            //count++;
                        //}
                        //count = 0;
                    //}
                }
            }
        }

    }
}
