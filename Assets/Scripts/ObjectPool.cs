using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ObjectPool : MonoBehaviour
{
    [HideInInspector]
    public static ObjectPool Instance;
    public List<GameObject> pooledTiles;

    public GameObject[] tilesToPool;
    [Range(30, 100)]
    public int numberOfTiles = 30;

    private GameObject tileSpawner;
    private Queue<int> activatedTiles;

    private void Awake() {
        if (ObjectPool.Instance == null) {
            ObjectPool.Instance = this;
        } else {
            Destroy(this);
        }
    }

    private void Start() {
        this.pooledTiles = new List<GameObject>();
        this.activatedTiles = new Queue<int>();

        this.tileSpawner = GameObject.FindWithTag("TileManager");

        // TODO: Add an extra object for pick-ups
        // 12.5% 0-Tile, 12.5% 1-Tile, 50% 2-Tile, 5% 3-Tile, 20% Pick-ups
        GameObject tile = null;
        for (int i = 0; i < this.numberOfTiles; ++i) {
            double spawnPercentage = Math.Floor((double) this.numberOfTiles / (double) (i + 1));

            if (spawnPercentage <= 12.5f) { // 0-Tile
                tile = Instantiate(this.tilesToPool[0]) as GameObject;
            } else if (spawnPercentage <= 25f) { // 1-Tile
                if (spawnPercentage <= 16.5f) { // Var #1
                    tile = Instantiate(this.tilesToPool[1]) as GameObject;
                } else if (spawnPercentage <= 20.5) { // Var #2
                    tile = Instantiate(this.tilesToPool[2]) as GameObject;
                } else { // Var #3
                    tile = Instantiate(this.tilesToPool[3]) as GameObject;
                }
            } else if (spawnPercentage <= 75f) { // 2-Tile
                if (spawnPercentage <= 41f) { // Var #1
                    tile = Instantiate(this.tilesToPool[4]) as GameObject;
                } else if (spawnPercentage <= 57f) { // Var #2
                    tile = Instantiate(this.tilesToPool[5]) as GameObject;
                } else { // Var #3
                    tile = Instantiate(this.tilesToPool[6]) as GameObject;
                }
            } else if (spawnPercentage <= 80f) {
                tile = Instantiate(this.tilesToPool[7]) as GameObject;
            }

            if (tile != null) {
                tile.SetActive(false);
                this.pooledTiles.Add(tile);
            }
        }
    }

    public void ActivateTile(float tileOffset) {
        GameObject tile = null;
        int selectedIndex = -1;

        do
        {
            selectedIndex = Random.Range(0, this.pooledTiles.Count);
            tile = this.pooledTiles[selectedIndex];
        } while (!tile.activeInHierarchy);

        activatedTiles.Enqueue(selectedIndex);
        tile.SetActive(true);
        tile.transform.SetParent(this.tileSpawner.transform);
        tile.transform.position = Vector3.forward * tileOffset;
    }

    public void DeactivateTile() {
        if (activatedTiles.Count != 0) {
            int indexTileToDeactivate = activatedTiles.Dequeue();

            this.pooledTiles[indexTileToDeactivate].SetActive(false);
        }
    }
}
