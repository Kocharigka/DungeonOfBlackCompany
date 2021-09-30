using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    private float moveSpeed;
    private Vector3 direction;
    private string effect;
    public float MoveSpeed
    {
        get { return moveSpeed; }
        set { moveSpeed = value; }
    }
    public string Effect
    {
        get { return effect; }
        set { effect = value; }
    }

    public void Start()
    {
        direction.y = Mathf.Cos((360 - transform.eulerAngles.z) * Mathf.Deg2Rad);
        direction.x = Mathf.Sin((360 - transform.eulerAngles.z) * Mathf.Deg2Rad);
    }
    void Update()
    {
        transform.position += direction * Time.deltaTime * moveSpeed;
        Destroy(gameObject,5);        
    }
}
