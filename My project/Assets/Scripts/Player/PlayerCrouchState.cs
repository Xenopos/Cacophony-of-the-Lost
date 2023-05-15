using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;

public class PlayerCrouchState : PlayerBaseState {
    private float crouchSpeed = 2f;

    public override void EnterState(PlayerStateManager player) {
        Debug.Log("Crouch");

        // Set the player's speed to crouch speed
        player.currentSpeed = crouchSpeed;

        // Set the player's animation to crouch
        player.animator.SetBool("isCrouching", true);
    }

    public override void ExitState(PlayerStateManager player) {
        
    }

    public override void OnTriggerEnter2D(PlayerStateManager player, Collider2D collider) {
        
    }

    public override void UpdateState(PlayerStateManager player) {
        if (Input.GetKey(KeyCode.LeftControl)) {
            if (Input.GetAxisRaw("Horizontal") != 0) {
                player.currentSpeed = crouchSpeed;
                player.direction = Input.GetAxisRaw("Horizontal") == 1 ? true : false;

                if (player.direction) {
                    Debug.Log("Crouch right");
                    player.rigidBody.velocity = new Vector2(crouchSpeed, player.rigidBody.velocity.y);
                }
                else {
                    Debug.Log("Crouch left");
                    player.rigidBody.velocity = new Vector2(-crouchSpeed, player.rigidBody.velocity.y);
                }
            } else {
                player.rigidBody.velocity = Vector2.zero;
                player.currentSpeed = 0f;
            } 
        } else {
            // Set the player's animation to idle
            player.animator.SetBool("isCrouching", false);
            player.SwitchState(player.IdleState);
        }
    }
}