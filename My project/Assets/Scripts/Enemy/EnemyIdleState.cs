using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enemy;

public class EnemyIdleState : EnemyBaseState {
    private float idleDuration = 2f;
    private float idleTimer = 0f;

    public override void EnterState(EnemyStateManager enemy) {
        Debug.Log("Idle");
        
        // Timer start for idle duration
        idleTimer = 0f;

        // Stop movement
        enemy.myRigidBody.velocity = Vector2.zero;

        // Set idle animation
        enemy.myAnimator.SetBool("isIdle", true);
    }

    public override void ExitState(EnemyStateManager enemy) {

    }

    // public override void OnCollisionEnter2D(EnemyStateManager enemy, Collision2D collision) {

    // }

    public override void OnTriggerEnter2D(EnemyStateManager enemy, Collider2D collider) {
        
    }

    public override void UpdateState(EnemyStateManager enemy) {
        // Increase timer
        idleTimer += Time.deltaTime;

        // Check if player is in chase radius and if enemy is facing player
        float distanceFromPlayer = Vector2.Distance(enemy.transform.position, enemy.player.transform.position);
        if (distanceFromPlayer <= enemy.chaseRadius && enemy.IsPlayerFacingEnemy()) {
            // Reset idle animation

            // Switch to chase state
            enemy.SwitchState(enemy.ChaseState);
        }
        
        // Check if idle duration has been reached
        if (idleTimer >= idleDuration) {
            // Reverse patrol direction
            enemy.patrolDirection = !enemy.patrolDirection;

            // Reset idle animation
            enemy.myAnimator.SetBool("isIdle", false);
            
            // Switch to patrol state
            enemy.SwitchState(enemy.PatrolState);
        }
    }
}