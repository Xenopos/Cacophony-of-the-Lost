using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;

public class PlayerWalkState : PlayerBaseState {
    public override void EnterState(PlayerStateManager player) {
        Debug.Log("Walk");
        player.currentSpeed = player.walkSpeed;
    }

    public override void ExitState(PlayerStateManager player) {
       
    }

    public override void OnTriggerEnter2D(PlayerStateManager player, Collider2D collider) {
        
    }

    public override void UpdateState(PlayerStateManager player) {
        if (Input.GetAxisRaw("Horizontal") != 0) {
            player.direction = Input.GetAxisRaw("Horizontal") == 1 ? true : false;
            if (Input.GetKey(KeyCode.LeftShift)) {
                player.myAnimator.SetBool("isWalking", false);
                player.SwitchState(player.RunState);
            }
            if (Input.GetKey(KeyCode.LeftControl)) {
                player.SwitchState(player.CrouchState);
            }
            if (player.direction) {
                Debug.Log("Walk Right");
                player.myRigidBody.velocity = new Vector2(player.walkSpeed, player.myRigidBody.velocity.y);
            }
            else {
                Debug.Log("Walk Left");
                player.myRigidBody.velocity = new Vector2(-player.walkSpeed, player.myRigidBody.velocity.y);
            }
        } else {
            player.SwitchState(player.IdleState);
        } 
    }
}