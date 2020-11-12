using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    private Transform player;

    private bool cameraMode = true;
    private bool switchMode = false;
    private Vector3 thirdPerson;
    private Vector3 cameraRotation;

    void Start()
    {
        this.player = GameObject.FindWithTag("Player").transform;

        this.thirdPerson = new Vector3(0f, 1.5f, -2f);
        this.cameraRotation = new Vector3(20f, 0f, 0f);
    }

    // Update is called once per frame
    private void Update()
    {
        if (switchMode) {
            this.switchMode = false;
            this.cameraMode = !this.cameraMode;

            this.cameraRotation.x = this.cameraMode ? 20f : 0f;
        }
        
        this.transform.position = player.position + (cameraMode ? this.thirdPerson : Vector3.zero);
        this.transform.rotation = Quaternion.Euler(this.cameraRotation);
    }

    public void setSwitchMode() {
        switchMode = true;
    }
}
