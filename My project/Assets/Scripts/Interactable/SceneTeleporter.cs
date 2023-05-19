using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Player;
using Enemy;

namespace Interactable {
    public class SceneTeleporter : MonoBehaviour {
        private Collider2D collider;
        [SerializeField]
        private ContactFilter2D filter;
        private List<Collider2D> collidedObjects = new List<Collider2D>(1);

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

            // One clue
            ClueObject clue = GameObject.Find("Clue").GetComponent<ClueObject>();

            if (collidedObject.name == "Player") {
                if (Input.GetKeyDown(KeyCode.E) && clue.isFound) {
                    OnInteract();
                }
            }
        }

        public void OnInteract() {
            Debug.Log("Interacted with Player");

            PlayerStateManager playerStateManager = GameObject.Find("Player").GetComponent<PlayerStateManager>(); 
            playerStateManager.SwitchState(playerStateManager.IdleState);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}