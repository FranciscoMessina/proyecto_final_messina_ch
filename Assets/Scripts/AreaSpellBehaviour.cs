using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaSpellBehaviour : MonoBehaviour
{
    [SerializeField] private float timeOnScreen;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        timeOnScreen -= Time.deltaTime;
        if (timeOnScreen <= 0)
            Destroy(this.gameObject);
    }
}
