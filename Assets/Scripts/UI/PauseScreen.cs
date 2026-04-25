using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;
using System;

public class PauseScreen : MonoBehaviour
{

    [SerializeField] GameObject PauseMenuUI, PauseButtonUI, gameOverUI;
    public static PauseScreen instance;
    private void OnEnable()
    {
        PlayerController.IsGameOver += GameOver;
    }

    private void OnDisable()
    {
        PlayerController.IsGameOver -= GameOver;
    }
    public void PauseGame()
    {
        Time.timeScale = 0f;
        PauseButtonUI.SetActive(false);
        PauseMenuUI.SetActive(true);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        PauseButtonUI.SetActive(true);
        PauseMenuUI.SetActive(false);
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void GameOver()
    {
        Time.timeScale = 0f;
        gameOverUI.SetActive(true);
        PauseButtonUI.SetActive(false);
    }
}
