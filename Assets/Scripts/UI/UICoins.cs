using UnityEngine;
using TMPro; 

public class UICoins : MonoBehaviour
{
    public TextMeshProUGUI coinsText;

    void Update()
    {
        
        if (GameManager.instance != null)
        {
            coinsText.text = "Coins: " + GameManager.instance.currentCoins;
        }
    }

    
    public void UpdateCoins(int amount)
    {
        coinsText.text = "Coins: " + amount;
    }
}