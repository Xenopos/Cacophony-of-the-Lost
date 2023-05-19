using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KaylaAI;

public class KaylaCrouchState : KaylaBaseState { 
    private float crouchSpeed;

    public override void EnterState(KaylaStateManager player) {
        Debug.Log("Kayla Crouch State");

        Debug.Log("Crouch speed: " + player.suzuneStateManager.currentSpeed);

        player.animator.SetBool("isCrouching", true);
        player.currentSpeed = 0f;
        
        player.direction = player.suzuneStateManager.direction;
    }

    public override void OnTriggerEnter2D(KaylaStateManager player, Collider2D collider) {

    }

    public override void UpdateState(KaylaStateManager player) {
        switch (player.suzuneStateManager.currentState) {
            case PlayerCrouchState crouchState:
                crouchSpeed = player.suzuneStateManager.currentSpeed;
                player.currentSpeed = crouchSpeed;
                player.direction = player.suzuneStateManager.direction;
                if (player.direction) {
                    Debug.Log("Crouch right");
                    player.rigidBody.velocity = new Vector2(crouchSpeed, player.rigidBody.velocity.y);
                } else {
                    Debug.Log("Crouch left");
                    player.rigidBody.velocity = new Vector2(-crouchSpeed, player.rigidBody.velocity.y);
                }
                break; 

            case PlayerWalkState walkState:
                player.animator.SetBool("isCrouching", false);
                player.SwitchState(player.WalkState);
                break;

            case PlayerRunState runState:
                player.animator.SetBool("isCrouching", false);
                player.SwitchState(player.RunState);
                break;

            case PlayerIdleState idleState:
                player.animator.SetBool("isCrouching", false);
                player.SwitchState(player.IdleState);
                break;

            default:
                player.animator.SetBool("isCrouching", false);
                player.SwitchState(player.IdleState);
                break;
        }
    }

    public override void ExitState(KaylaStateManager player) {

    }
}