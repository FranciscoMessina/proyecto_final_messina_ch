using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaSpellBehaviour : MonoBehaviour
{
    [SerializeField] private float timeOnScreen;
    private SphereCollider _sc;
    public GameObject _testsphere;
    private float _rt = 0;

    // Start is called before the first frame update
    void Start()
    {
        _sc = GetComponent<SphereCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        _rt += Time.deltaTime;
        _sc.radius = Mathf.Lerp(2, 31, _rt);
        Debug.Log("the collider radius is: " + _sc.radius);
        timeOnScreen -= Time.deltaTime;
        if (timeOnScreen <= 0)
            Destroy(this.gameObject);
    }
}

