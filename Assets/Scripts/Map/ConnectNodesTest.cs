using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectNodesTest : MonoBehaviour
{
    Vector3 pos1 = new Vector3(45f, 1.88f, 70.0f);
    Vector3 pos2 = new Vector3(62f, 1.88f, 70.0f);
    Vector3 pos3 = new Vector3(45f, 1.88f, 55.0f);
    Vector3 pos4 = new Vector3(62f, 1.88f, 55.0f);
    Vector3 pos5 = new Vector3(45f, 1.88f, 55.0f);  
    Vector3 pos6 = new Vector3(45f, 1.88f, 70.0f);  
    //Make this a safehouse healing zone, to rest and relax
    Vector3[] worldPositions = new Vector3[6];
    private LineRenderer lineRenderer;
    void Start()
    {
        //Initialize the transforms of the line to positions in an array
        worldPositions[0] = pos1;
        worldPositions[1] = pos2;
        worldPositions[2] = pos4;
        worldPositions[3] = pos5;
        worldPositions[4] = pos3;
        worldPositions[5] = pos6;
        //Debug.Log(worldPositions[0].ToString("0.000"));
        //Debug.Log(worldPositions[1].ToString("0.000"));
        DrawLineBetweenPoint();
    }

    void Update()
    {
        
    }
    struct MyStruct
    {
        int i, j;

        public MyStruct(int a, int b)
        {
            i = a;
            j = b;
        }
    }

    static MyStruct[] myTable = new MyStruct[3]
    {
   new MyStruct(0, 0),
   new MyStruct(1, 1),
   new MyStruct(2, 2)
    };

    void DrawLineBetweenPoint()
    {
        //Uses line renderer component to set variables
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.startColor = Color.green;
        lineRenderer.endColor = Color.green;
        lineRenderer.startWidth = 0.3f;
        lineRenderer.endWidth = 0.3f;
        lineRenderer.positionCount = worldPositions.Length;
        //Debug.Log("Line rend pos " + lineRenderer.positionCount.ToString());
        lineRenderer.SetPositions(worldPositions);
    }

    private void OnDrawGizmos()
    {
        //Visualize line in Scene view
        Gizmos.color = Color.green;
        Gizmos.DrawLine(pos1, pos2);
        Gizmos.DrawLine(pos2, pos4);
        Gizmos.DrawLine(pos4, pos3);
        Gizmos.DrawLine(pos3, pos1);
        Gizmos.DrawSphere(pos1, 0.2f);
        Gizmos.DrawSphere(pos2, 0.2f);
        Gizmos.DrawSphere(pos3, 0.2f);
        Gizmos.DrawSphere(pos4, 0.2f);
    }
}
