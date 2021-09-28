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
    private int maxHealth=100;
    private int currentHealth;
    public Transform projectileHolder;
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
        animator.SetFloat("Horizontal", direction.x);
        animator.SetFloat("Vertical", direction.y);
        animator.SetFloat("Speed", direction.sqrMagnitude);


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
}
