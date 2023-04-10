using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float speed = 2.0f; // Movement speed
    public float distance = 5.0f; // Distance to move before turning around
    public float waitTime = 2.0f; // Time to wait before turning around

    private float initialPosition; // Starting position
    private float moveX; // Current movement direction
    private bool isWaiting = false; // Flag to indicate if the enemy is waiting to turn around

    private Animator animator; // Animator component

    // Start is called before the first frame update
    void Start()
    {
        initialPosition = transform.position.x;
        moveX = 1.0f; // Start moving towards the maximum distance

        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // Move the enemy horizontally
        transform.Translate(new Vector3(moveX * speed * Time.deltaTime, 0.0f, 0.0f));

        // Check if the enemy has reached the maximum distance or returned to the initial position
        if (Mathf.Abs(transform.position.x - initialPosition) >= distance && moveX > 0.0f)
        {
            // Stop moving and wait for a few seconds
            if (!isWaiting)
            {
                moveX = 0.0f;
                isWaiting = true;
                StartCoroutine(WaitAndTurnAround());
            }
        }
        else if (Mathf.Abs(transform.position.x - initialPosition) < 0.1f && moveX < 0.0f)
        {
            // Stop moving and wait for a few seconds
            if (!isWaiting)
            {
                moveX = 0.0f;
                isWaiting = true;
                StartCoroutine(WaitAndTurnAround());
            }
        }

        // Set the animation parameters
        animator.SetFloat("MoveX", moveX);

        if (moveX == 1 || moveX == -1)
        {
        animator.SetFloat("LastMoveX", moveX);
        }


        // Update the movement direction
        if (!isWaiting)
        {
            if (Mathf.Abs(transform.position.x - initialPosition) >= distance)
            {
                moveX = -1.0f;
            }
            else if (Mathf.Abs(transform.position.x - initialPosition) < 0.1f)
            {
                moveX = 1.0f;
            }
        }
    }

    // Coroutine to wait for a few seconds and turn around
    IEnumerator WaitAndTurnAround()
    {
        yield return new WaitForSeconds(waitTime);
        moveX = -moveX;
        isWaiting = false;
    }
}
