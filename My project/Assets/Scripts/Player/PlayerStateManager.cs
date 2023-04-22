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

    public bool direction = true; // true = right, false = left
    public float walkSpeed = 5f;
    public float runSpeed = 10f;
    public float crouchSpeed = 2f;
    public float attackCooldown = 1f;
    public float damage = 1f;

    public void Start() {   
        myRigidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        // enemy = GameObject.Find("Enemy");

        currentState = IdleState;
        currentState.EnterState(this);
    }

    void OnTriggerEnter2D(Collider2D collider) {
        currentState.OnTriggerEnter2D(this, collider);
    }

    public void Update() {
        currentState.UpdateState(this);
    }

    public void SwitchState(PlayerBaseState newState) {
        currentState.ExitState(this);
        currentState = newState;
        currentState.EnterState(this);
    }
}