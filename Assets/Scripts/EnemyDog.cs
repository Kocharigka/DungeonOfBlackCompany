using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDog : Enemy
{
    private bool inBite = false;
    Coroutine bite = null;
    SectorChooser chooser = new SectorChooser();
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
        enemyName = "Dog";
        MaxHealth = 20;
        moveSpeed = 4f;
        HealhBarOffset = new Vector3(0, 1, 0);
        attackRadius = 1;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }
    private void FixedUpdate()
    {
        if (!active)
        {
            animator.SetFloat("Speed", 0);
            return;
        }
        if (IsStunned)
        {
            StopCoroutine(bite);
            inBite = false;
            moveSpeed = 4f;
            return;
        }
        Vector2 playerDir = chooser.sectorToVector(transform.position, player.transform.position);
        animator.SetFloat("Horizontal", -playerDir.x);
        animator.SetFloat("Vertical", -playerDir.y);
        Vector2 playerPosition = getPlayerPosition();
        if (Vector2.Distance(playerPosition, transform.position) <= attackRadius)
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

        animator.SetFloat("Speed", 1);
        }

        if (Vector2.Distance(playerPosition, transform.position) <= 15)
        {
            transform.position = Vector2.MoveTowards(transform.position, playerPosition, moveSpeed * Time.fixedDeltaTime);
        }

    }
    IEnumerator WaitForBite()
    {
        animator.SetFloat("Speed", 0);
        moveSpeed = 0;
        animator.SetTrigger("Attack");
        yield return new WaitForSeconds(1.35f);
        moveSpeed = 4f;
        inBite = false;
       
    }
    public Vector2 getPlayerPosition()
    {
        return player.GetComponent<Transform>().position;
    }
}
