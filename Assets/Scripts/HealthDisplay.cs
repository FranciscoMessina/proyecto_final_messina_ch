using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthDisplay : MonoBehaviour
{   
    [SerializeField] Text healthText;
    [SerializeField] PlayerController player;

    // Update is called once per frame
    void Update()
    {
        healthText.text = $"HEALTH: {player.GetHealth()}";
    }
}
