using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyArcher : Enemy
{
    private Shooter shooter;
    private float cooldown;
    private float cdTimer;
    private float dangerRadius = 10f;
    Vector2 run;
    void Awake()
    {
        EnemyName = "Skeleton-archer";
        MaxHealth = 20;
        HealhBarOffset = new Vector3(0, 2f, 0);
        DefaultMoveSpeed = 3;
        shooter = GetComponent<Shooter>();
        spell = Resources.Load<SpellData>("OtherSpells/ArrowEnemy");
        cooldown = spell.cooldown;
    }

    public override void FollowPlayer(Vector2 playerPosition)
    {
        if (Vector2.Distance(transform.position, playerPosition) <= dangerRadius&&!nearWall)
        {
            run = ((Vector2)transform.position - playerPosition).normalized;
            Animator.SetBool("CanMove", true);
            rb.MovePosition(rb.position + run * MoveSpeed * Time.deltaTime * 10);

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

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Walls"&&Physics2D.Raycast(transform.position, run,Mathf.Infinity,objLayer).transform.gameObject.name == "Walls")
        {
            nearWall = true;
        }
        else
        {
            nearWall = false;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Walls")
        {
            nearWall = false;
        }
    }
}

