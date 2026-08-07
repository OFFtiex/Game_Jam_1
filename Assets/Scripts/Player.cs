using UnityEngine.InputSystem;
using UnityEngine;

public enum AgeState    {   Baby, MidAge, Ded   }

public class Player : MonoBehaviour
{

    [Header("Player_Movement")]
    public Rigidbody2D Player_body;
    public float maxSpeed => CurrentAge switch
    {
        AgeState.Baby => 3.0f,
        AgeState.MidAge => 5.0f,
        AgeState.Ded => 1.5f,
        _ => 5.0f
    };
    public float jumpForce => CurrentAge switch
    {
        AgeState.Baby => 2f,
        AgeState.MidAge => 3.5f,
        AgeState.Ded => 0.3f,
        _ => 5.0f
    };
    public float smoothing => CurrentAge switch
    {
        AgeState.Baby => 2.0f,
        AgeState.MidAge => 10.0f,
        AgeState.Ded => 4f,
        _ => 5.0f
    };
    private float smoothedInput;

    [Header ("Sprite_Render")]
    public SpriteRenderer Player_model;
    public Sprite sp; // current sprite (can change between Baby, Mid_Age and Ded sprites )

    [Header("Box")]
    public float Box_radius = 1f;
    public LayerMask Box_Layer;
    public bool Is_near_to_Box;
    public Transform Box_Check;
    public GameObject BB;

    [Header("Player_additional")]
    public AgeState CurrentAge = AgeState.Baby;
    public BoxCollider2D playerCollider;
    private bool Pull_or_not = false;

    void Start()
    {
        Player_body = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<BoxCollider2D>();
        Player_model = GetComponent<SpriteRenderer>();
        BB = GameObject.FindWithTag("Box");
        CurrentAge = AgeState.Baby;

}
    
    void Update()
    {
        if (Is_near_to_Box  && CurrentAge == AgeState.MidAge) 
        {
            Is_Carrying(); // checks if the player pressed the button to enter "Drag Mode"
            // "Drag Mode" is the status when you can move or pull an object

            if (Box_Check.transform.position.y > BB.transform.position.y)   {  return;  }


            if (Pull_or_not == true)
            {
                if (Box_Check.transform.position.x < BB.transform.position.x)
                {

                    if (smoothedInput < 0)
                    {
                        BB.transform.position = new Vector2(Box_Check.transform.position.x + 1.5f, Box_Check.transform.position.y);
                    }
                }
                else if (Box_Check.transform.position.x > BB.transform.position.x)
                {
                    if (smoothedInput > 0)
                    {
                        BB.transform.position = new Vector2(Box_Check.transform.position.x - 1.5f, Box_Check.transform.position.y);
                    }
                }
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
        // Moving
        float targetInput = 0f;
        if (Keyboard.current != null)
        {
            if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed) targetInput = 1f;
            else if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed) targetInput = -1f;
        }
        smoothedInput = Mathf.MoveTowards(smoothedInput, targetInput, smoothing * Time.deltaTime);//Smoothing
        Player_body.linearVelocity = new Vector2(maxSpeed * smoothedInput, Player_body.linearVelocity.y);

        // Jumping
        if (Keyboard.current != null && Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            Player_body.linearVelocity = new Vector2(Player_body.linearVelocity.x, jumpForce);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) // changes current "Main" box
    {
        if (collision.gameObject.tag == "Box")
        {
            BB = collision.gameObject.transform.parent.gameObject;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Damage_Pike")
        {

            UnityEngine.SceneManagement.SceneManager.LoadScene("Game_Jam_");
        }
    }
}
