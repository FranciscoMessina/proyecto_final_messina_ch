using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCollision : MonoBehaviour
{

    private void OnTriggerEnter(Collider other) {
        switch(other.gameObject.tag) {
            case "Hitter":
                HitterDamage(other);
                break;
            case "Caster":
                CasterDamage(other);
                break;
            case "Tanker":
                TankerDamage(other);
                break;
            case "Object":
                ObjectDamage(other);
                break;
            default:
                Debug.Log("No matching tag");
                break;
        }
    }

    private void OnTriggerStay(Collider other) {
        switch(other.gameObject.tag) {
            case "Hitter":
                HitterDamage(other);
                break;
            case "Caster":
                CasterDamage(other);
                break;
            case "Tanker":
                TankerDamage(other);
                break;
            case "Object":
                ObjectDamage(other);
                break;
            default:
                Debug.Log("No matching tag");
                break;
        }
    }

    private void ObjectDamage(Collider other)
    {
        Debug.Log("Object Collider");
    }

    private void TankerDamage(Collider other)
    {
        Tanker enemy = other.gameObject.GetComponent<Tanker>();
        Debug.Log(enemy);
        enemy.TakeDamage(10);
        Debug.Log("Tanker Collider");
        
    }

    private void CasterDamage(Collider other)
    {
        Caster enemy = other.gameObject.GetComponent<Caster>();
        Debug.Log(enemy);

        enemy.TakeDamage(10);
        Debug.Log("Caster Collider");
    }

    private void HitterDamage(Collider other)
    {
        Hitter enemy = other.gameObject.GetComponent<Hitter>();
        Debug.Log(enemy);

        enemy.TakeDamage(10);
        Debug.Log("Hitter Collider");
    }
}
