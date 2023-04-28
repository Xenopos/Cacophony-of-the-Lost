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
    public bool isRunning;
    public bool isCrouching; // new variable for storing whether the player is crouching

    void Start()
    {
        movementspeed = defaultSpeed;
    }

    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            movementspeed = boostedSpeed;
            isRunning = true;
        }
        else
        {
            movementspeed = defaultSpeed;
            isRunning = false;
        }

        if (Input.GetKey(KeyCode.LeftControl))
        {
            isCrouching = true;
            gameObject.layer = LayerMask.NameToLayer("PlayerCrouch");
            movementspeed = defaultSpeed / 2f; // Set movement speed to half of default speed when crouching
        }
        else
        {
            isCrouching = false;
            gameObject.layer = LayerMask.NameToLayer("Default");

        }

        movement.x = Input.GetAxisRaw("Horizontal");
        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("speed", movement.sqrMagnitude);

        // Check if the player is running and trigger the running animation if true
        if (isRunning && !isJumping && !isCrouching)
        {
            animator.SetBool("isRunning", true);
        }
        else
        {
            animator.SetBool("isRunning", false);
        }

        // Check if the player is crouching and trigger the crouching animation if true
        if (isCrouching)
        {
            animator.SetTrigger("isCrouching");
        }
        else
        {
            animator.ResetTrigger("isCrouching");
        }

        if (Input.GetAxisRaw("Horizontal") == 1 || Input.GetAxisRaw("Horizontal") == -1)
        {
            animator.SetFloat("LastInputX", Input.GetAxisRaw("Horizontal"));
        }
        if (Input.GetButtonDown("Jump"))
        {
            rb.AddForce(new Vector2(0f, jumpforce));
        }

        rb.MovePosition(rb.position + movement * movementspeed * Time.fixedDeltaTime);
    }

    
}
