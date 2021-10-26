using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScript : MonoBehaviour
{
    PlayerController player;
    void Start()
    {
        player = GetComponentInParent<PlayerController>();
    }
    void Update()
    {
        Attack();
    }

    void Attack()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && !player.isAttacking)
        {
            player.damagedEnemies.Clear();
            player.isAttacking = true;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision);
        if (collision.tag=="Enemy")
        {
            if (player.damagedEnemies.Contains(collision))
            {
                return;
            }
            else
            {
                collision.GetComponent<Enemy>().GetDamage(player.damage);
                collision.GetComponent<Enemy>().GetStun();
                player.damagedEnemies.Add(collision);

            }
        }
    }
}
