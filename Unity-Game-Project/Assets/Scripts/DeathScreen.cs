using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathScreen : MonoBehaviour
{

    public GameObject deathscreenUI;
    public static bool playerDead = false;

    [SerializeField] private AudioSource deathSound;
    public void ActivateDeathScreen()
    {
        playerDead = true;
        deathscreenUI.SetActive(true);
        deathSound.Play();
        Time.timeScale = 0f;
    }
}
