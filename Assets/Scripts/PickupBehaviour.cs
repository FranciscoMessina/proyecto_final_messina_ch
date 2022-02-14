using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupBehaviour : MonoBehaviour
{
    private enum pickupType {
        powerup,
        extralife,
        heal
    }

    [SerializeField] pickupType type;
    [SerializeField] GameObject cubes;

    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        cubes.transform.Rotate(transform.up * 100 * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(this.gameObject);
    }
}
