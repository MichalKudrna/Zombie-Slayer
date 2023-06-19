using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Normal : Enemy
{
    private void Start()
    {
        animator = GetComponent<Animator>();
        player = GameObject.Find("Player");
        agent = GetComponent<NavMeshAgent>();
        damage = 15 + Player.difficulty * 5;
        hp = 50 + Player.difficulty * 25;
        xp = 10;
        zvuk.volume = AudioManager.vol;
    }
    private void Update()
    {
        if (!dead)
        {
            playerInRange = Physics.CheckSphere(transform.position, sightRange, whatPlayer);
            playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatPlayer);
            RaycastHit raycast;
            if (!playerInRange && !playerInAttackRange) Patrol();
            if (playerInRange && !playerInAttackRange && Physics.Raycast(transform.position, player.transform.position - transform.position, out raycast))
            {
                if (raycast.transform.tag != "zed") Chase();
            }
            if (playerInRange && playerInAttackRange) Attack();
            if (onFire && Time.time >= nextFire)
            {
                Fire();
                nextFire = Time.time + fireWait;
            }
        }
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Fire"))
        {
            onFire = true;
            fire.Play();
        }
    }
}
