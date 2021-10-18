using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 direction;
    private float moveSpeed = 5f;
    public Animator animator;
    private float attackRange = 1f;  
    private int maxHealth=100;
    private int currentHealth;
    public Transform projectileHolder;
    public bool isAttacking = false;
    public static PlayerController instance;

    private void Awake()
    {
        instance = this;
    }
    public float AttackRange
    {
        get { return attackRange; }
        set { attackRange = value; }
    }


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
        if (direction!=new Vector2(0,0))
        {
            animator.SetFloat("Horizontal", direction.x);
            animator.SetFloat("Vertical", direction.y);
        }
        if (!isAttacking)
        {
            animator.SetFloat("Speed", direction.sqrMagnitude);
        }
        else
        {
            animator.SetFloat("Speed", 0);
        }
        Attack();


    }
    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime);
    }
    
    
    public void GetDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log(currentHealth);
    }
    void Attack()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && !isAttacking)
        {
            isAttacking = true;
        }
    }
}
