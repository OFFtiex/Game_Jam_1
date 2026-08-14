using UnityEngine;

public class NextLvlButton : MonoBehaviour
{
    public string nextLvlName;

    public void LoadNextLvl(){
        Time.timeScale = 1;
        UnityEngine.SceneManagement.SceneManager.LoadScene(nextLvlName);
    }
}
