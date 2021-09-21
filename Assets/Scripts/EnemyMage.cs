using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMage : Enemy
{
    // Start is called before the first frame update
    void Awake()
    {
        EnemyName = "Sceleton-archer";
        MaxHealth = 20;
        HealhBarOffset = new Vector3(0, 2f, 0);
        MoveSpeed = 2;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
