using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    public Transform player; // Reference to the player's transform
    public float speed = 6f; // AI movement speed

    private Rigidbody2D rb;
    private BoxCollider2D boxCollider;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();

        // Enable isTrigger on the box collider
        boxCollider.isTrigger = true;
    }

    private void FixedUpdate()
    {
        // Move towards the player
        Vector2 direction = (player.position - transform.position).normalized;
        rb.MovePosition(rb.position + direction * speed * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Do something when the AI collides with something
        if (collision.gameObject.CompareTag("Player"))
        {
            // Player has been hit by the AI
            Debug.Log("Player has been hit!");
        }
    }
}
