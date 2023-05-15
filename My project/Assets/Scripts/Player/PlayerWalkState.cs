using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;

public class PlayerWalkState : PlayerBaseState {
    private float walkSpeed = 4f;

    public override void EnterState(PlayerStateManager player) {
        Debug.Log("Walk State");
        
        // Set the player's speed to walk speed
        player.currentSpeed = walkSpeed;
    }

    public override void ExitState(PlayerStateManager player) {
       
    }

    public override void OnTriggerEnter2D(PlayerStateManager player, Collider2D collider) {
        
    }

    public override void UpdateState(PlayerStateManager player) {
        if (Input.GetAxisRaw("Horizontal") != 0) {
            player.direction = Input.GetAxisRaw("Horizontal") == 1 ? true : false;
            
            if (Input.GetKey(KeyCode.LeftShift)) {
                player.SwitchState(player.RunState);
            }

            if (Input.GetKey(KeyCode.LeftControl)) {
                player.SwitchState(player.CrouchState);
            }

            if (player.direction) {
                Debug.Log("Walk Right");
                player.rigidBody.velocity = new Vector2(walkSpeed, player.rigidBody.velocity.y);
            } else {
                Debug.Log("Walk Left");
                player.rigidBody.velocity = new Vector2(-walkSpeed, player.rigidBody.velocity.y);
            }
        } else {
            player.SwitchState(player.IdleState);
        } 
    }
}