using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RouletteWheel : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Run <GameObject>(Dictionary<GameObject, int> items)
    {
        float max = 0;

        foreach (var Item in items)
        {
            max += Item.Value;
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
