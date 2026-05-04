using UnityEngine;
using UnityEngine.UI;

public class UIHearts : MonoBehaviour
{
    public GameObject[] hearts;

    public void UpdateHearts(int currentLives)
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            hearts[i].SetActive(i < currentLives);
        }
    }
}
