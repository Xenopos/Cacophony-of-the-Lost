using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;

public class PlayerRunState : PlayerBaseState {
    public override void EnterState(PlayerStateManager player) {
        Debug.Log("Run");
        player.currentSpeed = player.runSpeed;
        player.myAnimator.SetBool("isRunning", true);
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
                    player.myRigidBody.velocity = new Vector2(player.runSpeed, player.myRigidBody.velocity.y);
                }
                else {
                    Debug.Log("Run left");
                    player.myRigidBody.velocity = new Vector2(-player.runSpeed, player.myRigidBody.velocity.y);
                }
            } else if (Input.GetAxisRaw("Horizontal") == 0) { 
                player.myRigidBody.velocity = Vector2.zero;
                player.currentSpeed = 0f;
                player.myAnimator.SetBool("isRunning", false);
                player.SwitchState(player.IdleState);
            }
        } else {
            player.myAnimator.SetBool("isRunning", false);
            player.SwitchState(player.IdleState);
        } 
    }
}