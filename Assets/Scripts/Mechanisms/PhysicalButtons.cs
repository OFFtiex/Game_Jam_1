using UnityEngine;

public class PhysicalButtons : MonoBehaviour
{
    [Header("Sprites")]
    [SerializeField] private Sprite standard;
    [SerializeField] private Sprite pressed;

    private SpriteRenderer _visibleSprite;
    private Door _door;

    private void Start()
    {
        if (_visibleSprite == null)
        {
            _visibleSprite = GetComponent<SpriteRenderer>();

            if (standard != null)
            {
                _visibleSprite.sprite = standard;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (_visibleSprite != null && pressed != null && _door != null)
        {
            _door.Open();
            _visibleSprite.sprite = pressed;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (_visibleSprite != null && standard != null && _door != null)
        {
            _door.Close();
            _visibleSprite.sprite = standard;
        }
    }
}
