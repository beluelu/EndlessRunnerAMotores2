using UnityEngine;

public class Floor : MonoBehaviour
{
    public GameObject zombiePrefab;
    private GameObject currentZombie;

    public void ActivateFloor()
    {
        // limpiar zombie anterior
        if (currentZombie != null)
        {
            Destroy(currentZombie);
        }

        // probabilidad de spawn
        if (Random.value < 0.5f)
        {
            SpawnZombie();
        }
    }

    void SpawnZombie()
    {
        currentZombie = Instantiate(zombiePrefab, transform);

        // POSICIÓN LOCAL 🔥 (esto evita el bug)
        currentZombie.transform.localPosition = new Vector3(0, 0, 5f);
        currentZombie.transform.localRotation = Quaternion.identity;
    }
}
