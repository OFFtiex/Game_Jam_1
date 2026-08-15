using UnityEngine;
using UnityEngine.UI;
public class WinUI : MonoBehaviour
{
    public GameObject winUI;
    public int nextLvlValue;

    private void OnCollisionEnter2D(Collision2D collision){
        if (collision.gameObject.CompareTag("Player")){
            PlayerPrefs.SetInt("ReachedLevelValue", nextLvlValue);
            Time.timeScale = 0;
            if (collision.gameObject.TryGetComponent<Player>(out Player player))
            {
                player.enabled = false;
            }
            winUI.SetActive(true);
        }
    }
}
