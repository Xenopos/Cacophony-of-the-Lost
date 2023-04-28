using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enemy;

public class EnemyPatrolState : EnemyBaseState {
    private float patrolSpeed;
    private float patrolRadius;
    private float initialPosition;
    private bool patrolDirection;   

    private GameObject player;

    public override void EnterState(EnemyStateManager enemy) {
        patrolDirection = enemy.patrolDirection;     
        initialPosition = enemy.transform.position.x;
        patrolSpeed = enemy.patrolSpeed;
        patrolRadius = enemy.patrolRadius;

        enemy.currentSpeed = patrolSpeed;
    }

    public override void ExitState(EnemyStateManager enemy) {

    }

    public override void UpdateState(EnemyStateManager enemy) {
        float distanceFromStart = enemy.transform.position.x - initialPosition;
        if (patrolDirection) {
            enemy.myRigidBody.velocity = new Vector2(patrolSpeed, 0f);
        }  else {
            enemy.myRigidBody.velocity = new Vector2(-patrolSpeed, 0f);
        }

        bool isPlayerCrouching = enemy.IsPlayerCrouching();
        bool isPlayerFacingEnemy = enemy.IsPlayerFacingEnemy();

        // Check if player is in chase radius and if enemy is facing player
        float distanceFromPlayer = Vector2.Distance(enemy.transform.position, enemy.player.transform.position);
        Debug.Log("Distance from player: " + distanceFromPlayer);
        Debug.Log("Is player facing enemy: " + isPlayerFacingEnemy);
        if (Mathf.Abs(distanceFromPlayer) <= enemy.chaseRadius && isPlayerFacingEnemy && !isPlayerCrouching) {
            // Switch to chase state
            enemy.SwitchState(enemy.ChaseState);
        }
        // Check if enemy is out of patrol radius
        if (Mathf.Abs(distanceFromStart) >= patrolRadius) {
            // Switch to idle state
            enemy.SwitchState(enemy.IdleState);
        }
    }

    public override void OnTriggerEnter2D(EnemyStateManager enemy, Collider2D collider) {
        
    }
}