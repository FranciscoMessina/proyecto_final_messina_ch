using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 8;
    [SerializeField] private float sideSpeed = 6;
    private Rigidbody _rb;
    private CinemachineTouchInputMapper _cM;

    private float vInput;
    private float hInput;

    private Animator _anim;

    private bool canShoot = true;
    [SerializeField] private float shootDelay = 1;
    private bool canArea = true;
    [SerializeField] private float areaDelay = 4;
    private float shootTimer;
    private float areaTimer;

    [SerializeField] private GameObject iceSpell;
    [SerializeField] private float spellSpeed;
    [SerializeField] private Transform spellSpawnPoint;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _anim = GetComponent<Animator>();
        _cM = GetComponent<CinemachineTouchInputMapper>();

        
        
    }

    // Update is called once per frame
    private void Update()
    {
        vInput = Input.GetAxis("Vertical") * moveSpeed;
        hInput = Input.GetAxis("Horizontal") * sideSpeed;


        if (canShoot == false && shootTimer >= 0)
        {
            shootTimer -= Time.deltaTime;
        }
        else if (canShoot == false && shootTimer <= 0)
        {
            canShoot = true;
        }
    }


    void FixedUpdate()
    {

        Walk();

        if (Input.GetButton("Fire1") && canShoot)
        {
            Shoot();
            shootTimer = shootDelay;
        }

        else if (Input.GetMouseButtonDown(1) && canArea)
        {
            _anim.SetInteger("AnimNum", 5);
        }



    }

    void Shoot()
    {
        Invoke("CastSpell", .7f) ;
        //GameObject newspell = Instantiate(iceSpell, spellSpawnPoint.position, this.transform.rotation) as GameObject;
        _anim.SetInteger("AnimNum", 4);
        canShoot = false;
        Debug.Log("Shoot called");
    }

    private void CastSpell()
    {
        GameObject newspell = Instantiate(iceSpell, spellSpawnPoint.position, this.transform.rotation) as GameObject;
        Rigidbody spellRB =  newspell.GetComponent<Rigidbody>();

        spellRB.velocity = this.transform.forward * spellSpeed;
    }

    void Walk()
    {
        if (Input.GetButton("Vertical"))
        {

            if (Input.GetAxis("Vertical") == -1) { _anim.SetInteger("AnimNum", -1); }
            else if (Input.GetAxis("Vertical") == 1) { _anim.SetInteger("AnimNum", 1); }
        }
        else if (Input.GetButton("Horizontal"))
        {


            if (Input.GetAxis("Horizontal") == -1) { _anim.SetInteger("AnimNum", -2); }
            else if (Input.GetAxis("Horizontal") == 1) { _anim.SetInteger("AnimNum", 2); }
        }
        else
        {
            _anim.SetInteger("AnimNum", 0);
        }

        Vector3 forward = this.transform.forward * vInput * Time.fixedDeltaTime;

        Vector3 sideways = this.transform.right * hInput * Time.fixedDeltaTime;

        _rb.MovePosition(this.transform.position + forward + sideways);
    }
}
