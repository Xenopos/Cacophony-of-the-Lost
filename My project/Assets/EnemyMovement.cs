using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public float mvmntspeed;// the mvmntspeed at which the AI moves
    public float distance; // the distance the AI moves in either direction
    public float followRadius; // the radius within which the AI will follow the player
    public float attackRange; // the range at which the AI will attack the player
    public float attackDelay; // the delay between attacks in seconds
    public Transform player; // a reference to the player's Transform component
    public float followmvmntspeed;
    
    private float startPos; // the starting position of the AI
    private bool movingRight = true; // whether the AI is currently moving right or left
    private bool isFollowingPlayer = false; // whether the AI is currently following the player
    private float lastAttackTime = Mathf.NegativeInfinity; // the time of the last attack, set to negative infinity initially
    
    void Start()
    {
        startPos = transform.position.x; // set the starting position to the current position
    }

    void Update()
    {
        // check if the AI has reached the end of its movement range
        if (transform.position.x > startPos + distance)
        {
            movingRight = false;
        }
        else if (transform.position.x < startPos - distance)
        {
            movingRight = true;
        }

        // check if the player is within the follow radius
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        if (distanceToPlayer < followRadius)
        {
            isFollowingPlayer = true;
            mvmntspeed = followmvmntspeed; // increase mvmntspeed if following player
        }
        else
        {
            isFollowingPlayer = false;
            mvmntspeed = 3f; // reset mvmntspeed if not following player
        }

        // check if the player is within the attack range and if enough time has passed since the last attack
        if (distanceToPlayer < attackRange && Time.time - lastAttackTime > attackDelay)
        {
            // attack the player and reset the last attack time
            Debug.Log("AI attacked player!");
            lastAttackTime = Time.time;
        }

        // move the AI in the correct direction
        if (isFollowingPlayer)
        {
            // move towards the player
            Vector2 directionToPlayer = (player.position - transform.position).normalized;
            transform.position = new Vector2(transform.position.x + directionToPlayer.x * mvmntspeed * Time.deltaTime, transform.position.y);
        }
        else if (movingRight)
        {
            // move to the right
            transform.position = new Vector2(transform.position.x + mvmntspeed * Time.deltaTime, transform.position.y);
        }
        else
        {
            // move to the left
            transform.position = new Vector2(transform.position.x - mvmntspeed * Time.deltaTime, transform.position.y);
        }
    }
}
