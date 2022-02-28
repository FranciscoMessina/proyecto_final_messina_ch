using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class VignetteBehavior : MonoBehaviour
{
    private GameManager _gm;
    private PlayerController player;
    [SerializeField] private PostProcessVolume volume;

    [SerializeField] private Vignette _vignette;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("est� corriendo");
        _gm = GameManager.instance;
        player = _gm.GetPlayerReference();
        bool encontro = volume.profile.TryGetSettings(out _vignette);
        //vignette = volume.GetComponent<Vignette>();

        Debug.Log(encontro + "encontro el coso");

        _vignette.intensity.value = 0.01f;
        _vignette.smoothness.value = 0.3f;
        var colorParameter = new UnityEngine.Rendering.PostProcessing.ColorParameter();
        colorParameter.value = Color.red;
        _vignette.color = colorParameter;
    }

    // Update is called once per frame
    void Update()
    {
        _vignette.intensity.value = 1 - (player.GetHealth() / 100);
    }
}
