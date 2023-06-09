using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HungerSpawner : MonoBehaviour
{
    public GameObject spawnHunger;
    public int numItemsToSpawn = 10;
    public int counter = 0;

    public float itemXSpread = 10;
    public float itemYSpread = 0.5f;
    public float itemZSpread = 10;
    void Start()
    {
        //Loop through number of prefabs to spawn
        for (int i = 0; i < numItemsToSpawn; i++)
        {
            SpreadHunger();

        }
    }

    void Update()
    {
        if (numItemsToSpawn == 0)
        {
            SpreadHunger();
        }
        if (counter == 0)
        {
            SpreadHunger();
        }
    }

    void SpreadHunger()
    {
        //Find random position in space
        Vector3 randPosition = new Vector3(Random.Range(-itemXSpread, itemXSpread), Random.Range(-itemYSpread, itemYSpread), Random.Range(-itemZSpread, itemZSpread)) + transform.position;
        //Create a copy and instantiate the object
        GameObject clone = Instantiate(spawnHunger, randPosition, spawnHunger.transform.rotation);
        counter++;
        if (clone == null)
        {
            counter--;
        }
    }
}
