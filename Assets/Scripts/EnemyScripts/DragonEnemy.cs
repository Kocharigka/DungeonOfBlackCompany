using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonEnemy : MonoBehaviour
{
    float moveSpeed = 5;
    Animator animator;
    Vector2 direction;
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        direction.x = Input.GetAxisRaw("Horizontal");
        direction.y = Input.GetAxisRaw("Vertical");
        animator.SetFloat("Horizontal", direction.x);
        animator.SetFloat("Vertical", direction.y);
        if (direction.sqrMagnitude != 0) {
        animator.SetBool("CanMove", true);
        }
        else
        {
            animator.SetBool("CanMove", false);
        }
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            animator.SetTrigger("Attack");
        }
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            animator.SetTrigger("Shoot");
        }
        rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime);

    }
}
