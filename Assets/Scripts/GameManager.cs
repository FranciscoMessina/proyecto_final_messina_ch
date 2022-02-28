using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{   
    public static GameManager instance;
    private int score;
    private OnDeathDrops onDeathDrops;

    
    public List<Caster> casterList = new List<Caster>();

    private PlayerController player;
    private GameBehaviour _gb;

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

        player = FindObjectOfType<PlayerController>();
    }
    void Start()
    {
        DontDestroyOnLoad(this);
        onDeathDrops = GetComponent<OnDeathDrops>();
        
    }


    public void SetPlayerReference(PlayerController player)
    {
        this.player = player;
    }

    public void SetGameBehaviorReference(GameBehaviour gb)
    {
        this._gb = gb;
    }
    public PlayerController GetPlayerReference() {
        return this.player;
    }

    public void DamagePlayer(int dmg)
    {
        player.TakeDamage(dmg) ;
    }

    public void HealPlayer(int heal)
    {
        player.Heal(heal);
    }

    public void RaiseScore(int addScore)
    {
        score += addScore;
    }

    public void AddCasterToArray(Caster caster)
    {
        casterList.Add(caster);
    }

    public void GenerateDrop(Vector3 location)
    {
        Vector3 spawnLocation = new Vector3(location.x, 8, location.z);
        GameObject newDrop = Instantiate(onDeathDrops.getDrop(), spawnLocation /*+ (transform.up * 4)*/, new Quaternion()) as GameObject;
        Debug.Log(newDrop.name);
        //Instantiate(newDrop, location);
    }

    public void AddPoints(int pointsToAdd)
    {
        _gb.Points += pointsToAdd;
    }

    
}
