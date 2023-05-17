using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;

public class PlayerStateManager : MonoBehaviour {
    // States 
    public PlayerBaseState currentState;
    public PlayerWalkState WalkState = new PlayerWalkState();
    public PlayerIdleState IdleState = new PlayerIdleState();
    public PlayerAttackState AttackState = new PlayerAttackState();
    public PlayerCrouchState CrouchState = new PlayerCrouchState();
    public PlayerRunState RunState = new PlayerRunState();
    public PlayerHideState HideState = new PlayerHideState();

    // Player components
    public Rigidbody2D rigidBody;
    public Animator animator;
    public CircleCollider2D circleCollider;

    // Enemy components 
    public List<EnemyStateManager> enemyStateManagers = new List<EnemyStateManager>();

    // Player variables
    public bool direction; 
    public float currentSpeed;
    public float maxHealth;
    public float health;
    public float attackRadius;
    public bool canAttack = true;
    public float attackCooldown = 2f;
    public float attackTimer = 0f;
    // public float attackCooldown;
    // public float attackTimer;

    public void Start() {  
        // Initialize components
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        circleCollider = GetComponent<CircleCollider2D>();
        circleCollider.radius = 0.6f;

        // Initialize enemies
        GameObject[] enemyObjects = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemyObjects) {
            enemyStateManagers.Add(enemy.GetComponent<EnemyStateManager>());
        }

        // Initialize variables
        currentSpeed = 0f;
        direction = true;
        maxHealth = 10f;
        health = maxHealth;
        attackRadius = 2f;
        // attackCooldown = 2f;
        // attackTimer = 0f;
        
        // Initialize current state to IdleState
        currentState = IdleState;
        currentState.EnterState(this);
    }

    void OnTriggerEnter2D(Collider2D collider) {
        currentState.OnTriggerEnter2D(this, collider);
    }

    public void FixedUpdate() {
        if (!canAttack) {
            attackTimer += Time.deltaTime;
            if (attackTimer >= attackCooldown) {
                canAttack = true;
                attackTimer = 0f;
            }
        }

        if (canAttack) {
            circleCollider.enabled = true;
        }

        SetAnimatorVariables();
        Set2DColliderOffset();
        currentState.UpdateState(this);
    }

    public void SwitchState(PlayerBaseState newState) {
        currentState.ExitState(this);
        currentState = newState;
        currentState.EnterState(this);
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

        // Attack
        if (currentState.Equals(AttackState)) {
            animator.SetBool("isAttacking", true);
        } else {
            animator.SetBool("isAttacking", false);
        }
    }

    public void TakeDamage(float damage) {
        health -= damage;
        Debug.Log("Player health: " + health);
        if (health <= 0) {
            Debug.Log("Player died");
        }
    }

    public void Set2DColliderOffset() {
        float offsetRight = 0.48f;
        float offsetLeft = -0.5f;
        float offset;
        if (!direction) offset = offsetLeft;
        else offset = offsetRight;
        circleCollider.offset = new Vector2(offset, circleCollider.offset.y);
    }
}