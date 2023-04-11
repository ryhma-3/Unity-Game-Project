using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState
{
    attack,
    interact,
    walk,
    stagger,
    idle
}

// Hahmon ohjausfunktio
public class PlayerMovement : MonoBehaviour
{
    public PlayerState currentState;

    // pelihahmon nopeuden säätäminen
    public float speed;
    
    //Rigidbody 2D määrittelee hahmon fysiikan.
    public Rigidbody2D rb;
    // Animator määrittelee hahmon animaatiot.
    public Animator animator;

    // Vector 2 määrittelee liikkumisen suunnan.
    Vector3 movement;

    public VectorValue startingPosition;

    public FloatValue currentHealth;

    public static bool isSprinting;

    private PlayerHealth health;

    [SerializeField] private AudioSource walkSoundEffect;
    [SerializeField] private AudioSource meleeSoundEffect;

    void Start()
    {
        currentState = PlayerState.walk;
        walkSoundEffect.Pause();
        transform.position = startingPosition.initialValue;
        health = GameObject.FindWithTag("Healthsystem").GetComponent<PlayerHealth>();
    }


    // Update-funktio välittää näppäinkomennot pelille 
    void Update()
    {
        //Vaakasuoraisen ja pystysuoraisen liikkumisen määrittely
        movement = Vector3.zero;
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        if (Input.GetButtonDown("attack") && currentState != PlayerState.attack && currentState != PlayerState.stagger)
        {
            StartCoroutine(AttackCo());
        }
        else if (currentState == PlayerState.walk || currentState == PlayerState.idle)
        {
            UpdateAnimation();
            
        }

            if (!isSprinting)
        {
            speed = 5;
        }
        else
        {
            speed = 10;
        }
    }

    private IEnumerator AttackCo()
    {
        animator.SetBool("attacking", true);
        currentState = PlayerState.attack;
        meleeSoundEffect.Play();
        yield return null;
        animator.SetBool("attacking", false);
        yield return new WaitForSeconds(.33f);
        currentState = PlayerState.walk;
    }

    void UpdateAnimation()
    {
        if (movement != Vector3.zero)
        {
            
            MoveCharacter();
            // Välittää arvoja pelaajan suunnasta animaattorille.
            animator.SetFloat("Horizontal", movement.x);
            animator.SetFloat("Vertical", movement.y);
            walkSoundEffect.UnPause();
            // Välittää arvon pelaajan nopeudesta animaattorille. 
            animator.SetBool("moving", true);
            
        }
        else
        {
            walkSoundEffect.Pause();
            animator.SetBool("moving", false);
        }
    }
    // FixedUpdate-funktio liikuttaa pelaajaa. 
    void MoveCharacter()
    {
        rb.MovePosition(transform.position + movement * speed * Time.fixedDeltaTime);
    }

    public void Knock(float knockTime, float damage)
    {
        health.TakeDamage(damage);
        if (currentHealth.RuntimeValue > 0)
        {
            StartCoroutine(KnockCo(knockTime));
        } else
        {
            //death handling
            this.gameObject.SetActive(false);
        }
    }


    private IEnumerator KnockCo(float knockTime)
    {
        
        if (rb != null)
        {
            yield return new WaitForSeconds(knockTime);
            rb.velocity = Vector2.zero;
            currentState = PlayerState.idle;
            rb.velocity = Vector2.zero;
        }
    }




}
