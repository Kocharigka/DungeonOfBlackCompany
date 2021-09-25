using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDog : Enemy
{
    private bool inBite = false;
    Coroutine bite = null;
    SectorChooser chooser = new SectorChooser();
    // Start is called before the first frame update
    void Awake()
    {
        EnemyName = "Dog";
        MaxHealth = 20;
        DefaultMoveSpeed = 4f;
        HealhBarOffset = new Vector3(0, 1, 0);
        AttackRadius = 1;

    }
    IEnumerator WaitForBite()
    {
        Animator.SetBool("CanMove", false);
        MoveSpeed = 0;
        Animator.SetTrigger("Attack");
        yield return new WaitForSeconds(1.35f);
        MoveSpeed = 4f;
        inBite = false;

    }
    public override void PerformAttack(Vector2 playerPosition)
    {
        Vector2 playerDir = chooser.sectorToVector(transform.position, Player.transform.position);
        Animator.SetFloat("Horizontal", -playerDir.x);
        Animator.SetFloat("Vertical", -playerDir.y);
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
        if (!inBite&&!IsStunned)
        {
            Animator.SetBool("CanMove", true);
        }
        if (Vector2.Distance(playerPosition, transform.position) <= 15)
        {
            transform.position = Vector2.MoveTowards(transform.position, playerPosition, MoveSpeed * Time.deltaTime);
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
}
