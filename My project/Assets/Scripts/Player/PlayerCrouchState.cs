using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;

public class PlayerCrouchState : PlayerBaseState {
    public override void EnterState(PlayerStateManager player) {
        Debug.Log("Crouch");

        // Set direction
        player.direction = Input.GetAxisRaw("Horizontal") == 1 ? true : false;

        // Set crouch animation
    }

    public override void ExitState(PlayerStateManager player) {
        
    }

    public override void OnTriggerEnter2D(PlayerStateManager player, Collider2D collider) {
        
    }

    public override void UpdateState(PlayerStateManager player) {
        if (Input.GetKey(KeyCode.LeftControl)) {
            if (Input.GetAxisRaw("Horizontal") != 0) {
                // Update direction
                player.direction = Input.GetAxisRaw("Horizontal") == 1 ? true : false;

                // Crouch to direction
                if (player.direction) {
                    Debug.Log("Crouch right");

                    player.myRigidBody.velocity = new Vector2(player.crouchSpeed, player.myRigidBody.velocity.y);
                }
                else {
                    Debug.Log("Crouch left");

                    player.myRigidBody.velocity = new Vector2(-player.crouchSpeed, player.myRigidBody.velocity.y);
                }
            } else {
                // Stop movement
                player.myRigidBody.velocity = Vector2.zero;
            } 
        } else {
            // Reset crouch animation

            // Switch to idle state
            player.SwitchState(player.IdleState);
        }
    }
}