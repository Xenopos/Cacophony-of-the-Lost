using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;

public class PlayerHideState : PlayerBaseState {
    private float hideSpeed = 0f;

    public override void EnterState(PlayerStateManager player) {
        Debug.Log("Hide");

        // Set the player's speed to hide speed and set the player's velocity to zero
        player.currentSpeed = hideSpeed;
        player.rigidBody.velocity = Vector2.zero;

        // Set the player's animation to hide
        // player.animator.SetBool("isHiding", true);
        player.animator.SetBool("isCrouching", true);
    }

    public override void ExitState(PlayerStateManager player) {
        
    }

    public override void OnTriggerEnter2D(PlayerStateManager player, Collider2D collider) {
        
    }

    public override void UpdateState(PlayerStateManager player) {
        
    }
}