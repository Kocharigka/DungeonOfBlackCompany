using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    private float moveSpeed;
    private Vector3 direction;
    private MagicEffect effect;
    public int damage=10;
    public float MoveSpeed
    {
        get { return moveSpeed; }
        set { moveSpeed = value; }
    }
    public MagicEffect Effect
    {
        get { return effect; }
        set 
        {
            effect.damage = damage;
            effect = value;
        }
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
