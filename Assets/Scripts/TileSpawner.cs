using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSpawner : MonoBehaviour
{
    public GameObject emptyTile;
    public ObjectPool objectPool;

    private Transform player;
    private const float tileLength = 2.5f;
    private const float playerOffset = 2.5f;
    private float tileOffset = 1.25f;
    private int numberOfTiles;

    private void Start()
    {
        this.player = GameObject.FindWithTag("Player").transform;
        this.numberOfTiles = this.objectPool.numberOfTiles;

        for (int i = 0; i < 3; ++i) {
            this.SpawnTile(true);
        }

        for (int i = 0; i < this.numberOfTiles; ++i)
        {
            this.SpawnTile();
        }
    }

    
    private void Update()
    {
        this.numberOfTiles = this.objectPool.numberOfTiles;

        // Destroy starting platforms
        if (player.position.z > 3 * tileLength + playerOffset) { // 3 Starting
            GameObject[] startingPlatforms = GameObject.FindGameObjectsWithTag("StartPlatforms");

            foreach (GameObject item in startingPlatforms)
            {
                Destroy(item);
            }
        }

        if (player.position.z > tileOffset - this.numberOfTiles * tileLength + playerOffset) {
            this.objectPool.DeactivateTile();
            this.SpawnTile();
        }
    }

    private void SpawnTile(bool spawnEmpty = false) {
        GameObject tile = null;

        if (spawnEmpty) {
            tile = Instantiate(this.emptyTile);
            tile.gameObject.tag = "StartPlatforms";

            tile.transform.SetParent(this.transform);
            tile.transform.position = new Vector3(0f, this.objectPool.yPosition, tileOffset);
            tile.transform.rotation = Quaternion.Euler(this.objectPool.xRotation, 0f, 0f);
        } else {
            this.objectPool.ActivateTile(this.tileOffset);
        }

        this.tileOffset += tileLength;
    }
}
