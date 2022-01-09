using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnDeathDrops : MonoBehaviour
{

    private Dictionary<GameObject, int> _drops = new Dictionary<GameObject, int>();
    [SerializeField] List<GameObject> items = new List<GameObject>();
    [SerializeField] List<int> chances = new List<int>();

    private RouletteWheel _roulette;
        
    // Start is called before the first frame update
    void Start()
    {
        _roulette = new RouletteWheel();
        for (int i = 0; i < items.Count; i++)
        {
            _drops.Add(items[i], chances[i]);
        }
    }

    public GameObject getDrops()
    {
        var drop = _roulette.Run(_drops) ;

        return drop;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
