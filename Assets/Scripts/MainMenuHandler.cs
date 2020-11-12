using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuHandler : MonoBehaviour
{
    [Header ("Panels")]
    public GameObject optionsPanel;
    public GameObject mainPanel;
    public GameObject helpPanel;
    public GameObject creditsPanel;
    public void StartGame() {
        SceneManager.LoadScene("Sandbox");
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

    public void QuitGame() {
        Application.Quit();
    }
}
