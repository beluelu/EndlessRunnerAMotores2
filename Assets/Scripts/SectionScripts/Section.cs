using System.Collections.Generic;
using System.Collections;
using NUnit.Framework;
using UnityEngine;

public class Section : MonoBehaviour
{
    public List<GameObject> obstacles;
    public float speed;

    [Header("Coins")]
    public GameObject coinPrefab;
    public Transform[] coinLanes;

    private List<GameObject> currentCoins = new List<GameObject>();

    private static int lastRandomIndex = -1;

    private void Start()
    {
        //remote config
        if (RemoteConfigManager.Instance != null)
        {
            speed = RemoteConfigManager.Instance.playerSpeed;
        }

        obstacles = new List<GameObject>();

        foreach (Transform child in transform)
        {
            if (child.CompareTag("Obstacle"))
            {
                obstacles.Add(child.gameObject);
            }
        }

        EnableRandomObstacle();
    }

    public void EnableRandomObstacle()
    {
        foreach (GameObject obstacle in obstacles)
        {
            obstacle.SetActive(false);
        }

        ClearCoins();

        int randomIndex = lastRandomIndex;

        while (randomIndex == lastRandomIndex)
        {
            randomIndex = Random.Range(0, obstacles.Count);
        }

        lastRandomIndex = randomIndex;

        GameObject selected = obstacles[randomIndex];
        selected.SetActive(true);

        SpawnCoins();

        Floor floorScript = selected.GetComponent<Floor>();

        if (floorScript != null)
        {
            floorScript.ActivateFloor();
        }
    }

    void SpawnCoins()
    {
        if (coinLanes.Length == 0) return;

        int randomLane = Random.Range(0, coinLanes.Length);

        int amount = 5;

        //remote config
        if (RemoteConfigManager.Instance != null)
        {
            amount = RemoteConfigManager.Instance.coinsAmount;
        }

        for (int i = 0; i < amount; i++)
        {
            Vector3 spawnPos = coinLanes[randomLane].position;
            spawnPos.z += i * 0.8f;

            Collider[] hits = Physics.OverlapSphere(spawnPos, 1f);

            bool blocked = false;

            foreach (Collider hit in hits)
            {
                if (hit.CompareTag("Obstacle"))
                {
                    blocked = true;
                    break;
                }
            }

            if (!blocked)
            {
                GameObject coin = Instantiate(
                    coinPrefab,
                    spawnPos,
                    coinPrefab.transform.rotation,
                    transform
                );

                currentCoins.Add(coin);
            }
        }
    }

    void ClearCoins()
    {
        foreach (GameObject coin in currentCoins)
        {
            if (coin != null)
            {
                Destroy(coin);
            }
        }

        currentCoins.Clear();
    }

    void Update()
    {
        transform.Translate(Vector3.back * speed * Time.deltaTime);

        if (transform.position.z <= -20)
        {
            transform.Translate(Vector3.forward * 20 * 5);
            EnableRandomObstacle();
        }
    }
}
