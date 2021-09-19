using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySlime : MonoBehaviour
{
    private bool inJump=false;
    private bool preparing=false;
    Vector2 jumpPosition;
    Coroutine jump = null;
    HealthScript health;
    private int colDamage;
    private PlayerController player;
    public bool active = true;
    private float attackRadius;
    private string enemyName;
    private float moveSpeed;
    private Animator animator;
    // Start is called before the first frame update
    void Awake()
    {
        animator = GetComponent<Animator>();
        health = GetComponent<HealthScript>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        enemyName = "Slime";
        health.MaxHealth = 20;        
        health.HealhBarOffset = new Vector3(0, 0.5f, 0);
        attackRadius = 5;
        moveSpeed = 2f;
        colDamage = 10;
        
    }

    private void FixedUpdate()
    {
        if (!active)
            return;
        if(health.IsStunned)
        {          
            StopCoroutine(jump);
            moveSpeed = 2f;
            inJump = false;
            preparing = false;
            animator.speed = 1;
            return;
        }
            
        Vector2 playerPosition = getPlayerPosition();
        if (inJump)
        {
            playerPosition = jumpPosition;
        }
        if (Vector2.Distance(playerPosition, transform.position) <= attackRadius)
        {
            if (!inJump)
            {
                inJump = true;
                preparing = true;
                jump = StartCoroutine(WaitForJump());
            }
        }
        if (Vector2.Distance(playerPosition, transform.position) <= 15 && !preparing)
        {
            transform.position = Vector2.MoveTowards(transform.position, playerPosition, moveSpeed * Time.fixedDeltaTime);
        }
    }
    IEnumerator WaitForJump()
    {

        animator.speed = 4;
        yield return new WaitForSeconds(2f);
        jumpPosition =getPlayerPosition();
        int distance = (int)Vector2.Distance(transform.position, jumpPosition);
        preparing = false;
        animator.speed = 10 / distance > 1 ? 10 / distance : 1;
        if (!health.IsStunned){
        animator.SetTrigger("Attack");
        }
        moveSpeed = 30f;
        yield return new WaitForSeconds(0.2f);
        animator.speed = 1;
        yield return new WaitForSeconds(2f);
        moveSpeed = 2f;
        inJump = false;

    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            player.GetDamage(colDamage);
        }
    }
    public Vector2 getPlayerPosition()
    {
        return player.GetComponent<Transform>().position;
    }
}
