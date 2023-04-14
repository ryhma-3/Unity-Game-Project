using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaSystem : MonoBehaviour
{

    private float lerpTimer;
    public float maxStamina;
    public float chipSpeed = 2f;
    public Image frontStamina;
    public Image backStamina;
    public float stamina;

    void Start()
    {
        stamina = maxStamina;
    }

    // Update is called once per frame
    void Update()
    {
        stamina = Mathf.Clamp(stamina, 0, maxStamina);
        UpdateStaminaUI();
        //Enabled 0 stamina sprint for Debug purposes, to make sure player cant sprint at 0 stamina change >= in the if clause to >.
        if (Input.GetKey(KeyCode.LeftShift) && stamina >= 0)
        {
            PlayerMovement.isSprinting = true;
            LooseStamina(0.25f);
        } else
        {
           PlayerMovement.isSprinting = false;
        }

        if (stamina < 100 && !Input.GetKey(KeyCode.LeftShift))
        {
            RestoreStamina(0.1f);
        }
    }

    public void UpdateStaminaUI()
    {
        float fillFront = frontStamina.fillAmount;
        float fillBack = backStamina.fillAmount;
        float StaminaFraction = stamina / maxStamina;
        if (fillBack > StaminaFraction)
        {
            frontStamina.fillAmount = StaminaFraction;
            backStamina.color = Color.clear;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / chipSpeed;
            percentComplete = percentComplete * percentComplete;
            backStamina.fillAmount = Mathf.Lerp(fillBack, StaminaFraction, percentComplete);
        }
        if (fillFront < StaminaFraction)
        {
            backStamina.color = Color.white;
            backStamina.fillAmount = StaminaFraction;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / chipSpeed;
            percentComplete = percentComplete * percentComplete;
            frontStamina.fillAmount = Mathf.Lerp(fillFront, backStamina.fillAmount, percentComplete);
        }
    }

    public void LooseStamina(float staminaLost)
    {
        stamina -= staminaLost;
        lerpTimer = 0f;
    }

    public void RestoreStamina(float staminaGained)
    {
        stamina += staminaGained;
        lerpTimer = 0f;
    }
}
