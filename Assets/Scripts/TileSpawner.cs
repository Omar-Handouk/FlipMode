using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSpawner : MonoBehaviour
{
    [HideInInspector]
    public static TileSpawner Instance;

    public GameObject emptyTile;

    private Transform player;
    private const float tileLength = 2.5f;
    private const float playerOffset = 2.5f;
    private float tileOffset = 1.25f;
    private int numberOfTiles;
    private ObjectPool objectPool;

    private void Awake() {
        if (TileSpawner.Instance == null) {
            TileSpawner.Instance = this;
        } else {
            Destroy(this);
        }
    }

    private void Start()
    {
        // Destroy Debug Platform
        // TODO: Remove Debug Platform
        Destroy(GameObject.FindWithTag("Debug Platform"));
        
        this.player = GameObject.FindWithTag("Player").transform;
        this.objectPool = GameObject.FindWithTag("PoolManager").GetComponent<ObjectPool>();
        this.numberOfTiles = this.objectPool.numberOfTiles;

        for (int i = 0; i < this.numberOfTiles; ++i) {
            if (i < 3) {
                this.SpawnTile(true);
            } else {
                this.SpawnTile();
            }
        }
    }

    
    private void Update()
    {
        this.numberOfTiles = this.objectPool.numberOfTiles;

        if (player.position.z > 6.25f + playerOffset){ // Remove starting platforms
            GameObject[] startPlatforms = GameObject.FindGameObjectsWithTag("StartPlatforms");

            foreach (GameObject item in startPlatforms)
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
            tile.transform.position = Vector3.forward * tileOffset;
        } else {
            this.objectPool.ActivateTile(this.tileOffset);
        }

        this.tileOffset += tileLength;
    }
}
