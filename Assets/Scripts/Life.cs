using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Life : MonoBehaviour
{
    // Attributes
    public int scoreGive = 30;

    // Detect when the player collision with
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Player")) {
            // Give score
            Game.obj.AddScore(scoreGive);
            // Give life
            Player.obj.AddLive();
            // Show effect
            FXManager.obj.ShowPop(transform.position);
            // Non active object
            gameObject.SetActive(false);
        }
    }
}
