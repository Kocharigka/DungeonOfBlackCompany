using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceLance : ProjectileScript
{
    void Awake()
    {
        MoveSpeed = 20;
    }
    public void OnTriggerEnter2D(Collider2D collider)
    {
        Debug.Log(collider);
        if (collider.gameObject.tag != "Player")
        {
            collider.gameObject.GetComponent<Enemy>().GetDamage(Effect.damage);
            collider.gameObject.GetComponent<Enemy>().ApplyEffect(Effect);

            Destroy(gameObject);
        }

    }
}
