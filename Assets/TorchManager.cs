using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchManager : MonoBehaviour
{
    [SerializeField] private ParticleSystem _ps;
    private bool sizeChanged = false;

    public void ChangeColor(Color color)
    {
        _ps.startColor = color;
    
        Debug.Log("Color Changed to" + color);
    }

    public void TurnOnOff()
    {
        if (_ps.isPlaying) _ps.Stop();
        if (!_ps.isPlaying) _ps.Play();

        Debug.Log("Torches turned On/Off");
    }

    public void changeSize()
    {
        if (!sizeChanged)
            _ps.transform.localScale *= 4;

        else _ps.transform.localScale /= 4;

        sizeChanged = !sizeChanged;

        Debug.Log("Torches size changed");
    }
}
