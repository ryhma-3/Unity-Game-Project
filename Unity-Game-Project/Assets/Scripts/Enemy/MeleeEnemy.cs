using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : skeleton
{
    public float toMaxHealthRadius;
    public float attackAnimationDelay = (1f);
    [SerializeField] private AudioSource attackSoundEffect;

    void Start()
    {
        anim.SetBool("wakeUp", true);
    }

    public override void CheckDistance()
    {
        if (Vector3.Distance(target.position,
                transform.position) <= chaseRadius &&
            Vector3.Distance(target.position,
                transform.position) > attackRadius)
        {
            if (currentState == EnemyState.idle || currentState == EnemyState.walk &&
                currentState != EnemyState.stagger)
            {
                Vector3 temp = Vector3.MoveTowards(transform.position,
                    target.position,
                    moveSpeed * Time.deltaTime);
                changeAnim(temp - transform.position);
                myRigidbody.MovePosition(temp);
                ChangeState(EnemyState.walk);
                anim.SetBool("wakeUp", true);
            }
        }
        else if (Vector3.Distance(target.position,
              transform.position) <= chaseRadius &&
          Vector3.Distance(target.position,
              transform.position) <= attackRadius)
        {
            if (currentState == EnemyState.walk &&
                currentState != EnemyState.stagger)
            {
                myRigidbody.bodyType = RigidbodyType2D.Static;
                myRigidbody.bodyType = RigidbodyType2D.Dynamic;
                StartCoroutine(AttackCo());
            }
        }

        else if (Vector3.Distance(target.position,
              transform.position) > chaseRadius)
        {
            if(Vector3.Distance(target.position,
              transform.position) > toMaxHealthRadius)
            {
                health = maxHealth;
            }

            myRigidbody.bodyType = RigidbodyType2D.Static;
            myRigidbody.bodyType = RigidbodyType2D.Dynamic;
            anim.SetBool("wakeUp", false);
        }
    }

    public IEnumerator AttackCo()
    {
        currentState = EnemyState.attack;
        //attackSoundEffect.Play();
        anim.SetBool("attack", true);
        yield return new WaitForSeconds(attackAnimationDelay);
        currentState = EnemyState.walk;
        anim.SetBool("attack", false);
    }
}