using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitter : MonoBehaviour
{
    private Transform _playerPosition; 
    [SerializeField] private float speed = 5;

    // Start is called before the first frame update
    void Start()
    {
        _playerPosition = GameManager.instance.playerPosition;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Move()
    {
        var direction = (_playerPosition.transform.position -  transform.position).normalized;
        Quaternion rotation = Quaternion.LookRotation(_playerPosition.transform.position - transform.position);

        var distance = Vector3.Distance(transform.position, _playerPosition.transform.position);

        if (distance < 25)
        {
            transform.position = transform.position * speed * Time.deltaTime;
        }
        Debug.Log("Hitter");
    }
}
