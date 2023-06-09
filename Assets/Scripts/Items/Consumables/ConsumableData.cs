using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "consumableDataConfig", menuName = "ScriptableObjects/Consumables/Consumable Data")]

public class ConsumableData : ScriptableObject
{
    public string consumableName;
    public float incrementValue;
}
