using UnityEngine;

public class Door : MonoBehaviour
{
    public SpriteRenderer DoorModel;
    public Sprite NewSprite;
    public BoxCollider2D BoxCollider;

    private Sprite _oldSprite;

    private void Start()
    {
        BoxCollider = GetComponent<BoxCollider2D>();
        DoorModel = GetComponent<SpriteRenderer>();

        if (DoorModel != null)
        {
            _oldSprite = DoorModel.sprite;
        }
    }
}
