using UnityEngine;

public class NextLvlButton : MonoBehaviour
{
    public string nextLvl;

    public void LoadCurrentScene(){
        Time.timeScale = 1;
        UnityEngine.SceneManagement.SceneManager.LoadScene(nextLvl);
    }
}
