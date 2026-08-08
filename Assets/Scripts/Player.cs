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
        _ => 3.0f
    };
    public float jumpForce => CurrentAge switch
    {
        AgeState.Baby => 2f,
        AgeState.MidAge => 3.5f,
        AgeState.Ded => 0.3f,
        _ => 2.0f
    };
    public float smoothing => CurrentAge switch
    {
        AgeState.Baby => 2.0f,
        AgeState.MidAge => 10.0f,
        AgeState.Ded => 4f,
        _ => 2.0f
    };
    private float smoothedInput;
    float targetInput = 0f;

    [Header ("Sprite_Render")]
    public SpriteRenderer Player_model;
    public Sprite sp; // current sprite (can change between Baby, Mid_Age and Ded sprites )

    [Header("Box")]
    private bool Pull_or_not = false;
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

    public BoxCollider2D collider;
    public Vector2 original_Collider_Offset;

    [Header("Player_additional")]
    private ParticleSystem walking_particles_Instance;
    [SerializeField] private ParticleSystem walking_particles;
    [SerializeField] private AgeState ageState;
    public AgeState CurrentAge
    {
        get => ageState;
        set
        {
            if (ageState == value) return;
            ageState = value;
            UpdateColliderParameters();
        }
    }
    private bool isDead = false;

    [Header("Colliders")]
    public BoxCollider2D playerCollider => cachedCollider;
    private BoxCollider2D cachedCollider;
    private Vector2 babyOffset;
    private Vector2 babySize;

    //                                              Unity functions

    void Start()
    {
        animator = GetComponent<Animator>();
        Player_body = GetComponent<Rigidbody2D>();
        Player_model = GetComponent<SpriteRenderer>();
        ExtraJump = ExtraJumpValue;
        BB = GameObject.FindWithTag("Box");

        cachedCollider = GetComponent<BoxCollider2D>();
        if (cachedCollider != null)
        {
            babySize = cachedCollider.size;
            babyOffset = cachedCollider.offset;

            UpdateColliderParameters();
        }
    }
    void Update()
    {
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
            Kill("Fell Through the World");
        }
    }

    void FixedUpdate()
    {
        Is_near_to_Box = Physics2D.OverlapCircle(Box_Check.position, Box_radius, Box_Layer);
        // Moving
        targetInput = 0f;
        if (Keyboard.current != null)
        {
            if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed)        targetInput =  1f;  //Spawn_Particles();
            else if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed)    targetInput = -1f;  //Spawn_Particles();
        }
        SetAnimation(targetInput);
        smoothedInput = Mathf.MoveTowards(smoothedInput, targetInput, smoothing * Time.deltaTime);//Smoothing
        Player_body.linearVelocity = new Vector2(maxSpeed * smoothedInput, Player_body.linearVelocity.y);

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
        Is_near_to_Box = Physics2D.OverlapCircle(Box_Check.position, Box_radius, Box_Layer);
        Is_Grounded = Physics2D.OverlapCircle(Ground_Check.position, Ground_radius, Ground_Layer);
    }

    private void OnTriggerEnter2D(Collider2D other) // changes current "Main" box
    {
        if (other.CompareTag("Box"))
        {
            BB = other.transform.parent.gameObject;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Damage_Pike"))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("Game_Jam_");
        }
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

    //                                              Custom functions

    private void UpdateColliderParameters() 
    {
        if (cachedCollider == null) return;

        switch (ageState)
        {
            case AgeState.Baby:
                cachedCollider.size = babySize;
                cachedCollider.offset = babyOffset;
                break;

            case AgeState.MidAge:
                float midHeight = babySize.y * 1.5f;
                cachedCollider.size = new Vector2(babySize.x, midHeight);
                cachedCollider.offset = new Vector2(babyOffset.x, babyOffset.y + (midHeight - babySize.y) / 2f);
                break;

            case AgeState.Ded:
                float dedHeight = babySize.y * 1.2f;
                cachedCollider.size = new Vector2(babySize.x, dedHeight);
                cachedCollider.offset = new Vector2(babyOffset.x, babyOffset.y + (dedHeight - babySize.y) / 2f);
                break;
        }
    }

    private void Spawn_Particles()
    {
        walking_particles_Instance = Instantiate(walking_particles, transform.position, Quaternion.identity);
    }

    public void Kill(string cause = "Curiosity")
    {
        if (isDead) return;

        Debug.Log($"Entity was killed by: {cause}");
        Die();
    }

    private void Die()
    {
        isDead = true;
        // Coming Soon: анимация, выключение управления, респавн, ......
    }
}