using UnityEngine;

public class Player : MonoBehaviour
{

    [Header("Player Movement")]
    public Rigidbody2D Player_body;
    public float Move_Speed = 3f;
    public float jumpForce = 10f;


    void Start()
    {
        Player_body = GetComponent<Rigidbody2D>();
    }

    
    void Update()
    {
        float move_Input = Input.GetAxis("Horizontal");
        Player_body.linearVelocity = new Vector2(Move_Speed  * move_Input, Player_body.linearVelocity.y);



        if (Input.GetKeyDown(KeyCode.Space))
        {
            Player_body.linearVelocity = new Vector2(Player_body.linearVelocity.x, jumpForce);
        }
        
        //if (Input.GetKeyDown(KeyCode.Tab))
        //{
        //    Player_body.mass = 1000f;
        //}
    }
}
