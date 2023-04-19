using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinScreen : MonoBehaviour
{

    public GameObject victoryUI;
    public void ActivateVictory()
    {
        DeathScreen.playerDead = true;
        victoryUI.SetActive(true);
        Time.timeScale = 0f;
    }
}
