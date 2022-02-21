using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour
{
    [SerializeField] Text scoreText;
    [SerializeField] GameBehaviour gameBehaviour;


    private void Start()
    {
        if (gameBehaviour == null)
        {
            gameBehaviour = FindObjectOfType<GameBehaviour>();
        }
    }
    // Update is called once per frame
    void Update()
    {
        scoreText.text = $"SCORE: {gameBehaviour.Points}";
    }
}

