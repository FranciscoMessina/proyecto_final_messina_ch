using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitter : MonoBehaviour
{

    private GameManager _gm;
    [SerializeField] private Transform target; 
    [SerializeField] private float speed = 5;
    [SerializeField] private float rotationSpeed = 5;

    [SerializeField] private float maxFollowDistance = 50;
    [SerializeField] private float minFollowDistance = 4;

    private float distance;

    private Animator _anim;

    [SerializeField] private float attackDelay;
    private float attackCooldown;
    private bool canAttack;

    // Start is called before the first frame update
    void Start()
    {
        _anim = GetComponent<Animator>();
        _gm = GameManager.instance;
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector3.Distance(transform.position, target.position);

        if (canAttack == false && attackCooldown >= 0) attackCooldown -= Time.deltaTime;
        else if (canAttack == false && attackCooldown <= 0) canAttack = true;
    }

    private void FixedUpdate()
    {
        Move();
        Attack();
    }

    public void Move()
    {
        var direction = (target.position -  transform.position).normalized;
        Quaternion rotation = Quaternion.LookRotation(target.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);

        if (distance < maxFollowDistance && distance >= minFollowDistance)
        {
            _anim.SetInteger("hAnim", 1);
            transform.position += direction * speed * Time.deltaTime;
        }
        else _anim.SetInteger("hAnim", 0);

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
}

