using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ObjectPool : MonoBehaviour
{
    [HideInInspector]
    public List<GameObject> pooledTiles;
    public List<GameObject> activePooledTiles;

    public GameObject[] tilesToPool;
    [Range(10, 100)]
    public int numberOfTiles = 10;
    public GameObject tileSpawner;

    [Header ("Position")]
    public float yPosition = 0f;
    public float xRotation = 0f;

    private void Start() {
        this.pooledTiles = new List<GameObject>();

        // 12.5% 0-Tile, 12.5% 1-Tile, 50% 2-Tile, 5% 3-Tile, 20% Pick-ups
        for (int i = 0; i < this.numberOfTiles; ++i) {
            GameObject tile = null;
            double spawnPercentage = Math.Floor(((double) (i + 1) / (double) this.numberOfTiles) * 100f);

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
            } else {
                tile = Instantiate(this.tilesToPool[8]) as GameObject;
            }

            if (tile != null) {
                tile.transform.SetParent(this.tileSpawner.transform);
                this.pooledTiles.Add(tile);
                tile.SetActive(false);
            }
        }

        // Flooring causes the actual count to be less
        this.numberOfTiles = this.pooledTiles.Count;
    }

    public void ActivateTile(float tileOffset) {
        if (this.numberOfTiles <= this.activePooledTiles.Count) {
            return;
        }

        GameObject tile = null;
        int selectedIndex = -1;

        do
        {
            selectedIndex = Random.Range(0, this.pooledTiles.Count);
            tile = this.pooledTiles[selectedIndex];
        } while (tile.activeInHierarchy);

        this.activePooledTiles.Add(tile);
        this.pooledTiles.RemoveAt(selectedIndex);
        tile.SetActive(true);
        tile.transform.SetParent(this.tileSpawner.transform);
        tile.transform.position = new Vector3(0f, this.yPosition, tileOffset);
        tile.transform.rotation = Quaternion.Euler(this.xRotation, 0f, 0f);
    }

    public void DeactivateTile() {
        if (this.activePooledTiles.Count != 0) {
            GameObject tile = this.activePooledTiles[0];
            tile.SetActive(false);
            this.activePooledTiles.RemoveAt(0);

            this.pooledTiles.Add(tile);
        }
    }
}
