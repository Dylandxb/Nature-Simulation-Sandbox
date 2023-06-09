using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCIcon : MonoBehaviour
{
    public GameObject idleIcon;
    [SerializeField] private Transform lookAt;
    [SerializeField] private Vector3 offset;
    private Camera cam;

    private void Start()
    {
        cam = Camera.main;
    }

    private void Update()
    {
        //Fixes the game object to the NPC transform position
        //Offset is used to displace the icon around the npc
        Vector3 pos = cam.WorldToScreenPoint(lookAt.position + offset);
        if (transform.position != pos)
        {
            transform.position = pos;
        }
    }
}
