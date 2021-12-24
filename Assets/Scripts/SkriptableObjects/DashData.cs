using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewDash", menuName = "Spells/Dash")]

public class DashData : ScriptableObject
{
    public Sprite sprite;
    public float speed;
    public string dashName;
}
