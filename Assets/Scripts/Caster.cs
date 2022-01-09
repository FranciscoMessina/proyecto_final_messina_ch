using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Caster : MonoBehaviour
{
    private GameManager _gm;
    [SerializeField] private Transform target; 
    [SerializeField] private float rotationSpeed = 5;
    private Animator _anim;

    [SerializeField] private GameObject bloodSpell;
    [SerializeField] private float spellSpeed;
    [SerializeField] private Transform spellSpawnPoint;

    [SerializeField] private float castDelay;
    private float castCooldown;
    private bool canCast;

    private OnDeathDrops onDeathDrops;

    private Quaternion rotation;

    // Start is called before the first frame update
    void Start()
    {
        _anim = GetComponent<Animator>();
        _gm = GameManager.instance;
        _gm.AddCasterToArray(this);
        onDeathDrops = new OnDeathDrops();
    }

    // Update is called once per frame
    void Update()
    {
        rotation = Quaternion.LookRotation(target.position - transform.position);

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

        rotation = Quaternion.LookRotation(target.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
        _anim.SetFloat("rotate", 1);


    }

    /*private void onCollisionStay(Collision collision)
    {
        if(collision.gameObject.tag == "PlayerSpell")
        {
            Die();
        }
    }*/

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "PlayerSpell")
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
        _anim.SetTrigger("die");
        Destroy(this.gameObject, 2.0f);
        canCast = false;
        GameObject newDrop = onDeathDrops.getDrops();
        Instantiate(newDrop);
    }
}
