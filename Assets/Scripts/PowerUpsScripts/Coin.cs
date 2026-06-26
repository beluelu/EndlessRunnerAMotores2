using UnityEngine;

public class Coin : MonoBehaviour
{
    public int value = 1;

    void Update()
    {
        transform.Rotate(Vector3.forward * 150f * Time.deltaTime, Space.Self);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerStats stats = other.GetComponent<PlayerStats>();

            if (stats != null)
            {
                int total = value * stats.coinMultiplier;

                stats.AddCoins(total);

                
                if (GameManager.instance != null)
                {
                    GameManager.instance.AddCoins(total);
                }
            }
            Destroy(gameObject);
        }
    }
}
