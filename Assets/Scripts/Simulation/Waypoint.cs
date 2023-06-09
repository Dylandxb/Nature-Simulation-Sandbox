using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Runtime.InteropServices.WindowsRuntime;

public class Waypoint : MonoBehaviour
{
    public Image image;
    public Transform target;
    public TextMeshProUGUI distanceText;
    Vector3 origin = new Vector3(0, 0, 0);
    private bool reached;
    void Start()
    {
        
    }

    void Update()
    {
        CheckIfReached();
        if (reached)
        {
            float minX = image.GetPixelAdjustedRect().width / 2;
            float maxX = Screen.width - minX;
            float minY = image.GetPixelAdjustedRect().height / 2;
            float maxY = Screen.height - minY;
            Vector2 pos = Camera.main.WorldToScreenPoint(target.position);
            if (Vector3.Dot((target.position - transform.position), transform.forward) < 0)
            {
                if (pos.x < Screen.width / 2)
                {
                    pos.x = maxX;
                }
                else
                {
                    pos.x = minX;
                }
            }

            pos.x = Mathf.Clamp(pos.x, minX, maxX);
            pos.y = Mathf.Clamp(pos.y, minY, maxY);
            if(image != null)
            {
                image.transform.position = pos;
                distanceText.text = Vector3.Distance(target.position, transform.position).ToString("0" + "m");
            }
            else
            {
                return;
            }

        }
        else
        {
            image.transform.position = origin;
            return;
        }

    }

    void CheckIfReached()
    {
        if (Vector3.Distance(target.position, PlayerController.instance.transform.position) < 1.5f)
        {
            Destroy(image);
            Destroy(distanceText);
        }
        reached = true;
    }
}
