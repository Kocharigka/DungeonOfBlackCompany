using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonAttack : MonoBehaviour
{
    bool hit = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && !hit)
        {
            hit = true;
            InvokeRepeating("ClearHit", 1, 1);
            collision.GetComponent<PlayerController>().GetDamage(10);
        }
    }
    void ClearHit()
    {
        hit = false;
    }
}
