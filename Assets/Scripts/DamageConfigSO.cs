using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

[CreateAssetMenu(fileName = "Damage Config", menuName = "Guns/Damage Config", order = 1)]
public class DamageConfigSO : ScriptableObject
{
    //public int damage;
    public MinMaxCurve damageCurve;
    public int GetDamage(float distance = 0)
    {
        return Mathf.CeilToInt(damageCurve.Evaluate(distance, Random.value));
    }
}
