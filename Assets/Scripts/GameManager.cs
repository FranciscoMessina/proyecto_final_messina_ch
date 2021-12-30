using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{   
    public static GameManager instance;

    private int score;

    public List<Caster> casterList;

    private PlayerController player;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void SetPlayerReference(PlayerController player)
    {
        this.player = player;
    }

    public PlayerController GetPlayerReference() {
        return this.player;
    }

    public void DamagePlayer(int dmg)
    {
        player.health -= dmg;
    }

    public void RaiseScore(int addScore)
    {
        score += addScore;
    }

    public void AddCasterToArray(Caster caster)
    {
        casterList.Add(caster);
    }
}
