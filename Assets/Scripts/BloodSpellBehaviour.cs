using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodSpellBehaviour : MonoBehaviour
{
    [SerializeField] private float damage = 35;
    [SerializeField] private float maxLifeTime = 100;
    float timeSinceSpawn = 0;

    private void Update() {
        timeSinceSpawn += Time.deltaTime;

        if( timeSinceSpawn >= maxLifeTime) {
            Destroy(this.gameObject);
        }
    }


    private void OnCollisionEnter(Collision other) {
           PlayerController player = other.gameObject.GetComponent<PlayerController>();
           if(player != null) {
               Debug.Log(player);
               player.TakeDamage(damage);
           }
           Destroy(this.gameObject, 0.2f);

    }
}
