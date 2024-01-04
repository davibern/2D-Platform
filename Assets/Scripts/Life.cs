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
            Game.obj.AddScore(scoreGive);
            
            Player.obj.AddLive();

            gameObject.SetActive(false);
        }
    }
}
