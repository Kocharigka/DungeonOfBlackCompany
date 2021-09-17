using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy :MonoBehaviour
{
    private string enemyName;
    public string EnemyName
    {
        get { return enemyName; }
        set { name = value; }
    }

    private int maxHealth;
    public int MaxHealth
    {
        get { return maxHealth; }
        set { maxHealth = value; }
    }

    private int currentHealth;
    private bool isStunned = false;
    public bool IsStunned
    {
        get{ return isStunned; }
        set { isStunned = value; }
    }

    private float stunTime=2f;
    private Dictionary<string, float> resists;

    private SpriteRenderer sprite;
    private Animator animator;
    public Animator Animator
    {
        get { return animator; }
        set { animator = value; }
    }
    [SerializeField] private Slider healthBar;
    public Slider HealthBar
    {
        get { return healthBar; }
        set { healthBar = value; }
    }
    private Vector3 healhBarOffset;
    public Vector3 HealhBarOffset
    {
        get { return healhBarOffset; }
        set { healhBarOffset = value; }
    }
    private float moveSpeed = 1;
    public float MoveSpeed
    {
        get { return moveSpeed; }
        set { moveSpeed = value; }
    }
    private float attackRadius;
    public float AttackRadius
    {
        get { return attackRadius; }
        set { attackRadius = value; }
    }
    private bool isDead = false;
    public bool IsDead
    {
        get { return isDead; }
        set { IsDead = value; }
    }
    public bool active = false;
    private PlayerController player;
    public PlayerController Player
    {
        get { return player; }
        set { player = value; }
    }
    private int colDamage;
    public int ColDamage
    {
        get { return colDamage; }
        set { colDamage = value; }
    }

    private void Start()
    {
        currentHealth = maxHealth;
        healthBar.maxValue = MaxHealth;
        healthBar.value = healthBar.maxValue;
        sprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }
    void Update()
    {
        healthBar.transform.position = Camera.main.WorldToScreenPoint(transform.position + healhBarOffset);
    }
    public void GetStun()
    {
        isStunned = true;
        animator.speed = 0;
        StartCoroutine(WaitForStunToEnd());
        animator.speed = 1;
    }
    public void GetDamage(int damage)
    {
        healthBar.gameObject.SetActive(true);
        if(!isStunned)
        {
            GetStun();
        }

        currentHealth -= damage;
        healthBar.value = currentHealth;
        if (currentHealth <= 0)
        {
            isDead = true;
            animator.speed=0;
            enabled = false;
            StartCoroutine(WaitForDeath());
        }
    }
    IEnumerator WaitForStunToEnd()
    {
        sprite.color = Color.black;
        yield return new WaitForSeconds(stunTime);
        isStunned = false;
        sprite.color = Color.white;

    }
    IEnumerator WaitForDeath()
    {
        sprite.color = Color.white;
        healthBar.gameObject.SetActive(false);
        yield return new WaitForSeconds(1f);
        gameObject.SetActive(false);

    }
    public Vector2 getPlayerPosition()
    {
        return player.GetComponent<Transform>().position;
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            player.GetDamage(colDamage);
        }
    }
}
