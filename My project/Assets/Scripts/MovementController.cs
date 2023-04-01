using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    public Animator animator;
    public float movementspeed = 5f;
    public float jumpforce = 10f;
    public Rigidbody2D rb; 
    Vector2 movement;
    public bool isJumping;

    void Start() 
    {
        
    }

    void FixedUpdate()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("speed", movement.sqrMagnitude);
 
        //LastInputX lets shi to face on the last direction on idle

        if(Input.GetAxisRaw("Horizontal") == 1 || Input.GetAxisRaw("Horizontal") ==  -1 )
        {
            animator.SetFloat("LastInputX", Input.GetAxisRaw("Horizontal"));
        }
        if(Input.GetButtonDown("Jump"))
        {
        rb.AddForce(new Vector2(0f, jumpforce));
        }
 
rb.MovePosition(rb.position + movement * movementspeed * Time.fixedDeltaTime);
    }


}
