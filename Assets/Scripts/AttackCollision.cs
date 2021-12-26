using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCollision : MonoBehaviour
{
    private void OnCollisionEnter(Collision other) {
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

    private void ObjectDamage(Collision other)
    {
        Debug.Log("Object Collision");
        Destroy(other.gameObject);
        Destroy(this.gameObject);
    }

    private void TankerDamage(Collision other)
    {
        Debug.Log("Tanker Collision");
        Destroy(other.gameObject);
        Destroy(this.gameObject);

    }

    private void CasterDamage(Collision other)
    {
        Debug.Log("Caster Collision");
        Destroy(other.gameObject);
        Destroy(this.gameObject);

    }

    private void HitterDamage(Collision other)
    {
        Debug.Log("Hitter Collision");
        Destroy(other.gameObject);
        Destroy(this.gameObject);

    }
}
