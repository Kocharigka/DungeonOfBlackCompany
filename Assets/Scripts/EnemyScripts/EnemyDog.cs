using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDog : Enemy
{
    private bool inBite = false;
    Coroutine bite = null;
    GameObject head;
    float curve;
    // Start is called before the first frame update
    void Awake()
    {
        curve = Random.Range(-0.5f, 0.5f);
        head = GetComponentInChildren<Collider2D>().gameObject;
        EnemyName = "Dog";
        MaxHealth = 20;
        DefaultMoveSpeed = 4f;
        HealhBarOffset = new Vector3(0, 1, 0);
        AttackRadius = 1;
        Damage = 10;
        

    }
    IEnumerator WaitForBite()
    {
        Animator.SetTrigger("Attack");
        yield return new WaitForSeconds(1.35f);
        inBite = false;

    }
    public override void PerformAttack(Vector2 playerPosition)
    {
        if (Vector2.Distance(playerPosition, transform.position) <= AttackRadius)
        {
            if (!inBite)
            {
                inBite = true;
                bite = StartCoroutine(WaitForBite());
                return;
            }
        }
    }
    public override void FollowPlayer(Vector2 playerPosition)
    {
        if (!inBite&&!IsStunned&&active)
        {
            Animator.SetBool("CanMove", true);
        }
        if (Vector2.Distance(playerPosition, transform.position) <= 15)
        {
            Vector2 run = (playerPosition-rb.position).normalized;
            if (Mathf.Abs(run.x)> Mathf.Abs(run.y))
            {
                run.y += curve;
            }
            else if (Mathf.Abs(run.x) <= Mathf.Abs(run.y))
            {
                run.x += curve;
            }
            
            //else
            //{
            //    run.x += Random.Range(-1, 1);
            //    run.y += Random.Range(-1, 1);
            //}
            rb.MovePosition(rb.position + run * MoveSpeed * Time.deltaTime * 10);
        }
    }

    public override void SetDefault()
    {
        if (bite != null)
        {
            StopCoroutine(bite);
        }
        MoveSpeed = 4f;
        inBite = false;
    }

    public void Bite()
    {
        var hit = Physics2D.OverlapCircleAll(head.transform.position, 1);
        foreach (var target in hit)
        {
            if (target.tag=="Player")
            {
                target.GetComponent<PlayerController>().GetDamage(Damage);
            }
        }
    }
}
