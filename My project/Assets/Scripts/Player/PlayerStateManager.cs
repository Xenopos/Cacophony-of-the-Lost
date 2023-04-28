using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;

public class PlayerStateManager : MonoBehaviour {
    public PlayerBaseState currentState;
    public PlayerWalkState WalkState = new PlayerWalkState();
    public PlayerIdleState IdleState = new PlayerIdleState();
    public PlayerAttackState AttackState = new PlayerAttackState();
    public PlayerCrouchState CrouchState = new PlayerCrouchState();
    public PlayerRunState RunState = new PlayerRunState();

    public Rigidbody2D myRigidBody;
    public Animator myAnimator;
    public GameObject enemy;
    public CircleCollider2D myCircleCollider;

    public EnemyStateManager enemyStateManager;

    public bool direction = true; // true = right, false = left
    public float walkSpeed = 5f;
    public float runSpeed = 10f;
    public float crouchSpeed = 2f;  
    public float currentSpeed;
    public float attackCooldown = 1f;
    public float damage = 1f;
    public float health;
    public float maxHealth = 10f;
    public float attackTimer = 0f;

    public void Start() {   
        myRigidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myCircleCollider = GetComponent<CircleCollider2D>();
        enemyStateManager = enemy.GetComponent<EnemyStateManager>();

        myCircleCollider.radius = 0.6f;

        health = maxHealth;

        currentState = IdleState;
        currentState.EnterState(this);
    }

    void OnTriggerEnter2D(Collider2D collider) {
        currentState.OnTriggerEnter2D(this, collider);
    }

    public void FixedUpdate() {
        SetSpeed();
        SetDirection();
        Set2DColliderOffset();
        currentState.UpdateState(this);
    }

    public void SwitchState(PlayerBaseState newState) {
        currentState.ExitState(this);
        currentState = newState;
        currentState.EnterState(this);
    }

    public void SetDirection() {
        if (direction) {
            myAnimator.SetFloat("Direction", 1);
        } else {
            myAnimator.SetFloat("Direction", -1);
        }
    }

    public void SetSpeed() {
        myAnimator.SetFloat("Speed", currentSpeed);
        myAnimator.SetFloat("Horizontal", currentSpeed);
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
        myCircleCollider.offset = new Vector2(offset, myCircleCollider.offset.y);
    }
}