using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    SectorChooser chooser = new SectorChooser();
    private Rigidbody2D rb;
    [HideInInspector]public Vector2 direction;
    [HideInInspector]public float moveSpeed;
    [HideInInspector]public Animator animator;
    private float attackRange = 1f;  
    private float maxHealth=100;
    private float currentHealth;
    [HideInInspector]public Transform projectileHolder;
    [HideInInspector]public bool isAttacking = false;
    [HideInInspector]public static PlayerController instance;
    [HideInInspector]public float defaultMoveSpeed;
    [HideInInspector]public bool canFlip;
    [HideInInspector]public bool isCast = false;
    [HideInInspector]public MagicController magic;
    bool isDead = false;
    [HideInInspector]public float damage=10;
    [HideInInspector]public List<Collider2D> damagedEnemies=new List<Collider2D>();
    public Collider2D hurtBox;
    public Slider healthbar;
    public bool minimapOn;
    public GameObject minimap;
    [HideInInspector]public WeaponData weapon;
    [HideInInspector]public DashData dash;
    public Dictionary<int, SpellData> spells;
    public int money=1000;
    public bool bying = false;
    public bool blockInput=false;

    public float AttackRange
    {
        get { return attackRange; }
        set { attackRange = value; }
    }
    private void Awake()
    {
        spells = new Dictionary<int, SpellData>();
        instance = this;
        animator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        magic = GetComponent<MagicController>();
        moveSpeed = defaultMoveSpeed;
        canFlip = true;
        currentHealth = maxHealth;
        healthbar.maxValue = maxHealth;
        healthbar.value = currentHealth;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameController.paused||blockInput)
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            MinimapContorol();            
        }
        direction.x = Input.GetAxisRaw("Horizontal");
        direction.y = Input.GetAxisRaw("Vertical");
        if (direction!=new Vector2(0,0) && canFlip)
        {
            animator.SetFloat("Horizontal", direction.x);
            animator.SetFloat("Vertical", direction.y);
        }
        animator.SetFloat("Speed", direction.sqrMagnitude);
    }

    public void MinimapContorol()
    {
        minimapOn = !minimapOn;
        minimap.SetActive(minimapOn);
    }
    private void FixedUpdate()
    {
        healthbar.value = currentHealth;
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
        Debug.Log(currentHealth);
      //  Debug.Log(currentHealth);
        if (currentHealth<=0)
        {
            isDead = true;
            GetComponentInChildren<Canvas>().gameObject.SetActive(false);
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
        if (GameController.paused)
        {
            return;
        }
        Vector2 flip= chooser.sectorToVector(Camera.main.ScreenToWorldPoint(Input.mousePosition), transform.position);
        animator.SetFloat("Horizontal", flip.x);
        animator.SetFloat("Vertical", flip.y);
    }
    public void UpdateHealth(float up)
    {
        maxHealth *= up;
        currentHealth *= up;
        healthbar.maxValue = maxHealth;
        healthbar.value = currentHealth;

    }
    public void RestoreHealth(float hp)
    {
        currentHealth = currentHealth + hp > maxHealth ? maxHealth : currentHealth + hp;
        healthbar.value = currentHealth;
    }
    public float getHealthPercents()
    {
        return currentHealth / maxHealth;
    }
    public void AddMoney()
    {
        money += 30;
    }
    public SpellData TakeSpell(SpellData spell,KeyCode key)
    {
        foreach (var component in GetComponents<SpellControl>())
        {
            if (component.key==key)
            {
                return component.ChangeSpell(spell);
            }
        }
        return null;
    }
}
