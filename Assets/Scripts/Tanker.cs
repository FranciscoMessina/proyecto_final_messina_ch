using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Tanker : BaseEnemy
{
    private Transform healerTarget;
    private float attackCooldown;
    private bool canAttack;
    private float damage;


    [SerializeField] private TankerData dataValues;


    // Start is called before the first frame update
    void Start()
    {
        // startingLocation = transform.position;
        // randomSpot = Random.Range(0, patrolPoints.Length);


        _anim = GetComponent<Animator>();
        _gm = GameManager.instance;
        Invoke("GetTarget", 0.1f);

        currentHealth = maxHealth;

        navMeshAgent = GetComponent<NavMeshAgent>();

        damage = dataValues.damage;
        speed = dataValues.speed;

    }

    void Update()
    {
        distanceToTarget = Vector3.Distance(target.position, transform.position);

        if(currentHealth < maxHealth) {
            if(DetectCaster() && healerTarget) {
                FallBack();
            }
        } else {
            if(isProvoked)
            {
                EngageTarget();
            } 
            else if (distanceToTarget <= chaseRange)
            {
                isProvoked = true;
            }
        }

        if (canAttack == false && attackCooldown >= 0) attackCooldown -= Time.deltaTime;
        else if (canAttack == false && attackCooldown <= 0) canAttack = true;
    }

    public void FallBack()
    {
        Quaternion rotation = Quaternion.LookRotation(healerTarget.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);

        if (distanceToTarget < chaseRange && distanceToTarget >= navMeshAgent.stoppingDistance)
        {
            _anim.SetInteger("animState", 1);
            navMeshAgent.SetDestination(healerTarget.position);
        }
        else _anim.SetInteger("animState", 0);

    }

    // private bool DetectPlayer()
    // {
    //     //RaycastHit hit;
    //     var direction = (target.position - transform.position).normalized;

    //     var collidedWithPlayer = Physics.Raycast(tankerEyes.position, direction, /*out hit,*/ maxFollowDistance, playerLayer);
    //     Debug.Log("Player Detected:" + collidedWithPlayer);

    //     return collidedWithPlayer;
    // }

    private bool DetectCaster()
    {
        //the idea here is to find a Caster near, to back away and get healed
        foreach (Caster c in _gm.casterList) {
       
            RaycastHit hit;
            Collider casterCollider = c.GetComponent<Collider>(); ;
            Physics.Raycast(raycastOrigin.position, c.transform.position, out hit, chaseRange, enemiesLayer);
            Debug.Log(hit.collider.CompareTag("Caster"));
            if (hit.collider == casterCollider) { 
                healerTarget = c.transform; 
                Debug.Log("Caste hit");
                return true;
            } else {
                return false;
            }
        };

        return false;

    }
}

