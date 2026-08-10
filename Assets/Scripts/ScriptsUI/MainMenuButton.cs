using UnityEngine;

public class MainMenuButton : MonoBehaviour
{
    public void MainMenuReturn(){
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }
}
