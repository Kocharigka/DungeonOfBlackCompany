using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyArcher : Enemy
{
    void Awake()
    {
        EnemyName = "Sceleton-archer";
        MaxHealth = 20;
        HealhBarOffset = new Vector3(0, 2f, 0);
        MoveSpeed = 2;
    }
}
