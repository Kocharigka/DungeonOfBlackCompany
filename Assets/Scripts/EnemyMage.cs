using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMage : Enemy
{
    private Shooter shooter;
    SectorChooser chooser = new SectorChooser();
    private float cooldown = 5f;

    private float cdTimer;
    private float dangerRadius=10f;
    private float tpCooldown=20;
    private float tpTimer;
    private MagicEffect effect = new MagicEffect();
    void Awake()
    {
        EnemyName = "Wizard-arcane";
        MaxHealth = 40;
        HealhBarOffset = new Vector3(0, 1.3f, 0);
        DefaultMoveSpeed = 2;
        shooter = GetComponent<Shooter>();
        shooter.Cooldown = 2    ;
        shooter.ProjectileName = "arcaneBolt";
        shooter.Delay = 0;
        tpTimer = tpCooldown;
    }

    public override void FollowPlayer(Vector2 playerPosition)
    {
        if (!IsStunned&&active)
        {
            Animator.SetBool("CanMove", true);
        }
        Vector2 playerDir = chooser.sectorToVector(transform.position, Player.transform.position);
        Animator.SetFloat("Horizontal", playerDir.x);
        Animator.SetFloat("Vertical", playerDir.y);
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
                transform.position = Vector2.MoveTowards(transform.position,(Vector2)transform.position+run, MoveSpeed * Time.deltaTime);
            }
        }
        tpTimer += Time.deltaTime;
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
        shooter.Shoot(transform.position, chooser.getAngle(getPlayerPosition(), transform.position),effect);
    }

    private void Teleport()
    {
    }
}
