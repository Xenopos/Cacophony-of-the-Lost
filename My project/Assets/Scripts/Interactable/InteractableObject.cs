using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;
using Enemy;

namespace Interactable {
    public class InteractableObject : MonoBehaviour {
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
            EnemyStateManager enemyStateManager = GameObject.Find("Zed!").GetComponent<EnemyStateManager>();

            // Change player sorting layer to be behind object
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
    }
}