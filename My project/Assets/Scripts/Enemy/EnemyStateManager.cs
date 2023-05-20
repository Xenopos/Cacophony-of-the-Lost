using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enemy;

public class EnemyStateManager : MonoBehaviour {
    // States
    public EnemyBaseState currentState;
    public EnemyIdleState IdleState = new EnemyIdleState();
    public EnemyPatrolState PatrolState = new EnemyPatrolState();
    public EnemyChaseState ChaseState = new EnemyChaseState();
    public EnemyAttackState AttackState = new EnemyAttackState();
 
    // Enemy components
    public Rigidbody2D rigidBody;
    public Animator animator;
    public CircleCollider2D circleCollider;

    // Player components
    public GameObject player;
    public PlayerStateManager playerStateManager; 

    public bool direction; // true = right, false = left
    public float currentSpeed;
    public float health;
    public float maxHealth;
    public float chaseRadius;
    public bool hasChased;

    public float patrolRadius = 5f;
    public float attackRadius = 1.4f;
    public float patrolSpeed = 3f;
    public float chaseSpeed = 5f;
    public float deathTimer;

    public void Start() {
        // Initialize components
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        circleCollider = GetComponent<CircleCollider2D>();
        player = GameObject.Find("Player");
        playerStateManager = player.GetComponent<PlayerStateManager>();

        // Initialize variables
        direction = true;
        currentSpeed = 0f;
        maxHealth = 10f;
        health = maxHealth;
        chaseRadius = 3f;
        hasChased = false;
        
        // Initialize current state to PatrolState
        currentState = PatrolState;
        currentState.EnterState(this);
    }


    void OnTriggerEnter2D(Collider2D collider) {
        currentState.OnTriggerEnter2D(this, collider);
    }

    public void FixedUpdate() {
        SetAnimatorVariables();
        Set2DColliderOffset();
        currentState.UpdateState(this);
    }

    public void SwitchState(EnemyBaseState newState) {
        currentState.ExitState(this);
        currentState = newState;
        currentState.EnterState(this);
    }

    public bool IsPlayerAttacking() {
        if (playerStateManager.currentState == playerStateManager.AttackState) {
            return true;
        }
        return false;
    }

    // Helper function to check if enemy is facing player
    public bool IsPlayerFacingEnemy() {
        return playerStateManager.direction != direction;
    }

    // Helper function to check if player is crouching
    public bool IsPlayerCrouching() {
        if (playerStateManager.currentState == playerStateManager.CrouchState || playerStateManager.currentState == playerStateManager.HideState) {
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(), player.GetComponent<Collider2D>());
            return true;
        } 

        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), player.GetComponent<Collider2D>(), false);
        return false;
    }

    // Helper function to take damage from player
    public void TakeDamage(float damage) {
        health -= damage;
        Debug.Log("Enemy health: " + health);
        if (health <= 0) {
            Debug.Log("Enemy died");
            animator.SetBool("isDead", true);
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(), player.GetComponent<Collider2D>());
            this.enabled = false;
            PlayerStateManager.sanityLevel -= 10f;
            hasChased = false;
            
            Debug.Log("Sanity level: " + PlayerStateManager.sanityLevel);
        }
    }


    public void SetAnimatorVariables() {
        // Direction
        if (direction) {
            animator.SetFloat("Direction", 1);
        } else {
            animator.SetFloat("Direction", -1);
        }

        // Speed
        animator.SetFloat("Speed", currentSpeed);
        animator.SetFloat("Horizontal", currentSpeed);
    }

    public void Set2DColliderOffset() {
        float offsetRight = 0.36f;
        float offsetLeft = -0.5f;
        float offset;
        if (!direction) offset = offsetLeft;
        else offset = offsetRight;
        circleCollider.offset = new Vector2(offset, circleCollider.offset.y);
    }
}