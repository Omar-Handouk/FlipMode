using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public static bool muteAll = false;
    public static bool isFlipped = false;
    public static bool isGameOver = false;
    
    private void Awake() {
        if (GameManager.Instance == null) {
            GameManager.Instance = this;
            DontDestroyOnLoad(this.gameObject);
        } else {
            Destroy(this);
        }
    }

    public void ToggleMute() {
        GameManager.muteAll = !GameManager.muteAll;

        if (GameManager.muteAll) {
            AudioManager.Instance.StopAll();
        } else {
            AudioManager.Instance.Play("Calm");
        }
    }
}
