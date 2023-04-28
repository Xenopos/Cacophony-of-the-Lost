using UnityEngine;

namespace Enemy {
    public abstract class EnemyBaseState {
        public abstract void EnterState(EnemyStateManager enemy);
        // public abstract void OnCollisionEnter2D(EnemyStateManager enemy, Collision2D collision);
        public abstract void OnTriggerEnter2D(EnemyStateManager enemy, Collider2D collider);
        public abstract void UpdateState(EnemyStateManager enemy);
        public abstract void ExitState(EnemyStateManager enemy);
        private void FixedUpdate(EnemyStateManager enemy) {
            UpdateState(enemy);
        }
    } 
}
