using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMeleeDetector : MonoBehaviour
{
    private GameManager _gm;
    private int dmg;
    private bool canHit = true;
    private float hitDelay = 1.5f;
    private float hitTimer;

    private void Start()
    {
        _gm = GameManager.instance;
        dmg = GetComponentInParent<BaseEnemy>().meleeDamage;
    }

    private void Update()
    {
        if (canHit == false) hitTimer -= Time.deltaTime;
        if (hitTimer <= 0) canHit = true;
                
    }

    private void OnTriggerEnter(Collider other)
    {
        if (canHit == true) 
        {
        _gm.GetPlayerReference().TakeDamage(dmg);
            canHit = false;
            hitTimer = hitDelay;
        }
    }
}
