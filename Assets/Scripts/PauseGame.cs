using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame : MonoBehaviour
{
    public static bool isPaused = false;
    public GameObject pauseUI;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused == true)
            {
                if (pauseUI.activeInHierarchy)
                {
                    Resume();
                    AudioManager.StopSound(true);
                }
                
            }
            else
            { 
                Pause();
                AudioManager.StopSound(false);
            }
        }
    }
    public void Resume()
    {
        pauseUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }
    public void Pause()
    {
        pauseUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }
}
