using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private Animator animator; 

    // Chase
    public GameObject player;
    public bool isChasing = false;
    private float distanceFromPlayer;
    public float chaseSpeed = 10.0f; 
    public float chaseRadius = 2.0f;

    // Patrol
    Rigidbody2D myRigidBody;
    private bool isMoving = true;
    private float initialPosition; 
    private bool isGoingLeft = true;
    private bool isPatrolling = true;
    public float patrolSpeed = 2.0f;
    public float patrolRadius = 5.0f;
    public float patrolWaitTime = 2.0f;

    void Start() {
        initialPosition = transform.position.x;
        animator = GetComponent<Animator>();
        myRigidBody = GetComponent<Rigidbody2D>();
    }

    void Update() {        
        CheckPlayerDistance();

        if (!isMoving) return;
        
        if (!isChasing) {
            Patrol();
        } else if (isChasing) {
            Chase();
        } 

    }
    
    /*
        @brief: Makes the enemy patrol back and forth about a radius

        @param: None

        @return: None
    */
    void Patrol() {
        float distanceFromStart = transform.position.x - initialPosition;
        if (isPatrolling) {
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
    }
    
    /*
        @brief: Checks if the player is within the chase radius and facing the enemy

        @param: None

        @return: None
    */
    void CheckPlayerDistance() {
        distanceFromPlayer = Vector2.Distance(transform.position, player.transform.position);
        if (distanceFromPlayer < chaseRadius && IsPlayerFacingEnemy()) {
            isChasing = true;

            StopCoroutine(WaitAndTurnAround());
            isMoving = true;
        } 
    }

    /*
        @brief: Checks if the player is facing the enemy

        @param: None

        @return: bool
    */
    private bool IsPlayerFacingEnemy()
    {
        Vector2 directionToPlayer = (player.transform.position - transform.position).normalized;
        float dotProduct = Vector2.Dot(directionToPlayer, transform.localScale);
        return dotProduct < 0f;
    }

    /*
        @brief: Makes the enemy chase the player nonstop

        @param: None

        @return: None
    */
    void Chase() {
        isPatrolling = false;
        transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, chaseSpeed * Time.deltaTime);
    }

    /*
        @brief: Makes the enemy wait for a certain amount of time and then turn around

        @param: None

        @return: None
    */
    IEnumerator WaitAndTurnAround() {
        isMoving = false;
        yield return new WaitForSeconds(patrolWaitTime);

        if (isPatrolling) {
            Debug.Log("Turning around");
            isGoingLeft = !isGoingLeft;
            transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
        }
        isMoving = true;
    }
}
