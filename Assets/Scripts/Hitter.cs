using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitter : MonoBehaviour
{
    [SerializeField] private Transform target; 
    [SerializeField] private float speed = 5;
    [SerializeField] private float rotationSpeed = 5;

    [SerializeField] private float maxFollowDistance = 50;
    [SerializeField] private float minFollowDistance = 4;


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    public void Move()
    {
        var direction = (target.position -  transform.position).normalized;
        Quaternion rotation = Quaternion.LookRotation(target.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);

        var distance = Vector3.Distance(transform.position, target.position);

        if (distance < maxFollowDistance && distance >= minFollowDistance)
        {
            transform.position += direction * speed * Time.deltaTime;
        }

    }
}

