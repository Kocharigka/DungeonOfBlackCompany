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
    float flyCount=6;
    bool fly = false;
    Vector2 flydir = new Vector2();
    Transform head;
    bool flyEnd = false;
    List<Vector2> directions = new List<Vector2>() { Vector2.up, Vector2.down, Vector2.left, Vector2.right };

    Vector2 run;
    private void Awake()
    {
        boss = true;
        EnemyName = "Dragon";
        MaxHealth = 400;
        HealhBarOffset = new Vector3(0, 0, 0);
        DefaultMoveSpeed = 2;
        shooter = GetComponentInChildren<Shooter>();
        head = shooter.gameObject.transform;
        spell = Resources.Load<SpellData>("Spells/FireballDragon");
        cooldown = spell.cooldown;
        cdTimer = cooldown;
        AttackRadius = 4;
        hardTimer = hardCooldown;
    }

    public override void FollowPlayer(Vector2 playerPosition)
    {
        if (HealthBar.value > MaxHealth/2 || flyEnd)
        {
            Follow(playerPosition);
        }
        else
        {
            Fly();
        }
        
    }
    void Follow(Vector2 playerPosition)
    {
        if (hardTimer < hardCooldown)
        {
            Animator.SetBool("CanMove", false);
            return;
        }
        if (Vector2.Distance(transform.position, playerPosition) <= followRadius && Vector2.Distance(transform.position, playerPosition) > AttackRadius - 1)
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

    void Fly()
    {
        if (!fly)
        {
            fly = true;
            FacePlayer = false;
            Animator.SetBool("FlyUp", true);
            StartCoroutine(_fly());
        }

    }

    IEnumerator _fly()
    {
        Animator.SetBool("FlyUp", true);

        InvokeRepeating("flyUp", 0, 0.03f);
        yield return new WaitUntil(()=>!Room.InBounds(rb.position));
        yield return new WaitForSeconds(0.3f);

        CancelInvoke("flyUp");
        Animator.SetBool("Fly", true);
        for (int i = 0; i < flyCount; i++)
        {
            flydir = directions[Random.Range(0, 4)];
            Animator.SetFloat("Horizontal", flydir.x);
            Animator.SetFloat("Vertical", flydir.y);

            var seed = Random.Range(0, 1000);
            Random.InitState(seed);
            var multY = Room.bounds.center.y > 0 ? 1 : -1;
            var multX = Room.bounds.center.x > 0 ? 1 : -1;

            var start = new Vector2(-flydir.x*multX, -flydir.y*multY);
            rb.position = (Vector2)Room.bounds.center + Room.bounds.max*2 * start;
            var endPos= (Vector2)Room.bounds.center + new Vector2(Mathf.Abs(Room.bounds.max.x),Mathf.Abs(Room.bounds.max.y))* flydir;
            InvokeRepeating("flyDir", 0, 0.03f);
            yield return new WaitUntil(() => Room.InBounds(rb.position));
            StartCoroutine(_flyAttack());
            yield return new WaitUntil(() => !Room.InBounds(rb.position));
            yield return new WaitForSeconds(0.3f);
            CancelInvoke("flyDir");
            yield return new WaitForSeconds(1f);
        }
        Animator.SetBool("Fly", false);

        InvokeRepeating("flyDown", 0, 0.03f);
        rb.position = (Vector2)Room.bounds.center + new Vector2(0,20);
        yield return new WaitUntil(() => rb.position.y<Room.bounds.center.y);
        CancelInvoke("flyDown");
        flyEnd = true;
        FacePlayer = true;
        fly = false;
        Animator.SetBool("FlyUp", false);
        yield return new WaitForSeconds(1);
    }
    void flyUp()
    {
        rb.MovePosition(rb.position + Vector2.up * MoveSpeed * Time.deltaTime * RunMultiplier);
    }
    void flyDown()
    {
        rb.MovePosition(rb.position + Vector2.down * MoveSpeed * Time.deltaTime * RunMultiplier);
    }
    void flyDir()
    {
        rb.MovePosition(rb.position + flydir * MoveSpeed * Time.deltaTime * RunMultiplier);
    }
    public override void PerformAttack(Vector2 playerPosition)
    {
        if (HealthBar.value > MaxHealth / 2 || flyEnd)
        {
            Attack(playerPosition);
        }
        else
        {
            //FlyAttack();
        }
    }
    void Attack(Vector2 playerPosition)
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
            hardTimer += Time.deltaTime;
        }
    }
    void FlyAttack()
    {
        if (!fly)
        {
            StartCoroutine(_flyAttack());
        }
    }

    IEnumerator _flyAttack()
    {
        for (int i = 0; i < 100; i++)
        {
            var angle = chooser.getAngle(getPlayerPosition(), transform.position);
            shooter.Shoot(head.position, angle, spell);
            shooter.Shoot(head.position, angle + 15, spell);
            shooter.Shoot(head.position, angle - 15, spell);
            yield return new WaitForSeconds(0.02f);
        }
        yield return new WaitForSeconds(3f);
    }
    public override void SetDefault()
    {
        MoveSpeed = DefaultMoveSpeed;
    }

    public void ShootProjectile()
    {
        var angle = chooser.getAngle(getPlayerPosition(), transform.position);
        shooter.Shoot(head.position, angle, spell);
        shooter.Shoot(head.position, angle+15, spell);
        shooter.Shoot(head.position, angle-15, spell);
    }
}
