using UnityEngine;
using UnityEngine.UI;

public class LvlAvailability : MonoBehaviour
{
    public int lvl;

    void Start(){
        Button button = GetComponent<Button>();

        if (PlayerPrefs.GetInt("ReachedLevelValue") < lvl){
            button.interactable = false;
        }
    }
}
