using UnityEngine;
using UnityEngine.InputSystem;
public class Door : MonoBehaviour
{
    private SpriteRenderer visibleSprite;
    public Sprite standard;
    public Sprite moved;
    public GameObject DD;
    public BoxCollider2D collider;

    private void Start()
    {
        DD = GameObject.FindWithTag("SILENTCHECK");
        collider = GetComponent<BoxCollider2D>();
        if (visibleSprite == null)
        {
            visibleSprite = GetComponent<SpriteRenderer>();
            visibleSprite.sprite = standard;
        }
    }

    public void Update()
    {
        if (DD == null)
        {
            visibleSprite.sprite = moved;
            collider.size = new Vector2(0, 0);
        }
    }

    // Door_model.sprite = new_sprite;
    // boxCollider.size = new Vector2(0,0);
}