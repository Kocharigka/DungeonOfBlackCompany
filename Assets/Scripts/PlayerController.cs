using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 direction;
    private float moveSpeed = 5f;
    private Animator animator;
    private float attackRange = 1f;
    public LayerMask enemyLayer;
    private int maxHealth=100;
    private int currentHealth;
    SectorChooser chooser = new SectorChooser();


    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        direction.x = Input.GetAxisRaw("Horizontal");
        direction.y = Input.GetAxisRaw("Vertical");
        animator.SetFloat("Horizontal", direction.x);
        animator.SetFloat("Vertical", direction.y);
        animator.SetFloat("Speed", direction.sqrMagnitude);
        if (Input.GetMouseButtonDown(0))
        {
            performAttack();
        }
    }
    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime);
    }
    private void performAttack()
    {        
        string sector = chooser.getSector(chooser.getAngle(Camera.main.ScreenToWorldPoint(Input.mousePosition),transform.position));
        //play Animation
        Collider2D[] hitEnemites = Physics2D.OverlapCircleAll(transform.position, attackRange, enemyLayer);
        foreach (Collider2D enemyCollider in hitEnemites)
        {
            Enemy enemy = enemyCollider.GetComponent<Enemy>();
            if (chooser.targetInSector(sector, enemy, transform.position))
            {
                enemy.GetDamage(10);
            }
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(new Vector2(transform.position.x+0.2f,transform.position.y+0.2f), attackRange);
    }
    
    public void GetDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log(currentHealth);
    }
}
