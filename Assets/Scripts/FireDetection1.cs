using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireDetection1 : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Fire")
        {
            transform.GetComponentInParent<Enemy>().onFire = true;
            transform.GetComponentInParent<Enemy>().fire.Play();
        }
    }
}