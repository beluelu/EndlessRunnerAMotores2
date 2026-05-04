using System.Collections;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public int maxLives = 3;
    public int currentLives;

    // estados
    public bool hasShield = false;
    public int coinMultiplier = 1;

    private PlayerAnimation playerAnim;

    // 🔥 referencia UI
    public UIHearts uiHearts;

    void Start()
    {
        currentLives = maxLives;
        playerAnim = GetComponent<PlayerAnimation>();

        if (uiHearts != null)
            uiHearts.UpdateHearts(currentLives);
    }

    public void TakeDamage(int amount)
    {
        if (hasShield)
        {
            Debug.Log("🛡️ daño bloqueado");
            return;
        }

        currentLives -= amount;
        currentLives = Mathf.Clamp(currentLives, 0, maxLives);

        if (uiHearts != null)
            uiHearts.UpdateHearts(currentLives);

        if (currentLives <= 0)
        {

            if (playerAnim != null)
            {
                StartCoroutine(playerAnim.Fall());
            }
        }
    }

    public void Heal(int amount)
    {
        if (currentLives >= maxLives) return;

        currentLives += amount;
        currentLives = Mathf.Clamp(currentLives, 0, maxLives);

        if (uiHearts != null)
            uiHearts.UpdateHearts(currentLives);
    }

    // -------- POWER UPS --------

    public void ActivateShield(float duration)
    {
        StartCoroutine(ShieldCoroutine(duration));
    }

    IEnumerator ShieldCoroutine(float duration)
    {
        hasShield = true;
        yield return new WaitForSeconds(duration);
        hasShield = false;
    }

    public void ActivateDoubleCoins(float duration)
    {
        StartCoroutine(DoubleCoins(duration));
        Section[] sections = FindObjectsOfType<Section>();

        foreach (Section section in sections)
        {
            section.DuplicateCoins();
        }
    }

    IEnumerator DoubleCoins(float duration)
    {
        coinMultiplier = 2;
        yield return new WaitForSeconds(duration);
        coinMultiplier = 1;
    }
}