using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CasterData", menuName = "ProyectoCoder/CasterData", order = 0)]
public class CasterData : ScriptableObject
{

    public int casterHealth;
    public int casterDamage;
    public int walkSpeed;
    public int spellSpeed;
}
