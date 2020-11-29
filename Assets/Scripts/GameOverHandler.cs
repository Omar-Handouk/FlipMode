using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverHandler : MonoBehaviour
{
    public GameObject gameOverPanel;
    public GameObject pauseMenuPanel;

    public string mainGameScene;

    private void Update() {
        if (GameManager.isGameOver) {
            this.ShowGameOverPanel();
        }
    }

    public void ShowGameOverPanel() {
        Time.timeScale = 0f;

        this.pauseMenuPanel.SetActive(false);
        this.gameOverPanel.SetActive(true);
    }

    public void RestartGame() {
        Time.timeScale = 1f;
        GameManager.isGameOver = false;
        GameManager.isFlipped = false;
        SceneManager.LoadScene(mainGameScene);
    }

    public void LoadMenu() {
        Time.timeScale = 1f;
        GameManager.isGameOver = false;
        GameManager.isFlipped = false;
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame() {
        Application.Quit();
    }
}
