using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shotgun : Gun
{
    private static int level = 0;
    private static float xp = 0;
    private static float xpToNextLevel = 50;
    void Start()
    {
        damage = 8;
        range = 15;
        fireRate = 1;
        nextToFire = 0;
        ammo = 20;
        maxAmmo = 80;
        textChange();
        name = "Brokovnice";
    }
    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time >= nextToFire && ammo > 0 && !PauseGame.isPaused)
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
        bool mrtvy = false;
        for (int i=0;i<5;i++) {
            Vector3 direction = fpsCam.transform.forward;
            Vector3 spread=new Vector3();
            spread += fpsCam.transform.up * Random.Range(-0.7f, 0.7f); 
            spread += fpsCam.transform.right * Random.Range(-0.7f, 0.7f);
            direction += spread.normalized * Random.Range(0f, 0.2f);
            if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
            {
                Enemy enemy = hit.transform.GetComponentInParent<Enemy>();

                if (enemy != null)
                {
                    hits+=0.2f;
                    if (enemy.hp < damage && !mrtvy)
                    {
                        killed++;
                        mrtvy = true;
                    }
                    if (Player.powerUp)
                        enemy.TakeDamage(damage * 4);
                    else enemy.TakeDamage(damage);
                }
            } }
        ammo--;
        textChange();
    }
}
