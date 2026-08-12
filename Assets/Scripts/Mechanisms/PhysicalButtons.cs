using UnityEngine;

public class PhysicalButtons : MonoBehaviour
{
    [Header("Sprite_Render")]
    private SpriteRenderer _visibleSprite;

    public Sprite Standard;
    public Sprite Pressed;
    private Door _Door;
    private void Start()
    {
        if (_visibleSprite == null)
        {
            _visibleSprite = GetComponent<SpriteRenderer>();

            if (Standard != null)
            {
                _visibleSprite.sprite = Standard;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (_visibleSprite != null && Pressed != null)
        {
            _Door.Open();
            _visibleSprite.sprite = Pressed;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (_visibleSprite != null && Standard != null)
        {
            _Door.Close();
            _visibleSprite.sprite = Standard;
        }
    }
}
