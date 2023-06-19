using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinMenu : MonoBehaviour
{
    public GameObject winMenu;
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "LevelEnd")
        {
            winMenu.SetActive(true);
            Time.timeScale = 0f;
            PauseGame.isPaused = true;
        }
    }
}
