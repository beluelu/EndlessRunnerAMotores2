using UnityEngine;

public class Tile : MonoBehaviour
{
    public GameObject obstaclePrefab;

    public float moveSpeed = 10f;

    void Update()
    {
        transform.Translate(Vector3.back * moveSpeed * Time.deltaTime);
    }

    
    public void SpawnObstacle()
    {
        if (obstaclePrefab == null) return;

        int lane = Random.Range(0, 3);

        float laneDistance = 2f;
        float xPos = (lane - 1) * laneDistance;

        
        float zOffset = Random.Range(5f, 5f);
        float zPos = transform.position.z + zOffset;

        Vector3 spawnPosition = new Vector3(xPos, 1, zPos);

        GameObject obs = Instantiate(obstaclePrefab, spawnPosition, Quaternion.identity);

        
        obs.transform.SetParent(transform, true);
    }
}