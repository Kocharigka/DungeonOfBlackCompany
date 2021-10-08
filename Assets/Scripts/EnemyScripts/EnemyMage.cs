using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMage : Enemy
{
    private Shooter shooter;
    private float cooldown = 5f;

    private float cdTimer;
    private float dangerRadius=10f;
    private float tpCooldown=20;
    private float tpTimer;
    private SpellData spell;
    void Awake()
    {
        EnemyName = "Wizard-arcane";
        MaxHealth = 40;
        HealhBarOffset = new Vector3(0, 1.3f, 0);
        DefaultMoveSpeed = 2;
        shooter = GetComponent<Shooter>();
        tpTimer = tpCooldown;
        spell = Resources.Load<SpellData>("Spells/ArcaneBolt");
        cooldown = spell.cooldown;
 
    }

    public override void FollowPlayer(Vector2 playerPosition)
    {
        if (!IsStunned&&active)
        {
            Animator.SetBool("CanMove", true);
        }
        if (Vector2.Distance(transform.position,playerPosition)<=dangerRadius)
        {
            if (tpTimer >= tpCooldown)
            {
                Teleport();
                tpTimer = 0;
            }
            else
            {
                Vector2 run = (Vector2)transform.position - playerPosition;
                rb.MovePosition(rb.position + run.normalized * MoveSpeed * Time.deltaTime*10);
                //transform.position = Vector2.MoveTowards(transform.position,(Vector2)transform.position+run, MoveSpeed * Time.deltaTime);
            }
        }
        tpTimer += Time.deltaTime;
    }
    public override void PerformAttack(Vector2 playerPosition)
    {
        if (cdTimer <= 0)
        {
            rb.velocity = Vector2.zero;
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
       shooter.Shoot(transform.position, chooser.getAngle(getPlayerPosition(), transform.position),spell);
    }

    private void Teleport()
    {
    }
}
