using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public int level;
    public int diff;
    public bool arena;
    public int hp;
    public int ammo1;
    public int fired1;
    public int killed1;
    public string name1;
    public float hits1;
    public int ammo2;
    public int fired2;
    public int killed2;
    public string name2;
    public float hits2;
    public int ammo4;
    public int fired4;
    public int killed4;
    public string name4;
    public float hits4;
    public int ammo3;
    public int fired3;
    public int killed3;
    public string name3;
    public float hits3;
    public float cas;
    public GameData()
    {
        level = 1;
        diff = 0;
        arena = false;
        ammo1=0;
        fired1=0;
        killed1=0;
        name1="";
        hits1=0;
        ammo2 = 0;
        fired2 = 0;
        killed2 = 0;
        name2 = "";
        hits2 = 0;
        ammo3 = 0;
        fired3 = 0;
        killed3 = 0;
        name3 = "";
        hits3 = 0;
        ammo4 = 0;
        fired4 = 0;
        killed4 = 0;
        name4 = "";
        hits4 = 0;
    }
}
