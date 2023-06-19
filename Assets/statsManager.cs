using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class statsManager : MonoBehaviour
{
    int selected;
    public GameObject weaponMenu;
    private void Start()
    {
        selected = 0;
        foreach (Transform weapon in weaponMenu.transform)
        {
            weapon.gameObject.SetActive(true);
        }
        Jina();
    }
    public void Next()
    {
        if (selected == weaponMenu.transform.childCount) selected = 0;
        else selected++;
        Jina();
    }
    public void Prev()
    {
        if (selected <= 0) selected = weaponMenu.transform.childCount;
        else selected--;
        Jina();
    }
    public void Jina()
    {
        if(selected == weaponMenu.transform.childCount)
        {
            transform.GetChild(0).GetComponent<Text>().text = "Granát";
            transform.GetChild(1).GetComponent<Text>().text = "Vyhození: "+Grenade.thrown;
            transform.GetChild(2).GetComponent<Text>().text = "Zabití: "+Grenade.killed;
            transform.GetChild(3).GetComponent<Text>().text = "";
            transform.GetChild(4).GetComponent<Text>().text = "";
            transform.GetChild(5).GetComponent<Text>().text = "";
        } else
        {
            int i = 0;
            foreach(Transform weapon in weaponMenu.transform)
            {
                if (i == selected)
                {
                    weapon.gameObject.SetActive(true);
                    transform.GetChild(0).GetComponent<Text>().text = weapon.GetComponent<Gun>().name;
                    transform.GetChild(1).GetComponent<Text>().text = "Výstřely: " + weapon.GetComponent<Gun>().fired;
                    transform.GetChild(2).GetComponent<Text>().text = "Zabití: " + weapon.GetComponent<Gun>().killed;
                    transform.GetChild(3).GetComponent<Text>().text = "Přesnost: " + (float)((float)weapon.GetComponent<Gun>().hits / weapon.GetComponent<Gun>().fired)*100+"%";
                    transform.GetChild(4).GetComponent<Text>().text = "Poškození: "+weapon.GetComponent<Gun>().damage;
                    transform.GetChild(5).GetComponent<Text>().text = "";
                }
                
                i++;
            }
            
        }
    }
}
