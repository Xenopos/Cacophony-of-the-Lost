using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Player;
using UnityEngine.SceneManagement;

namespace Powerups {
    public class Powerup : MonoBehaviour {
        public void OnButtonClick(Button button) {
            Debug.Log("Button clicked " + button.name);
            
            if (button.name == "Suzune") {
                PlayerStateManager.damage += 2f;
                Debug.Log("Suzune powerup: " + PlayerStateManager.damage + " damage");
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            } else {
                PlayerStateManager.health += 2f;
                PlayerStateManager.maxHealth += 1f;

                Debug.Log("Kayla powerup: " + PlayerStateManager.health + " health");
                            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }
    }
}