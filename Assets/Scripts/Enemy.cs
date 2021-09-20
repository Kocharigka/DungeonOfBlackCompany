using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    private float stunTime = 2f;

    private bool isStunned = false;
    public bool IsStunned
    {
        get { return isStunned; }
        set { isStunned = value; }
    }

    private int maxHealth;
    public int MaxHealth
    {
        get { return maxHealth; }
        set { maxHealth = value; }
    }
    private int currentHealth;
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
    private bool isDead = false;
    public bool IsDead
    {
        get { return isDead; }
        set { IsDead = value; }
    }
    Animator animator;
    private SpriteRenderer sprite;
    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        currentHealth = maxHealth;
        healthBar.maxValue = MaxHealth;
        healthBar.value = healthBar.maxValue;
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.transform.position = Camera.main.WorldToScreenPoint(transform.position + healhBarOffset);
    }
    public void GetStun()
    {
        StartCoroutine(WaitForStunToEnd());
    }

    IEnumerator WaitForStunToEnd()
    {
        isStunned = true;
        animator.SetBool("Stunned", true);
        animator.SetFloat("Speed", 0);
        sprite.color = Color.black;
        yield return new WaitForSeconds(stunTime);
        animator.SetBool("Stunned", false);
        animator.SetFloat("Speed", 1);
        isStunned = false;
        sprite.color = Color.white;

    }
    public void GetDamage(int damage)
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
    IEnumerator WaitForDeath()
    {
        sprite.color = Color.white;
        healthBar.gameObject.SetActive(false);
        yield return new WaitForSeconds(1f);
        gameObject.SetActive(false);

    }
}
