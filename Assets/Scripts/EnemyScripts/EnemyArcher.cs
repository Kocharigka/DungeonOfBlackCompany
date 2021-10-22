using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyArcher : Enemy
{
    private Shooter shooter;
    private float cooldown;
    private float cdTimer;
    private float dangerRadius = 10f;
    private SpellData spell;
    void Awake()
    {
        EnemyName = "Sceleton-archer";
        MaxHealth = 20;
        HealhBarOffset = new Vector3(0, 2f, 0);
        DefaultMoveSpeed = 3;
        shooter = GetComponent<Shooter>();
        spell = Resources.Load<SpellData>("Spells/ArrowEnemy");
        cooldown = spell.cooldown;
    }

    public override void FollowPlayer(Vector2 playerPosition)
    {
        if (!IsStunned && active && Vector2.Distance(transform.position, playerPosition) <= dangerRadius)
        {
            Animator.SetBool("CanMove", true);
        }
        if (Vector2.Distance(transform.position, playerPosition) <= dangerRadius)
        {
            Vector2 run = (Vector2)transform.position - playerPosition;
            //transform.position = Vector2.MoveTowards(transform.position, (Vector2)transform.position + run, MoveSpeed * Time.deltaTime);
            rb.MovePosition(rb.position + run.normalized * MoveSpeed * Time.deltaTime * 10);

        }
        else
        {
            Animator.SetBool("CanMove", false);
        }
    }
    public override void PerformAttack(Vector2 playerPosition)
    {
        if (cdTimer <= 0)
        {
            Animator.SetTrigger("Attack");
            cdTimer = cooldown;
        }
        else
        {
            cdTimer -= Time.deltaTime;

        }
    }
    public override void SetDefault()
    {
        MoveSpeed = DefaultMoveSpeed;
    }

    public void ShootProjectile()
    {
       shooter.Shoot(transform.position, chooser.getAngle(getPlayerPosition(), transform.position), spell);
    }
}
