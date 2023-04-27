using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinScreen : MonoBehaviour
{

    public GameObject victoryUI;
    public GameObject explosionsparent;
    public Image[] explosions;
    public void ActivateVictory()
    {
        DeathScreen.playerDead = true;
        victoryUI.SetActive(true);
        Time.timeScale = 0f;
        
        StartCoroutine(TemporarilyDeactivate());
    }

    private IEnumerator TemporarilyDeactivate()
    {
        
            yield return new WaitForSecondsRealtime(4);
            explosionsparent.SetActive(true);
    }
}
