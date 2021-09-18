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
        MoveSpeed = 4f;
        HealhBarOffset = new Vector3(0, 1, 0);
        AttackRadius = 2;
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }
    private void FixedUpdate()
    {
        if (!active)
            return;
        if (IsStunned)
        {
            StopCoroutine(bite);
            inBite = false;
            MoveSpeed = 4f;
            return;
        }
        Vector2 playerDir = chooser.sectorToVector(transform.position, Player.transform.position);
        Animator.SetFloat("Horizontal", -playerDir.x);
        Animator.SetFloat("Vertical", -playerDir.y);
        Vector2 playerPosition = getPlayerPosition();
        if (Vector2.Distance(playerPosition, transform.position) <= AttackRadius)
        {
            if (!inBite)
            {                
                inBite = true;
                bite = StartCoroutine(WaitForBite());
                return;
                
            }
        }
        if (!inBite)
        {

        Animator.SetFloat("Speed", 1);
        }

        if (Vector2.Distance(playerPosition, transform.position) <= 15)
        {
            transform.position = Vector2.MoveTowards(transform.position, playerPosition, MoveSpeed * Time.deltaTime);
        }

    }
    IEnumerator WaitForBite()
    {
        Animator.SetFloat("Speed", 0);
        MoveSpeed = 0;
        Animator.SetTrigger("Attack");
        yield return new WaitForSeconds(1.35f);
        MoveSpeed = 4f;
        inBite = false;
       
    }
}
