using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.VisualScripting;

public class Stamina : MonoBehaviour, IConsumable
{
    public ConsumableData staminaData;
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
        PlayerStats.instance.currentStaminaValue += staminaData.incrementValue;
    }

    void Update()
    {
        transform.Rotate(new Vector3(0f, Time.deltaTime * angleRotate, 0f), Space.World);
        tempPos = posOffset;
        //tempPos.y = Mathf.Sin(Time.fixedTime * Mathf.PI * frequency) * amplitude + 2.5f;
        transform.position = tempPos;
    }


}
