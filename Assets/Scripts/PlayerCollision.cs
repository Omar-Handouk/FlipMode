using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    private PlayerBehaviour player;

    private void Awake() {
        this.player = GameObject.FindWithTag("Player").GetComponent<PlayerBehaviour>();
    }
    private void OnTriggerEnter(Collider other) {
        string colliderTag = other.tag;

        other.gameObject.SetActive(false);

        if (colliderTag == "Collectable") {
            string collectableColor = other.gameObject.GetComponent<Collectable>().GetColorName();
            string playerColor = this.player.GetPlayerColor().name;
            
            if (collectableColor == playerColor) {
                if (!GameManager.isFlipped) { // Normal Mode
                    player.SetScore();
                    AudioManager.Instance.Play("Collectable");
                } else {
                    player.SetScore(-5);
                    AudioManager.Instance.Play("Wrong");
                }
            } else {
                if (!GameManager.isFlipped) {
                    player.SetScore(-5);
                    AudioManager.Instance.Play("Wrong");
                } else {
                    player.SetScore();
                    AudioManager.Instance.Play("Collectable");
                }
            }
        } else if (colliderTag == "HP") {
            player.SetHealth();
            AudioManager.Instance.Play("PowerUp");
        } else {
            player.SetHealth(-1);
            AudioManager.Instance.Play("Hit");
        }
    }
}
