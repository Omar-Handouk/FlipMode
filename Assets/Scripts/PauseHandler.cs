using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseHandler : MonoBehaviour
{
    public static bool gameIsPaused = false;
    public GameObject pauseMenu;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
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
        gameIsPaused = false;
    }

    void Pause() {
        Time.timeScale = 0f;
        pauseMenu.SetActive(true);
        gameIsPaused = true;
    }

    public void LoadMenu() {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame() {
        Application.Quit();
    }
}
