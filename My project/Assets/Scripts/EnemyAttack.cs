using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    private Animator animator;

    public int damage;
    public float attackRadius = 1.5f;
    public float attackDelay = 1.30f;

    public bool isAttacking = false;
    public bool waitAttack = false;
    private CircleCollider2D attackCollider;
    private float distanceFromPlayer;
    
    private float timeSinceAttack = 0f;

    public PlayerHealth playerHealth;
    public EnemyMovement enemyMovement;

    void Start()
    {
        animator = GetComponent<Animator>();
        attackCollider = GetComponent<CircleCollider2D>();
        attackCollider.enabled = false;
    }

    void Update()
    {
        if (waitAttack) return;

        CheckPlayerDistance();
    }

    void CheckPlayerDistance() {
        distanceFromPlayer = Vector2.Distance(transform.position, enemyMovement.player.transform.position);
        if (distanceFromPlayer <= attackRadius && enemyMovement.isChasing) {
            Debug.Log("Attack");
            OnAttack();
        } 
    }

    void OnAttack() {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, attackRadius);
        
        foreach (Collider2D hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Player"))
            {
                StartCoroutine(WaitAndAttack());

            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            //Debug.Log("Player hit");
            playerHealth.TakeDamage(damage);
        }
    }

    IEnumerator WaitAndAttack()
    {
        waitAttack = true;
        isAttacking = true;

        attackCollider.enabled = true;
        
        animator.SetBool("isAttacking", true);
        animator.SetBool("isChasing", false);
        enemyMovement.isChasing = false;

        yield return new WaitForSeconds(attackDelay);

        Debug.Log("Attack");

        isAttacking = false;
        attackCollider.enabled = false;
        animator.SetBool("isAttacking", false);
        waitAttack = false;
    }
}
