using UnityEngine;

public class Floor : MonoBehaviour
{
    public GameObject zombiePrefab;
    private GameObject currentZombie;

    public void ActivateFloor()
    {
        if (currentZombie != null)
        {
            Destroy(currentZombie);
        }

        bool canSpawn = true;

        //remote config
        if (RemoteConfigManager.Instance != null)
        {
            canSpawn = RemoteConfigManager.Instance.spawnZombie;
        }

        if (canSpawn && Random.value < 0.5f)
        {
            SpawnZombie();
        }
    }

    void SpawnZombie()
    {
        currentZombie = Instantiate(zombiePrefab, transform);

        currentZombie.transform.localPosition = new Vector3(0, 0, 5f);
        currentZombie.transform.localRotation = Quaternion.identity;
    }
}
