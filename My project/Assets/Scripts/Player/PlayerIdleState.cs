using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;

public class PlayerIdleState : PlayerBaseState {
    private float idleSpeed = 0f;

    public override void EnterState(PlayerStateManager player) {
        Debug.Log("Idle State");

        // Set the player's speed to idle speed and set the player's velocity to zero
        player.currentSpeed = idleSpeed;
        player.rigidBody.velocity = Vector2.zero;
    }

    public override void ExitState(PlayerStateManager player) {
        
    }

    public override void OnTriggerEnter2D(PlayerStateManager player, Collider2D collider) {
        
    }

    public override void UpdateState(PlayerStateManager player) {
        if (Input.GetAxisRaw("Horizontal") == 1 || Input.GetAxisRaw("Horizontal") == -1) {
            if (Input.GetAxisRaw("Horizontal") == 1) {
                player.direction = true;
            } else {
                player.direction = false;
            }
            
            if (Input.GetKey(KeyCode.LeftShift)) {
                player.SwitchState(player.RunState);
            }

            if (Input.GetKey(KeyCode.LeftControl)) {
                player.SwitchState(player.CrouchState);
            }            
            player.SwitchState(player.WalkState);
        }

        if (Input.GetKey(KeyCode.LeftControl)) {
            player.SwitchState(player.CrouchState);
        }

        if (Input.GetKey(KeyCode.Space)) {
            player.SwitchState(player.AttackState);
        }
    }
}