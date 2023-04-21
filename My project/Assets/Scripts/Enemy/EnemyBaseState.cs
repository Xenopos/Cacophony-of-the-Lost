using UnityEngine;

namespace Enemy {
    public abstract class EnemyBaseState {
        public abstract void EnterState(EnemyStateManager enemy);
        public abstract void ExitState(EnemyStateManager enemy);
        public abstract void UpdateState(EnemyStateManager enemy);
    } 
}
