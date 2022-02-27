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
    private Quaternion rotation;


    [SerializeField] private CasterData dataValues;

    // Start is called before the first frame update
    void Start()
    {
        _anim = GetComponent<Animator>();
        _gm = GameManager.instance;
        _gm.AddCasterToArray(this);
        Invoke("GetTarget", 0.1f);

        // For patrol behaviour.
        // startingLocation = transform.position;
        // randomSpot = Random.Range(0, patrolPoints.Length);
    

        currentHealth = maxHealth;

        spellSpeed = dataValues.spellSpeed;
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
        FaceTarget();
        if (canCast == true)
        {
            Cast();
            canCast = false;
            castCooldown = castDelay;
        }
    }

    public override void FaceTarget()
    {
        base.FaceTarget();
        _anim.SetFloat("rotate", 1);
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


    public override void Die() {
        base.Die();
        canCast = false;
    }
}
