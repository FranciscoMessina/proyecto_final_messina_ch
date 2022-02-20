using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour
{
    [SerializeField] Text livesText;
    [SerializeField] GameBehaviour gamebeh;

    // Update is called once per frame
    void Update()
    {
        livesText.text = $"Score: {gamebeh.Points}";
    }
}

