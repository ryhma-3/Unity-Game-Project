using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float moveSpeed;
    public float checkRadius;
    //public float attackRadius;

    //public bool shouldRotate;

    public LayerMask playerMask;
    //public Animator anim;
    //public Transform enemy;
    public Rigidbody2D rb;
    public Animator anim;

    private Transform target;
    private bool isInChaseRange;

    private Vector2 movement;
    private Vector3 dir;


    private void Start()
    {
        target = GameObject.FindWithTag("Player").transform;
    }
    private void Update()
    {
        isInChaseRange = Physics2D.OverlapCircle(transform.position, checkRadius, playerMask);
        anim.SetBool("isMoving", isInChaseRange);

        dir = target.position - transform.position;
        dir.Normalize();
        movement = dir;

        /*if (dir.x > 0)
        {
            enemy.eulerAngles = new Vector3(0, 0, 0);
        }
        else
        {
            enemy.eulerAngles = new Vector3(0, 180, 0);
        }
        */
    }

    private void FixedUpdate()
    {
        if (isInChaseRange)
        {
            MoveCharacter();
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }

    private void MoveCharacter()
    {
        rb.MovePosition((Vector2)transform.position + (movement * moveSpeed * Time.deltaTime));
    }
}