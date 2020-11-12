using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviourTesting : MonoBehaviour
{
    private Rigidbody rb;
    
    void Start()
    {
        this.rb = this.GetComponent<Rigidbody>();
    }
    void Update()
    {
        Camera.main.transform.position = this.transform.position + new Vector3(0f, 1f, -2f);
    }

    private void FixedUpdate() {
        rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, 5f);
    }
}
