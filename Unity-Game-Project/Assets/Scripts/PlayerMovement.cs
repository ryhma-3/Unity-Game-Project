using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Hahmon ohjausfunktio
public class PlayerMovement : MonoBehaviour
{
    // pelihahmon nopeuden säätäminen
    public float moveSpeed = 5f;
    
    //Rigidbody 2D määrittelee hahmon fysiikan.
    public Rigidbody2D rb;
    // Animator määrittelee hahmon animaatiot.
    public Animator animator;

    // Vector 2 määrittelee liikkumisen suunnan.
    Vector2 movement;
    
    // Update-funktio välittää näppäinkomennot pelille 
    void Update()
    {
        //Vaakasuoraisen ja pystysuoraisen liikkumisen määrittely
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        
        // Välittää arvoja pelaajan suunnasta animaattorille.
        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        // Välittää arvon pelaajan nopeudesta animaattorille. 
        animator.SetFloat("Speed", movement.sqrMagnitude);
    }
    // FixedUpdate-funktio liikuttaa pelaajaa. 
    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }


}
