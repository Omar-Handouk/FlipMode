using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    private Transform player;
    private bool cameraMode = true; // True-Third Persion
    private Vector3 thirdPerson;
    private Vector3 cameraRotation;
    private Vector3 sideView;
    private Vector3 sideRotation;
    void Start()
    {
        this.player = GameObject.FindWithTag("Player").transform;

        this.thirdPerson = new Vector3(0f, 1.5f, -2f);
        this.cameraRotation = new Vector3(20f, 0f, 0f);
        this.sideView = new Vector3(5f, 1.5f, 0f);
        this.sideRotation = new Vector3(0f, -90f, 0f);
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C) && !GameManager.isGameOver) {
            this.cameraMode = !this.cameraMode;
        }

        if (GameManager.isFlipped) {
            this.thirdPerson.y = -1.5f;
            this.cameraRotation.x = -20f;
        } else {
            this.thirdPerson.y = 1.5f;
            this.cameraRotation.x = 20f;
        }

        this.transform.position = (cameraMode ? player.position : new Vector3(0f, 0f, player.position.z)) + (cameraMode ? this.thirdPerson : this.sideView);
        this.transform.rotation = Quaternion.Euler(cameraMode ? this.cameraRotation: this.sideRotation);
        Camera.main.fieldOfView = (cameraMode ? 60f : 90f);
    }

    public void ChangeCameraMode() {
        this.cameraMode = !this.cameraMode;
    }
}
