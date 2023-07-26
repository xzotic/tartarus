using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public EnemyHealth enemyHealth;
    //public EnemyMovement enemyMovement;
    public EnemyPain enemyPain;

    public void Start()
    {
        //enemyHealth.OnTakeDamage += enemyPain.HandlePain;
        enemyHealth.OnDeath += Die;
    }

    private void Die(Vector3 position)
    {
        //enemyMovement.StopMoving();
        enemyPain.HandleDeath();
    }
}
