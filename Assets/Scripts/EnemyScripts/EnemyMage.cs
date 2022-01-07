using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMage : Enemy
{
    private Shooter shooter;
    private float cooldown;

    private float cdTimer;
    private float dangerRadius=10f;
    private float tpCooldown=20;
    private float tpTimer;
    GameObject staff;
    Vector2 run;
    void Awake()
    {
        EnemyName = "Wizard-arcane";
        MaxHealth = 40;
        HealhBarOffset = new Vector3(0, 1.3f, 0);
        DefaultMoveSpeed = 2;
        shooter = GetComponentInChildren<Shooter>();
        staff = shooter.gameObject;
        tpTimer = tpCooldown;
        cooldown = spell.cooldown;
        cdTimer = cooldown;
 
    }

    public override void FollowPlayer(Vector2 playerPosition)
    {
        if (Vector2.Distance(transform.position, playerPosition) <= 4&&tpTimer>tpCooldown&&cdTimer>2)
        {
            tpTimer = 0;
            //Animator.SetTrigger("Teleport");
        }
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
        tpTimer += Time.deltaTime;
    }
    public override void PerformAttack(Vector2 playerPosition)
    {
        if (cdTimer > cooldown&&tpTimer>5)
        {
            Animator.SetTrigger("Attack");
            cdTimer = 0;
        }
        else
        {
            cdTimer += Time.deltaTime;

        }
    }
    public override void SetDefault()
    {
        MoveSpeed = DefaultMoveSpeed;
    }

    public void ShootProjectile()
    {
       shooter.Shoot(staff.transform.position, chooser.getAngle(getPlayerPosition(), transform.position),spell);
    }

    public void Teleport()
    {
        Vector2 farest=new Vector2();
        float far=0;
        var room = GetComponentInParent<Room>().getCorners();
        var playerPos = getPlayerPosition();
        foreach (var corner in room)
        {
            var dist = Vector2.Distance(corner, playerPos);
            if (dist>far)
            {
                far = dist;
                farest = corner;
            } 
            
        }
        Debug.Log(farest);
        transform.position = farest;
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Walls" && Physics2D.Raycast(transform.position, run,Mathf.Infinity,objLayer).transform.gameObject.name == "Walls")
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
