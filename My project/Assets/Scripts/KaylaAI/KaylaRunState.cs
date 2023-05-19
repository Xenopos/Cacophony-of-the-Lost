using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KaylaAI;

public class KaylaRunState : KaylaBaseState {
    private float runSpeed;

    public override void EnterState(KaylaStateManager player) {
        Debug.Log("Kayla Run State");

        runSpeed = player.suzuneStateManager.currentSpeed;

        player.currentSpeed = runSpeed;
        player.direction = player.suzuneStateManager.direction;
    }

    public override void OnTriggerEnter2D(KaylaStateManager player, Collider2D collider) {
        
    }

    public override void UpdateState(KaylaStateManager player) {
        switch (player.suzuneStateManager.currentState) {
            case PlayerRunState runState:
                if (player.direction) {
                    Debug.Log("Run right");
                    player.rigidBody.velocity = new Vector2(runSpeed, player.rigidBody.velocity.y);
                } else {
                    Debug.Log("Run left");
                    player.rigidBody.velocity = new Vector2(-runSpeed, player.rigidBody.velocity.y);
                }
                break;

            case PlayerWalkState walkState:
                player.SwitchState(player.WalkState);
                break;

            case PlayerCrouchState crouchState:
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