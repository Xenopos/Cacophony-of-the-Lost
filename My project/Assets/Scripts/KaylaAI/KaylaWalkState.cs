using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KaylaAI;

public class KaylaWalkState : KaylaBaseState {
    private float walkSpeed = 3f;

    public override void EnterState(KaylaStateManager player) {
        Debug.Log("Kayla Walk State");

        player.currentSpeed = player.suzuneStateManager.currentSpeed;
        player.direction = player.suzuneStateManager.direction;
    }

    public override void OnTriggerEnter2D(KaylaStateManager player, Collider2D collider) {
        
    }

    public override void UpdateState(KaylaStateManager player) {
        switch (player.suzuneStateManager.currentState) {
            case PlayerWalkState walkState:
                if (player.direction) {
                    Debug.Log("Kayla Walk Right");
                    player.rigidBody.velocity = new Vector2(walkSpeed, player.rigidBody.velocity.y);
                } else {
                    Debug.Log("Kayla Walk Left");
                    player.rigidBody.velocity = new Vector2(-walkSpeed, player.rigidBody.velocity.y);
                }

                break;
            case PlayerIdleState idleState:
                player.SwitchState(player.IdleState);
                break;

            case PlayerRunState runState:
                player.SwitchState(player.RunState);
                break;

            case PlayerCrouchState crouchState:
                player.SwitchState(player.CrouchState);
                break;

            case PlayerAttackState attackState:
                player.SwitchState(player.CrouchState);
                break;

            default:
                player.SwitchState(player.IdleState);
                break;
        }
    }

    public override void ExitState(KaylaStateManager player) {

    }
}