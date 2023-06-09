using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumableSpawner : MonoBehaviour
{
    public GameObject spawnStrength;
    public int numItemsToSpawn = 10;
    public int counter = 0;

    public float itemXSpread = 10;
    public float itemYSpread = 0.5f;
    public float itemZSpread = 10;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < numItemsToSpawn; i++)
        {
            SpreadStrength();
            
        }
    }

    void Update()
    {
        if(numItemsToSpawn == 0)
        {
            SpreadStrength();
        }
        if(counter == 0)
        {
            SpreadStrength();
        }
        //Check if item counter is 0 then respawn them at a new desired location
    }
    void SpreadStrength()
    {
        Vector3 randPosition = new Vector3(Random.Range(-itemXSpread, itemXSpread), Random.Range(-itemYSpread, itemYSpread), Random.Range(-itemZSpread, itemZSpread)) + transform.position;
        GameObject clone = Instantiate(spawnStrength, randPosition, spawnStrength.transform.rotation);
        counter++;
        if(clone == null)
        {
            counter--;
        }
    }


}
