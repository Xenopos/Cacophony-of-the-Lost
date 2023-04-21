using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enemy;

public class EnemyChaseState : EnemyBaseState {
    private float chaseSpeed;

    public override void EnterState(EnemyStateManager enemy) {
        Debug.Log("Chase");

        // Set chase speed
        chaseSpeed = enemy.chaseSpeed;

        // Set chase animation
    }

    public override void ExitState(EnemyStateManager enemy) {

    }

    // Might need to change in case enemy is not facing player
    public override void UpdateState(EnemyStateManager enemy) {
        // Chase player
        // enemy.myRigidBody.velocity = new Vector2(enemy.transform.position.x - enemy.player.transform.position.x, 0f).normalized * chaseSpeed;
        enemy.transform.position = Vector2.MoveTowards(enemy.transform.position, enemy.player.transform.position, chaseSpeed * Time.deltaTime);

        // Check if player is in attack radius and if enemy is facing player
        float distanceFromPlayer = Vector2.Distance(enemy.transform.position, enemy.player.transform.position);
        if (distanceFromPlayer <= enemy.attackRadius && enemy.IsPlayerFacingEnemy()) {
            // Reset chase animation

            // Switch to attack state
            enemy.SwitchState(enemy.AttackState);
        }
    }
}