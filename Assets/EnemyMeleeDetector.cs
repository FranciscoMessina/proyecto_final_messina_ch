using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMeleeDetector : MonoBehaviour
{
    private GameManager _gm;
    private int dmg;
    private PlayerController player;

    private void Start()
    {
        _gm = GameManager.instance;
        dmg = GetComponentInParent<BaseEnemy>().meleeDamage;
    }

    private void OnTriggerEnter(Collider other)
    {
        _gm.GetPlayerReference().TakeDamage(dmg);         
    }
}
