using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuHandler : MonoBehaviour
{
    [Header ("Panels")]
    public GameObject optionsPanel;
    public GameObject mainPanel;
    public GameObject helpPanel;
    public GameObject creditsPanel;

    public string mainGameScene;
    private Text muteText;
    private void Start() {
        this.muteText = optionsPanel.transform.GetChild(1).GetChild(0).GetComponent<Text>();
        this.muteText.text = (GameManager.muteAll ? "Un-mute all" : "Mute all");

        if (!GameManager.muteAll) {
            AudioManager.Instance.Stop("Upbeat");
            AudioManager.Instance.Play("Calm");
        }
    }

    public void StartGame() {
        SceneManager.LoadScene(mainGameScene);
    }

    public void showMainPanel() {
        mainPanel.SetActive(true);
        optionsPanel.SetActive(false);
    }

    public void ShowOptionsPanel(string path) {
        switch (path) {
            case "Main":
                mainPanel.SetActive(false);
                break;
            case "Help":
                helpPanel.SetActive(false);
                break;
            default:
                creditsPanel.SetActive(false);
                break;
        }

        optionsPanel.SetActive(true);
    }

    public void ShowHelpPanel() {
        optionsPanel.SetActive(false);
        helpPanel.SetActive(true);
    }

    public void ShowCreditsPanel() {
        optionsPanel.SetActive(false);
        creditsPanel.SetActive(true);
    }

    public void ToggleMute() {
        GameManager.Instance.ToggleMute();
        this.muteText.text = (GameManager.muteAll ? "Un-mute all" : "Mute all");
    }

    public void QuitGame() {
        Application.Quit();
    }
}
