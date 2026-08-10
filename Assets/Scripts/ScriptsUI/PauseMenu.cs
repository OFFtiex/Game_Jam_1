using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{   
    public GameObject pauseMenu;
    public string currentScene;

    private bool isPaused = false;

    void Update()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            isPaused = !isPaused;
            if (isPaused)
            {
                pauseMenu.SetActive(true);
                Time.timeScale = 0;
            }
            else
            {
                pauseMenu.SetActive(false);
                Time.timeScale = 1;
            }
        }
    }

    public void RestartButton()
    {
        Time.timeScale = 1;
        UnityEngine.SceneManagement.SceneManager.LoadScene(currentScene);
    }

    public void MainMenuButton()
    {
        Time.timeScale = 1;
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }
}