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

    private int impulseDirection = 0; // 0-None, 1-Left, 2-Right, 3-Up
    private Vector3 movementVector;

    void Start()
    {
        this.rb = this.GetComponent<Rigidbody>();
        this.movementVector = this.rb.velocity;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate() {
        this.rb.velocity = this.movementVector;

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

    private void OnTriggerEnter(Collider other) {
        switch(other.transform.tag) {
            case "Spawner":
                RoomManager.ToggleSpawn();
                break;
            case "NWTrigger":
                other.transform.parent.gameObject.SetActive(false);
                break;
            case "SWTrigger":
                other.transform.parent.gameObject.SetActive(false);
                break;
        }
    }
}
