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
    private float maxHealth=100000;
    private float currentHealth;
    public Transform projectileHolder;
    public bool isAttacking = false;
    public static PlayerController instance;
    public float defaultMoveSpeed;
    public bool canFlip;
    public bool isCast = false;
    public MagicController magic;
    bool isDead = false;
    public float damage=10;
    public List<Collider2D> damagedEnemies=new List<Collider2D>();
    public Collider2D hurtBox;


    public float AttackRange
    {
        get { return attackRange; }
        set { attackRange = value; }
    }
    private void Awake()
    {
        instance = this;

    }

    // Start is called before the first frame update
    void Start()
    {
        magic = GetComponent<MagicController>();
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


    }
    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime*InventoryController.instance.powerUps["speed"]);
    }
    
    public void Invincivle(float dur=1f)
    {
        StartCoroutine(makeInvinsible(dur));
    }
    IEnumerator makeInvinsible(float dur)
    {
        hurtBox.enabled = false;
        yield return new WaitForSeconds(dur);
        hurtBox.enabled = true;

    }

    public void GetDamage(float damage)
    {
        if (isDead)
        {
            return;
        }
        currentHealth -= damage/InventoryController.instance.powerUps["armor"];
      //  Debug.Log(currentHealth);
        if (currentHealth<=0)
        {
            isDead = true;
            enabled = false;
            animator.SetTrigger("Die");
            var enemies=FindObjectsOfType<Enemy>();
            foreach (Enemy enemy in enemies)
            {
                enemy.active = false;
            }
            StartCoroutine(WaitForDeath());
            enabled = false;
        }
    }

    public void GetMagicDamage(float damage)
    {
        if (isDead)
        {
            return;
        }
        currentHealth -= damage;
        //  Debug.Log(currentHealth);
        if (currentHealth <= 0)
        {
            isDead = true;
            enabled = false;
            animator.SetTrigger("Die");
            var enemies = FindObjectsOfType<Enemy>();
            foreach (Enemy enemy in enemies)
            {
                enemy.active = false;
            }
            StartCoroutine(WaitForDeath());
            enabled = false;
        }
    }

    IEnumerator WaitForDeath()
    {
        magic.effectHolder.gameObject.SetActive(false);
        yield return new WaitForSeconds(2f);
        gameObject.SetActive(false);
    }
    public void FlipToDirection()
    {
        Vector2 flip= chooser.sectorToVector(Camera.main.ScreenToWorldPoint(Input.mousePosition), transform.position);
        animator.SetFloat("Horizontal", flip.x);
        animator.SetFloat("Vertical", flip.y);
    }
    public void UpdateHealth(float up)
    {
        maxHealth *= up;
        currentHealth *= up;

    }
    public void RestoreHealth(float hp)
    {
        currentHealth = currentHealth + hp > maxHealth ? maxHealth : currentHealth + hp;
    }
}
