using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "collectableDataConfig", menuName = "ScriptableObjects/Collectables/Collectable Data")]

public class CollectableData : ScriptableObject
{
    public string collectableName;
    public float value;
    public Sprite collectIcon;
}
