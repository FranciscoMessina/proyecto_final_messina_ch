using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour
{
    [SerializeField] Text scoreText;
    [SerializeField] GameBehaviour gameBehaviour;

    // Update is called once per frame
    void Update()
    {
        scoreText.text = $"SCORE: {gameBehaviour.Points}";
    }
}

