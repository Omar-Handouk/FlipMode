using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInformation : MonoBehaviour
{
    private GameObject[] coords;
    private GameObject[] vels;

    private Transform playerPosition;
    private Rigidbody playerBody;

    void Start()
    {
        coords = GameObject.FindGameObjectsWithTag("Coords");
        vels = GameObject.FindGameObjectsWithTag("Vels");
        
        GameObject player = GameObject.FindWithTag("Player");
        playerPosition = player.GetComponent<Transform>();
        playerBody = player.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        foreach (GameObject o in coords) {
            switch (o.name) {
                case "X-Coord":
                    o.GetComponent<Text>().text = playerPosition.position.x.ToString();
                    break;
                case "Y-Coord":
                    o.GetComponent<Text>().text = playerPosition.position.y.ToString();
                    break;
                default:
                    o.GetComponent<Text>().text = playerPosition.position.z.ToString();
                    break;
            }
        }

        foreach (GameObject o in vels)
        {
            switch (o.name)
            {
                case "vX":
                    o.GetComponent<Text>().text = playerBody.velocity.x.ToString();
                    break;
                case "vY":
                    o.GetComponent<Text>().text = playerBody.velocity.y.ToString();
                    break;
                default:
                    o.GetComponent<Text>().text = playerBody.velocity.z.ToString();
                    break;
            }
        }
    }
}
