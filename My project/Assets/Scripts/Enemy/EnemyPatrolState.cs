using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enemy;

public class EnemyPatrolState : EnemyBaseState {
    private float patrolSpeed = 3f;
    private float patrolRadius = 5f;
    private float initialPosition;

    private GameObject player;

    public override void EnterState(EnemyStateManager enemy) {
        initialPosition = enemy.transform.position.x;
        enemy.currentSpeed = patrolSpeed;
    }

    public override void ExitState(EnemyStateManager enemy) {

    }

    public override void UpdateState(EnemyStateManager enemy) {
        float distanceFromStart = enemy.transform.position.x - initialPosition;
        if (enemy.direction) {
            enemy.rigidBody.velocity = new Vector2(patrolSpeed, 0f);
        }  else {
            enemy.rigidBody.velocity = new Vector2(-patrolSpeed, 0f);
        }

        bool isPlayerCrouching = enemy.IsPlayerCrouching();
        bool isPlayerFacingEnemy = enemy.IsPlayerFacingEnemy();

        // Check if player is in chase radius and if enemy is facing player
        float distanceFromPlayer = Vector2.Distance(enemy.transform.position, enemy.player.transform.position);
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
        Debug.Log("Tiggrer from patrol");
    }
}