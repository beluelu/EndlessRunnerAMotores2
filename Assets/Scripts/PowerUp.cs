using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public enum PowerUpType
    {
        Shield,
        DoubleCoins,
        SecondChance,
        Heal // ❤️
    }

    public PowerUpType type;
    public float duration = 5f;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        PlayerStats stats = other.GetComponent<PlayerStats>();
        if (stats == null) return;

        switch (type)
        {
            case PowerUpType.Shield:
                stats.ActivateShield(duration);
                break;

            case PowerUpType.DoubleCoins:
                stats.ActivateDoubleCoins(duration);
                break;

            case PowerUpType.SecondChance:
                stats.ActivateSecondChance();
                break;

            case PowerUpType.Heal:
                stats.Heal(1); // ❤️ ACA ESTÁ LA MAGIA
                break;
        }

        Destroy(gameObject);
    }
}
