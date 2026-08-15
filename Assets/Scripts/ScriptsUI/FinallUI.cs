using UnityEngine;
using UnityEngine.UI;

public class FinallUI : MonoBehaviour
{
    public Image finalImage;

    private float transparencyValue = 0;
    private bool toFadeIn = false;


    private void Update(){
        if(toFadeIn){
            transparencyValue += Time.deltaTime * 0.4f;             
            finalImage.color = new Color(1, 1, 1, transparencyValue);
            if (finalImage.color.a >= 1f){
                toFadeIn = false;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision){
        if(collision.gameObject.CompareTag("Player")){
            toFadeIn = true;
            Debug.Log("Colis");
        }
    }
}
