using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enemy;

public class EnemyStateManager : MonoBehaviour {
    EnemyBaseState currentState;
    public EnemyIdleState IdleState = new EnemyIdleState();
    public EnemyPatrolState PatrolState = new EnemyPatrolState();
    public EnemyChaseState ChaseState = new EnemyChaseState();
    public EnemyAttackState AttackState = new EnemyAttackState();

    public Rigidbody2D myRigidBody;
    public Animator myAnimator;
    public GameObject player;

    public bool patrolDirection = true; // true = right, false = left
    public float patrolRadius = 5f;
    public float chaseRadius = 3f;
    public float attackRadius = 1.4f;
    public float patrolSpeed = 1f;
    public float chaseSpeed = 2f;
    public float damage = 1f;
    public float attackDelay = 1f;

    public void Start() {
        myRigidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        player = GameObject.Find("Player");

        currentState = PatrolState;
        currentState.EnterState(this);
    }

    public void Update() {
        currentState.UpdateState(this);
    }

    public void SwitchState(EnemyBaseState newState) {
        currentState.ExitState(this);
        currentState = newState;
        currentState.EnterState(this);
    }

    // Helper function to check if enemy is facing player
    public bool IsPlayerFacingEnemy() {
        Vector2 directionToPlayer = (player.transform.position - transform.position).normalized;
        float dotProduct = Vector2.Dot(directionToPlayer, transform.right);
        return dotProduct > 0f; // Check if dotProduct is greater than 0 to determine if the player is facing the enemy
    }

}