using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMage : Enemy
{
    private Shooter shooter;
    SectorChooser chooser = new SectorChooser();
    private float cooldown = 5f;
    private float timer;
    public Transform SpawnTransform;
    void Awake()
    {
        EnemyName = "Wizard-arcane";
        MaxHealth = 20;
        HealhBarOffset = new Vector3(0, 1.3f, 0);
        MoveSpeed = 1;
        shooter = GetComponent<Shooter>();
        shooter.Cooldown = 2    ;
        shooter.ProjectileName = "fireball";
        shooter.Delay = 0;
    }

    public override void FollowPlayer(Vector2 x)
    {
        base.FollowPlayer(x);
    }
    public override void PerformAttack(Vector2 playerPosition)
    {
        if (timer <= 0)
        {
            Animator.SetTrigger("Attack");
            timer = cooldown;
        }
        else
        {
            timer -= Time.deltaTime;
        }
    }
    public override void SetDefault()
    {
        base.SetDefault();
    }
    private void Shoot()
    {
        shooter.Shoot(SpawnTransform.position, chooser.getAngle(getPlayerPosition(), transform.position));
    }
}
