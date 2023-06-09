using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Date : MonoBehaviour
{
    public TextMeshProUGUI dateText;
    string date;
    void Start()
    {
        //Gets real time date, displays with TMPro
        date = System.DateTime.UtcNow.ToLocalTime().ToString("dd-MM-yyyy");
        dateText.text = date;
    }

    void Update()
    {
        
    }
}
