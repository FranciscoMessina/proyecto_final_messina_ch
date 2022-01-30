using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class EventSceneManager : MonoBehaviour
{
    public event Action onSunset;
    public event Action onSunrise;
    public event Action<int> onClimateChange;

    public UnityEvent<Color> onColorChange;
    public UnityEvent onSwitchOnOff;
    public UnityEvent onSizeChange;

    private Color colorA;
    private Color colorB;
    [SerializeField] private EventSceneHandler _esh;

    //[SerializeField] private GameObject[] torches;

    private float daytime = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        onSunrise += _esh.Sunrise;
        onSunset += _esh.Sunset;
        onClimateChange += _esh.ClimateChange;


        colorA = Color.blue;
        colorB = Color.red;
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            int clim = UnityEngine.Random.Range(1, 4);
            onClimateChange?.Invoke(clim);
            Debug.Log("OnClimateChange Called, clim = " + clim);
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            onColorChange?.Invoke(colorA);
            Debug.Log("OnColorChange Called");
        }

        if (Input.GetKeyDown(KeyCode.N))
        {
            onColorChange?.Invoke(colorB);
            Debug.Log("OnColorChange Called");
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            onSizeChange?.Invoke();
            Debug.Log("OnSizChange Called");
        }

        if (Input.GetKeyDown(KeyCode.V))
        {
            onSwitchOnOff?.Invoke();
            Debug.Log("OnSwitchOnOff Called");
        }


        if (daytime >= 24f) daytime = 0;
    }




    void FixedUpdate()
    {
        daytime += Time.deltaTime;

        if (daytime >= .0f && daytime <= .1f )
        {
            onSunrise?.Invoke();
            Debug.Log("OnSunrise Called");
        }

        if (daytime >= 14f && daytime <= 14.1f )
        {
            onSunset?.Invoke();
            Debug.Log("OnSunset Called");

        }
    }
}