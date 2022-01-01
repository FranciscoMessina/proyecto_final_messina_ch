using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Tanker : MonoBehaviour
{
    private Transform target;
    private Transform healerTarget;

    [SerializeField] private float speed = 5;    
    [SerializeField] private float rotationSpeed = 5;
    [SerializeField] private float maxFollowDistance = 50;
    [SerializeField] private float minFollowDistance = 4;

    private GameManager _gm;

    private int currentHealth;
    [SerializeField] private int maxHealth = 100;
    
    private float distance;

    private Animator _anim;

    [SerializeField] private float attackDelay;
    private float attackCooldown;
    private bool canAttack;

    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private LayerMask enemiesLayer;

    // Start is called before the first frame update
    void Start()
    {
        _anim = GetComponent<Animator>();
        _gm = GameManager.instance;
        Invoke("GetTarget", 0.1f);
        currentHealth = maxHealth;
    }

    void Update()
    {
        distance = Vector3.Distance(transform.position, target.position);

        if (canAttack == false && attackCooldown >= 0) attackCooldown -= Time.deltaTime;
        else if (canAttack == false && attackCooldown <= 0) canAttack = true;
    }

    private void FixedUpdate()
    {
        /*if (currentHealth <= maxHealth / 4) {
            try { FallBack(); } catch (Exception e){ Debug.Log(e); } }
        else*/ if (DetectPlayer()) { Move(); }
        Move();
        Attack();
    }

    private void GetTarget() {
        target = _gm.GetPlayerReference().transform;
    }

    public void Move()
    {
        var direction = (target.position - transform.position).normalized;
        Quaternion rotation = Quaternion.LookRotation(target.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);

        if (distance < maxFollowDistance && distance >= minFollowDistance)
        {
            _anim.SetInteger("tAnim", 1);
            transform.position += direction * speed * Time.deltaTime;
        }
        else _anim.SetInteger("tAnim", 0);

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
        if (distance < minFollowDistance)
        {
            _anim.SetTrigger("attack");
            canAttack = false;
            attackCooldown = attackDelay;
        }
    }

    private bool DetectPlayer()
    {
        //RaycastHit hit;
        var direction = (target.position - transform.position).normalized;

        var collidedWithPlayer = Physics.Raycast(transform.position, direction, /*out hit,*/ maxFollowDistance, playerLayer);
        Debug.Log("Player Detected:" + collidedWithPlayer);

        return collidedWithPlayer;
    }

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

