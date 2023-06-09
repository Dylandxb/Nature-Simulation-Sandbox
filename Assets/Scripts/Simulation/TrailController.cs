using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class TrailController : MonoBehaviour
{
    private LineRenderer lineRenderer;
    [SerializeField] private Transform[] trailPoints;
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        //GenerateCollider();
    }

    void Update()
    {
        //Number of positions to set equal to number of points
        lineRenderer.positionCount = trailPoints.Length;
        for (int i = 0; i < trailPoints.Length; i++)
        {
            lineRenderer.SetPosition(i, trailPoints[i].position);
        }
    }
    //Fade line renderer out on collision
    public void GenerateCollider()
    {
        MeshCollider collider = GetComponent<MeshCollider>();

        if(collider == null)
        {
            collider = gameObject.AddComponent<MeshCollider>();
        }

        Mesh mesh = new Mesh();
        lineRenderer.BakeMesh(mesh, true);
        collider.sharedMesh = mesh;
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("colliding");
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("colliding");
        }
    }
}
