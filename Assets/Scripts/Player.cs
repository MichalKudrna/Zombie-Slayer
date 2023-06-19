using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    private static Dictionary<int, int> cihlyVLevelu = new Dictionary<int, int>()
    {
        { 1, 5 },
        { 2, 5 },
        { 3, 5 },
        { 4, 5 }
    };
    public static int currentLevel = 1;
    public GameObject brana;
    public static int pocetCihel;
    public GameObject cihlyText;
    public int cihly = -1;
    public static int difficulty = 0;
    public static int hp = 100;
    public GameObject hpText;
    public Slider slider;
    public static bool powerUp = false;
    public static bool arena = false;
    public static float cas = 0;
    public GameObject casText;
    public GameObject loseMenu;
    public GameObject hitScreen;
    public void TakeDamage(float amount)
    {
        hp -= (int)amount;
        if (hp <= 0f)
        {
            hp = 0;
            loseMenu.SetActive(true);
            Time.timeScale = 0f;
            PauseGame.isPaused = true;
        }
        hpText.GetComponent<Text>().text = hp + "";
        slider.value = hp;
        var color = hitScreen.GetComponent<Image>().color;
        color.a = 0.8f;
        hitScreen.GetComponent<Image>().color = color;
        }
    public void cihlyUpdate(int cih)
    {
        if (Player.arena)
        {
            cihlyText.GetComponent<Text>().text = "";
        }
        else
        {
            cihly += cih;
            cihlyText.GetComponent<Text>().text = cihly + " / " + pocetCihel;
            if (cihly == pocetCihel)
            {
                Destroy(brana);
            }
        }
    }
    public static void NovyLevel()
    {
        pocetCihel = cihlyVLevelu[currentLevel];
    }
    public void Update()
    {
        if (arena) cas += Time.deltaTime; else cas -= Time.deltaTime;
        if (cas > 300 & arena) PlayerPrefs.SetInt("Ach2", 1);
        casText.GetComponent<Text>().text = (int)cas+"";
        if (cas < 0f & !arena)
        {
            loseMenu.SetActive(true);
            Time.timeScale = 0f;
            PauseGame.isPaused = true;
        }
        if (hitScreen != null)
        {
            if (hitScreen.GetComponent<Image>().color.a > 0)
            {
                var color = hitScreen.GetComponent<Image>().color;
                color.a -= 0.01f;
                hitScreen.GetComponent<Image>().color = color;
            }
        }
    }
    public void Start()
    {
        if (arena) cas = 0; else cas = 20;
    }
}
