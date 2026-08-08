using UnityEngine;
using UnityEngine.UI;
public class WinUI : MonoBehaviour
{
    public GameObject winUI;

    private void OnCollisionEnter2D(Collision2D collision){
        if (collision.gameObject.CompareTag("Player")){
            Time.timeScale = 0;
            winUI.SetActive(true);
        }
    }
}
