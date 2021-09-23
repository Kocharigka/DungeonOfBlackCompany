using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMage : Enemy
{
    // Start is called before the first frame update
    void Awake()
    {
        EnemyName = "Wizard-arcane";
        MaxHealth = 20;
        HealhBarOffset = new Vector3(0, 1.3f, 0);
        MoveSpeed = 1;
    }

}
