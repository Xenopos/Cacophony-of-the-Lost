using UnityEngine;

namespace KaylaAI {
    public abstract class KaylaBaseState {
        public abstract void EnterState(KaylaStateManager player);
        // public abstract void OnCollisionEnter2D(EnemyStateManager enemy, Collision2D collision);
        public abstract void OnTriggerEnter2D(KaylaStateManager player, Collider2D collider);
        public abstract void UpdateState(KaylaStateManager player);
        public abstract void ExitState(KaylaStateManager player);
        private void FixedUpdate(KaylaStateManager player) {
            UpdateState(player);
        }
    } 
}
