using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGameManager : MonoBehaviour
{
    private void Start() {
        if (!GameManager.muteAll) {
            AudioManager.Instance.Stop("Calm");
            AudioManager.Instance.Play("Upbeat");
        }
    }
}
