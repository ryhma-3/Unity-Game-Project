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
    public bool isBoss;
    public BossHealth script;

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
        if (isBoss)
        {
            script.TakeDamage();
        }
        StartCoroutine(Flash());
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

    public SpriteRenderer sprite;

    public IEnumerator Flash()
    {
        float HealthPercent = health/maxHealth + 0.1f;
        Color newColor = new(HealthPercent, 0f, 0f);
        sprite.color = newColor;
        yield return new WaitForSeconds(0.1f);
        sprite.color = Color.white;
    }

}
