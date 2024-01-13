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
    public int score;

    // Call first that Start and necesary to implement the Singleton
    void Awake() {
        obj = this;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        // Define the game is not paused
        isGamePaused = false;
        // At the start show the menu
        UIManager.obj.StartGame();
        // Set score with current points
        this.score = PlayerPrefs.GetInt("score");
    }

    // Method to save the score that player haves while is playing
    public void AddScore(int score) {
        this.score += score;
    }

    // Method to save the score into playerprefs
    public void SaveScore() {
        PlayerPrefs.SetInt("score", this.score);
    }

    // Method to get the score saved
    public int GetScore() {
        return PlayerPrefs.GetInt("score");
    }

    // Method to reset the score
    public void SetScore(int score) {
        this.score = score;
    }

    // Method when the game is over
    public void GameOver() {
        // Reset the current scene
        SceneManager.LoadScene(0);
        // Reset the score player prefs
        PlayerPrefs.SetInt("score", 0);
    }

    // Call to destroy the singleton object and when the game ends
    void OnDestroy() {
        obj = null;
    }
}
