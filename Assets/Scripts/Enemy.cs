
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public Animator animator;
    public float damage;
    public float hp;
    public float xp;
    public NavMeshAgent agent;
    public GameObject player;
    public LayerMask whatGround, whatPlayer;
    public Vector3 walkPoint;
    public bool walkPointSet;
    public float walkPointRange;
    public float timeBetweenAttacks;
    public bool attacked;
    public float sightRange, attackRange;
    public bool playerInRange, playerInAttackRange;
    public bool onFire = false;
    public ParticleSystem fire;
    public float nextFire = 0;
    public float fireWait = 1.5f;
    public GameObject ammo1;
    public GameObject ammo2;
    public GameObject ammo3;
    public GameObject ammo4;
    public AudioSource zvuk;
    public bool dead = false;
    private void Start()
    {
        player = GameObject.Find("Player");
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
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
    public void TakeDamage(float amount)
    {
        hp -= amount;
        if (hp <= 0f)
        {
            if (Player.arena) Player.cas += 2;
            Die();
            dead = true;
        }
    }
    void Die()
    {
        if (Random.value >= 0.5f)
        {
            float rand = Random.value;
            if(rand<=0.25f) Instantiate(ammo1, this.transform.position, this.transform.rotation); 
            else if(rand<=0.4f) Instantiate(ammo2, new Vector3(transform.position.x,transform.position.y+0.2f,transform.position.z), this.transform.rotation);
            else if(rand<=0.75f) Instantiate(ammo3, new Vector3(transform.position.x, transform.position.y + 0.2f, transform.position.z), this.transform.rotation);
            else Instantiate(ammo4, this.transform.position, this.transform.rotation);
        }
        agent.SetDestination(transform.position);
        animator.SetBool("dead", true);
        Destroy(gameObject,2.5f);
    }
    public void Patrol()
    {
        animator.SetBool("isAttacking", false);
        if (!walkPointSet) SearchWalkPoint();
        if (walkPointSet)
        {
            agent.SetDestination(walkPoint);
        }
        Vector3 distanceToPoint = transform.position - walkPoint;
        if (distanceToPoint.magnitude < 1f)
            walkPointSet = false;
    }

    private void SearchWalkPoint()
    {
        float randomZ = Random.Range(-walkPointRange,walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);
        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y ,transform.position.z + randomZ);
        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatGround))
            walkPointSet = true;
    }

    public void Chase()
    {
        animator.SetBool("isAttacking", false);
        agent.SetDestination(player.transform.position);
    }
    public void Attack()
    {
        agent.SetDestination(transform.position);
        animator.SetBool("isAttacking", true);
        if (!attacked)
        {
            attacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }
    private void ResetAttack()
    {
        attacked = false;
    }
    public void Fire()
    {
        hp -= 10f;
    }
    public void novaHlasitost(float novy)
    {
        zvuk.volume = novy;
    }
    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Fire"))
        {
            onFire = true;
            fire.Play();
        }
    }
}
