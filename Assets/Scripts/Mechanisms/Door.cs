using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private Sprite _opened;
    [SerializeField] private Sprite _closed;

    private SpriteRenderer _visibleSprite;
    private BoxCollider2D _boxCollider;
    private bool _isFullyOpen = false;

    private void Start()
    {
        if (_visibleSprite == null) _visibleSprite = GetComponent<SpriteRenderer>();
        if (_boxCollider == null) _boxCollider = GetComponent<BoxCollider2D>();
    }

    public void Open()
    {
        _visibleSprite.sprite = _opened;
        if (_boxCollider != null) _boxCollider.isTrigger = true;
    }

    public void FullOpen()
    {
        _isFullyOpen = true;
        Open();
    }

    public void Close()
    {
        if (_isFullyOpen)
        {
            Debug.Log("The door is stuck open!");
            return;
        }

        _visibleSprite.sprite = _closed;
        if (_boxCollider != null) _boxCollider.isTrigger = false;
    }
}
