using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Player;
using Scene;
using KaylaAI;

public class KaylaStateManager : MonoBehaviour {
    // States 
    public KaylaBaseState currentState;
    public KaylaWalkState WalkState = new KaylaWalkState();
    public KaylaIdleState IdleState = new KaylaIdleState();
    public KaylaCrouchState CrouchState = new KaylaCrouchState();
    public KaylaRunState RunState = new KaylaRunState();

    // Player components
    public Rigidbody2D rigidBody;
    public Animator animator;

    // Suzune components
    public PlayerStateManager suzuneStateManager;

    // Player variables
    public bool direction; 
    public float currentSpeed;

    public void Start() {  
        // Initialize components
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        suzuneStateManager = GameObject.Find("Player").GetComponent<PlayerStateManager>();

        // Initialize variables
        currentSpeed = 0f;
        direction = true;

        // Initialize current state to IdleState
        currentState = IdleState;
        currentState.EnterState(this);
    }

    void OnTriggerEnter2D(Collider2D collider) {
        currentState.OnTriggerEnter2D(this, collider);
    }

    public void FixedUpdate() {
        SetAnimatorVariables();
        currentState.UpdateState(this);
    }

    public void SwitchState(KaylaBaseState newState) {
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
    }
}