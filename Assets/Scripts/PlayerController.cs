using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 8;
    [SerializeField] private float sideSpeed = 6;
    private Rigidbody _rb;
<<<<<<< HEAD
    private CinemachineTouchInputMapper _cM;
=======
    [SerializeField] private Camera _cam;
>>>>>>> Main-Character-New-Animations

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

<<<<<<< HEAD
=======
    [SerializeField] private GameObject areaSpell;

>>>>>>> Main-Character-New-Animations
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _anim = GetComponent<Animator>();
<<<<<<< HEAD
        _cM = GetComponent<CinemachineTouchInputMapper>();
=======
        //_cam = Camera.main;
>>>>>>> Main-Character-New-Animations



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
<<<<<<< HEAD
=======

        if (canArea == false && areaTimer >= 0)
        {
            areaTimer -= Time.deltaTime;
        }
        else if (canArea == false && areaTimer <= 0)
        {
            canArea = true;
        }
>>>>>>> Main-Character-New-Animations
    }


    void FixedUpdate()
    {

<<<<<<< HEAD
        Walk();
=======
        Walk(vInput, hInput);
        Rotate();

        //float xAxis = Input.GetAxis("Vertical");
        //float zAxis = Input.GetAxis("Horizontal");
>>>>>>> Main-Character-New-Animations

        if (Input.GetButton("Fire1") && canShoot)
        {
            Shoot();
            shootTimer = shootDelay;
        }

<<<<<<< HEAD
        else if (Input.GetMouseButtonDown(1) && canArea)
        {
            _anim.SetInteger("AnimNum", 5);
=======
        else if (Input.GetButton("Fire2") && canArea)
        {
            Area();
            areaTimer = areaDelay;            
>>>>>>> Main-Character-New-Animations
        }



    }

    void Shoot()
    {
        Invoke("CastSpell", .6f);
        //GameObject newspell = Instantiate(iceSpell, spellSpawnPoint.position, this.transform.rotation) as GameObject;
<<<<<<< HEAD
        _anim.SetInteger("AnimNum", 4);
        canShoot = false;
        Debug.Log("Shoot called");
=======
        _anim.SetInteger("AnimNum", 1);
        canShoot = false;
        Debug.Log("Shoot called");
        //_anim.SetInteger("AnimNum", 0);
    }

    void Area()
    {
        Invoke("CastArea", 1f);
        _anim.SetInteger("AnimNum", 2);
        canArea = false;
        Debug.Log("Area called");
>>>>>>> Main-Character-New-Animations
    }

    private void CastSpell()
    {
        GameObject newspell = Instantiate(iceSpell, spellSpawnPoint.position, this.transform.rotation) as GameObject;
        Rigidbody spellRB = newspell.GetComponent<Rigidbody>();

        spellRB.velocity = this.transform.forward * spellSpeed;
    }

<<<<<<< HEAD
    void Walk()
    {
        if (Input.GetButton("Vertical"))
        {

            if (Input.GetAxis("Vertical") == -1) { _anim.SetInteger("AnimNum", -1); }
            else if (Input.GetAxis("Vertical") == 1) { _anim.SetInteger("AnimNum", 1); }
=======
    private void CastArea()
    {
        GameObject newarea = Instantiate(areaSpell, this.transform.position, this.transform.rotation) as GameObject;
    }

    void Rotate()
    {
        Quaternion CharacterRotation = _cam.transform.rotation;
        CharacterRotation.x = 0;
        CharacterRotation.z = 0;

        this.transform.rotation = CharacterRotation;
    }

    void Walk(float x, float z)
    {

        _anim.SetFloat("MoveX", x);
        _anim.SetFloat("MoveZ", z);


        /*if (Input.GetButton("Vertical"))
        {

            if (Input.GetAxis("Vertical") == -1) { _anim.SetInteger("MoveX", -1); }
            else if (Input.GetAxis("Vertical") == 1) { _anim.SetInteger("MoveX", 1); }
>>>>>>> Main-Character-New-Animations
        }
        else if (Input.GetButton("Horizontal"))
        {


<<<<<<< HEAD
            if (Input.GetAxis("Horizontal") == -1) { _anim.SetInteger("AnimNum", -2); }
            else if (Input.GetAxis("Horizontal") == 1) { _anim.SetInteger("AnimNum", 2); }
        }
        else
        {
            _anim.SetInteger("AnimNum", 0);
        }
=======
            if (Input.GetAxis("Horizontal") == -1) { _anim.SetInteger("MoveZ", -1); }
            else if (Input.GetAxis("Horizontal") == 1) { _anim.SetInteger("MoveZ", 1); }
        }
        else
        {
            _anim.SetInteger("MoveX", 0);
            _anim.SetInteger("Movez", 0);
        }*/
>>>>>>> Main-Character-New-Animations

        Vector3 forward = this.transform.forward * vInput * Time.fixedDeltaTime;

        Vector3 sideways = this.transform.right * hInput * Time.fixedDeltaTime;

        _rb.MovePosition(this.transform.position + forward + sideways);
    }
}