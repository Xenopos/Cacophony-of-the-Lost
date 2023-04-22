using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;

public class PlayerAttackState : PlayerBaseState {
    public override void EnterState(PlayerStateManager player) {
        Debug.Log("Attack");

        // Set direction
        player.direction = Input.GetAxisRaw("Horizontal") == 1 ? true : false;
    }

    public override void ExitState(PlayerStateManager player) {
       
    }

    public override void OnTriggerEnter2D(PlayerStateManager player, Collider2D collider) {
        
    }

    public override void UpdateState(PlayerStateManager player) {
        if (Input.GetKey(KeyCode.Space)) {
            Debug.Log("Attack");
        } else {
            // Reset attack animation

            // Switch to idle state
            player.SwitchState(player.IdleState);
        } 
    }
}