using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public Transform[] spawnpoints;
    public GameObject normal;
    public GameObject thiccBoi;
    public GameObject throwBoi;
    public float spawnChance = 60f+Player.difficulty*10;
    public int spawnTime = 4-Player.difficulty;
    float currentTime;
    void Start()
    {
        int i = 0;
        foreach(Transform spawn in transform)
        {
            spawnpoints[i] = spawn;
            i++;
        }
        currentTime = spawnTime;
    }
    void Update()
    {
        currentTime -= Time.deltaTime;
        if (currentTime <= 0)
        {
            currentTime += spawnTime;
            for (int i = 0; i < transform.childCount - 1; i++) { 
                if (Random.Range(1, 100) <= spawnChance)
                    SpawnEnemy(i);
            }
        }
        
    }
    void SpawnEnemy(int i)
    {
        int random = Random.Range(1, 100);
        if (random <= 64 - Player.difficulty * 7) { 
            Instantiate(normal, spawnpoints[i].transform.position, Quaternion.identity);
        } else if (random <= 86 - Player.difficulty * 4)
        {
            Instantiate(throwBoi, spawnpoints[i].transform.position, Quaternion.identity);
        } else
        {
            Instantiate(thiccBoi, spawnpoints[i].transform.position, Quaternion.identity);
        }
    }
}
