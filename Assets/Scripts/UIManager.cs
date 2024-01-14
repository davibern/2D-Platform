using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    // Singleton class
    public static UIManager obj;

    // Attributes
    public TextMeshProUGUI livesLbl;
    public TextMeshProUGUI scoreLbl;
    public Transform uiPanel;

    // Instantiate the singleton class
    private void Awake() {
        obj = this;
        // Init the scene with the current score
        scoreLbl.text = PlayerPrefs.GetInt("score").ToString();
    }

    // Update the lives text ui
    public void UpdateLives() {
        livesLbl.text = "" + Player.obj.lives;
    }

    // Update the score text ui
    public void UpdateScore() {
        scoreLbl.text = "" + Game.obj.score;
    }

    // Show the start menu
    public void StartGame() {
        AudioManager.obj.PlayGui();
        Game.obj.isGamePaused = false;
        uiPanel.gameObject.SetActive(false);
    }

    // Hide the start menu
    public void HideInitPanel() {
        AudioManager.obj.PlayGui();
        Game.obj.isGamePaused = false;
        uiPanel.gameObject.SetActive(false);
    }

    public void LoadGame() {
        // Reset score when player clic new game
        PlayerPrefs.SetInt("score", 0);
        // Load first level
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    // Destroy the class (singleton only must have one)
    private void OnDestroy() {
        obj = null;
    }
}
