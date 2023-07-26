using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(IDamageable))]
public class SpawnParticleSystemOnDeath : MonoBehaviour
{
    public IDamageable damageable;
    private ParticleSystem deathSystem;

    private void Awake()
    {
        damageable = GetComponent<IDamageable>();
    }

    private void OnEnable()
    {
        damageable.OnDeath += damageable_OnDeath;
    }

    private void damageable_OnDeath(Vector3 position)
    {
        Instantiate(deathSystem, position, Quaternion.identity);
    }
}
