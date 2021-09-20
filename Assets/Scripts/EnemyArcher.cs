using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyArcher : Enemy
{
    private string enemyName;
    private float moveSpeed;
    void Start()
    {
        enemyName = "Sceleton-archer";
        MaxHealth = 20;
        HealhBarOffset = new Vector3(0, 2f, 0);
        moveSpeed = 2;
    }
}
