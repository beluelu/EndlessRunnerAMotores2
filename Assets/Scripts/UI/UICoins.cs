using UnityEngine;
using TMPro; 

public class UICoins : MonoBehaviour
{
    public TextMeshProUGUI coinsText;

    public void UpdateCoins(int amount)
    {
        coinsText.text = "Coins: " + amount;
    }
}