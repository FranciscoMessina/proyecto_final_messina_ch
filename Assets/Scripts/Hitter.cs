using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitter : BaseEnemy
{
    private float attackCooldown;
    private bool canAttack;

    // Start is called before the first frame update
    void Start()
    {
        _anim = GetComponent<Animator>();
        _gm = GameManager.instance;
        Invoke("GetTarget", 0.1f);

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
        MoveToPlayer();
        Attack();
    }

    public override void MoveToPlayer()
    {
        base.MoveToPlayer();

        if (distance < maxFollowDistance && distance >= minFollowDistance)
        {
            _anim.SetInteger("hAnim", 1);
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

