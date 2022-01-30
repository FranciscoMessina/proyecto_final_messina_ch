using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventSceneHandler : MonoBehaviour
{
    [SerializeField] private EventSceneManager _esm;

    [SerializeField] private ParticleSystem wind;
    [SerializeField] private ParticleSystem rain;
    [SerializeField] private Light light;
    private Color color;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Sunset() 
    {
        Debug.Log("Sunset Handled");
        light.transform.rotation = Quaternion.Euler(new Vector3(-45, 0, 0));
    }

    public void Sunrise() 
    {
        Debug.Log("Sunrise Handled");
        light.transform.rotation = Quaternion.Euler(new Vector3(45, 0, 0));

    }

    public void ClimateChange(int climate) {
        if (climate == 1) { Sunny(); }
        if (climate == 2) { Windy(); }
        if (climate == 3) { Rainy(); }
        Debug.Log("ClimateChange Handled");
    }


    private void Sunny() 
    {
        if(wind.isPlaying) wind.Stop();
        if(rain.isPlaying) rain.Stop();
        light.color = Color.white;
    }


    private void Windy() 
    {
        if(!wind.isPlaying) wind.Play();
        if(rain.isPlaying) rain.Stop();
        light.color = Color.white;
    }


    private void Rainy()
    {
        if(wind.isPlaying) wind.Stop();
        if(!rain.isPlaying) rain.Play();
        light.color = Color.grey;
    }
}
