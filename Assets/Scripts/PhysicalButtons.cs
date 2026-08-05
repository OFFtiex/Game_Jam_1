using UnityEngine;

public class PhysicalButtons : MonoBehaviour
{
<<<<<<< HEAD
    
=======
    //public GameObject[] dependentObjects;//потом сделать обращения
>>>>>>> 2d415ea64c6e6ff81ec9c6cc3e129016b8c609fa

    [Header("Sprite_Render")]
    private SpriteRenderer visibleSprite;
    public Sprite standard;
    public Sprite pressed;

    private void Start()
    {
<<<<<<< HEAD
        
=======
        // Если поле пустое, скрипт сам найдет SpriteRenderer на этом объекте
>>>>>>> 2d415ea64c6e6ff81ec9c6cc3e129016b8c609fa
        if (visibleSprite == null)
        {
            visibleSprite = GetComponent<SpriteRenderer>();
            visibleSprite.sprite = standard;
        }
    }
<<<<<<< HEAD
    private void OnTriggerEnter2D(Collider2D collision)
=======
    private void OnCollisionEnter2D(Collision2D collision)
>>>>>>> 2d415ea64c6e6ff81ec9c6cc3e129016b8c609fa
    {
        visibleSprite.sprite = pressed;
    }

<<<<<<< HEAD
    private void OnTriggerExit2D(Collider2D collision)
=======
    private void OnCollisionExit2D(Collision2D collision)
>>>>>>> 2d415ea64c6e6ff81ec9c6cc3e129016b8c609fa
    {
        visibleSprite.sprite = standard;
    }
}
