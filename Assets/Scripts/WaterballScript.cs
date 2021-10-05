using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterballScript : ProjectileScript
{
    // Start is called before the first frame update
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
