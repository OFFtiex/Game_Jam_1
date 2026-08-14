using UnityEngine;

public class PhysicalLever : MonoBehaviour
{
    private enum LeverState { Left, Right, Neutral }

    [SerializeField] private Door[] _doors;

    [Header("Sprites")]
    [SerializeField] private Sprite _leftSprite;
    [SerializeField] private Sprite _rightSprite;

    private SpriteRenderer _spriteRenderer;
    private LeverState _currentState = LeverState.Neutral;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player") || collision.attachedRigidbody == null) return;

        float playerX = collision.attachedRigidbody.position.x;

        if (playerX < transform.position.x)
        {
            SwitchRight();
        }
        else
        {
            SwitchLeft();
        }
    }

    private void SwitchLeft()
    {
        if (_currentState == LeverState.Left) return;

        _currentState = LeverState.Left;

        if (_spriteRenderer != null) _spriteRenderer.sprite = _leftSprite;
        if (_doors == null) return;
        foreach (Door door in _doors)
        {
            if (door != null)
            {
                door.Close();
            }
        }
    }

    private void SwitchRight()
    {
        if (_currentState == LeverState.Right) return;

        _currentState = LeverState.Right;

        if (_spriteRenderer != null) _spriteRenderer.sprite = _rightSprite;
        if (_doors == null) return;
        foreach (Door door in _doors)
        {
            if (door != null)
            {
                door.Open();
            }
        }
    }
}
