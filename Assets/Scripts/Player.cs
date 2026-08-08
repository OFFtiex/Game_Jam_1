using UnityEngine;
using UnityEngine.InputSystem;

public enum AgeState
{
    Baby,
    MidAge,
    Ded
}
public class Player : MonoBehaviour
{

    [Header("Player_Movement")]
    public Rigidbody2D Player_body;
    public float Move_Speed = 3f;
    public float jumpForce = 5f;
    public float smoothing = 10f;
    private float move_Input;

    [Header ("Sprite_Render")]
    public SpriteRenderer Player_model;
    public Sprite sp; // current sprite (can change between Baby, Mid_Age and Ded sprites )

    [Header("Box")]
    public float Box_radius = 1f;
    public LayerMask Box_Layer;
    public bool Is_near_to_Box;
    public Transform Box_Check;
    public GameObject BB;

    [Header("Ground")]
    public float Ground_radius = 0.2f;
    public LayerMask Ground_Layer;
    public bool Is_Grounded;
    public Transform Ground_Check;


    [Header("Player_characteristics")]
    private Animator animator;
    public int ExtraJumpValue = 1;
    public int ExtraJump;
    public int Is_Baby = 0; // <------|
    public bool Is_Mid_Age = false; // <--|---Player's age
    public bool Is_Ded = false; // <------|

    public AgeState CurrentAge;


    [SerializeField] private ParticleSystem walking_particles;
    private ParticleSystem walking_particles_Instance;
    public bool Pull_or_not = false;





    public BoxCollider2D collider;
    public Vector2 original_Collider_Offset;



    void Start()
    {
        animator = GetComponent<Animator>();
        Player_body = GetComponent<Rigidbody2D>();
        collider = GetComponent<BoxCollider2D>();
        Player_model = GetComponent<SpriteRenderer>();
        ExtraJump = ExtraJumpValue;
        BB = GameObject.FindWithTag("Box");
        CurrentAge = AgeState.Baby;
        original_Collider_Offset = collider.offset;

}

    
    void Update()
    {
        // Moving
        float targetInput = 0f;
        if (Keyboard.current != null)
        {
            if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed)
            {
                targetInput = 1f;
                //Spawn_Particles();
            }
            else if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed)
            {
                targetInput = -1f;
                //Spawn_Particles();
            }
        }
        move_Input = Mathf.MoveTowards(move_Input, targetInput, smoothing * Time.deltaTime);//Smoothing
        Player_body.linearVelocity = new Vector2(Move_Speed * move_Input, Player_body.linearVelocity.y);

        // Jumping
        if (Is_Grounded)
        {
            ExtraJump = ExtraJumpValue;
            if (Keyboard.current != null && Keyboard.current.spaceKey.wasPressedThisFrame)
            {
                Player_body.linearVelocity = new Vector2(Player_body.linearVelocity.x, jumpForce);
            }
        }
        if ((ExtraJump != 0) && (Is_Grounded == false))
        {
            if (Keyboard.current != null && Keyboard.current.spaceKey.wasPressedThisFrame)
            {
                Player_body.linearVelocity = new Vector2(Player_body.linearVelocity.x, jumpForce);
                ExtraJump -= 1;
            }
        }
        SetAnimation(targetInput);


        // Fliping the sprite
        if (targetInput < 0f)
        {
            Player_model.flipX = true;
        }
        else
        {
            Player_model.flipX = false;
        }


        if (Is_near_to_Box  && CurrentAge == AgeState.MidAge) 
        {
            //Is_Carrying(); // checks if the player pressed the button to enter "Drag Mode"
            // "Drag Mode" is the status when you can move or pull an object

            //if (Box_Check.transform.position.y > BB.transform.position.y) { return; }


            if ((Keyboard.current.eKey.isPressed))
            {
                
                if (Box_Check.transform.position.x < BB.transform.position.x)
                {

                    if (targetInput < 0)
                    {
                        BB.transform.position = new Vector2(Box_Check.transform.position.x + 1.5f, Box_Check.transform.position.y);
                    }
                }
                else if (Box_Check.transform.position.x > BB.transform.position.x)
                {
                    if (targetInput > 0)
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



    void FixedUpdate()
    {
        Is_near_to_Box = Physics2D.OverlapCircle(Box_Check.position, Box_radius, Box_Layer);
        Is_Grounded = Physics2D.OverlapCircle(Ground_Check.position, Ground_radius, Ground_Layer);
    }

    private void SetAnimation(float targetInput)
    {
        if (Is_Grounded)
        {
            if (targetInput == 0)
            {
                if (CurrentAge == AgeState.Baby)
                {
                    animator.Play("Kid_Idle0_Animation");
                }
                else if (CurrentAge == AgeState.MidAge)
                {
                    animator.Play("Parent_Idle0_Animation");
                }
                
            }
            else
            {
                if (CurrentAge == AgeState.Baby)
                {
                    animator.Play("Kid_Run_Animation");
                }
                else if (CurrentAge == AgeState.MidAge)
                {
                    animator.Play("Parent_Run_Animation");
                }
            }
        }
        else 
        {
            if (Player_body.linearVelocityY > 0)
            {
                if (CurrentAge == AgeState.Baby)
                {
                    animator.Play("Kid_Jump_Animation");
                }
                else if (CurrentAge == AgeState.MidAge)
                {
                    animator.Play("Parent_Jump_Animation");
                }
            }
            else 
            {
                if (CurrentAge == AgeState.Baby)
                {
                    animator.Play("Kid_Fall_Animation");
                }
                else if (CurrentAge == AgeState.MidAge)
                {
                    animator.Play("Parent_Fall_Animation");
                }
            }
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
    private void Spawn_Particles()
    {
        walking_particles_Instance = Instantiate(walking_particles, transform.position, Quaternion.identity);
    }

}
