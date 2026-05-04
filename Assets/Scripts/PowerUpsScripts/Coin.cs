using UnityEngine;

public class Coin : MonoBehaviour
{
    public int value = 1;

    void Update()
    {
        transform.Rotate(0, 150f * Time.deltaTime, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerStats stats = other.GetComponent<PlayerStats>();

            int multiplier = 1;

            if (stats != null)
                multiplier = stats.coinMultiplier;

            Debug.Log("Moneda +" + (value * multiplier));

            Destroy(gameObject);
        }
    }
}
