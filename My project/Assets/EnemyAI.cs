using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    private Animator animator; 

    // General
    private bool isMoving = true;
    private float initialPosition; 

    // Chase
    public GameObject player;
    private bool isChasing = false;
    private float distanceFromPlayer;
    public float chaseSpeed = 3.0f; 
    public float chaseRadius = 2.0f;

    // Patrol
    Rigidbody2D myRigidBody;
    private bool isGoingLeft = true;
    public float patrolSpeed = 2.0f;
    public float patrolRadius = 5.0f;
    public float waitTime = 2.0f;

    void Start() {
        initialPosition = transform.position.x;
        animator = GetComponent<Animator>();
        myRigidBody = GetComponent<Rigidbody2D>();
    }

    void Update() {        
        if (!isMoving) return;
        
        if (!isChasing) {
            Patrol();
        } else if (isChasing) {
            Chase();
        }

        CheckChasing();
    }

    void Patrol() {
        float distanceFromStart = transform.position.x - initialPosition;
        if (isGoingLeft) {
            if (distanceFromStart <= -patrolRadius) {
                StartCoroutine(WaitAndTurnAround());
            }
            myRigidBody.velocity = new Vector2(-patrolSpeed, 0f);
        }
        else {
            if (distanceFromStart >= patrolRadius) {
                StartCoroutine(WaitAndTurnAround());
            }
            myRigidBody.velocity = new Vector2(patrolSpeed, 0f);
        }
    }
    
    void CheckChasing() {
        distanceFromPlayer = Vector2.Distance(transform.position, player.transform.position);
        if (distanceFromPlayer < chaseRadius && IsPlayerFacingEnemy()) {
            isChasing = true;
        } else {
            isChasing = false;
        }
    }

    private bool IsPlayerFacingEnemy()
    {
        Vector2 directionToPlayer = (player.transform.position - transform.position).normalized;
        float dotProduct = Vector2.Dot(directionToPlayer, transform.localScale);
        return dotProduct < 0f;
    }

    void Chase() {
        transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, chaseSpeed * Time.deltaTime);
        AfterChase();
    }

    void AfterChase() {
        initialPosition = transform.position.x;
    }

    IEnumerator WaitAndTurnAround() {
        isMoving = false;
        yield return new WaitForSeconds(waitTime);
        isGoingLeft = !isGoingLeft;

        if (isGoingLeft) {
            transform.localScale = new Vector3(1f, 1f, 1f);
        } else {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        
        isMoving = true;
    }
}
