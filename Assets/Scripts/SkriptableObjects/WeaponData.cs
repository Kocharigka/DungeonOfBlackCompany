using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName ="NewWeapon",menuName ="Weapons/NewWeapon")]
public class WeaponData : ScriptableObject
{
    public Sprite icon;
    public Sprite inGameSprite;
    public float weaponAnim;

}
