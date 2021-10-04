using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowScript : ProjectileScript
{
    void Awake()
    {
        MoveSpeed = 20;
    }
    // Start is called before the first frame update
    public void OnTriggerEnter2D(Collider2D collider)
    {
        
        if (collider.gameObject.layer==LayerMask.NameToLayer("Enemy"))
        {
            collider.gameObject.GetComponent<Enemy>().GetDamage(Effect.damage);
            collider.gameObject.GetComponent<Enemy>().ApplyEffect(Effect);
            Destroy(gameObject);
        }

    }
}
