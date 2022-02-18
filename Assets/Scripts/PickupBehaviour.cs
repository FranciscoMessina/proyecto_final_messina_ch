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
    private GameManager _gm;
    
    // Start is called before the first frame update
    void Start()
    {
        _gm = GameManager.instance;
    }

    // Update is called once per frame
    void Update()
    {
        cubes.transform.Rotate(transform.up * 100 * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (type == pickupType.heal) _gm.GetPlayerReference().Heal(20);
        else if (type == pickupType.powerup) _gm.GetPlayerReference().Heal(20);
        else if (type == pickupType.extralife) _gm.GetPlayerReference().OneUp();
        Destroy(this.gameObject);
    }
}
