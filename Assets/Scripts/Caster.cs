using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Caster : BaseEnemy
{

    [SerializeField] private GameObject bloodSpell;
    [SerializeField] private float spellSpeed;
    [SerializeField] private Transform spellSpawnPoint;
    [SerializeField] private float castDelay;
    public int baseDmg;
    private float castCooldown;
    private bool canCast;
    private bool dead = false;
    private Quaternion rotation;


    [SerializeField] private CasterData dataValues;

    // Start is called before the first frame update
    void Start()
    {
        _anim = GetComponent<Animator>();
        _gm = GameManager.instance;
        _gm.AddCasterToArray(this);
        Invoke("GetTarget", 0.1f);
    

        spellSpeed = dataValues.spellSpeed;
        maxHealth = dataValues.casterHealth;
        speed = dataValues.walkSpeed;
        baseDmg = dataValues.casterDamage;


    }

    // Update is called once per frame
    void Update()
    {
        if (canCast == false && castCooldown >= 0)
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
        Rotate();
        if (canCast == true)
        {
            Cast();
            canCast = false;
            castCooldown = castDelay;
        }
    }

    public void Rotate()
    {
        Quaternion rotation = Quaternion.LookRotation(target.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
        _anim.SetFloat("rotate", 1);
    }

    

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "PlayerSpell" && dead == false)
        {
            Die();
        }
    }

    private void Cast()
    {
        Invoke("CastSpell", .6f);
        _anim.SetTrigger("cast");
    }

    private void CastSpell()
    {
        GameObject newspell = Instantiate(bloodSpell, spellSpawnPoint.position, Quaternion.LookRotation((target.position - transform.position).normalized)) as GameObject;
        Rigidbody spellRB = newspell.GetComponent<Rigidbody>();

        spellRB.velocity = this.transform.forward * spellSpeed;
    }

    public void Die()
    {
        dead = true;
        _anim.SetTrigger("die");
        Destroy(this.gameObject, 2.0f);
        canCast = false;
        _gm.GenerateDrop(this.gameObject.transform.position);
    }
}
