using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyArcher : MonoBehaviour
{
    HealthScript health;
    private string enemyName;
    private float moveSpeed;
    void Start()
    {
        health = GetComponent<HealthScript>();
        enemyName = "Sceleton-archer";
        health.MaxHealth = 20;
        health.HealhBarOffset = new Vector3(0, 2f, 0);
        moveSpeed = 2;
    }
}
