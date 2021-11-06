using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonEnemy : Enemy
{
    private Shooter shooter;
    private SpellData spell;
    private float cooldown;
    private float cdTimer;
    private float followRadius = 100f;
    float hardCooldown=3;
    float hardTimer;

    Vector2 run;
    private void Awake()
    {
        boss = true;
        EnemyName = "Dragon";
        MaxHealth = 400;
        HealhBarOffset = new Vector3(0, 0, 0);
        DefaultMoveSpeed = 2;
        shooter = GetComponent<Shooter>();
        spell = Resources.Load<SpellData>("Spells/FireballDragon");
        cooldown = spell.cooldown;
        cdTimer = cooldown;
        AttackRadius = 4;
        hardTimer = hardCooldown;
    }

    public override void FollowPlayer(Vector2 playerPosition)
    {
        if (hardTimer<hardCooldown)
        {
            Animator.SetBool("CanMove", false);
            return;
        }
        if (Vector2.Distance(transform.position, playerPosition) <= followRadius && Vector2.Distance(transform.position, playerPosition)>AttackRadius-1)
        {
            run = (playerPosition - (Vector2)transform.position).normalized;
            Animator.SetBool("CanMove", true);
            rb.MovePosition(rb.position + run * MoveSpeed * Time.deltaTime * RunMultiplier);
        }
        else
        {
            Animator.SetBool("CanMove", false);
        }
    }
    public override void PerformAttack(Vector2 playerPosition)
    {
        if (hardTimer > hardCooldown)
        {
            if (Vector2.Distance(transform.position, playerPosition) <= AttackRadius)
            {
                Animator.SetTrigger("Attack");
                hardTimer = 0;

            }
            else if (cdTimer > cooldown && Vector2.Distance(transform.position, playerPosition) < 20)
            {
                Animator.SetTrigger("Shoot");
                cdTimer = 0;
                hardTimer = 0;
            }
            else
            {
                cdTimer += Time.deltaTime;
            }
           
        }
        else
        {
            hardTimer+=Time.deltaTime;
        }
    }
    public override void SetDefault()
    {
        MoveSpeed = DefaultMoveSpeed;
    }

    public void ShootProjectile()
    {
        var angle = chooser.getAngle(getPlayerPosition(), transform.position);
        shooter.Shoot(transform.position, angle, spell);
        shooter.Shoot(transform.position, angle+15, spell);
        shooter.Shoot(transform.position, angle-15, spell);
    }
}
