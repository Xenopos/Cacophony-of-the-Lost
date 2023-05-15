using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;

public class PlayerRunState : PlayerBaseState {
    private float runSpeed = 8f;

    public override void EnterState(PlayerStateManager player) {
        Debug.Log("Run");

        // Set the player's speed to run speed
        player.currentSpeed = runSpeed;
    }

    public override void ExitState(PlayerStateManager player) {
        
    }

    public override void OnTriggerEnter2D(PlayerStateManager player, Collider2D collider) {
        
    }

    public override void UpdateState(PlayerStateManager player) {
        if (Input.GetKey(KeyCode.LeftShift)) {
            if (Input.GetAxisRaw("Horizontal") != 0) {
                player.direction = Input.GetAxisRaw("Horizontal") == 1 ? true : false;
                
                if (player.direction) {
                    Debug.Log("Run right");
                    player.rigidBody.velocity = new Vector2(runSpeed, player.rigidBody.velocity.y);
                } else {
                    Debug.Log("Run left");
                    player.rigidBody.velocity = new Vector2(-runSpeed, player.rigidBody.velocity.y);
                }
            } else if (Input.GetAxisRaw("Horizontal") == 0) { 
                player.SwitchState(player.IdleState);
            }
        } else {
            player.SwitchState(player.IdleState);
        } 
    }
}