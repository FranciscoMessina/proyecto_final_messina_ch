using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 8;
    [SerializeField] private float sideSpeed = 6;
    private Rigidbody _rb;
    //private CinemachineTouchInputMapper _cM;

    private float vInput;
    private float hInput;

    [SerializeField] private Camera GameCamera;
    private Animator _anim;

    private bool canShoot = true;
    [SerializeField] private float shootDelay = 1;
    private bool canArea = true;
    [SerializeField] private float areaDelay = 4;
    private float shootTimer;
    private float areaTimer;
    private bool isCasting = false;

    private GameManager _gm;

    [SerializeField] private GameObject iceSpell;
    [SerializeField] private float spellSpeed;
    [SerializeField] private Transform spellSpawnPoint;

    [SerializeField] private GameObject areaSpell;

    [SerializeField] private int lives;
    [SerializeField] private int maxHealth = 100;
    private int currentHealth;
    //private int _health;

    //public event Action onDeath;

    /*public int health {
        get { return _health; }
        set { _health = value; }
    }*/

    private void Awake()
    {
        
    }


    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        Debug.Log(maxHealth);
        _rb = GetComponent<Rigidbody>();
        _anim = GetComponent<Animator>();
        _gm = GameManager.instance;
        _gm.SetPlayerReference(this);
    }

    // Update is called once per frame
    private void Update()
    {
        vInput = Input.GetAxis("Vertical") * moveSpeed;
        hInput = Input.GetAxis("Horizontal") * sideSpeed;

        //ChangeHealth();

        if (canShoot == false && shootTimer >= 0)
        {
            shootTimer -= Time.deltaTime;
        }
        else if (canShoot == false && shootTimer <= 0)
        {
            canShoot = true;
        }

        if (canArea == false && areaTimer >= 0)
        {
            areaTimer -= Time.deltaTime;
        }
        else if (canArea == false && areaTimer <= 0)
        {
            canArea = true;
        }
    }

    void FixedUpdate()
    {

        if (!isCasting) Walk();
        if (!isCasting) Rotate();

        if (Input.GetButton("Fire1") && canShoot && !isCasting)
        {
            Shoot();
            shootTimer = shootDelay;
            isCasting = true;
        }

        else if (Input.GetButton("Fire2") && canArea && !isCasting)
        {
            Area();
            areaTimer = areaDelay;
            isCasting = true;
        }
    }


    public int GetHealth() {
        return currentHealth;
    }

    public int GetLives()
    {
        return lives;
    }

    /*void ChangeHealth() {
        if (Input.GetKeyDown(KeyCode.J)) {
            Debug.Log('J');
            currentHealth -= 10;
        } 
        
        if(Input.GetKeyDown(KeyCode.K)) {
            Debug.Log('k');
            currentHealth += 10;
        }

        if(currentHealth <= 0) {
            currentHealth = 0;
        }
        if (currentHealth > maxHealth) {
          currentHealth = maxHealth;
        }
        // Debug.Log(currentHealth);

    }*/

    void Shoot()
    {
        //Invoke("CastSpell", .6f);
        _anim.SetTrigger("SpellTrig");
        canShoot = false;
        Debug.Log("Shoot called");
    }

    public void CastSpell()
    {
        GameObject newspell = Instantiate(iceSpell, spellSpawnPoint.position, this.transform.rotation) as GameObject;
        Rigidbody spellRB = newspell.GetComponent<Rigidbody>();

        spellRB.velocity = this.transform.forward * spellSpeed;
    }

    void Area()
    {
        //Invoke("CastArea", 1f);
        _anim.SetTrigger("AreaTrig");
        canArea = false;
        Debug.Log("Area called");
    }

    public void CastArea()
    {
        GameObject newarea = Instantiate(areaSpell, this.transform.position, this.transform.rotation) as GameObject;
    }

    void Walk()
    {
        _anim.SetFloat("MoveZ", Input.GetAxis("Horizontal"));
        _anim.SetFloat("MoveX", Input.GetAxis("Vertical"));

        //if (Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") == 0) { _anim.SetInteger("AnimNum", 0); }
        

        Vector3 forward = this.transform.forward * vInput * Time.fixedDeltaTime;
        Vector3 sideways = this.transform.right * hInput * Time.fixedDeltaTime;
        _rb.MovePosition(this.transform.position + forward + sideways);
    }

    void Rotate()
    {
        var CharacterRotation = GameCamera.transform.rotation;
        CharacterRotation.x = 0;
        CharacterRotation.z = 0;

        this.transform.rotation = CharacterRotation;
        
    }

    public void TakeDamage(int dmg)
    {
        currentHealth -= dmg;
        _anim.SetTrigger("stagger");
    }

    public void Heal(int healvalue)
    {
        currentHealth += healvalue;
    }

    public void OneUp()
    {
        lives += 1;
    }

    public void FinishedCasting()
    {
        isCasting = false;
    }
    
}