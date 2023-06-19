using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AchievementManager : MonoBehaviour
{
    public Sprite ano;
    public Sprite ne;
    private void Start()
    {
        int i = 0;
        foreach(Transform ach in transform)
        {
            string klic = "Ach" + i;
            if (PlayerPrefs.GetInt(klic)==1)
            {
                ach.GetComponentInChildren<Image>().sprite = ano;
            } else
            {
                ach.GetComponentInChildren<Image>().sprite = ne;
            }
            i++;
        }
    }
}
