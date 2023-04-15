using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public int damage;
    public EnemyHealth enemyHealth;

    private void OnCollisionEnter2D(Collision2D collision) 
    {
        if (collision.gameObject.tag == "Enemy") 
        {
            enemyHealth.TakeDamage(damage); 
        }
    }
}
