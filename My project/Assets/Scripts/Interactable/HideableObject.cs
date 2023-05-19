using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;
using Enemy;

namespace Interactable {
    public class HideableObject : MonoBehaviour {
        private Collider2D collider;
        [SerializeField]
        private ContactFilter2D filter;
        private List<Collider2D> collidedObjects = new List<Collider2D>(1);
        private bool hasInteracted = false;

        public void Start() {
            collider = GetComponent<Collider2D>();
        }

        public void Update() {
            collider.OverlapCollider(filter, collidedObjects);
            foreach(var o in collidedObjects) {
                OnCollided(o);
            }
        }

        public void OnCollided(Collider2D collidedObject) {
            Debug.Log("Collided with " + collidedObject.name);
            Physics2D.IgnoreCollision(collider, collidedObject);

            if (collidedObject.name == "Player") {
                if (Input.GetKeyDown(KeyCode.H)) {
                    OnInteract();
                }
            }
        }

        public void OnInteract() {
            Debug.Log("Interacted with Player");

            SpriteRenderer playerSpriteRenderer = GameObject.Find("Player").GetComponent<SpriteRenderer>();
            PlayerStateManager playerStateManager = GameObject.Find("Player").GetComponent<PlayerStateManager>(); 
            
            // Get nearest enemy
            GameObject nearestEnemy = FindNearestEnemy(this.gameObject);
            EnemyStateManager enemyStateManager = nearestEnemy.GetComponent<EnemyStateManager>();


            if (!hasInteracted && enemyStateManager.hasChased == false) {
                hasInteracted = true;
                playerSpriteRenderer.sortingOrder = 0;
                playerStateManager.SwitchState(playerStateManager.HideState);
            } else {
                hasInteracted = false;
                playerSpriteRenderer.sortingOrder = 10;
                playerStateManager.animator.SetBool("isCrouching", false);
                playerStateManager.SwitchState(playerStateManager.IdleState);
            }
        }

        public GameObject FindNearestEnemy(GameObject targetObject) {
            GameObject nearestEnemy = null;
            float shortestDistance = Mathf.Infinity;
            float searchRadius = 10f;

            // Find all enemy objects or game entities
            GameObject[] enemyObjects = GameObject.FindGameObjectsWithTag("Enemy");

            // Iterate through the enemy objects
            foreach (GameObject enemyObject in enemyObjects)
            {
                // Calculate the distance between the target object and the current enemy object
                float distance = Vector3.Distance(targetObject.transform.position, enemyObject.transform.position);

                // Check if the distance is within the search radius and shorter than the current shortest distance
                if (distance <= searchRadius && distance < shortestDistance)
                {
                    shortestDistance = distance;
                    nearestEnemy = enemyObject;
                }
            }

            return nearestEnemy;
        }
    }

    
}