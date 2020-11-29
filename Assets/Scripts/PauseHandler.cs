using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseHandler : MonoBehaviour
{
    public static bool gameIsPaused = false;
    public GameObject pauseMenu;
    
    public string mainGameScene;

    private Text muteText; 

    private void Start() {
        this.muteText = this.transform.GetChild(1).GetChild(4).GetChild(0).GetComponent<Text>();

        this.muteText.text = (GameManager.muteAll ? "UN-MUTE ALL" : "MUTE ALL");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !GameManager.isGameOver) {
            if (gameIsPaused) {
                this.Resume();
            } else {
                this.Pause();
            }
        }
    }

    public void Resume() {
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
        AudioManager.Instance.Stop("Calm");
        AudioManager.Instance.UnPause("Upbeat");
        gameIsPaused = false;
    }

    public void Pause() {
        Time.timeScale = 0f;
        pauseMenu.SetActive(true);
        AudioManager.Instance.Pause("Upbeat");
        AudioManager.Instance.Play("Calm");
        gameIsPaused = true;
    }

    public void LoadMenu() {
        Time.timeScale = 1f;
        GameManager.isGameOver = false;
        SceneManager.LoadScene("MainMenu");
    }

    public void RestartGame() {
        Time.timeScale = 1f;
        GameManager.isGameOver = false;
        SceneManager.LoadScene(mainGameScene);
    }

    public void ToggleMute() {
        GameManager.Instance.ToggleMute();
        this.muteText.text = (GameManager.muteAll ? "UN-MUTE ALL" : "MUTE ALL");

        if (!GameManager.muteAll) {
            AudioManager.Instance.Play("Calm");
            AudioManager.Instance.Play("Upbeat");
            AudioManager.Instance.Pause("Upbeat");
        }
    }

    public void QuitGame() {
        Application.Quit();
    }
}
