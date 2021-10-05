using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicEffect
{
    public string name;
    public int damage;
    public int power;
    public int resonancePower;
    public MagicEffect(string name="None",int power=1, int resonancePower=1,int damage=10)
    {
        this.name = name;
        this.power = power;
        this.resonancePower = resonancePower;
        this.damage = damage;
    }
}
