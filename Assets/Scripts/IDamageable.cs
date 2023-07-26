using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    public int CurrentHealth { get; }
    public int MaxHealth { get; }
    public delegate void TakeDamageEvent(int dmg);
    public event TakeDamageEvent OnTakeDamage;
    public delegate void DeathEvent(Vector3 position);
    public event DeathEvent OnDeath;
    public void TakeDamage(int dmg);
}
