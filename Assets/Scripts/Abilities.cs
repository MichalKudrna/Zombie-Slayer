using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Abilities : MonoBehaviour
{
    public Slider grenLoad;
    public Slider fireLoad;
    public GameObject throwPoint;
    public GameObject grenade;
    public GameObject fireCollider;
    public ParticleSystem ohen;
    public int fireReload = 20;
    float grenadeReload = 15f;
    float nextToFire;
    float nextToGrenade;
    bool zapalene = true;
    private void Start()
    {
        nextToFire = Time.time-20f;
        nextToGrenade = Time.time-15f;
    }
    void Update()
    {
        if (Input.GetKey(KeyCode.F) && fireLoad.value > 0 && zapalene)
        {
            Invoke(nameof(Fire),1);
            fireLoad.value = fireReload;
            ohen.Play();
            zapalene = false;
            RaycastHit hit;
            if (Physics.Raycast(this.transform.position, this.transform.forward, out hit, 5))
            {
                Enemy enemy = hit.transform.GetComponentInParent<Enemy>();
                if (enemy != null)
                {
                    enemy.onFire = true;
                    enemy.fire.Play();
                }
            }
        }
        if (Input.GetKey(KeyCode.G) && Time.time >= nextToGrenade)
        {
            nextToGrenade = Time.time + grenadeReload;
            Grenadeth();
        }
        if (Time.time > nextToGrenade + 15f)
        {
            grenLoad.value = 15f;
        }
        else
        {
            grenLoad.value = Time.time - nextToGrenade +15f;
        }
        if (fireReload > 20) fireReload = 20;
    }
    public void Grenadeth()
    {
        GameObject projectile = Instantiate(grenade, throwPoint.transform.position, transform.rotation);
        Rigidbody rigidbody = projectile.GetComponent<Rigidbody>();
        Vector3 forceToAdd = transform.forward * 10 + transform.up*5;
        rigidbody.AddForce(forceToAdd,ForceMode.Impulse);
        Grenade.thrown++;
    }
    private void Fire()
    {
        fireCollider.SetActive(false);
        fireReload -= 1;
        zapalene = true;
    }
}
