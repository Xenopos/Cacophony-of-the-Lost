using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enemy;

public class EnemyAttackState : EnemyBaseState {
    private float damage;
    private bool canAttack;
    private float attackCooldown; // Cooldown between attacks in seconds
    private float attackTimer = 0f; // Timer to keep track of cooldown
    private CircleCollider2D attackCollider;


    public override void EnterState(EnemyStateManager enemy) {
        Debug.Log("Attack");
        
        // Set attack colider, attack cooldown, damage, and canAttack
        attackCollider = enemy.myCollider;
        attackCooldown = enemy.attackCooldown;
        damage = enemy.damage;
        canAttack = true;

        // Stop movement
        enemy.myRigidBody.velocity = Vector2.zero;

        // Set timer
        attackTimer = 0f;

        // Set attack animation
    }

    public override void ExitState(EnemyStateManager enemy) {

    }
    
    public override void UpdateState(EnemyStateManager enemy) {
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
        }

        // Check if player is still in attack radius and if enemy is facing player
        float distanceFromPlayer = Vector2.Distance(enemy.transform.position, enemy.player.transform.position);
        if (distanceFromPlayer > enemy.attackRadius || !enemy.IsPlayerFacingEnemy()) {
            // Reset attack animation

            // Switch to chase state
            enemy.SwitchState(enemy.ChaseState);
        }
    }   

    public override void OnTriggerEnter2D(EnemyStateManager enemy, Collider2D collision) {        
        Debug.Log(collision.name);
        // Check if the collision is with the player
        if (collision.CompareTag("Player")) {
            // Check if the enemy can attack based on cooldown
            if (canAttack) {
                // Initiate attack
                Debug.Log("Enemy is attacking player!");

                // Damage player

                // Set canAttack to false and disable attack colider to prevent multiple attacks
                canAttack = false;
                attackCollider.enabled = false;

                // Start cooldown timer
            }
        }
    }

    // public void OnTriggerExit2D(Collider2D other) {
    //     Debug.Log("Enemy is no longer attacking player!");

    //     // Check if the collision is with the player
    //     if (other.CompareTag("Player")) {
    //         Debug.Log("Enemy is no longer attacking player!");

    //         // Reset canAttack to true when the enemy and player colliders stop overlapping
    //         canAttack = true;
    //     }
    // }
}