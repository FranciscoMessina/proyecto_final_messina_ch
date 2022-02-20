using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class BaseEnemy : MonoBehaviour
{
    [SerializeField] protected float maxHealth = 100;
    [SerializeField] protected float speed = 10;
    [SerializeField] protected float rotationSpeed = 10;
    [SerializeField] protected float chaseRange = 20;
    [SerializeField] protected float attackDelay = 3;
    [SerializeField] protected Transform raycastOrigin;
    //[SerializeField] protected OnDeathDrops onDeathDrops;
    [SerializeField] protected Transform[] patrolPoints;
    [SerializeField] protected LayerMask playerLayer;
    [SerializeField] protected LayerMask enemiesLayer;
    protected NavMeshAgent navMeshAgent;
    protected bool isProvoked = false;
    protected Vector3 startingLocation;
    protected int randomSpot;
    protected float waitTime;
    public float startWaitTime;
    protected Transform target;
    protected GameManager _gm;
    protected Animator _anim;
    protected float distanceToTarget;
    public int meleeDamage;
    protected bool dead = false;

    protected float currentHealth;


    protected virtual void EngageTarget()
    {
        FaceTarget();
        if(distanceToTarget >= navMeshAgent.stoppingDistance) 
        {   
            ChaseTarget();
        }

        if(distanceToTarget <= navMeshAgent.stoppingDistance)
        {
            AttackTarget();
        }

    }

    protected void AttackTarget()
    {
        _anim.SetInteger("animState", 2);
        _anim.SetTrigger("attack");
    }

    protected void ChaseTarget()
    {
        _anim.ResetTrigger("attack");
        _anim.SetInteger("animState", 1);
         
        navMeshAgent.SetDestination(target.position);
        
    }

    protected void FaceTarget() {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
    }

    void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chaseRange);
    }

    public virtual void Die()
    {
        dead = true;
        _anim.SetTrigger("die");
        Destroy(this.gameObject, 2.0f);
        _gm.GenerateDrop(this.gameObject.transform.position);
    }

    public void TakeDamage(float dmg) 
    {
        currentHealth -= dmg;
        isProvoked = true;
        Debug.Log(currentHealth);

        if(currentHealth <= 0 && dead == false)
        {
            Die();
        }
    }

    // protected void Patrol()
    // {
    //     MoveToDestination(patrolPoints[randomSpot].position);
    //     // Debug.Log("Patrol Called");

    //     if(Vector3.Distance(transform.position, patrolPoints[randomSpot].position) < 1f) {
    //         if(waitTime <= 0) {
    //             randomSpot = Random.Range(0, patrolPoints.Length);
    //         } else {
    //             waitTime -= Time.deltaTime;
    //         }
    //     }
    // }

    // protected bool DetectPlayer()
    // {
    //     //RaycastHit hit;
    //     var direction = (target.position - transform.position).normalized;

    //     var collidedWithPlayer = Physics.Raycast(raycastOrigin.position, direction, /*out hit,*/ maxFollowDistance, playerLayer);
    //     if(collidedWithPlayer) Debug.Log("Player Detected:" + collidedWithPlayer);

    //     return collidedWithPlayer;
    // }


    protected void GetTarget() {
        target = _gm.GetPlayerReference().transform;
    }
}
