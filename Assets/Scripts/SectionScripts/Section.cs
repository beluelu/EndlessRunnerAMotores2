using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class Section : MonoBehaviour
{
    public List<GameObject> obstacles;
    public float speed;

    [Header("Coins")]
    public GameObject coinPrefab;
    public Transform[] coinLanes;

    private List<GameObject> currentCoins = new List<GameObject>();

    private static int lastRandomIndex = -1;

    [Header("PowerUps")]
    public GameObject[] powerUpPrefabs;

    public static bool isGameOver = false;

    private void Start()
    {
        isGameOver = false;

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
        //SpawnPowerUp();
    }

    void SpawnPowerUp()
    {
        if (powerUpPrefabs.Length == 0 || coinLanes.Length == 0) return;

        if (Random.value < 0.3f)
        {
            int randomPower = Random.Range(0, powerUpPrefabs.Length);
            int randomLane = Random.Range(0, coinLanes.Length);

            Vector3 spawnPos = coinLanes[randomLane].position;
            spawnPos.y += 1f;

            Instantiate(
                powerUpPrefabs[randomPower],
                spawnPos,
                Quaternion.identity,
                transform
            );
        }
    }

    void SpawnCoins()
    {
        if (coinLanes.Length == 0) return;

        int randomLane = Random.Range(0, coinLanes.Length);

        int amount = 5;

        if (RemoteConfigManager.Instance != null)
        {
            amount = RemoteConfigManager.Instance.coinsAmount;
        }


        PlayerStats playerStats = FindAnyObjectByType<PlayerStats>();

        if (playerStats != null)
        {
            amount *= playerStats.coinMultiplier;
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

    public void DuplicateCoins()
    {
        List<GameObject> newCoins = new List<GameObject>();

        foreach (GameObject coin in currentCoins)
        {
            if (coin == null) continue;

            Vector3 newPos = coin.transform.position;

            
            newPos.x += 0.5f;

            GameObject newCoin = Instantiate(
                coinPrefab,
                newPos,
                coin.transform.rotation,
                transform
            );

            newCoins.Add(newCoin);
        }

        currentCoins.AddRange(newCoins);
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
        
        if (isGameOver) return;

        transform.Translate(Vector3.back * speed * Time.deltaTime);

        if (transform.position.z <= -20)
        {
            transform.Translate(Vector3.forward * 20 * 5);
            EnableRandomObstacle();
        }
    }
}

