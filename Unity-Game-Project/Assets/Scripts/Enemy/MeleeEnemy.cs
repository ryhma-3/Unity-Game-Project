using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : skeleton
{

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
                StartCoroutine(AttackCo());
            }
        }

        else if (Vector3.Distance(target.position,
              transform.position) > chaseRadius)
        {
            anim.SetBool("wakeUp", false);
        }
    }

    public IEnumerator AttackCo()
    {
        currentState = EnemyState.attack;
        //attackSoundEffect.Play();
        anim.SetBool("attack", true);
        yield return new WaitForSeconds(1f);
        currentState = EnemyState.walk;
        anim.SetBool("attack", false);
    }
}