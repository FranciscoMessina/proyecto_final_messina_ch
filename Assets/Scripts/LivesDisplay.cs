using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LivesDisplay : MonoBehaviour
{
    [SerializeField] Text livesText;
    [SerializeField] PlayerController player;

    // Update is called once per frame
    void Update()
    {
        livesText.text = $"LIVES: {player.GetLives()}";
    }
}
