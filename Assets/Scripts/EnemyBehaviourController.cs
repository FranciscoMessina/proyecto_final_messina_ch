using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviourController : MonoBehaviour
{

    [SerializeField] private EnemyType enemyType;
    private Hitter _hitter;
    private Tanker _tanker;
    private Caster _caster;


    enum EnemyType
    {
        Hitter,
        Tanker,
        Caster,
    }
    // Start is called before the first frame update
    void Start()
    {
        _caster = GetComponent<Caster>();
        _hitter = GetComponent<Hitter>();
        _tanker = GetComponent<Tanker>();

    }

    // Update is called once per frame
    void Update()
    {
        switch (enemyType)
        {
            case EnemyType.Hitter:
                //_hitter.Move();
                break;
            case EnemyType.Tanker:
                _tanker.Move();
                break;
            case EnemyType.Caster:
                _caster.Move();
                break;


        }
    }
}
