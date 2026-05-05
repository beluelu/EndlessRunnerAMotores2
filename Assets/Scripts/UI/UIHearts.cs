using UnityEngine;
using UnityEngine.UI;

public class UIHearts : MonoBehaviour
{
    public GameObject[] heartModels;

    public void UpdateHearts(int currentLives)
    {
        for (int i = 0; i < heartModels.Length; i++)
        {
            
            if (i < currentLives)
            {
                heartModels[i].SetActive(true);
            }
            else
            {
                heartModels[i].SetActive(false);
            }
        }
    }
}
