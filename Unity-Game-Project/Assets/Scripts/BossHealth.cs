using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BossHealth : MonoBehaviour
{
    private float lerpTimer;
    public FloatValue maxHealth;
    public float chipSpeed = 2f;
    public Image frontHealthBar;
    public Image backHealthBar;
    public FloatValue playerCurrentHealth;
    public SignalSender playerHealthSignal;


    // Update is called once per frame
    void Update()
    {
        UpdateHealthUI();
        playerCurrentHealth.RuntimeValue = Mathf.Clamp(playerCurrentHealth.RuntimeValue, 0, maxHealth.RuntimeValue);
    }

    public void UpdateHealthUI()
    {
        float tempHealth = playerCurrentHealth.RuntimeValue;
        float fillFront = frontHealthBar.fillAmount;
        float fillBack = backHealthBar.fillAmount;
        float HealthFraction = tempHealth / maxHealth.RuntimeValue;
        if (fillBack > HealthFraction)
        {
            frontHealthBar.fillAmount = HealthFraction;
            backHealthBar.color = Color.red;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / chipSpeed;
            percentComplete = percentComplete * percentComplete;
            backHealthBar.fillAmount = Mathf.Lerp(fillBack, HealthFraction, percentComplete);
        }
        if (fillFront < HealthFraction)
        {
            backHealthBar.color = Color.green;
            backHealthBar.fillAmount = HealthFraction;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / chipSpeed;
            percentComplete = percentComplete * percentComplete;
            frontHealthBar.fillAmount = Mathf.Lerp(fillFront, backHealthBar.fillAmount, percentComplete);
        }
    }

    public void TakeDamage(float damage)
    {
        playerCurrentHealth.RuntimeValue -= damage;
        lerpTimer = 0f;
    }

    public void RestoreHealth(float healAmount)
    {
        playerCurrentHealth.RuntimeValue += healAmount;
        lerpTimer = 0f;
    }
}
