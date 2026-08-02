using UnityEngine;

public class Player : MonoBehaviour
{

    [Header("Player Movement")]
    public Rigidbody2D Player_body;


    void Start()
    {
        Player_body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float move_Input = Input.GetAxis("Horizontal");
        Player_body.linearVelocity = new Vector2(move_Input, Player_body.linearVelocity.y);
    }
}
