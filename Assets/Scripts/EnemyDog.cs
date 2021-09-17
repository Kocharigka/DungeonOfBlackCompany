using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDog : Enemy
{
    // Start is called before the first frame update
    void Start()
    {
        EnemyName = "Dog";
        MaxHealth = 20;
        MoveSpeed = 2f;
        //HealhBarOffset = new Vector3(0, 0.5f, 0);
        AttackRadius = 5;
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
