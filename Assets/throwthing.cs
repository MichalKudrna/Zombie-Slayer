using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class throwthing : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<Player>().TakeDamage(10+5*Player.difficulty);
        }
        Destroy(this.gameObject);
    }
}
