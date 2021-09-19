using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowScript : MonoBehaviour
{
    private float moveSpeed = 20;
    Vector3 direction;
    private Vector2 start;
    void Start()
    {
        start = transform.position;
        direction.y = Mathf.Cos((360 - transform.eulerAngles.z) * Mathf.Deg2Rad);
        direction.x = Mathf.Sin((360 - transform.eulerAngles.z) * Mathf.Deg2Rad);
    }
    void FixedUpdate()
    {
        transform.position += direction * Time.fixedDeltaTime * moveSpeed;
        if (Vector2.Distance(start, transform.position) > 30)
        {
            Destroy(this);
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision);
        if (collision.gameObject.tag != "Player")
        {            
            collision.gameObject.GetComponent<HealthScript>().GetDamage(4);
            Destroy(this);
        }

    }
}
