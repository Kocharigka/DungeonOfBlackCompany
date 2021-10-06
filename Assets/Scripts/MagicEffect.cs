using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicEffect
{
    public string name;
    public float damage;
    public float power;
    public float resonancePower;
    public MagicEffect(string name="None",float power=1, float resonancePower=1,float damage=10)
    {
        this.name = name;
        this.power = power;
        this.resonancePower = resonancePower;
        this.damage = damage;
    }
}
