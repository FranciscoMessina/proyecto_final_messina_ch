using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class BaseEnemy : MonoBehaviour
{
    [SerializeField] protected float maxHealth = 100;
    [SerializeField] protected int scoreOnDeath = 25;
    [SerializeField] protected float speed = 10;
    [SerializeField] protected float rotationSpeed = 10;
    [SerializeField] protected float chaseRange = 20;
    [SerializeField] protected Transform raycastOrigin;
    [SerializeField] protected LayerMask playerLayer;
    [SerializeField] protected LayerMask enemiesLayer;
    [SerializeField] protected AudioSource _as;
    [SerializeField] protected AudioClip deathClip;
    [SerializeField] protected AudioClip attackClip;
    protected NavMeshAgent navMeshAgent;
    protected bool isProvoked = false;
    // For PATROL behaviour on hold for now.
    // [SerializeField] protected Transform[] patrolPoints;
    // protected Vector3 startingLocation;
    // protected int randomSpot;
    // protected float waitTime;
    // public float startWaitTime;
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
        _as.clip = attackClip;
        _as.Play();
    }

    protected void ChaseTarget()
    {
        _anim.ResetTrigger("attack");
        _anim.SetInteger("animState", 1);
         
        navMeshAgent.SetDestination(target.position);
    }

    public virtual void FaceTarget() {
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
        Destroy(this.gameObject, 3.0f);
        _gm.AddPoints(scoreOnDeath);
        _gm.GenerateDrop(this.gameObject.transform.position);
        _as.clip = deathClip;
        Invoke("PlaySound", 0.5f);
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

    private void PlaySound()
    {
        _as.Play();
    }

    protected void GetTarget() {
        target = _gm.GetPlayerReference().transform;
    }
}
