using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    public GameObject explosion;
    float timer = 2;
    float countdown;
    float radius = 5;
    public static int killed = 0;
    public static int thrown = 0;
    private void Start()
    {
        countdown = timer;
    }
    private void Update()
    {
        countdown -= Time.deltaTime;
        if (countdown <= 0)
        {
            Explode();
        }
    }
    void Explode()
    {
        int p = 0;
        GameObject spawnedParticle = Instantiate(explosion, transform.position, transform.rotation);
        Destroy(spawnedParticle, 1);
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);
        foreach(Collider nearbyObject in colliders)
        {
            Enemy enemy = nearbyObject.transform.GetComponentInParent<Enemy>();

            if (enemy != null)
            {
                float distance = Vector3.Distance(transform.position, nearbyObject.transform.position);
                enemy.TakeDamage((5 - distance) * 15);
                if (enemy.hp <= (5 - distance) * 15)
                {
                    killed++;
                    p++;
                }
                if (p >= 5) PlayerPrefs.SetInt("Ach0", 1);
            }
        }
        Destroy(gameObject);
    }
} 
