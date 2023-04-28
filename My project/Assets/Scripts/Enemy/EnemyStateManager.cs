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
    public CircleCollider2D myCollider;

    public PlayerStateManager playerStateManager; 

    public bool patrolDirection = false; // true = right, false = left
    public float patrolRadius = 5f;
    public float chaseRadius = 3f;
    public float attackRadius = 1.4f;
    public float patrolSpeed = 1f;
    public float chaseSpeed = 2f;
    public float damage = 1f;
    public float attackCooldown = 1f;
    public float health;
    public float maxHealth = 10f;

    public void Start() {
        myRigidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myCollider = GetComponent<CircleCollider2D>();
        player = GameObject.Find("Player");
        playerStateManager = player.GetComponent<PlayerStateManager>();

        health = maxHealth;

        currentState = PatrolState;
        currentState.EnterState(this);
    }

    void OnTriggerEnter2D(Collider2D collider) {
        currentState.OnTriggerEnter2D(this, collider);
    }

    public void FixedUpdate() {
        currentState.UpdateState(this);
    }

    public void SwitchState(EnemyBaseState newState) {
        currentState.ExitState(this);
        currentState = newState;
        currentState.EnterState(this);
    }

    // Helper function to check if enemy is facing player
    public bool IsPlayerFacingEnemy() {
        return playerStateManager.direction != patrolDirection;
    }

    // Helper function to check if player is crouching
    public bool IsPlayerCrouching() {
        if (playerStateManager.currentState == playerStateManager.CrouchState) {
            Debug.Log("Player is crouching ESM");
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(), player.GetComponent<Collider2D>());
            return true;
        } 
        Debug.Log("Player is NOT crouching ESM");
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), player.GetComponent<Collider2D>(), false);
        return false;
    }

    // Helper function to take damage from player
    public void TakeDamage(float damage) {
        health -= damage;
        Debug.Log("Enemy health: " + health);
        if (health <= 0) {
            Debug.Log("Enemy died");
            Destroy(gameObject);
        }
    }
}