using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;

public class PlayerAttackState : PlayerBaseState {
    private float damage;
    private float attackCooldown = 0.6f; // Cooldown between attacks in seconds
    private float attackTimer = 0f; // Timer to keep track of cooldown
    private CircleCollider2D attackCollider;

    public override void EnterState(PlayerStateManager player) {
        Debug.Log("Attack");

        // Set variables
        damage = PlayerStateManager.damage;
        attackCollider = player.circleCollider;

        // Set the player's animation to attack
        player.animator.SetBool("isAttacking", true);

        foreach (EnemyStateManager enemy in player.enemyStateManagers) {
            if (enemy != null) {
                float distanceFromEnemy = Vector2.Distance(player.transform.position, enemy.transform.position);
                if (distanceFromEnemy <= player.attackRadius && player.canAttack) {
                    enemy.TakeDamage(damage);
                    attackCollider.enabled = false;
                    player.canAttack = false;
                }
            }
        }
    }

    public override void ExitState(PlayerStateManager player) {
       
    }

    public override void OnTriggerEnter2D(PlayerStateManager player, Collider2D collision) {
        // if (collision.GetComponent<BoxCollider2D>() != null) {
        //     Debug.Log("Player is attacking enemy!");
        //     if (collision.CompareTag("Enemy")) {
        //         if (player.canAttack) {
        //             Debug.Log("Player is attacking enemy for real!");

        //             // Damage player
        //             player.enemyStateManager.TakeDamage(damage);

        //             // Set canAttack to false and disable attack colider to prevent multiple attacks
        //             //canAttack = false;
        //         }    
        //     }
        // }
    }

    public override void UpdateState(PlayerStateManager player) {
        attackTimer += Time.deltaTime;
        if (attackTimer >= attackCooldown) {
            attackTimer = 0f;
            player.canAttack = true;
            player.animator.SetBool("isAttacking", false);
            player.SwitchState(player.IdleState);
        }

    }
}