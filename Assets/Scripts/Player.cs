using UnityEngine;

public class Player : MonoBehaviour
{

    [Header("Player_Movement")]
    public Rigidbody2D Player_body;
    public float Move_Speed = 3f;
    public float jumpForce = 10f;

    [Header ("Sprite_Render")]
    public SpriteRenderer Player_model;
    public Sprite sp; // current sprite (can change between Baby, Mid_Age and Ded sprites )

    [Header("Box")]
    public float Box_radius = 1f;
    public LayerMask Box_Layer;
    public bool Is_near_to_Box;
    public Transform Box_Check;
    public GameObject BB;

    [Header("Player_characteristics")]
    public int Is_Baby = 0; // <------|
    public bool Is_Mid_Age = false; // <--|---Player's age
    public bool Is_Ded = false; // <------|
    private bool Pull_or_not = false;





    public BoxCollider2D collider;


    void Start()
    {
        Player_body = GetComponent<Rigidbody2D>();
        collider = GetComponent<BoxCollider2D>();
        Player_model = GetComponent<SpriteRenderer>();
        BB = GameObject.FindWithTag("Box");


    }

    
    void Update()
    {
        // Moving
        float move_Input = Input.GetAxis("Horizontal");
        Player_body.linearVelocity = new Vector2(Move_Speed  * move_Input, Player_body.linearVelocity.y);



        if (Input.GetKeyDown(KeyCode.Space)) // Jumping
        {
            Player_body.linearVelocity = new Vector2(Player_body.linearVelocity.x, jumpForce);
        }


        
        
        if (Is_near_to_Box  && Is_Mid_Age == true) 
        {
            Is_Carrying(); // checks if the player pressed the button to enter "Drag Mode"
            // "Drag Mode" is the status when you can move or pull an object"

            if (Box_Check.transform.position.y > BB.transform.position.y)
            {
                return;
            }


            if (Pull_or_not == true)
            {
                if (Box_Check.transform.position.x < BB.transform.position.x)
                {

                    if (move_Input < 0)
                    {
                        BB.transform.position = new Vector2(Box_Check.transform.position.x + 1.5f, Box_Check.transform.position.y);
                    }
                }
                else if (Box_Check.transform.position.x > BB.transform.position.x)
                {
                    if (move_Input > 0)
                    {
                        BB.transform.position = new Vector2(Box_Check.transform.position.x - 1.5f, Box_Check.transform.position.y);
                    }
                }
                //if (move_Input > 0)
                //{
                //    BB.transform.position = new Vector2(Box_Check.transform.position.x - 1.5f, Box_Check.transform.position.y);
                //}
                //else if (move_Input < 0)
                //{
                //    BB.transform.position = new Vector2(Box_Check.transform.position.x + 1.5f, Box_Check.transform.position.y);
                //}
            }
            
            
        }

        if (transform.position.y < -20) // If you are low enough, you "die"
        {
            Debug.Log("Death");
        }
        
    }


    private void Is_Carrying()
    {
        if (Input.GetKeyDown(KeyCode.Tab) && Pull_or_not == false && Is_near_to_Box) // if you are close to the box and pressed Tab "Pull_or_not" activates and you can move an object
        {
            Pull_or_not = true;
        }
        else if (Input.GetKeyDown(KeyCode.Tab) && Pull_or_not == true && Is_near_to_Box) // press Tab next to the Box to exit "Drag Mode" and stop moving the object
        {
            Pull_or_not = false;
        }
    }
    void FixedUpdate()
    {
        Is_near_to_Box = Physics2D.OverlapCircle(Box_Check.position, Box_radius, Box_Layer);
    }










    private void OnCollisionEnter2D(Collision2D collision) // changes current "Main" box
    {
        if (collision.gameObject.tag == "Box")
        {
            BB = collision.gameObject;
        }


    }


}
