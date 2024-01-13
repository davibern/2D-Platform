using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    // Make the class to Singleton
    public static Game obj;

    // Attributes about the game manager
    public int maxLives = 3;
    public bool isGamePaused;
    public int score = 0;

    // Call first that Start and necesary to implement the Singleton
    void Awake() {
        obj = this;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        // Define the game is not paused
        isGamePaused = false;
        // At the start show the men
        UIManager.obj.StartGame();
    }

    // Method to save the score that player haves while is playing
    public void AddScore(int score) {
        this.score += score;
    }

    // Method when the game is over
    public void GameOver() {
        // Reset the current scene
        SceneManager.LoadScene(0);
    }

    // Call to destroy the singleton object and when the game ends
    void OnDestroy() {
        obj = null;
    }
}
