using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState
{
    attack,
    interact,
    walk
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

    public Signali playerHealthSignal;

    //viittaus playerHealth skriptiin hp-bar käsittelyä varten
    PlayerHealth health;

    void Start()
    {
        currentState = PlayerState.walk;
        transform.position = startingPosition.initialValue;
    }


    // Update-funktio välittää näppäinkomennot pelille 
    void Update()
    {
        //Vaakasuoraisen ja pystysuoraisen liikkumisen määrittely
        movement = Vector3.zero;
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        if (Input.GetButtonDown("attack") && currentState != PlayerState.attack )
        {
            StartCoroutine(AttackCo());
        }
        else if (currentState == PlayerState.walk)
        {
            UpdateAnimation();
        }
        
    }

    private IEnumerator AttackCo()
    {
        animator.SetBool("attacking", true);
        currentState = PlayerState.attack;
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
            // Välittää arvon pelaajan nopeudesta animaattorille. 
            animator.SetBool("moving", true);
        }
        else
        {
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
        currentHealth.RuntimeValue -= damage;
        if(currentHealth.RuntimeValue > 0)
        {
            playerHealthSignal.Raise();
            //StartCoroutine(KnockCo(knockTime));
        } else
        {
            //death handling
            this.gameObject.SetActive(false);
        }
    }


}
