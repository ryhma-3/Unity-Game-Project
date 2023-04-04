using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    public GameObject settingsMenuUI;
    public AudioMixer audioMixer;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void LoadMenu() 
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");

    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void OpenSettings()
    {
        pauseMenuUI.SetActive(false);
        settingsMenuUI.SetActive(true);
    }

    public void ExitSettings()
    {
        settingsMenuUI.SetActive(false);
        pauseMenuUI.SetActive(true);
    }

    public void SetVolume (float volume)
    {
        Debug.Log(volume);
        audioMixer.SetFloat("Volume", volume);
    }


}
