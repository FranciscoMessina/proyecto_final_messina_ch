using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    public bool showWinScreen = false;
    public bool showLooseScreen = false;
    private int pts;
    [SerializeField] private int pointsToNextLevel;
    private string labelText;
    [SerializeField] LevelManager lvlmgr;

    [SerializeField] private int nextLevel;
    private bool scoreReached = false;

    [SerializeField] Animator gameOver;
    [SerializeField] Animator fade;

    public int Points
    {
        get { return pts; }
        set { pts += value;

            if (pts == pointsToNextLevel) labelText = "Score to next level reached!";
            scoreReached = true;
        }
    }

    

    public void GoToNextLevel() 
    {
        Invoke("lvlmgr.LoadScene()", 2);
    }

    public void GoToMain()
    {
        lvlmgr.LoadScene(0);
    }

    public void Die() 
    {
        gameOver.SetTrigger("Ded");
        Invoke("FadeOut", 3f);
        Invoke("GoToMain", 5f);
    }

    private void FadeOut()
    {
        fade.SetTrigger("FadeOut");
    }
}
