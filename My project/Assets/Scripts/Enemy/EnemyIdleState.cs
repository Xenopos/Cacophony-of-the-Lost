using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enemy;

public class EnemyIdleState : EnemyBaseState {
    private float idleDuration = 2f;
    private float idleTimer = 0f;

    public override void EnterState(EnemyStateManager enemy) {
        Debug.Log("Idle");        
        idleTimer = 0f;
        enemy.myRigidBody.velocity = Vector2.zero;
        enemy.currentSpeed = 0f;
    }

    public override void ExitState(EnemyStateManager enemy) {

    }

    public override void OnTriggerEnter2D(EnemyStateManager enemy, Collider2D collider) {
        
    }

    public override void UpdateState(EnemyStateManager enemy) {
        idleTimer += Time.deltaTime;
        float distanceFromPlayer = Vector2.Distance(enemy.transform.position, enemy.player.transform.position);
        if (distanceFromPlayer <= enemy.chaseRadius && enemy.IsPlayerFacingEnemy() && !enemy.IsPlayerCrouching()) {
            enemy.SwitchState(enemy.ChaseState);
        }
        if (idleTimer >= idleDuration) {
            enemy.patrolDirection = !enemy.patrolDirection;
            enemy.SwitchState(enemy.PatrolState);
        }
    }
}