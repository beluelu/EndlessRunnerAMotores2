using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance;

    [Header("Billetera Global")]
    public int currency;

    void Awake()
    {
        
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadData();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    
    public void SaveData()
    {
        PlayerPrefs.SetInt("MonedasTotales", currency);
        PlayerPrefs.Save();
        Debug.Log("Monedas guardadas en el dispositivo: " + currency);
    }

    
    public void LoadData()
    {
        currency = PlayerPrefs.GetInt("MonedasTotales", 0);
    }
}
