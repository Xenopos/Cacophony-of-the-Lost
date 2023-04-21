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
        // Set patrol direction, speed, radius, and initial position
        patrolDirection = enemy.patrolDirection;     
        initialPosition = enemy.transform.position.x;
        patrolSpeed = enemy.patrolSpeed;
        patrolRadius = enemy.patrolRadius;

        // Set patrol animation
        if (patrolDirection) {
            Debug.Log("Patrol Right");
            // enemy.myAnimator.SetBool("isPatrolRight", true);
        }
        else {
            Debug.Log("Patrol Left");
            // enemy.myAnimator.SetBool("isPatrolLeft", true);
        }
    }

    public override void ExitState(EnemyStateManager enemy) {

    }

    public override void UpdateState(EnemyStateManager enemy) {
        float distanceFromStart = enemy.transform.position.x - initialPosition;

        // Go right
        if (patrolDirection) {
            enemy.myRigidBody.velocity = new Vector2(patrolSpeed, 0f);
        }
        // Go left
        else {
            enemy.myRigidBody.velocity = new Vector2(-patrolSpeed, 0f);
        }

        // Check if player is in chase radius and if enemy is facing player
        float distanceFromPlayer = Vector2.Distance(enemy.transform.position, enemy.player.transform.position);
        if (distanceFromPlayer <= enemy.chaseRadius && enemy.IsPlayerFacingEnemy()) {
            // Reset patrol animation

            // Switch to chase state
            enemy.SwitchState(enemy.ChaseState);
        }

        // Check if enemy is out of patrol radius
        if (Mathf.Abs(distanceFromStart) >= patrolRadius) {
            // Reset patrol animation
            // if (patrolDirection) {
            //     enemy.myAnimator.SetBool("isPatrolRight", false);
            // }
            // else {
            //     enemy.myAnimator.SetBool("isPatrolLeft", false);
            // }

            // Switch to idle state
            enemy.SwitchState(enemy.IdleState);
        }
    }
}