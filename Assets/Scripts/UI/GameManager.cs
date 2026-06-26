using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int currentCoins = 0;
    public int coinsToWin = 20;

    public GameObject victoryPanel;

    private void Awake()
    {
        instance = this;
        Time.timeScale = 1f;
    }

    public void AddCoins(int amount)
    {
        currentCoins += amount;

        Debug.Log("Monedas actuales: " + currentCoins);

        if (currentCoins >= coinsToWin)
        {
            WinGame();
        }
    }

    void WinGame()
    {
        Debug.Log("GANASTE");

        
        if (DataManager.Instance != null)
        {
            
            DataManager.Instance.currency += currentCoins;

            
            DataManager.Instance.SaveData();
        }
        else
        {
            
            Debug.LogWarning("DataManager no encontrado. Entrá desde el menú principal para que guarde.");
        }

        victoryPanel.SetActive(true);

        Time.timeScale = 0f;
    }
}
