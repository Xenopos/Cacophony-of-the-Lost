using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    public Animator animator;
    public float movementspeed = 5f;
    public float defaultSpeed = 5f;
    public float boostedSpeed = 10f;
    public float jumpforce = 10f;
    public Rigidbody2D rb;
    Vector2 movement;
    public bool isJumping;
    public bool isCrouching;
    public bool isRunning; // new variable for storing whether the player is running

    void Start()
    {
        movementspeed = defaultSpeed;
    }

    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            movementspeed = boostedSpeed;
            isRunning = true; // Set isRunning to true when shift key is held down
        }
        else
        {
            movementspeed = defaultSpeed;
            isRunning = false; // Set isRunning to false when shift key is released
        }

        movement.x = Input.GetAxisRaw("Horizontal");
        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("speed", movement.sqrMagnitude);

        // Check if the player is running and trigger the running animation if true
        if (isRunning && !isJumping)
        {
            animator.SetBool("isRunning", true);
        }
        else
        {
            animator.SetBool("isRunning", false);
        }

        if (Input.GetAxisRaw("Horizontal") == 1 || Input.GetAxisRaw("Horizontal") == -1)
        {
            animator.SetFloat("LastInputX", Input.GetAxisRaw("Horizontal"));
        }
        if (Input.GetButtonDown("Jump"))
        {
            rb.AddForce(new Vector2(0f, jumpforce));
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            isCrouching = !isCrouching;
            animator.SetBool("isCrouching", isCrouching);
        }

        // If the player is crouching, reduce their movement speed
        if (isCrouching)
        {
            defaultSpeed *= 0.5f;
        }

        rb.MovePosition(rb.position + movement * movementspeed * Time.fixedDeltaTime);
    }
}
