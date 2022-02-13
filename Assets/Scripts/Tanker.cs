using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Tanker : BaseEnemy
{
    private Transform healerTarget;
    private float currentHealth;
    private float attackCooldown;
    private bool canAttack;
    private float damage;

    [SerializeField] private TankerData dataValues;


    // Start is called before the first frame update
    void Start()
    {
        _anim = GetComponent<Animator>();
        _gm = GameManager.instance;
        Invoke("GetTarget", 0.1f);
        // startingLocation = transform.position;
        // randomSpot = Random.Range(0, patrolPoints.Length);
        // Debug.Log(currentHealth);

        currentHealth = dataValues.maxHealth;
        damage = dataValues.damage;
        speed = dataValues.speed;

    }

    void Update()
    {

        if (canAttack == false && attackCooldown >= 0) attackCooldown -= Time.deltaTime;
        else if (canAttack == false && attackCooldown <= 0) canAttack = true;
    }

    void FixedUpdate()
    {
        /*if (currentHealth <= maxHealth / 4) {
            try { FallBack(); } catch (Exception e){ Debug.Log(e); } }
        else*/
        
        
        if (DetectPlayer()) { 
            MoveToPlayer();
            Attack(); 
        } else {
            Patrol();
        }
        
    }


    public override void MoveToPlayer()
    {
        base.MoveToPlayer();

        if (distance < maxFollowDistance && distance >= minFollowDistance)
        {
            _anim.SetInteger("tAnim", 1);
            // transform.position += direction * speed * Time.deltaTime;
        }
        else _anim.SetInteger("tAnim", 0);

    }

    public override void MoveToDestination(Vector3 destination) {
        base.MoveToDestination(destination);

        if(distance > 1f) {
            _anim.SetInteger("tAnim", 1);
            // transform.position += base. * speed * Time.deltaTime;
        } else {
            _anim.SetInteger("tAnim", 0);
        }
    }

    public void FallBack()
    {
        var direction = (healerTarget.position - transform.position).normalized;
        Quaternion rotation = Quaternion.LookRotation(healerTarget.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);

        if (distance < maxFollowDistance && distance >= minFollowDistance)
        {
            _anim.SetInteger("tAnim", 1);
            transform.position += direction * speed * Time.deltaTime;
        }
        else _anim.SetInteger("tAnim", 0);

    }

    private void Attack()
    {
        if (distance < minFollowDistance && canAttack)
        {
            _anim.SetTrigger("attack");
            canAttack = false;
            attackCooldown = attackDelay;
        } else {
            _anim.ResetTrigger("attack");
        }
    }

    // private bool DetectPlayer()
    // {
    //     //RaycastHit hit;
    //     var direction = (target.position - transform.position).normalized;

    //     var collidedWithPlayer = Physics.Raycast(tankerEyes.position, direction, /*out hit,*/ maxFollowDistance, playerLayer);
    //     Debug.Log("Player Detected:" + collidedWithPlayer);

    //     return collidedWithPlayer;
    // }

    private void DetectCaster()
    {
        //the idea here is to find a Caster near, to back away and get healed
        foreach (Caster c in _gm.casterList) {
            RaycastHit hit;
            Collider casterCollider = c.GetComponent<Collider>(); ;
            Physics.Raycast(transform.position, c.transform.position, out hit, maxFollowDistance, enemiesLayer);
            if (hit.collider == casterCollider) { healerTarget = c.transform; break; }
        };

    }
}

