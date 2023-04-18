using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class potionUI : MonoBehaviour
{
    public TextMeshProUGUI textMesh;
    // Start is called before the first frame update
    public FloatValue potions;
    private PlayerHealth health;

    void Start()
    {
        health = GameObject.FindWithTag("Healthsystem").GetComponent<PlayerHealth>();
    }

    private void Update()
    {
        textMesh.text = potions.RuntimeValue.ToString();
    }

    public void UsePotion()
    {
        potions.RuntimeValue -= 1;
        health.RestoreHealth(25);
    }
}
