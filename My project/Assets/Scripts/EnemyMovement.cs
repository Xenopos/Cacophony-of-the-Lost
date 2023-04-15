using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private Animator animator; 
    public EnemyAttack enemyAttack;
    public MovementController playerMovement;
    private BoxCollider2D movementCollider;


    // Chase
    public GameObject player;
    public bool isChasing = false;
    private float distanceFromPlayer;
    public float chaseSpeed = 10.0f; 
    public float chaseRadius = 2.0f;
    public bool hasChased = false;

    // Patrol
    Rigidbody2D myRigidBody;
    private bool isMoving = true;
    private float initialPosition; 
    public bool isGoingLeft = true;
    private bool isPatrolling = true;
    public float patrolSpeed = 2.0f;
    public float patrolRadius = 5.0f;
    public float patrolWaitTime = 2.0f;

    // Attack

    void Start() {
        initialPosition = transform.position.x;
        animator = GetComponent<Animator>();
        myRigidBody = GetComponent<Rigidbody2D>();
    }

    void Update() {   
        if (enemyAttack.isAttacking) return;    
        // if (!isMoving) return;
        CheckState();
    }
    
    void CheckState() {
        // if (playerMovement.isCrouching) {
        //     movementCollider.enabled = false; 
        // } else {
        //     movementCollider.enabled = true;
        // }
        
        distanceFromPlayer = Vector2.Distance(transform.position, player.transform.position);
        if (distanceFromPlayer < chaseRadius && IsPlayerFacingEnemy() && distanceFromPlayer > enemyAttack.attackRadius) {
            Debug.Log("Chase");
            Debug.Log(distanceFromPlayer);
            Debug.Log(enemyAttack.attackRadius);
            Debug.Log(distanceFromPlayer > enemyAttack.attackRadius);
            OnChase();
        } else if ((distanceFromPlayer >= chaseRadius || !IsPlayerFacingEnemy()) && !hasChased) {
            OnPatrol();
        } else if (hasChased && !enemyAttack.isAttacking && IsPlayerFacingEnemy() && distanceFromPlayer > enemyAttack.attackRadius) {
            Debug.Log("Chase hasChased");
            OnChase();
        }
    }

    void OnChase() {
        if (!isMoving) {
            StopCoroutine(WaitAndTurnAround());

            if (!IsPlayerFacingEnemy()) {
                isGoingLeft = !isGoingLeft;
                transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
            }

            isMoving = true;
        }

        isPatrolling = false;
        isChasing = true;
        animator.SetBool("isChasing", true);
        hasChased = true;

        if (player.transform.position.x < transform.position.x) {
            animator.SetBool("isGoingLeft", true);
        } else {
            animator.SetBool("isGoingLeft", false);
        }

        transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, chaseSpeed * Time.deltaTime);
    }

    void OnPatrol() {
        if (!isMoving) return;

        isPatrolling = true;
        isChasing = false;
        animator.SetBool("isChasing", false);

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

    private bool IsPlayerFacingEnemy()
    {
        Vector2 directionToPlayer = (player.transform.position - transform.position).normalized;
        float dotProduct = Vector2.Dot(directionToPlayer, transform.localScale);
        return dotProduct < 0f;
    }

    public IEnumerator WaitAndTurnAround() {
        isMoving = false;
        yield return new WaitForSeconds(patrolWaitTime);

        if (isPatrolling) {
            Debug.Log("Turning around");
            isGoingLeft = !isGoingLeft;

            if (isGoingLeft) {
                animator.SetBool("isGoingLeft", true);
            } else {
                animator.SetBool("isGoingLeft", false);
            }

            transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
        }
        isMoving = true;
    }
}
