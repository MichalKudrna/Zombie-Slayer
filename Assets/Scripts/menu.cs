using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menu : MonoBehaviour
{
    public GameObject loseMenu;
    public void GuitGame()
    {
        Application.Quit();
    }
    public void GoToMenu()
    {
        if(loseMenu.activeInHierarchy) DataPersistanceManager.instance.LoadGame();
        Time.timeScale = 1f;
        SceneManager.LoadSceneAsync("Menu");
    }
    public void NextLevel()
    {
        Player.currentLevel++;
        SceneManager.LoadScene("Level"+Player.currentLevel);
        Player.NovyLevel();
        Time.timeScale = 1f;
        PauseGame.isPaused = false;
    }
    public void SetDiff(int diff)
    {
        Player.difficulty = diff;
    }
    public void setArena()
    {
        Player.arena = !Player.arena;
    }
    public void Save()
    {
        DataPersistanceManager.instance.SaveGame();
    }
    public void Again()
    {
        DataPersistanceManager.instance.LoadGame();
        loseMenu.SetActive(false);
        Time.timeScale = 1f;
        PauseGame.isPaused = false;
        foreach(Enemy ene in FindObjectsOfType<Enemy>())
        {
            Destroy(ene.gameObject);
        }
        if (!Player.arena)
        {
            SceneManager.LoadScene("Level" + Player.currentLevel);
        }else
        {
            SceneManager.LoadScene("Arena");
            Player.cas = 0f;
        }
        
    }
}
