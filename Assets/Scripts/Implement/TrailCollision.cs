using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))] 
public class TrailCollision : MonoBehaviour
{
    TrailController trailController;
    List<Vector2> trailCollide = new List<Vector2>();
    void Start()
    {
        trailController = GetComponent<TrailController>();
    }

    // Update is called once per frame
    void Update()
    {
       // trailCollide = CalculateCollision();
       //Set transparency of line renderer component to 0 whilst collidinng with it
    }
}
