using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OverheadDisplayHandler : MonoBehaviour
{
    private Text scoreText;
    private Text healthText;
    private PlayerBehaviour player;

    private void Awake() {
        this.scoreText = GameObject.FindWithTag("Score").GetComponent<Text>();
        this.healthText = GameObject.FindWithTag("Health").GetComponent<Text>();
        this.player = GameObject.FindWithTag("Player").GetComponent<PlayerBehaviour>();
    }

    private void Update() {
        this.scoreText.text = this.player.GetScore().ToString();
        this.healthText.text = this.player.GetHealth().ToString();
    }
}
