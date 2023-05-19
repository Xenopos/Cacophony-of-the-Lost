using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Player;
using Enemy;
using Scene; 
using System;

namespace Interactable {
    public class DoorTeleportObject : MonoBehaviour {
        private Collider2D collider;
        [SerializeField]
        private ContactFilter2D filter;
        private List<Collider2D> collidedObjects = new List<Collider2D>(1);
        public Transform destination1; // First teleportation destination
        public Transform destination2; // Second teleportation destination
        public bool isAtDestination1 = true;

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

            PlayerStateManager playerStateManager = GameObject.Find("Player").GetComponent<PlayerStateManager>(); 
            playerStateManager.SwitchState(playerStateManager.IdleState);

            if (isAtDestination1) {
                GameObject.Find("Player").transform.position = destination2.position;
                isAtDestination1 = false;
            } else {
                GameObject.Find("Player").transform.position = destination1.position;
                isAtDestination1 = true;
            }
        }
    }
}