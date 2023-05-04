using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BossHealth : MonoBehaviour
{
    private float lerpTimer;
    public float chipSpeed = 2f;
    public Image frontHealthBar;
    public Image backHealthBar;
    public MeleeEnemy script;
    private float BossMaxHP;
    private float BossHP;
    public string Name;
    public bool playerIsClose = false;

    public GameObject HealthUI;

    public TextMeshProUGUI textMesh;

    private void Start()
    {
        BossMaxHP = script.maxHealth;
        textMesh.text = Name;
    }

    // Update is called once per frame
    void Update()
    {
    
        UpdateHealthUI();
        BossHP = Mathf.Clamp(script.health, 0, BossMaxHP);
        CheckPlayer();
    }

    public void UpdateHealthUI()
    {
        float tempHealth = BossHP;
        float fillFront = frontHealthBar.fillAmount;
        float fillBack = backHealthBar.fillAmount;
        float HealthFraction = tempHealth / BossMaxHP;
        if (fillBack > HealthFraction)
        {
            frontHealthBar.fillAmount = HealthFraction;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / chipSpeed;
            percentComplete = percentComplete * percentComplete;
            backHealthBar.fillAmount = Mathf.Lerp(fillBack, HealthFraction, percentComplete);
        }
        if (fillFront < HealthFraction)
        {
            backHealthBar.fillAmount = HealthFraction;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / chipSpeed;
            percentComplete = percentComplete * percentComplete;
            frontHealthBar.fillAmount = Mathf.Lerp(fillFront, backHealthBar.fillAmount, percentComplete);
        }
    }


    public void CheckPlayer()
    {
        if (Vector3.Distance(script.target.position,
              transform.position) < script.toMaxHealthRadius)
        {
            HealthUI.SetActive(true);
        }

        if (Vector3.Distance(script.target.position,
              transform.position) > script.toMaxHealthRadius)
        {
            HealthUI.SetActive(false);
        }
    }

    public void TakeDamage()
    {
        lerpTimer = 0f;
    }

    public void RestoreHealth()
    {
        lerpTimer = 0f;
    }
}
