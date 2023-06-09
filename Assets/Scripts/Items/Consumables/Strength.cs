using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Strength : MonoBehaviour, IConsumable
{
    public ConsumableData strengthData;
    private float angleRotate = 15.0f;
    private float amplitude = 0.5f;
    private float frequency = 1.0f;
    Vector3 posOffset = new Vector3();
    Vector3 tempPos = new Vector3();
    void Start()
    {
        posOffset = transform.position;

    }
    public void Consume()
    {
        Destroy(gameObject);
        PlayerStats.instance.currentStrengthValue += strengthData.incrementValue;
    }

    void Update()
    {
        transform.Rotate(new Vector3(0f, Time.deltaTime * angleRotate, 0f), Space.World);
        tempPos = posOffset;
        //tempPos.y = Mathf.Sin(Time.fixedTime * Mathf.PI * frequency) * amplitude + 2.5f;
        transform.position = tempPos;
    }
}
