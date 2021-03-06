using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameBehaviour : MonoBehaviour
{
    private int pts;
    [SerializeField] private int pointsToNextLevel;
    private string labelText;
    [SerializeField] LevelManager lvlmgr;
    GameManager _gm;

    [SerializeField] private int nextLevel;

    [SerializeField] Animator pointsReached;
    [SerializeField] Animator gameOver;
    [SerializeField] Animator fade;

    [SerializeField] Canvas _pause;
    private bool isPaused;

    public int Points
    {
        get { return pts; }
        set { pts += value;

            //if (pts == pointsToNextLevel) gameOverText.text = "Score to next level reached!";
            pointsReached.SetTrigger("Ded");
            Invoke("FadeOut", 5f);
            Invoke("GoToNextLevel", 7f);
        }
    }


    private void Start()
    {
        _gm = GameManager.instance;
        _gm.SetGameBehaviorReference(this);
        isPaused = false;
    }



    public void GoToNextLevel() 
    {
        lvlmgr.LoadScene(nextLevel);
    }

    public void GoToMain()
    {
        lvlmgr.LoadScene(0);
    }

    public void Die() 
    {
        //gameOverText.text = "U DED LOL";
        gameOver.SetTrigger("Ded");
        Invoke("FadeOut", 3f);
        Invoke("GoToMain", 5f);
    }

    private void FadeOut()
    {
        fade.SetTrigger("FadeOut");
    }

    public void PauseGame()
    {
        isPaused = !isPaused;
        if (!isPaused)
        {
            _pause.gameObject.SetActive(true);
            Time.timeScale = 0f;
        }
        else if (isPaused)
        {
            Time.timeScale = 1f;
            _pause.gameObject.SetActive(false);
        }
    }
}
