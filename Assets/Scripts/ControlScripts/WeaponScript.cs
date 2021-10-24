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
            player.isAttacking = true;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision);
        if (collision.tag=="Enemy")
        {
            collision.GetComponent<Enemy>().GetDamage(player.damage);
            collision.GetComponent<Enemy>().GetStun();

        }
    }
}
