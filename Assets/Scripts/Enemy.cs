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

    private void Start()
    {
        currentHealth = maxHealth;
        healthBar.maxValue = MaxHealth;
        healthBar.value = healthBar.maxValue;
        sprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        healthBar.transform.position = Camera.main.WorldToScreenPoint(transform.position + healhBarOffset);
    }
    public void GetStun()
    {
        isStunned = true;
        StartCoroutine(WaitForStunToEnd());
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
            animator.speed = 0;
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
        healthBar.gameObject.transform.Find("Fill Area").Find("Fill").GetComponent<Image>().color = new Color(256, 256, 256, 0);
        healthBar.gameObject.SetActive(false);
        yield return new WaitForSeconds(1f);
        gameObject.SetActive(false);

    }
    public void SetAnimatorSpeed(int speed)
    {
        animator.speed = speed;
    }
}
