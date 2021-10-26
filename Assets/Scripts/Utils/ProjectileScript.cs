using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    private float moveSpeed;
    private Vector3 direction;
    private MagicEffect effect=new MagicEffect();
    private Animator anim;
    private string target;
    public float MoveSpeed
    {
        set { moveSpeed = value; }
    }
    public MagicEffect Effect
    {
        set { effect = value; }
    }
    public void Init(SpellData data)
    {
        moveSpeed = data.speed;
        anim.runtimeAnimatorController=data.animator;
        effect.damage = data.damage;
        effect.resonancePower = data.resonancePower;
        effect.power = data.effectPower;
        effect.name = data.element;
        target = data.target;
    }

    public void Awake()
    {
        direction.y = Mathf.Cos((360 - transform.eulerAngles.z) * Mathf.Deg2Rad);
        direction.x = Mathf.Sin((360 - transform.eulerAngles.z) * Mathf.Deg2Rad);
        anim = GetComponent<Animator>();
    }
    void Update()
    {
        transform.position += direction * Time.deltaTime * moveSpeed;
        Destroy(gameObject,5);        
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == target)
        {
            collider.gameObject.BroadcastMessage("GetDamage", effect.damage);
            collider.gameObject.GetComponent<MagicController>().ApplyEffect(effect);
            Destroy(gameObject);
        }

    }
}
