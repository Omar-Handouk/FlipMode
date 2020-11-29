using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardHandler : MonoBehaviour
{
    private bool freeMode = false; // Keyboard Mode: false-Endless Runner Mode, true-Free Mode
    private CameraBehaviour cameraBehaviour;
    private PlayerBehaviour playerBehaviour;

    private Vector3 movementVector;
    private float horizontalAxis;
    private float verticalAxis;
    private int lastAccelerometerDirection = 0;

    public GameObject playerInformation;
    void Start()
    {
        this.cameraBehaviour = Camera.main.GetComponent<CameraBehaviour>();
        this.playerBehaviour = this.GetComponent<PlayerBehaviour>();

        this.movementVector = new Vector3(0f, 0f, 0f);
        this.horizontalAxis = 0f;
        this.verticalAxis = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.isGameOver) {
            return;
        }

        if (freeMode) {
            this.horizontalAxis = Input.GetAxis("Horizontal");
            this.verticalAxis = Input.GetAxis("Vertical");
            
            this.movementVector.x = this.horizontalAxis;
            this.movementVector.y = 0f;
            this.movementVector.z = this.verticalAxis;

        } else {
            this.verticalAxis = 1f;

            this.movementVector.x = this.horizontalAxis;
            this.movementVector.y = 0f;
            this.movementVector.z = this.verticalAxis;

            
        }

        this.playerBehaviour.setMovementVector(this.movementVector);
        
        Vector3 accelerometer = Input.acceleration;

        if (accelerometer.x >= -0.5f && accelerometer.x <= 0.5f && this.lastAccelerometerDirection != 0) {
            this.lastAccelerometerDirection = 0;
        } else if (accelerometer.x < -0.5f && this.lastAccelerometerDirection == 0) {
            this.lastAccelerometerDirection = 1;
            this.playerBehaviour.setImpulseDirection(1);
        } else if (accelerometer.x > 0.5f && this.lastAccelerometerDirection == 0) {
            this.lastAccelerometerDirection = 2;
            this.playerBehaviour.setImpulseDirection(2);
        }

        // TODO: Change Space behaviour to change platforms
        if (Input.GetKeyDown(KeyCode.O) && this.playerInformation != null) { // Debug Information
            this.playerInformation.SetActive(!playerInformation.activeSelf);
        } else if (Input.GetKeyDown(KeyCode.Space) && freeMode) {
                this.playerBehaviour.setImpulseDirection(3);
        } else if (Input.GetKeyDown(KeyCode.Space) && !freeMode) { // Flip Mode
                this.FlipMode();
        } else if (Input.GetKeyDown(KeyCode.I)) { // Change Keyboard Mode
            this.freeMode = !this.freeMode;
        } else if (Input.GetKeyDown(KeyCode.Z) && freeMode) {
            this.playerBehaviour.setImpulseDirection(1);
        } else if (Input.GetKeyDown(KeyCode.X) && freeMode) {
            this.playerBehaviour.setImpulseDirection(2);
        } else if (Input.GetKeyDown(KeyCode.A) && !freeMode) {
            this.playerBehaviour.setImpulseDirection(1);
        } else if (Input.GetKeyDown(KeyCode.D) && !freeMode) {
            this.playerBehaviour.setImpulseDirection(2);
        } else if (Input.GetKeyDown(KeyCode.R)) {
            this.playerBehaviour.ChangeColor();
        } else if (Input.GetKeyDown(KeyCode.E)) {
            this.playerBehaviour.SetHealth();
        } else if (Input.GetKeyDown(KeyCode.Q)) {
            this.playerBehaviour.SetScore();
        }
    }

    public void FlipMode() {
        GameManager.isFlipped = !GameManager.isFlipped;
        AudioManager.Instance.Play("Mode");
    }
}
