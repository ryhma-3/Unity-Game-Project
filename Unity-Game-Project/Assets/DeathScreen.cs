using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathScreen : MonoBehaviour
{

    public GameObject deathscreenUI;
    public static bool playerDead = false;
    public void ActivateDeathScreen()
    {
        playerDead = true;
        deathscreenUI.SetActive(true);
        Time.timeScale = 0f;
    }
}
