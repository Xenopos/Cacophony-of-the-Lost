using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;

public class PlayerCrouchState : PlayerBaseState {
    public override void EnterState(PlayerStateManager player) {
        Debug.Log("Crouch");
        player.currentSpeed = player.crouchSpeed;
        player.myAnimator.SetTrigger("isCrouching");
    }

    public override void ExitState(PlayerStateManager player) {
        
    }

    public override void OnTriggerEnter2D(PlayerStateManager player, Collider2D collider) {
        
    }

    public override void UpdateState(PlayerStateManager player) {
        if (Input.GetKey(KeyCode.LeftControl)) {
            if (Input.GetAxisRaw("Horizontal") != 0) {
                player.currentSpeed = player.crouchSpeed;
                player.direction = Input.GetAxisRaw("Horizontal") == 1 ? true : false;

                if (player.direction) {
                    Debug.Log("Crouch right");
                    player.myRigidBody.velocity = new Vector2(player.crouchSpeed, player.myRigidBody.velocity.y);
                }
                else {
                    Debug.Log("Crouch left");
                    player.myRigidBody.velocity = new Vector2(-player.crouchSpeed, player.myRigidBody.velocity.y);
                }
            } else {
                player.myRigidBody.velocity = Vector2.zero;
                player.currentSpeed = 0f;
            } 
        } else {
            player.myAnimator.ResetTrigger("isCrouching");
            player.SwitchState(player.IdleState);
        }
    }
}