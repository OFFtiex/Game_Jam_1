using UnityEngine;

public class PhysicalButtons : MonoBehaviour
{
    

    [Header("Sprite_Render")]
    private SpriteRenderer visibleSprite;
    public Sprite standard;
    public Sprite pressed;

    private void Start()
    {
        
        if (visibleSprite == null)
        {
            visibleSprite = GetComponent<SpriteRenderer>();
            visibleSprite.sprite = standard;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        visibleSprite.sprite = pressed;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        visibleSprite.sprite = standard;
    }
}
