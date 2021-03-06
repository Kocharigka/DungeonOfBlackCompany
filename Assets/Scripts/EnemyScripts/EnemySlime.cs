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
        DefaultMoveSpeed = 2f;
        Damage = 10;
        
    }

    IEnumerator WaitForJump()
    {

        Animator.speed = 4;
        yield return new WaitForSeconds(2f);
        jumpPosition =getPlayerPosition();
        int distance = (int)Vector2.Distance(transform.position, jumpPosition)>1? (int)Vector2.Distance(transform.position, jumpPosition):1;
        preparing = false;
        Animator.speed = 10 / distance > 1 ? 10 / distance : 1;
        if (!IsStunned){
        Animator.SetTrigger("Attack");
        }
        yield return new WaitForSeconds(0.2f);
        Animator.speed = 1;
        yield return new WaitForSeconds(2f);
        inJump = false;

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {  
        if (collision.gameObject.tag == "Player"&&inJump)
        {
            Player.GetDamage(spell.damage);
            Player.magic.ApplyEffect(new MagicEffect(spell.element, spell.effectPower, spell.resonancePower, spell.damage));
        }
    }
    public override void PerformAttack(Vector2 playerPosition)
    {
        if (Vector2.Distance(playerPosition, transform.position) <= AttackRadius)
        {
            if (!inJump)
            {
                inJump = true;
                preparing = true;
                jump = StartCoroutine(WaitForJump());
            }
        }
    }
    public override void FollowPlayer(Vector2 playerPosition)
    {
        if (inJump)
        {
            playerPosition = jumpPosition;
        }
        if (Vector2.Distance(playerPosition, transform.position) <= 15 && !preparing)
        {
            transform.position = Vector2.MoveTowards(transform.position, playerPosition, MoveSpeed * Time.deltaTime);
        }
    }

    public override void SetDefault()
    {
        if (jump != null)
        {
            StopCoroutine(jump);
        }
        MoveSpeed = 2f;
        inJump = false;
        preparing = false;
        Animator.speed = 1;
    }
}
