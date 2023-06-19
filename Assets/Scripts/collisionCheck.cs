using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collisionCheck : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Fist")
        {
            this.GetComponentInParent<Player>().TakeDamage(other.transform.root.GetComponent<Enemy>().damage);
        }
        if (other.gameObject.tag == "Gold")
        {
            Destroy(other.gameObject);
            this.GetComponentInParent<Player>().cihlyUpdate(1);
            this.transform.GetChild(0).GetComponent<Abilities>().fireReload += 5;
            Player.cas += 10;
        }
        if (other.gameObject.tag == "Ammo")
        {
            this.transform.GetChild(0).GetChild(0).GetComponent<WeaponSwitch>().AddAmmo(other.gameObject.GetComponent<ammo>().id);
            Destroy(other.gameObject);
        }
        if (other.gameObject.tag == "powerup")
        {
            Destroy(other.transform.parent.gameObject);
            Player.powerUp = true;
        }
        if (other.gameObject.tag == "death")
        {
            Player.cas = 0f;
        }
    }
}
