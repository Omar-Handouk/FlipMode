using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    public GameObject[] roomPrefabs;

    private Transform playerPosition;
    private float roomOffset = 20f;
    private float playerOffset = 10f;
    private float roomLength = 20f;
    private static bool shouldSpawn = false;


    private void Start() {
        this.playerPosition = GameObject.FindWithTag("Player").transform;
    }

    private void Update() {
        if (shouldSpawn) {
            shouldSpawn = false;

            this.SpawnRoom();
        }

        if (playerPosition.position.z > roomOffset - roomLength + playerOffset) {
            this.SpawnRoom();
        }
    }

    public static void ToggleSpawn() {
        shouldSpawn = true;
    }

    private void SpawnRoom() {
        GameObject room;
        room = Instantiate(roomPrefabs[0]) as GameObject;
        room.transform.SetParent(this.transform);
        room.transform.position = Vector3.forward * roomOffset;

        roomOffset += roomLength;
    }
}
