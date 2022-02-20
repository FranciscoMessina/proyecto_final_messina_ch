using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Hitter : BaseEnemy
{
    private float attackCooldown;
    private bool canAttack;
    // private bool dead = false;
    public int dmg = 10;

    // Start is called before the first frame update
    void Start()
    {
        _anim = GetComponent<Animator>();
        _gm = GameManager.instance;
        Invoke("GetTarget", 0.1f);
        navMeshAgent = GetComponent<NavMeshAgent>();
        currentHealth = maxHealth;


        startingLocation = transform.position;
        randomSpot = Random.Range(0, patrolPoints.Length);
    }

    // Update is called once per frame
    void Update()
    {
        distanceToTarget = Vector3.Distance(transform.position, target.position);

        if(isProvoked)
        {
            EngageTarget();
        } 
        else if (distanceToTarget <= chaseRange)
        {
            isProvoked = true;
        }

        if (canAttack == false && attackCooldown >= 0) attackCooldown -= Time.deltaTime;
        else if (canAttack == false && attackCooldown <= 0) canAttack = true;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "PlayerSpell" && dead == false)
        {
            TakeDamage(10);
        }
    }


    // public void Die()
    // {
    //     dead = true;
    //     _anim.SetTrigger("die");
    //     Destroy(this.gameObject, 2.0f);
    //     _gm.GenerateDrop(this.gameObject.transform.position);
    // }

    
}

