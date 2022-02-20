using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    public bool showWinScreen = false;
    public bool showLooseScreen = false;
    private int pts;
    [SerializeField] private int pointsToNextLevel;
    private string labelText;

    [SerializeField] private int NextLevel;
    private bool scoreReached = false;

    public int Points
    {
        get { return pts; }
        set { pts += value;

            if (pts == pointsToNextLevel) labelText = "Score to next level reached!";
            scoreReached = true;
        }
    }

    private void OnGUI()
    {
        
    }

    public void Die() { }
}
