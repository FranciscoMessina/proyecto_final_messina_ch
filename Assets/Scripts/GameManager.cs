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
        GameObject newDrop = Instantiate(onDeathDrops.getDrop(), location + (transform.up * 4), new Quaternion()) as GameObject;
        Debug.Log(newDrop.name);
        //Instantiate(newDrop, location);
    }
}
