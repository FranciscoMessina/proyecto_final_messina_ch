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


    // Start is called before the first frame update
    void Start()
    {
        startingLocation = transform.position;
        randomSpot = Random.Range(0, patrolPoints.Length);
    }

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

        // _anim.SetInteger("animState", 0);
    }

    protected void AttackTarget()
    {
        _anim.SetInteger("animState", 2);
        _anim.SetTrigger("attack");
        // GetComponent<Animator>().SetBool("attack", true);
    }

    protected void ChaseTarget()
    {
        _anim.ResetTrigger("attack");
        _anim.SetInteger("animState", 1);
         

        // GetComponent<Animator>().SetBool("attack", false);
        // GetComponent<Animator>().SetTrigger("move");
        navMeshAgent.SetDestination(target.position);
    }

    protected void FaceTarget() {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
    }

    // public virtual void MoveToPlayer()
    // {
    //     distanceToTarget = Vector3.Distance(transform.position, target.position);
    //     var direction = (target.position -  transform.position).normalized;
    //     Quaternion rotation = Quaternion.LookRotation(target.position - transform.position);
    //     transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);

    //     // TODO: Change animation to be reusable between enemies
    //     if (distance < maxFollowDistance && distance >= minFollowDistance)
    //     {// _anim.SetInteger("hAnim", 1);
    //         transform.position += direction * speed * Time.deltaTime;
    //     }
    //     // else _anim.SetInteger("hAnim", 0);

    // }

    void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chaseRange);
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

    
    // public virtual void MoveToDestination(Vector3 destination) {

    //     distance = Vector3.Distance(transform.position, destination);

    //     var direction = (destination - transform.position).normalized;
    //     direction.y = 0;
    //     Quaternion rotation = Quaternion.LookRotation(destination - transform.position);
    //     rotation.x = 0;
    //     transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);

    //     // TODO: Change animation to be reusable between enemies
    //     if(distance > 1f) {
    //         // _anim.SetInteger("tAnim", 1);
    //         transform.position += direction * speed * Time.deltaTime;
    //     } else {
    //         // _anim.SetInteger("tAnim", 0);
    //     }
    // }

    protected void GetTarget() {
        target = _gm.GetPlayerReference().transform;
    }
}
