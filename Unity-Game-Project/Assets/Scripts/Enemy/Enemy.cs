using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyState
{
    idle,
    walk,
    attack,
    stagger
}

public class Enemy : MonoBehaviour
{

    [Header("State Machine")]
    public EnemyState currentState;

    [Header("Enemy Stats")]
    public float maxHealth;
    public float health;
    public string enemyName;
    public int baseAttack;
    public float moveSpeed;
    public Vector2 homePosition;
    private bool alive = true;

    [SerializeField] private AudioSource damageSoundEffect;


    private void OnEnable()
    {
        transform.position = homePosition;
        health = maxHealth;
        currentState = EnemyState.idle;
    }

    private void TakeDamage(float damage)
    {
        damageSoundEffect.Play();
        health -= damage;
        if (health <= 0)
        {
            
            this.gameObject.SetActive(false);
            alive = false;
        }
    
    }


    public void Knock(Rigidbody2D myRigidbody, float knockTime, float damage)
    {
        if (alive) { 
        StartCoroutine(KnockCo(myRigidbody, knockTime));
        TakeDamage(damage);
        }
    }

    private IEnumerator KnockCo(Rigidbody2D myRigidbody, float knockTime)
    {
        if (myRigidbody != null)
        {
            yield return new WaitForSeconds(knockTime);
            myRigidbody.velocity = Vector2.zero;
            currentState = EnemyState.idle;
            myRigidbody.velocity = Vector2.zero;
        }
    }

}
