using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;

public class PlayerAttackState : PlayerBaseState {
    private float damage;
    private bool canAttack;
    private float attackCooldown; // Cooldown between attacks in seconds
    private float attackTimer = 0f; // Timer to keep track of cooldown

    public override void EnterState(PlayerStateManager player) {
        Debug.Log("Attack");
        attackCooldown = player.attackCooldown;
        damage = player.damage;
        canAttack = true;
        player.myAnimator.SetBool("isAttacking", true);
    }

    public override void ExitState(PlayerStateManager player) {
       
    }

    public override void OnTriggerEnter2D(PlayerStateManager player, Collider2D collision) {
        if (collision.GetComponent<BoxCollider2D>() != null) {
            if (collision.CompareTag("Enemy")) {
                if (canAttack) {
                    if (Input.GetKey(KeyCode.Space)) {
                        Debug.Log("Player is attacking enemy");
                        player.enemyStateManager.TakeDamage(damage);
                    }
                }    
            }
        }
        player.myAnimator.SetBool("isAttacking", false);
    }

    public override void UpdateState(PlayerStateManager player) {
        attackTimer += Time.deltaTime;
        if (attackTimer >= attackCooldown) {
            attackTimer = 0f;
            canAttack = true;
        }
        player.myAnimator.SetBool("isAttacking", false);
        player.SwitchState(player.IdleState);
    }
}