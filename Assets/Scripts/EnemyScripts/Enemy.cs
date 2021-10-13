using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    #region privateStatic
    private string enemyName;
    private float moveSpeed;
    private float defaultMoveSpeed;
    private float maxHealth;
    [SerializeField] private Slider healthBar;
    private Vector3 healhBarOffset;
    private Animator animator;
    private SpriteRenderer sprite;
    private PlayerController player;
    private float attackRadius;
    private float currentHealth;
    private MagicController magic;
    private float offset=0;
    private float spawnDuration=10;
    private string effectName;
    public Rigidbody2D rb;
    public SectorChooser chooser = new SectorChooser();
    private bool isImpulse = false;
    #endregion privateStatic
    #region publicFields

    public bool IsImpulse
    {
        get { return isImpulse; }
        set { isImpulse = value; }
    }
    public string EffectName
    {
        get { return effectName; }
        set { effectName = value; }
    }
    public float DefaultMoveSpeed
    {
        get { return defaultMoveSpeed; }
        set { defaultMoveSpeed = value; }
    }
    public string EnemyName
    {
        get { return enemyName; }
        set { enemyName = value; }
    }
    public float MoveSpeed
    {
        get { return moveSpeed; }
        set { moveSpeed = value; }
    } 
    public bool IsStunned
    {
        get { return isStunned; }
        set { isStunned = value; }
    }
    public float MaxHealth
    {
        get { return maxHealth; }
        set { maxHealth = value; }
    }
    public Slider HealthBar
    {
        get { return healthBar; }
        set { healthBar = value; }
    }
    public Vector3 HealhBarOffset
    {
        get { return healhBarOffset; }
        set { healhBarOffset = value; }
    }
    public bool IsDead
    {
        get { return isDead; }
        set { IsDead = value; }
    }
    public Animator Animator
    {
        get { return animator; }
        set { animator = value; }
    }
    public PlayerController Player
    {
        get { return player; }
        set { player = value; }
    }
    public float AttackRadius
    {
        get { return attackRadius; }
        set { attackRadius = value; }
    }

    #endregion publicFields
    #region bools
    private bool isDead = false;
    private bool isStunned = false;
    public bool active;
    #endregion bools

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        currentHealth = maxHealth;
        healthBar.maxValue = MaxHealth;
        healthBar.value = healthBar.maxValue;
        moveSpeed = defaultMoveSpeed;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        magic = GetComponent<MagicController>();
        animator.SetTrigger("Spawn");
        Invoke("setSpawnDuration", 0.2f);
    }

    // Update is called once per frame
    void Update()
    {
        offset += Time.deltaTime;
        healthBar.transform.position = Camera.main.WorldToScreenPoint(transform.position + healhBarOffset);
        Vector2 playerDir = chooser.sectorToVector(transform.position, Player.transform.position);
        Animator.SetFloat("Horizontal", -playerDir.x);
        Animator.SetFloat("Vertical", -playerDir.y);
        if (offset <= spawnDuration)
        {
            return;
        }
        else
        {
        }
        if (!active)
        {
            return;
        }

        if (isImpulse)
        {
            GetStun(0.2f);
            rb.AddForce(((Vector2)transform.position - getPlayerPosition()).normalized*25, ForceMode2D.Force);
            return;
        }
        if (isStunned)
        {
            return;
        }
        if (magic.WaterStatus() == 0)
        {
            PerformAttack(getPlayerPosition());
        }
        FollowPlayer(getPlayerPosition());
        if (Input.GetKeyDown(KeyCode.K))
        {
            GetDamage(100);
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            rb.AddForce(-getPlayerPosition(),ForceMode2D.Impulse);
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            rb.velocity = new Vector2(0, 0);
        }
    }

    #region utils
    public Vector2 getPlayerPosition()
    {
        return player.GetComponent<Transform>().position;
    }
    public void setDefaultSpeed()
    {
        if (magic.canMove())
        {
            moveSpeed = defaultMoveSpeed;
        }
    }
    private void OnDestroy()
    {
        GetComponentInParent<Room>().Killed(gameObject);
    }
    void setSpawnDuration()
    {
        spawnDuration = animator.GetCurrentAnimatorClipInfo(0)[0].clip.length - 0.2f;
    }
    #endregion utils

    #region getStun
    public void GetStun(float duration=0.5f)
    {
        SetDefault();
        StartCoroutine(WaitForStunToEnd(duration));
    }

    IEnumerator WaitForStunToEnd(float duration)
    {
        moveSpeed = 0;
        isStunned = true;
        animator.Rebind();
        animator.SetBool("Stunned", true);
        animator.SetBool("CanMove", false);
        sprite.color = Color.black;
        yield return new WaitForSeconds(duration);
        animator.SetBool("Stunned", false);
        animator.SetBool("CanMove", true);
        isStunned = false;
        sprite.color = Color.white;
        moveSpeed = defaultMoveSpeed;

    }
    #endregion getStun

    #region getDamage
    public void GetDamage(float damage)
    {
        Debug.Log(damage);
        if (!isDead)
        {
            healthBar.gameObject.SetActive(true);
            currentHealth -= damage;
            healthBar.value = currentHealth;
        
            if (currentHealth <= 0)
            {
                isDead = true;
                animator.SetTrigger("Die");
                magic.effectHolder.gameObject.SetActive(false);
                magic.enabled = false;
                enabled = false;
                StartCoroutine(WaitForDeath());
            }
        }
    }
    IEnumerator WaitForDeath()
    {
        sprite.color = Color.white;
        healthBar.gameObject.SetActive(false);
        magic.effectHolder.gameObject.SetActive(false);
        yield return new WaitForSeconds(1f);
        gameObject.SetActive(false);
        Destroy(gameObject);

    }
    #endregion getDamage

    #region magic
    public void Slow()
    {
        moveSpeed = defaultMoveSpeed / 2;
    }
    #endregion magic

    #region overrides
    public virtual void FollowPlayer(Vector2 playerPosition)
    {
    }
    public virtual void PerformAttack(Vector2 playerPosition)
    {
    }
    public virtual void SetDefault()
    {

    }
    #endregion overrides



}

