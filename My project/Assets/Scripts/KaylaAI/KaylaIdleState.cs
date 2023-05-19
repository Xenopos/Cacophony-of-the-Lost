using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KaylaAI;
using Player;

public class KaylaIdleState : KaylaBaseState {
    private float idleSpeed = 0f;

    public override void EnterState(KaylaStateManager player) {
        Debug.Log("Kayla Idle State");

        player.currentSpeed = idleSpeed;
        player.rigidBody.velocity = Vector2.zero;
        player.direction = player.suzuneStateManager.direction;
    }

    public override void OnTriggerEnter2D(KaylaStateManager player, Collider2D collider) {
        
    }

    public override void UpdateState(KaylaStateManager player) {
        switch (player.suzuneStateManager.currentState) {
            case PlayerWalkState walkState:
                player.SwitchState(player.WalkState);
                break;

            case PlayerRunState runState:
                player.SwitchState(player.RunState);
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