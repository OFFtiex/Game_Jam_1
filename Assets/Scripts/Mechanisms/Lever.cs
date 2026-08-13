using UnityEngine;
using UnityEngine.InputSystem;
public class Lever : MonoBehaviour
{
    private SpriteRenderer visibleSprite;
    public Sprite standard;
    public Sprite moved;
    
    public bool Is_Near_lever;
    public Transform secondChild;
    private void Start()
    {
        Transform secondChild = transform.GetChild(1);
        if (visibleSprite == null)
        {
            visibleSprite = GetComponent<SpriteRenderer>();
            visibleSprite.sprite = standard;
        }
    }

    public void Update()
    {
        if (secondChild == null)
        {
            visibleSprite.sprite = moved;
        }
    }
    //private void OnTriggerEnter2D(Collider2D collision)
    //{

    //    if (collision.gameObject.tag == "Player")
    //    {
    //        Debug.Log("A");


    //    }
    //}
}
