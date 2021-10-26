using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="NewSpell",menuName ="Spells/ProjectileSpell")]
public class SpellData : ScriptableObject
{
    public Sprite sprite;
    public float speed;
    public RuntimeAnimatorController animator;
    public float damage;
    public string element;
    public float effectPower;
    public float resonancePower;
    public Sprite inGameSprite;
    public string triggerName;
    public int moneyCost;
    public float cooldown;
    public string target;
}
