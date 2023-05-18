using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;
using Enemy;

namespace Interactable {
    public class ClueObject : MonoBehaviour {
        private Collider2D collider;
        [SerializeField]
        private ContactFilter2D filter;
        private List<Collider2D> collidedObjects = new List<Collider2D>(1);
        private bool hasInteracted = false;
        public bool isFound = false;

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
                if (Input.GetKeyDown(KeyCode.E)) {
                    OnInteract();
                }
            }
        }

        public void OnInteract() {
            Debug.Log("Interacted with Player");

            SpriteRenderer playerSpriteRenderer = GameObject.Find("Player").GetComponent<SpriteRenderer>();
            PlayerStateManager playerStateManager = GameObject.Find("Player").GetComponent<PlayerStateManager>(); 

            // Change player sorting layer to be behind object
            if (!hasInteracted ) {
                Debug.Log("Clue found!");
                hasInteracted = true;
                isFound = true;
                playerSpriteRenderer.sortingOrder = 0;
            } else {
                hasInteracted = false;
                playerSpriteRenderer.sortingOrder = 10;
            }
        }
    }
}