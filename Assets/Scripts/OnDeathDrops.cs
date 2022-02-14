using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnDeathDrops : MonoBehaviour
{

    private Dictionary<GameObject, float> _drops = new Dictionary<GameObject, float>();
    [SerializeField] public List<GameObject> items = new List<GameObject>();
    [SerializeField] public List<int> chances = new List<int>();

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

    public GameObject getDrop()
    {
        GameObject drop = Run<GameObject>(_drops);

        return drop;
    }

    // Update is called once per frame
    public T Run<T>(Dictionary<T, float> items)
    {
        float max = 0;

        foreach (var item in items)
        {
            max += item.Value;
        }

        float random = Random.Range(0, max);

        foreach (var item in items)
        {
            random -= item.Value;
            if (random <= 0)
            {
                return item.Key;

            }
        }
        return default;
    }
}
