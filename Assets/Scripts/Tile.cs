using UnityEngine;

public class Tile : MonoBehaviour
{
    public GameObject obstaclePrefab;

    void Start()
    {
        SpawnObstacle();
    }

    void SpawnObstacle()
    {
        int lane = Random.Range(0, 3);

        float laneDistance = 2f;
        float xPos = (lane - 1) * laneDistance;

        Vector3 spawnPosition = new Vector3(xPos, 1, transform.position.z + Random.Range(-8f, 8f));

        GameObject obs = Instantiate(obstaclePrefab, spawnPosition, Quaternion.identity);
        obs.transform.SetParent(transform, true);
    }
}