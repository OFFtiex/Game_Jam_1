using UnityEngine;

public class PhysicalLever : MonoBehaviour
{
    [SerializeField] private Door _Door;

    [Header("Спрайты состояний")]
    [SerializeField] private Sprite _LeftSprite;
    [SerializeField] private Sprite _RightSprite;

    private SpriteRenderer spriteRenderer;
    private enum LeverState { Left, Right, Neutral }
    private LeverState _CurrentState = LeverState.Neutral;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;

        if (collision.transform.position.x < transform.position.x)
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
        if (_CurrentState == LeverState.Left) return;

        _CurrentState = LeverState.Left;
        spriteRenderer.sprite = _LeftSprite;

        if (_Door != null)
        {
            _Door.Close();
        }
    }

    private void SwitchRight()
    {
        if (_CurrentState == LeverState.Right) return;

        _CurrentState = LeverState.Right;
        spriteRenderer.sprite = _RightSprite;

        if (_Door != null)
        {
            _Door.Open();
        }
    }
}
