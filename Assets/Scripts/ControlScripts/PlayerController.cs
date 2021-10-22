using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    SectorChooser chooser = new SectorChooser();
    private Rigidbody2D rb;
    public Vector2 direction;
    public float moveSpeed;
    public Animator animator;
    private float attackRange = 1f;  
    private int maxHealth=100;
    private int currentHealth;
    public Transform projectileHolder;
    public bool isAttacking = false;
    public static PlayerController instance;
    public float defaultMoveSpeed;
    public bool canFlip;
    public bool isCast = false;
    public MagicController magic;


    public float AttackRange
    {
        get { return attackRange; }
        set { attackRange = value; }
    }


    // Start is called before the first frame update
    void Start()
    {
        magic = GetComponent<MagicController>();
        instance = this;
        moveSpeed = defaultMoveSpeed;
        canFlip = true;
        currentHealth = maxHealth;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        direction.x = Input.GetAxisRaw("Horizontal");
        direction.y = Input.GetAxisRaw("Vertical");
        if (direction!=new Vector2(0,0) && canFlip)
        {
            animator.SetFloat("Horizontal", direction.x);
            animator.SetFloat("Vertical", direction.y);
        }
        animator.SetFloat("Speed", direction.sqrMagnitude);
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
    public void FlipToDirection()
    {
        Vector2 flip= chooser.sectorToVector(Camera.main.ScreenToWorldPoint(Input.mousePosition), transform.position);
        animator.SetFloat("Horizontal", flip.x);
        animator.SetFloat("Vertical", flip.y);
    }
}
