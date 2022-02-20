using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceSpellBehaviour : MonoBehaviour
{
    [SerializeField] private float timeOnScreen;
    [SerializeField] private float damage = 35;


    // Update is called once per frame
    void Update()
    {
        timeOnScreen -= Time.deltaTime;
        if (timeOnScreen <= 0)
            Destroy(this.gameObject);
    }

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
    }

    private void TankerDamage(Collider other)
    {
        Tanker enemy = other.gameObject.GetComponent<Tanker>();
        enemy.TakeDamage(damage);
    }

    private void CasterDamage(Collider other)
    {
        Caster enemy = other.gameObject.GetComponent<Caster>();
        enemy.TakeDamage(damage);
    }

    private void HitterDamage(Collider other)
    {
        Hitter enemy = other.gameObject.GetComponent<Hitter>();
        enemy.TakeDamage(damage);

    }

    
}
