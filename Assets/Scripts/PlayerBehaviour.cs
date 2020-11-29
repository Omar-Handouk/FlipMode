using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    private Rigidbody rb;

    [Header ("Speed multipliers")]
    public float horizontalSpeed = 1f;
    public float verticalSpeed = 1f;

    [Header ("Impulse multipler")]
    public float sideImpulseMultiplier = 1f;
    public float jumpImpulseMultiplier = 1f;

    public ColorMode[] colorModes;
    private ColorMode playerColor;
    
    private int health = 3;
    private int score = 0;

    private int impulseDirection = 0; // 0-None, 1-Left, 2-Right, 3-Up
    private Vector3 movementVector;

    private float waitTime = 15.0f;
    private float timer = 0.0f;
    private bool firstColorChange = true;

    public float speedUpDivisor = 250f;
    private float lastScore = 0f;
    private float speedUp = 0f;

    void Start()
    {
        this.rb = this.GetComponent<Rigidbody>();
        this.movementVector = this.rb.velocity;

        this.ChangeColor();
    }

    // Update is called once per frame
    void Update()
    {
        this.timer += Time.deltaTime;
        if (this.timer > this.waitTime) {
            this.timer -= this.waitTime;
            this.ChangeColor();   
        }
    }

    void FixedUpdate() {
        // Speed up scoring
        if (this.score - this.lastScore >= 50) {
            this.speedUp += (float) this.score / this.speedUpDivisor;
            this.verticalSpeed += this.speedUp;
            this.lastScore = this.score;
        }

        this.rb.velocity = this.movementVector;
        //----------

         if (GameManager.isFlipped) {
        this.rb.velocity = new Vector3(this.rb.velocity.x, 50f, this.rb.velocity.z);
        } else {
            this.rb.velocity = new Vector3(this.rb.velocity.x, -50f, this.rb.velocity.z);
        }

        switch(this.impulseDirection) {
            case 1:
                this.impulseDirection = 0;
                rb.AddForce(Vector3.left * this.sideImpulseMultiplier, ForceMode.Impulse);
                break;
            case 2:
                rb.AddForce(Vector3.right * this.sideImpulseMultiplier, ForceMode.Impulse);
                this.impulseDirection = 0;
                break;
            case 3:
                rb.AddForce(Vector3.up * this.jumpImpulseMultiplier, ForceMode.Impulse);
                this.impulseDirection = 0;
                break;
            default:
                this.impulseDirection = 0;
                break;
        }
    }

    public void setMovementVector(Vector3 movementVector) {
        movementVector.x *= this.horizontalSpeed;
        movementVector.y = this.rb.velocity.y;
        movementVector.z *= this.verticalSpeed;

        this.movementVector = movementVector;
    }

    public Vector3 getMovementVector() {
        return this.movementVector;
    }

    public void setImpulseDirection(int impulseDirection) {
        this.impulseDirection = impulseDirection;
    }

    public int getImpulseDirection() {
        return this.impulseDirection;
    }

    public void ChangeColor() {
        this.playerColor = colorModes[Random.Range(0, colorModes.Length)];
        Material material = this.playerColor.material;
        this.gameObject.GetComponent<Renderer>().material = material;
        
        if (this.firstColorChange) {
            this.firstColorChange = false;
        } else {
            AudioManager.Instance.Play("Switch");
        }
    }

    public ColorMode GetPlayerColor() {
        return this.playerColor;
    }

    public void SetHealth(int value = 1) {
        if (this.health == 0 && value < 0 || this.health == 3 && value > 0) {
            return;
        }

        this.health += value;

        if (this.health == 0) {
            GameManager.isGameOver = true;
            AudioManager.Instance.StopAll();
            AudioManager.Instance.Play("GameOver");
        }
    }

    public int GetHealth() {
        return this.health;
    }

    public void SetScore(int value = 10) {
        if (score <= 0 && value < 0) {
            score = 0;
            return;
        }

        this.score += value;
    }

    public int GetScore() {
        return this.score;
    }
}
