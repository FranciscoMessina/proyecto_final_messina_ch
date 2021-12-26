using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Caster : MonoBehaviour
{
    [SerializeField] private Transform target; 
    [SerializeField] private float rotationSpeed = 5;
    private Animator _anim;

    [SerializeField] private float castDelay;
    private float castCooldown;
    private bool canCast;

    // Start is called before the first frame update
    void Start()
    {
        _anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(canCast == false && castCooldown >= 0)
        {
            castCooldown -= Time.deltaTime;
        }
        else if(canCast == false && castCooldown <= 0)
        {
            canCast = true;
        }
    }

    private void FixedUpdate()
    {
        Move();
        if (canCast == true)
        {
            Cast();
            canCast = false;
            castCooldown = castDelay;
        }
    }

    public void Move()
    {

        Quaternion rotation = Quaternion.LookRotation(target.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
        _anim.SetFloat("rotate", 1);


    }

    private void Cast()
    {
        _anim.SetTrigger("cast");
    }

    public void Die()
    {
        _anim.SetTrigger("die");
    }
}
