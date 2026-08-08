using UnityEngine;

public class PhysicalButtons : MonoBehaviour
{

    //public GameObject[] dependentObjects;//last

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

    private void OnCollisionEnter2D(Collision2D collision)

    {
        visibleSprite.sprite = pressed;
    }

    private void OnCollisionExit2D(Collision2D collision)

    {
        visibleSprite.sprite = standard;
    }
}
