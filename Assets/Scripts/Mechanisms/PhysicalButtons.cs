using UnityEngine;

public class PhysicalButtons : MonoBehaviour
{
    [SerializeField] private Door[] _doors;

    [Header("Sprites")]
    [SerializeField] private Sprite _standard;
    [SerializeField] private Sprite _pressed;

    private SpriteRenderer _visibleSprite;

    private void Start()
    {
        if (_visibleSprite == null)
        {
            _visibleSprite = GetComponent<SpriteRenderer>();

            if (_standard != null)
            {
                _visibleSprite.sprite = _standard;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (_visibleSprite != null && _pressed != null && _doors != null)
        {
            foreach (Door door in _doors)
            {
                if (door != null) 
                {
                    door.Open();
                }
            }
            _visibleSprite.sprite = _pressed;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (_visibleSprite != null && _standard != null && _doors != null)
        {
            foreach (Door door in _doors)
            {
                if (door != null)
                {
                    door.Close();
                }
            }
            _visibleSprite.sprite = _standard;
        }
    }
}
