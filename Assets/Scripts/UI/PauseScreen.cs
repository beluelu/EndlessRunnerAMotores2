using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseScreen : MonoBehaviour
{
    public static bool Ispaused = false;
    [SerializeField] private GameObject PauseMenuUI, GameOverMenuUI;
    [SerializeField] private GameObject PauseButton;

    public void GameOver()
    {
        GameOverMenuUI.SetActive(true);
        PauseButton.SetActive(false);
        Time.timeScale = 0f;
        Ispaused = true;
    }
   
    public void PauseGame()
    {
        if (!Ispaused)
        {
            PauseMenuUI.SetActive(true);
            PauseButton.SetActive(false);
            Time.timeScale = 0f;
            Ispaused = true;
        }
    }

    public void ResumeGame()
    {
        if (Ispaused)
        {
            PauseMenuUI.SetActive(false);
            PauseButton.SetActive(true);
            Time.timeScale = 1f;
            Ispaused = false;
        }
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        Ispaused = false;
        SceneManager.LoadScene("Run");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
