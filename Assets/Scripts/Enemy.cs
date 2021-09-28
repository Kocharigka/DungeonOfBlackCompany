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
    private float stunTime = 2f;
    private int maxHealth;
    [SerializeField] private Slider healthBar;
    private Vector3 healhBarOffset;
    private Animator animator;
    private SpriteRenderer sprite;
    private PlayerController player;
    private float attackRadius;
    private int currentHealth;
    #endregion privateStatic
    #region publicFields
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
    public int MaxHealth
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
        sprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        currentHealth = maxHealth;
        healthBar.maxValue = MaxHealth;
        healthBar.value = healthBar.maxValue;
        moveSpeed = defaultMoveSpeed;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.transform.position = Camera.main.WorldToScreenPoint(transform.position + healhBarOffset);
        if (!active)
        {
            return;
        }
        if (isStunned)
        {
            return;
        }
        PerformAttack(getPlayerPosition());
        FollowPlayer(getPlayerPosition());
    }

    #region utils
    public Vector2 getPlayerPosition()
    {
        return player.GetComponent<Transform>().position;
    }
    public void setDefaultSpeed()
    {
        Debug.Log("setSpeed");
        moveSpeed = defaultMoveSpeed;
    }
    #endregion utils

    #region getStun
    public void GetStun()
    {
        SetDefault();
        StartCoroutine(WaitForStunToEnd());
    }

    IEnumerator WaitForStunToEnd()
    {
        moveSpeed = 0;
        isStunned = true;
        animator.SetBool("Stunned", true);
        animator.SetBool("CanMove", false);
        sprite.color = Color.black;
        yield return new WaitForSeconds(stunTime);
        animator.SetBool("Stunned", false);
        animator.SetBool("CanMove", true);
        isStunned = false;
        sprite.color = Color.white;
        moveSpeed = defaultMoveSpeed;

    }
    #endregion getStun

    #region getDamage
    public void GetDamage(int damage)
    {
        if (!isDead)
        {
            healthBar.gameObject.SetActive(true);
        if (!isStunned)
        {
            GetStun();
        }

        currentHealth -= damage;
        healthBar.value = currentHealth;
        
            if (currentHealth <= 0)
            {
                isDead = true;
                animator.SetTrigger("Die");
                enabled = false;
                StartCoroutine(WaitForDeath());
            }
        }
    }
    IEnumerator WaitForDeath()
    {
        sprite.color = Color.white;
        healthBar.gameObject.SetActive(false);
        yield return new WaitForSeconds(1f);
        gameObject.SetActive(false);
        Destroy(gameObject);

    }
    #endregion getDamage

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
