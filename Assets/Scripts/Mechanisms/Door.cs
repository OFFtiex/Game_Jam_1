using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private Animator _doorAnimator;

    private BoxCollider2D _boxCollider;
    private bool _isFullyOpen = false;

    private void Start()
    {
        if (_doorAnimator == null) _doorAnimator = GetComponent<Animator>();
        if (_boxCollider == null) _boxCollider = GetComponent<BoxCollider2D>();
    }

    public void Open()
    {
        if (_doorAnimator != null) _doorAnimator.SetTrigger("Open");
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

        if (_doorAnimator != null) _doorAnimator.SetTrigger("Close");
        if (_boxCollider != null) _boxCollider.isTrigger = false;
    }
}
