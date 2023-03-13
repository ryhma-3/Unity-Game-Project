using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//tekoäly-funktio vihollishahmon ohjaamiseen. 
public class EnemyAI : MonoBehaviour
{
    //alustetaan muuttujia. 
    public float moveSpeed;
    public float checkRadius;
    
    public LayerMask playerMask;
    public Rigidbody2D rb;

    private Transform target;
    private bool isInChaseRange;

    private Vector2 movement;
    private Vector3 dir;
    
    // Käyttämätöntä koodia tulevaa käyttöä varten.
    //public float attackRadius;
    //public bool shouldRotate;
    //public Animator anim;
    //public Transform enemy;

    // Tätä funktiota kutsutaan kerran ohjelman käynnistyessä 
    // Pelaalle annetaan "Player"-nimike, jonka perusteella tekoäly havaitsee pelaajan hahmon. 
    private void Start()
    {
        target = GameObject.FindWithTag("Player").transform;
    }
    // tämä funktio varmistaa sen, onko pelaajahahmo tekoälynjahtausalueella.
    // se myös määrittelee jahtaussuunnan.
    private void Update()
    {
        isInChaseRange = Physics2D.OverlapCircle(transform.position, checkRadius, playerMask);
        //anim.SetBool("isMoving", isInChaseRange);

        dir = target.position - transform.position;
        dir.Normalize();
        movement = dir;
        // seuraavat käyttämättömät koodinpätkät ovat säästetty tulevaa animaatiota varten. 
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
    //fixed update funktiota kutsutaan 50 kertaa sekunnissa. Kun pelaaja-hahmo on tekoälyn jahtausalueella, 
    //se alkaa seuraamaan pelaajaa kutsumalla funktiota MoveCharacter().
    //Jos pelaaja liikkuu tämän alueen ulkopuolelle, vektori nollataan ja tekoäly lopettaa jahtaamisen. 
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
    // funktio, jolla vihollishahmot liikuu kartalla. 
    private void MoveCharacter()
    {
        rb.MovePosition((Vector2)transform.position + (movement * moveSpeed * Time.deltaTime));
    }
}
