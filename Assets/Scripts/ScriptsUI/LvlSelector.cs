using UnityEngine;
using UnityEngine.InputSystem;

public class LvlSelector : MonoBehaviour
{   
    public GameObject MainMenuUI;
    public GameObject LvlSelectorUI;

    void Update()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            LvlSelectorUI.SetActive(false);
            MainMenuUI.SetActive(true);
        }
    }
}
