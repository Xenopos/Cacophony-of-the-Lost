using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;

public class PlayerWalkState : PlayerBaseState {
    public override void EnterState(PlayerStateManager player) {
        Debug.Log("Walk");

        // Set direction
        player.direction = Input.GetAxisRaw("Horizontal") == 1 ? true : false;
    }

    public override void ExitState(PlayerStateManager player) {
       
    }

    public override void OnTriggerEnter2D(PlayerStateManager player, Collider2D collider) {
        
    }

    public override void UpdateState(PlayerStateManager player) {
        if (Input.GetAxisRaw("Horizontal") != 0) {
            // Update direction
            player.direction = Input.GetAxisRaw("Horizontal") == 1 ? true : false;

            if (Input.GetKey(KeyCode.LeftShift)) {
                // Switch to run state
                player.SwitchState(player.RunState);
            }

            if (Input.GetKey(KeyCode.LeftControl)) {
                // Switch to crouch state
                player.SwitchState(player.CrouchState);
            }

            // Walk to direction and update animation
            if (player.direction) {
                Debug.Log("Walk Right");

                // Update animation to walk right

                player.myRigidBody.velocity = new Vector2(player.walkSpeed, player.myRigidBody.velocity.y);
            }
            else {
                Debug.Log("Walk Left");

                // Update animation to walk left 

                player.myRigidBody.velocity = new Vector2(-player.walkSpeed, player.myRigidBody.velocity.y);
            }
        } else {
            // Reset walk animation

            // Switch to idle state
            player.SwitchState(player.IdleState);
        } 
    }
}