using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;

public class PlayerRunState : PlayerBaseState {
    public override void EnterState(PlayerStateManager player) {
        Debug.Log("Run");

        // Set direction
        player.direction = Input.GetAxisRaw("Horizontal") == 1 ? true : false;

        // Set run animation
    }

    public override void ExitState(PlayerStateManager player) {
        
    }

    public override void OnTriggerEnter2D(PlayerStateManager player, Collider2D collider) {
        
    }

    public override void UpdateState(PlayerStateManager player) {
        if (Input.GetKey(KeyCode.LeftShift)) {
            if (Input.GetAxisRaw("Horizontal") != 0) {
                // Update direction
                player.direction = Input.GetAxisRaw("Horizontal") == 1 ? true : false;

                // Run to direction
                if (player.direction) {
                    Debug.Log("Run right");

                    player.myRigidBody.velocity = new Vector2(player.runSpeed, player.myRigidBody.velocity.y);
                }
                else {
                    Debug.Log("Run left");

                    player.myRigidBody.velocity = new Vector2(-player.runSpeed, player.myRigidBody.velocity.y);
                }
            } else {
                // Reset walk animation

                // Switch to idle state
                player.SwitchState(player.IdleState);
            } 
        }

        // if (Input.GetAxisRaw("Horizontal") == 1 || Input.GetAxisRaw("Horizontal") == -1) {
        //     // Reset run animation

        //     // Switch to walk state
        //     player.SwitchState(player.WalkState);
        // }

        // if (Input.GetKey(KeyCode.LeftControl)) {
        //     // Reset run animation

        //     // Switch to crouch state
        //     player.SwitchState(player.CrouchState);
        // }

        // if (Input.GetKey(KeyCode.Space)) {
        //     // Reset run animation

        //     // Switch to attack state
        //     player.SwitchState(player.AttackState);
        // }

        // if (Input.GetAxisRaw("Horizontal") == 0) {
        //     // Reset run animation

        //     // Switch to idle state
        //     player.SwitchState(player.IdleState);
        // }

    }
}