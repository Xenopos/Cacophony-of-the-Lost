using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enemy;

public class EnemyAttackState : EnemyBaseState {
    private float damage = 1f;
    private bool canAttack = true;
    private float attackCooldown = 2f; // Cooldown between attacks in seconds
    private float attackTimer = 0f; // Timer to keep track of cooldown
    private CircleCollider2D attackCollider;
    private GameObject player;


    public override void EnterState(EnemyStateManager enemy) {
        Debug.Log("Attack");
        
        // Set variables
        attackCollider = enemy.circleCollider;
        attackCollider.enabled = true;
        player = enemy.player;

        // Stop movement
        enemy.rigidBody.velocity = Vector2.zero;
        enemy.currentSpeed = 0f;

        // Set timer
        attackTimer = 0f;

        // Set attack animation
        enemy.animator.SetBool("isAttacking", true);
    }

    public override void ExitState(EnemyStateManager enemy) {

    }
    
    public override void UpdateState(EnemyStateManager enemy) {
        Debug.Log("Enemy attack colider enabled:");
        Debug.Log(attackCollider.enabled);
        // Increase timer
        attackTimer += Time.deltaTime;

        // Check if attack cooldown has been reached
        if (attackTimer >= attackCooldown) {
            // Reset attack animation

            // Reset timer
            attackTimer = 0f;

            // Set canAttack to true
            canAttack = true;
            attackCollider.enabled = true;

            // Set attack animation
            enemy.animator.SetBool("isAttacking", true);
        }

        // Check if player is still in attack radius and if enemy is facing player
        float distanceFromPlayer = Vector2.Distance(enemy.transform.position, enemy.player.transform.position);
        if (distanceFromPlayer > enemy.attackRadius) {
            // Reset attack animation
            enemy.animator.SetBool("isAttacking", false);

            // Switch to chase state
            enemy.SwitchState(enemy.ChaseState);
        }
    }   

    public override void OnTriggerEnter2D(EnemyStateManager enemy, Collider2D collision) {        
        if (collision.GetComponent<BoxCollider2D>() != null) {
            Debug.Log("Enemy is attacking player!");
            if (collision.CompareTag("Player")) {
                if (canAttack) {
                    Debug.Log("Enemy is attacking player for real!");

                    // Damage player
                    enemy.playerStateManager.TakeDamage(damage);

                    // Set canAttack to false and disable attack colider to prevent multiple attacks
                    canAttack = false;
                    attackCollider.enabled = false;
                }    
            }
        }
    }
}