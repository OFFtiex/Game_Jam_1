using UnityEngine;

public class PhysicalButtons : MonoBehaviour
{
<<<<<<< HEAD
    //public GameObject[] dependentObjects;//потом сделать обращения
=======
    
>>>>>>> JopaLien

    [Header("Sprite_Render")]
    private SpriteRenderer visibleSprite;
    public Sprite standard;
    public Sprite pressed;

    private void Start()
    {
<<<<<<< HEAD
        // Если поле пустое, скрипт сам найдет SpriteRenderer на этом объекте
=======
        
>>>>>>> JopaLien
        if (visibleSprite == null)
        {
            visibleSprite = GetComponent<SpriteRenderer>();
            visibleSprite.sprite = standard;
        }
    }
<<<<<<< HEAD
    private void OnCollisionEnter2D(Collision2D collision)
=======
    private void OnTriggerEnter2D(Collider2D collision)
>>>>>>> JopaLien
    {
        visibleSprite.sprite = pressed;
    }

<<<<<<< HEAD
    private void OnCollisionExit2D(Collision2D collision)
=======
    private void OnTriggerExit2D(Collider2D collision)
>>>>>>> JopaLien
    {
        visibleSprite.sprite = standard;
    }
}
