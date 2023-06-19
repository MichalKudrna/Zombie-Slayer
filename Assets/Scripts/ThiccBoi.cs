using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ThiccBoi : Enemy
{
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        player = GameObject.Find("Player");
        agent = GetComponent<NavMeshAgent>();
        damage = 40 + Player.difficulty * 10;
        hp = 250 + Player.difficulty * 60;
        xp = 25;
        zvuk.volume = AudioManager.vol;
    }

    // Update is called once per frame
    void Update()
    {
        if (!dead)
        {
            playerInRange = Physics.CheckSphere(transform.position, sightRange, whatPlayer);
            playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatPlayer);
            RaycastHit raycast;
            if (!playerInRange && !playerInAttackRange) Patrol();
            if (playerInRange && !playerInAttackRange && Physics.Raycast(transform.position, player.transform.position - transform.position, out raycast))
            {
                if (raycast.transform == player)
                    Chase();
            }
            if (playerInRange && playerInAttackRange) Attack();
            if (onFire && Time.time >= nextFire)
            {
                Fire();
                nextFire = Time.time + fireWait;
            }
        }
    }
}
