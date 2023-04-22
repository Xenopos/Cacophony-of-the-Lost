using UnityEngine;

namespace Player {
    public abstract class PlayerBaseState {
        public abstract void EnterState(PlayerStateManager player);
        // public abstract void OnCollisionEnter2D(EnemyStateManager enemy, Collision2D collision);
        public abstract void OnTriggerEnter2D(PlayerStateManager player, Collider2D collider);
        public abstract void UpdateState(PlayerStateManager player);
        public abstract void ExitState(PlayerStateManager player);
    } 
}
