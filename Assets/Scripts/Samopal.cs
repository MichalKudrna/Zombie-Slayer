using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Samopal : Gun
{
    private void Start()
    {
        damage = 8;
        range = 100;
        fireRate = 5;
        nextToFire = 0;
        ammo = 50;
        maxAmmo = 250;
        textChange();
        name = "Samopal";
    }
    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time >= nextToFire && ammo > 0)
        {
            nextToFire = Time.time + 1f / fireRate;
            Shoot();
            fired++;
        }
        if (Player.powerUp)
        {
            powerTime -= Time.deltaTime;
            if (powerTime <= 0) Player.powerUp = false;
        }
    }
    void Shoot()
    {
        muzzleFlash.Play();
        zvuk.Play();
        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            Enemy enemy = hit.transform.GetComponentInParent<Enemy>();
            
            if (enemy != null)
            {
                hits++;
                if (enemy.hp <= damage)
                {
                    killed++;
                }
                if (Player.powerUp)
                    enemy.TakeDamage(damage * 4);
                else enemy.TakeDamage(damage);
            }
        }
        ammo--;
        textChange();
    }
}
