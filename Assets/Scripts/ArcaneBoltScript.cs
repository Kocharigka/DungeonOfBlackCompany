using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcaneBoltScript : ProjectileScript
{
    void Awake()
    {
        MoveSpeed = 20;
    }
    // Start is called before the first frame update
    public void OnTriggerEnter2D(Collider2D collider)
    {
        Debug.Log(collider);
        if (collider.gameObject.tag == "Player")
        {
            collider.gameObject.GetComponent<PlayerController>().GetDamage(10);
            Destroy(gameObject);
        }

    }
}
