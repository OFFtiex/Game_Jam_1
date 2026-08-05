using UnityEngine;

public class PhysicalButtons : MonoBehaviour
{

    //public GameObject[] dependentObjects;//потом сделать обращения

    

    //public GameObject[] dependentObjects;//потом сделать обращения


    [Header("Sprite_Render")]
    private SpriteRenderer visibleSprite;
    public Sprite standard;
    public Sprite pressed;

    private void Start()
    {

        // Если поле пустое, скрипт сам найдет SpriteRenderer на этом объекте


        // Если поле пустое, скрипт сам найдет SpriteRenderer на этом объекте


        if (visibleSprite == null)
        {
            visibleSprite = GetComponent<SpriteRenderer>();
            visibleSprite.sprite = standard;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)

    private void OnTriggerEnter2D(Collider2D collision)


    private void OnTriggerEnter2D(Collider2D collision)

    private void OnCollisionEnter2D(Collision2D collision)

    {
        visibleSprite.sprite = pressed;
    }


    private void OnCollisionExit2D(Collision2D collision)

    private void OnTriggerExit2D(Collider2D collision)


    private void OnTriggerExit2D(Collider2D collision)

    private void OnCollisionExit2D(Collision2D collision)

    {
        visibleSprite.sprite = standard;
    }
}
