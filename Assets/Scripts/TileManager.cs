using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    public GameObject tilePrefab;
    public int tilesOnScreen = 5;
    public float tileLength = 20f;
    public Transform player;

    private float spawnZ = 0;
    private List<GameObject> activeTiles = new List<GameObject>();

    void Start()
    {
        for (int i = 0; i < tilesOnScreen; i++)
        {
            SpawnTile();
        }
    }

    void Update()
    {
        if (activeTiles.Count > 0)
        {
            GameObject lastTile = activeTiles[activeTiles.Count - 1];

            if (lastTile.transform.position.z < (tilesOnScreen * tileLength))
            {
                SpawnTile();
            }
        }

        DeleteTile();
    }

    void SpawnTile()
    {
        Vector3 spawnPosition;

        if (activeTiles.Count == 0)
        {
            spawnPosition = Vector3.forward * spawnZ;
        }
        else
        {
            GameObject lastTile = activeTiles[activeTiles.Count - 1];
            spawnPosition = lastTile.transform.position + Vector3.forward * tileLength;
        }

        GameObject tile = Instantiate(tilePrefab, spawnPosition, Quaternion.identity);

        Tile tileScript = tile.GetComponent<Tile>();

        if (tileScript != null)
        {
            
            if (activeTiles.Count >= 3)
            {
                tileScript.SpawnObstacle();
            }
        }

        activeTiles.Add(tile);
    }

    void DeleteTile()
    {
        if (activeTiles.Count > 0 && activeTiles[0].transform.position.z < -30)
        {
            Destroy(activeTiles[0]);
            activeTiles.RemoveAt(0);
        }
    }
}