using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPotion : MonoBehaviour
{
    public float chance;
    public GameObject potion;

    private void OnDisable()
    {
        if (!this.gameObject.scene.isLoaded) return;
        GeneratePotion(chance);
    }

    public void GeneratePotion(float chance)
    {   
        if (Random.Range(0, 100) <= chance ) 
        {
            GameObject prefab_gameobject = Instantiate(potion, transform.position, Quaternion.identity);
            prefab_gameobject.name = "Potion";
        }
        
    }
}
