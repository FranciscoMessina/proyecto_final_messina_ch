using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControler : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float sideSpeed;
    private Rigidbody _rb;

    private float vInput;
    private float hInput;

    private Animator _anim;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        
            vInput = Input.GetAxis("Vertical") * moveSpeed;
            hInput = Input.GetAxis("Horizontal") * sideSpeed;

            if (Input.GetButton("Vertical"))
            {
            //transform.Translate(0, 0, Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime);
            //_rb.MovePosition(this.transform.position + this.transform.forward * vInput * Time.deltaTime);

            if (Input.GetAxis("Vertical") == -1) { _anim.SetInteger("AnimNum", -1); }
            else if (Input.GetAxis("Vertical") == 1) { _anim.SetInteger("AnimNum", 1); }
        }

            else if (Input.GetButton("Horizontal"))
            {
                //transform.Rotate(0, Input.GetAxis("Horizontal") * rotateSpeed * Time.deltaTime, 0);

                if (Input.GetAxis("Horizontal") == -1) { _anim.SetInteger("AnimNum", -2); }
                else if (Input.GetAxis("Horizontal") == 1) { _anim.SetInteger("AnimNum", 2); }
            }

            else
            {
            _anim.SetInteger("AnimNum", 0);
            }
        
    }


    void FixedUpdate()
    {
        
            //vInput = Input.GetAxis("Vertical") * moveSpeed;
            //hInput = Input.GetAxis("Horizontal") * rotateSpeed;


            Vector3 rotation = Vector3.up * hInput;

            Quaternion angleRot = Quaternion.Euler(rotation * Time.fixedDeltaTime);

            _rb.MovePosition(this.transform.position + this.transform.forward * vInput * Time.fixedDeltaTime);

            _rb.MoveRotation(_rb.rotation * angleRot);
        

    }
}
