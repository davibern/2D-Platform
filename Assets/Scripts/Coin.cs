using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    // Attributes
    public int scoreGive = 100;

    // Detect when the player collision with
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Player")) {
            // Give score
            Game.obj.AddScore(scoreGive);
            // Show effect
            FXManager.obj.ShowPop(transform.position);
            // Play audio clip
            AudioManager.obj.PlayCoin();
            // Non active object
            gameObject.SetActive(false);
        }
    }
}
