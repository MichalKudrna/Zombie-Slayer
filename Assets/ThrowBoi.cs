using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ThrowBoi : Enemy
{
    public GameObject throwThing;
    public GameObject throwPoint;
    private void Start()
    {
        player = GameObject.Find("Player");
        agent = GetComponent<NavMeshAgent>();
        damage = 10 + Player.difficulty * 5;
        hp = 40 + Player.difficulty * 15;
        xp = 8;
        zvuk.volume = AudioManager.vol;
    }
    private void Update()
    {
        if (!dead)
        {
            playerInRange = Physics.CheckSphere(transform.position, sightRange, whatPlayer);
            playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatPlayer);
            RaycastHit raycast;
            if (!playerInAttackRange) Patrol();
            if (playerInAttackRange && Physics.Raycast(transform.position, player.transform.position - transform.position, out raycast))
            {
                if (raycast.transform.tag != "zed")
                {
                    if (Physics.Raycast(transform.position, transform.forward, out raycast))
                    {
                        if (raycast.transform.tag == "Player" || raycast.transform.tag == "MainCamera" || raycast.transform.tag == "Fire")
                        {
                            agent.SetDestination(this.transform.position);
                            Attack();
                        }
                        else
                        {
                            agent.SetDestination(this.transform.position);
                            Vector3 dir = player.transform.position - transform.position;
                            dir.y = 0;
                            transform.rotation = Quaternion.LookRotation(dir);
                        }
                    }
                }
                else Patrol();
            }
        }
    }
    new public void Attack()
    {
        if (!attacked)
        {
            animator.SetBool("isAttacking", true);
            attacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
            Invoke(nameof(Hozeni), 1f);
        }

    }
    private void ResetAttack()
    {
        attacked = false;
    }
    private void Hozeni()
    {
        GameObject projectile = Instantiate(throwThing, throwPoint.transform.position, transform.rotation);
        Rigidbody rigidbody = projectile.GetComponent<Rigidbody>();
        Vector3 forceToAdd = transform.forward * 14 + transform.up * 3;
        rigidbody.AddForce(forceToAdd, ForceMode.Impulse);
    }
}