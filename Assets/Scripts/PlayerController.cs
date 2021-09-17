using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 direction;
    public float moveSpeed = 5f;
    private Animator animator;
    private float attackRange = 1f;
    public LayerMask enemyLayer;
    private int maxHealth=100;
    private int currentHealth;

    private Dictionary<string, (int, int)> attackSectors= new Dictionary<string, (int, int)>() 
    {
        //{"LU",(0,90) },
        {"R",(45,135)},
        //{"LD",(90,180)},
        {"D",(135,225)},
        //{"RD",(180,270)},
        {"L",(225,315)}
        //{"RU",(270,360)}
    };
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
        if (Input.GetMouseButtonDown(0))
        {
            performAttack();
        }
    }
    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime);
    }
    private void performAttack()
    {
        string sector = getSector(getAngle(Camera.main.ScreenToWorldPoint(Input.mousePosition)));
        //play Animation
        Collider2D[] hitEnemites = Physics2D.OverlapCircleAll(transform.position, attackRange, enemyLayer);
        foreach (Collider2D enemyCollider in hitEnemites)
        {
            Enemy enemy = enemyCollider.GetComponent<Enemy>();
            float angle=getAngle(enemyCollider.gameObject.transform.position);
            if (sector != "U")
            {   
                if (attackSectors[sector].Item1 <= angle && angle <= attackSectors[sector].Item2 && !enemy.IsDead)
                {
                    enemy.GetDamage(2);
                }
            }
            else if(315<=angle && angle<360 || 0<angle&& angle< 45 && !enemy.IsDead)
            {
                enemy.GetDamage(2);
            }
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(new Vector2(transform.position.x+0.2f,transform.position.y+0.2f), attackRange);
    }
    private float getAngle(Vector3 point)
    {
        point = new Vector2(point.x - transform.position.x, point.y - transform.position.y);
        float angle = Vector2.Angle(new Vector2(0,10), point);
        if (point.x < 0)
        {
            angle = 360 - angle;
        }

        return angle;
    }
    private string getSector(float angle)
    {
        foreach (KeyValuePair<string,(int,int)> sector in attackSectors)
        {

            if (sector.Value.Item1 <= angle && angle <= sector.Value.Item2)
            {
                return sector.Key;
            }
        }
        return "U";
    }
    public void GetDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log(currentHealth);
    }
}
