using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionScript : MonoBehaviour
{

    public bool playerIsClose = false;
    public FloatValue potioncounter;

    // Update is called once per frame
    void Update()
    {
        if (playerIsClose)
        {
            //Add a potion to the potion counter
            potioncounter.RuntimeValue += 1;
            //remove potion object
            Destroy(this.gameObject);
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsClose = true;
        }

    }
}
