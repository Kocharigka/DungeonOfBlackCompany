using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicEffect
{
    public string EffectName;
    public int damage;
    public int power;
    public int resonancePower;
    public MagicEffect(string name="None",int power=1, int resonancePower=1)
    {
        EffectName = name;
        this.power = power;
        this.resonancePower = resonancePower;
    }
}
