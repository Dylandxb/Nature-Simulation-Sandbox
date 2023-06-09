using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class CameraController : MonoBehaviour
{
    private CinemachineFreeLook camLook;
    void Start()
    {
        camLook = GetComponent<CinemachineFreeLook>();
    }

    void Update()
    {
        //If left ctrl from input manager is pressed, then recenter cinemachine camera to target player
        if (Input.GetAxis("Reset Camera") == 1)
        {
            camLook.m_RecenterToTargetHeading.m_enabled = true;
        }
        else
        {
            camLook.m_RecenterToTargetHeading.m_enabled = false;
        }
    }
}
