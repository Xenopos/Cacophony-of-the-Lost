using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enemy;

public class EnemyAttackState : EnemyBaseState {
    private float damage;
    private float attackDelay;

    public override void EnterState(EnemyStateManager enemy) {
        Debug.Log("Attack");
        
        // Set attack delay and damage
        attackDelay = enemy.attackDelay;
        damage = enemy.damage;

        // Set attack animation
    }

    public override void ExitState(EnemyStateManager enemy) {

    }
    
    public override void UpdateState(EnemyStateManager enemy) {
        // Check if player is still in attack radius and if enemy is facing player
        float distanceFromPlayer = Vector2.Distance(enemy.transform.position, enemy.player.transform.position);
        if (distanceFromPlayer > enemy.attackRadius || !enemy.IsPlayerFacingEnemy()) {
            // Reset attack animation

            // Switch to chase state
            enemy.SwitchState(enemy.ChaseState);
        }
    }   

    void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            Debug.Log("Player hit");
            
            // Minus player health
        }
    }

    void OnTriggerExit2D(Collider2D other) {
        // Check if the collision was with the player
        if (other.CompareTag("Player")) {
            // Stop attack
            Debug.Log("Enemy stopped attacking player!");

            // Reset attack animation
        }
    }
}