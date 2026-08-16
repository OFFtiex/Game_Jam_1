using UnityEngine;

public class Ded_Door : MonoBehaviour
{
    public SpriteRenderer render;
    public Sprite closed;
    public Sprite opened;
    public GameObject RR;
    private BoxCollider2D door_Collider;


    private void Start()
    {
        door_Collider = GetComponent<BoxCollider2D>();
        render = GetComponent<SpriteRenderer>();
        render.sprite = closed;
        RR = GameObject.FindWithTag("SILENTCHECK");
    }
    private void Update()
    {
        if (RR == null)
        {
            render.sprite = opened;
            door_Collider.size = new Vector2(0, 0); 
        }
    }
}
