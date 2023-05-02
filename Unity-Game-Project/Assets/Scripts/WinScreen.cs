using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinScreen : MonoBehaviour
{

    public GameObject victoryUI;
    public GameObject[] explosionsparent;

    [SerializeField] private AudioSource winSound;
    public void ActivateVictory()
    {
        DeathScreen.playerDead = true;
        victoryUI.SetActive(true);
        Time.timeScale = 0f;
        winSound.Play();
        StartCoroutine(TemporarilyDeactivate());
    }

    private IEnumerator TemporarilyDeactivate()
    {   
        foreach (GameObject parent in explosionsparent)
        {
            yield return new WaitForSecondsRealtime(1);
            parent.SetActive(true);
        }
    }
}
