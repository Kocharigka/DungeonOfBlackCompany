using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySlime : Enemy
{
    private bool inJump=false;
    private bool preparing=false;
    Vector2 jumpPosition;
    Coroutine jump = null;
    
    // Start is called before the first frame update
    void Awake()
    {
        EnemyName = "Slime";
        MaxHealth = 20;        
        HealhBarOffset = new Vector3(0, 0.5f, 0);
        AttackRadius = 5;
        MoveSpeed = 2f;
        ColDamage = 10;
    }

    private void FixedUpdate()
    {
        if (!active)
            return;
        if(IsStunned)
        {          
            StopCoroutine(jump);
            MoveSpeed = 2f;
            inJump = false;
            preparing = false;
            Animator.speed = 1;
            return;
        }
            
        Vector2 playerPosition = getPlayerPosition();
        if (inJump)
        {
            playerPosition = jumpPosition;
        }
        if (Vector2.Distance(playerPosition, transform.position) <= 15 && !preparing)
        {
            transform.position = Vector2.MoveTowards(transform.position, playerPosition, MoveSpeed * Time.deltaTime);
        }
        if (Vector2.Distance(playerPosition, transform.position) <= AttackRadius)
        {
            if (!inJump)
            {
                inJump = true;
                preparing = true;
                jump=StartCoroutine(WaitForJump());
            }
        }
    }
    IEnumerator WaitForJump()
    {

        Animator.speed = 4;
        yield return new WaitForSeconds(2f);
        jumpPosition =getPlayerPosition();
        int distance = (int)Vector2.Distance(transform.position, jumpPosition);
        preparing = false;
        Animator.speed = 10 / distance > 1 ? 10 / distance : 1;
        if (!IsStunned){
        Animator.SetTrigger("Jump");
        }
        MoveSpeed = 30f;
        yield return new WaitForSeconds(0.2f);
        Animator.speed = 1;
        yield return new WaitForSeconds(2f);
        MoveSpeed = 2f;
        inJump = false;

    }
}
