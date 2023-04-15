using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public int damage;
    public float attackRadius = 1f;
    public float attackDelay = 1f;

    public bool isAttacking = false;
    private CircleCollider2D attackCollider;
    private float timeSinceAttack = 0f;

    public PlayerHealth playerHealth;
    public EnemyMovement enemyMovement;

    void Start()
    {
        attackCollider = GetComponent<CircleCollider2D>();
        attackCollider.enabled = false;
    }

    void Update()
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, attackRadius);
        
        foreach (Collider2D hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Player"))
            {
                Attack();
                break;
            }
        }

        if (isAttacking)
        {
            timeSinceAttack += Time.deltaTime;
            if (timeSinceAttack >= attackDelay)
            {
                isAttacking = false;
                //enemyMovement.isChasing = false;
                // animator.SetBool("isAttacking", false);
                attackCollider.enabled = false;
                timeSinceAttack = 0f;
            }
        } 
    }

    void Attack()
    {
        if (!isAttacking)
        {
            isAttacking = true;
            // animator.SetBool("isAttacking", true);
            attackCollider.enabled = true;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player hit");
            playerHealth.TakeDamage(damage);
        }
    }
}
