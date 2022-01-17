using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy : MonoBehaviour
{
    [SerializeField] private float maxHealth = 100;
    [SerializeField] private float speed = 10;
    [SerializeField] private float rotationSpeed = 10;
    [SerializeField] private float maxFollowDistance = 50;
    [SerializeField] private float minFollowDistance = 4;
    [SerializeField] private float attackDelay = 3;
    [SerializeField] private Transform raycastOrigin;
    [SerializeField] private OnDeathDrops onDeathDrops;
    [SerializeField] private Transform[] patrolPoints;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private LayerMask enemiesLayer;
    private Vector3 startingLocation;
    private int randomSpot;
    private float waitTime;
    public float startWaitTime;
    private Transform target;
    private GameManager _gm;
    private Animator _anim;
    private float distance;
    // Start is called before the first frame update
    void Start()
    {
        _anim = GetComponent<Animator>();
        _gm = GameManager.instance;
        Invoke("GetTarget", 0.1f);
        startingLocation = transform.position;
        randomSpot = Random.Range(0, patrolPoints.Length);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MoveToPlayer()
    {
        var direction = (target.position -  transform.position).normalized;
        Quaternion rotation = Quaternion.LookRotation(target.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);

        // TODO: Change animation to be reusable between enemies
        if (distance < maxFollowDistance && distance >= minFollowDistance)
        {
            _anim.SetInteger("hAnim", 1);
            transform.position += direction * speed * Time.deltaTime;
        }
        else _anim.SetInteger("hAnim", 0);

    }

    private void Patrol()
    {
        MoveToDestination(patrolPoints[randomSpot].position);
        // Debug.Log("Patrol Called");

        if(Vector3.Distance(transform.position, patrolPoints[randomSpot].position) < 1f) {
            if(waitTime <= 0) {
                randomSpot = Random.Range(0, patrolPoints.Length);
            } else {
                waitTime -= Time.deltaTime;
            }
        }
    }

    private bool DetectPlayer()
    {
        //RaycastHit hit;
        var direction = (target.position - transform.position).normalized;

        var collidedWithPlayer = Physics.Raycast(raycastOrigin.position, direction, /*out hit,*/ maxFollowDistance, playerLayer);
        Debug.Log("Player Detected:" + collidedWithPlayer);

        return collidedWithPlayer;
    }

    
    public void MoveToDestination(Vector3 destination) {

        distance = Vector3.Distance(transform.position, destination);

        var direction = (destination - transform.position).normalized;
        direction.y = 0;
        Quaternion rotation = Quaternion.LookRotation(destination - transform.position);
        rotation.x = 0;
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);

        // TODO: Change animation to be reusable between enemies
        if(distance > 1f) {
            _anim.SetInteger("tAnim", 1);
            transform.position += direction * speed * Time.deltaTime;
        } else {
            _anim.SetInteger("tAnim", 0);
        }
    }

    private void GetTarget() {
        target = _gm.GetPlayerReference().transform;
    }
}
