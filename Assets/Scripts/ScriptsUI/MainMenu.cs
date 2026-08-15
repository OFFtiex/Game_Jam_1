using UnityEngine;

public class MainMenu : MonoBehaviour
{   
    public GameObject MainMenuUI;
    public GameObject LvlSelectorUI;


    void Start(){
        if (PlayerPrefs.GetInt("DoOnce") == 1) return;
        if (PlayerPrefs.GetInt("ReachedLevelValue") != 0){
            PlayerPrefs.DeleteKey("ReachedLevelValue");
        }
        PlayerPrefs.SetInt("DoOnce", 1);
        PlayerPrefs.Save();
    }

    public void GameStart(string lvlName){
        Destroy(gameObject);
        UnityEngine.SceneManagement.SceneManager.LoadScene(lvlName);
    }

    public void MoveToLvlSelector(){
        MainMenuUI.SetActive(false);
        LvlSelectorUI.SetActive(true);
        Time.timeScale = 1;
    }

    public void QuitGame(){
        PlayerPrefs.DeleteKey("DoOnce");
        Application.Quit();
    }
}   
