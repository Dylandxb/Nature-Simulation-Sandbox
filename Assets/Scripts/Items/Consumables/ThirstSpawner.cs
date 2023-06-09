using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirstSpawner : MonoBehaviour
{
    public GameObject spawnThirst;
    public int numItemsToSpawn = 10;
    public int counter = 0;

    public float itemXSpread = 10;
    public float itemYSpread = 0.5f;
    public float itemZSpread = 10;
    void Start()
    {
        for (int i = 0; i < numItemsToSpawn; i++)
        {
            SpreadThirst();

        }
    }

    void Update()
    {
        if (numItemsToSpawn == 0)
        {
            SpreadThirst();
        }
        if (counter == 0)
        {
            SpreadThirst();
        }
    }

    void SpreadThirst()
    {
        Vector3 randPosition = new Vector3(Random.Range(-itemXSpread, itemXSpread), Random.Range(-itemYSpread, itemYSpread), Random.Range(-itemZSpread, itemZSpread)) + transform.position;
        GameObject clone = Instantiate(spawnThirst, randPosition, spawnThirst.transform.rotation);
        counter++;
        if (clone == null)
        {
            counter--;
        }
    }
}
