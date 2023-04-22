using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;

public class PlayerIdleState : PlayerBaseState {
    public override void EnterState(PlayerStateManager player) {
        Debug.Log("Idle");

        // Set direction
        player.direction = Input.GetAxisRaw("Horizontal") == 1 ? true : false;

        // Stop movement
        player.myRigidBody.velocity = Vector2.zero;

        // Set idle animation
    }

    public override void ExitState(PlayerStateManager player) {
        
    }

    public override void OnTriggerEnter2D(PlayerStateManager player, Collider2D collider) {
        
    }

    public override void UpdateState(PlayerStateManager player) {
        if (Input.GetAxisRaw("Horizontal") == 1 || Input.GetAxisRaw("Horizontal") == -1) {
            // Reset idle animation
           
            if (Input.GetKey(KeyCode.LeftShift)) {
                // Switch to run state
                player.SwitchState(player.RunState);
            }

            if (Input.GetKey(KeyCode.LeftControl)) {
                // Switch to crouch state
                player.SwitchState(player.CrouchState);
            }
            
            // Switch to walk state
            player.SwitchState(player.WalkState);
        }

        if (Input.GetKey(KeyCode.LeftControl)) {
            // Reset idle animation

            // Switch to crouch state
            player.SwitchState(player.CrouchState);
        }

        if (Input.GetKey(KeyCode.Space)) {
            // Reset idle animation

            // Switch to attack state
            player.SwitchState(player.AttackState);
        }
    }
}