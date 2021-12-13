using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName = "Items/SimpleItem")]
public class ItemData : ScriptableObject
{
    public Sprite image;
    public string itemName;
    public string Attribute;
    public float power;
    public string description;
    public uint cost;
}
