using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 8;
    [SerializeField] private float sideSpeed = 6;
    [SerializeField] private AudioSource _as;
    [SerializeField] private AudioClip areaSpellSound;
    [SerializeField] private AudioClip iceSpellSound;
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
    [SerializeField] private float maxHealth = 100;
    private float currentHealth;
    [SerializeField] private Transform spawnLocation;
    [SerializeField] private GameBehaviour _gb;

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

        if (Input.GetKeyDown(KeyCode.Escape)) _gb.PauseGame();
    }

    void FixedUpdate()
    {

        if (!isCasting) Walk();
        if (!isCasting) Rotate();

        if (Input.GetButton("Fire1") && canShoot && !isCasting)
        {
            Shoot();
            _as.clip = iceSpellSound;
            PlaySound();
            shootTimer = shootDelay;
            isCasting = true;
        }

        else if (Input.GetButton("Fire2") && canArea && !isCasting)
        {
            Area();
            _as.clip = areaSpellSound;
            Invoke("PlaySound", .7f);
            areaTimer = areaDelay;
            isCasting = true;
        }
    }


    public float GetHealth() {
        return currentHealth;
    }

    public int GetLives()
    {
        return lives;
    }


    void Shoot()
    {
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
        GameObject newarea = Instantiate(areaSpell, this.transform.position + (transform.up/2), this.transform.rotation) as GameObject;
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

    public void TakeDamage(float damageToTake)
    {

        currentHealth -= damageToTake;
        if (currentHealth < 0) currentHealth = 0;

        if (currentHealth > 0)
        {
            _anim.SetTrigger("HitTrig");
            isCasting = true;
            Invoke("FinishedCasting", 1);
        }

        else if (currentHealth <= 0 && lives > 0)
        {
            Respawn();
            lives -= 1;
        }

        else if (currentHealth <= 0 && lives <= 0)
        {
            Die();
        }
    }

    public void Heal(int healvalue)
    {
        currentHealth += healvalue;
        if (currentHealth > maxHealth) currentHealth = maxHealth;
    }

    public void OneUp()
    {
        lives += 1;
    }

    public void FinishedCasting()
    {
        isCasting = false;
    }

    private void Respawn()
    {
        transform.position = spawnLocation.position;
        currentHealth = maxHealth;
    }

    private void Die()
    {
        _anim.SetTrigger("DeathTrig");
        isCasting = true;
        _gb.Die();
    }

    private void PlaySound()
    {
        _as.Play();
    }


}