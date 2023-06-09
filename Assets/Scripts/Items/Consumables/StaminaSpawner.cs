using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaminaSpawner : MonoBehaviour
{
    public GameObject spawnStamina;
    public int numItemsToSpawn = 10;
    public int counter = 0;

    public float itemXSpread = 10;
    public float itemYSpread = 0.5f;
    public float itemZSpread = 10;
    void Start()
    {
        for (int i = 0; i < numItemsToSpawn; i++)
        {
            SpreadStamina();

        }
    }

    void Update()
    {
        if (numItemsToSpawn == 0)
        {
            SpreadStamina();
        }
        if (counter == 0)
        {
            SpreadStamina();
        }
    }

    void SpreadStamina()
    {
        Vector3 randPosition = new Vector3(Random.Range(-itemXSpread, itemXSpread), Random.Range(-itemYSpread, itemYSpread), Random.Range(-itemZSpread, itemZSpread)) + transform.position;
        GameObject clone = Instantiate(spawnStamina, randPosition, spawnStamina.transform.rotation);
        counter++;
        if (clone == null)
        {
            counter--;
        }
    }
}
