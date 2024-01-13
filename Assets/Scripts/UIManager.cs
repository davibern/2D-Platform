using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

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
        Game.obj.isGamePaused = true;
        uiPanel.gameObject.SetActive(true);
    }

    // Hide the start menu
    public void HideInitPanel() {
        AudioManager.obj.PlayGui();
        Game.obj.isGamePaused = false;
        uiPanel.gameObject.SetActive(false);
    }

    // Destroy the class (singleton only must have one)
    private void OnDestroy() {
        obj = null;
    }
}
