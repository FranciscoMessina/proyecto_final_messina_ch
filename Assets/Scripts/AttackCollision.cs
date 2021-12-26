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

    private void ObjectDamage(Collider other)
    {
        Debug.Log("Object Collider");
        Destroy(other.gameObject);
        Destroy(this.gameObject);
    }

    private void TankerDamage(Collider other)
    {
        Debug.Log("Tanker Collider");
        Destroy(other.gameObject);
        Destroy(this.gameObject);

    }

    private void CasterDamage(Collider other)
    {
        Debug.Log("Caster Collider");
        Destroy(other.gameObject);
        Destroy(this.gameObject);

    }

    private void HitterDamage(Collider other)
    {
        Debug.Log("Hitter Collider");
        Destroy(other.gameObject);
        Destroy(this.gameObject);

    }
}
