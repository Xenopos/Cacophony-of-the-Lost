using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Player;

namespace Powerups {
    public class Powerup : MonoBehaviour {
        public void OnButtonClick(Button button) {
            Debug.Log("Button clicked " + button.name);
            
            if (button.name == "Suzune") {
                PlayerStateManager.damage += 5f;
                Debug.Log("Suzune powerup: " + PlayerStateManager.damage + " damage");
            } else {
                PlayerStateManager.health += 2f;
                PlayerStateManager.maxHealth += 1f;
                PlayerStateManager.damage += 2f;

                Debug.Log("Kayla powerup: " + PlayerStateManager.health + " health");
                Debug.Log("Kayla powerup: " + PlayerStateManager.damage + " damage");
            }
        }
    }
}