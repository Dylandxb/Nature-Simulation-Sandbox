using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaminaController : MonoBehaviour
{
    [SerializeField] private GameObject staminaPrefab;
    public int staminaCount = 5;
    public LayerMask ground;
    public Vector2 positivePos, negativePos;
    public float distanceBetweenCheck;
    public float heightOfCheck = 10.0f, rangeOfCheck = 20.0f;
    private Transform tr;

    private void Awake()
    {
        tr = transform;
    }
    void Start()
    {
        SpawnStamina();
    }

    // Update is called once per frame
    void Update()
    {
        if (staminaCount <= 0)
        {
            SpawnStamina();
        }
        //Debug.Log(staminaCount);
    }

    void SpawnStamina()
    {
        for (int i = 0; i < staminaCount; i++)
        {
            Vector2 randCircle = Random.insideUnitCircle * rangeOfCheck;
            Vector3 spawnPos = new Vector3(tr.position.x + randCircle.x, 100, tr.position.z + randCircle.y);
            if (Physics.Raycast(spawnPos, Vector3.down, out RaycastHit hit,150, ground))
            {
                //if (spawnProbability > Random.Range(0, 101))
                //{
                //for (int i = 0; i < count; i++)
                //{
                GameObject stamina = Instantiate(staminaPrefab);
                stamina.transform.SetPositionAndRotation(new Vector3(spawnPos.x, hit.point.y + heightOfCheck, spawnPos.z), tr.rotation);
                stamina.transform.parent = tr;
                //Instantiate(staminaPrefab, hit.point, Quaternion.identity);
                Debug.Log("spawned");
                staminaCount++;
                //count++;
                //}
                //count = 0;
                //}
            }


        }
    }
}
