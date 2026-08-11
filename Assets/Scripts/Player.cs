using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
        AgeState.Baby => 5f,
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

    [Header("Sprite_Render")]
    public SpriteRenderer Player_model;
    public Sprite babySprite;
    public Sprite midAgeSprite;
    public Sprite dedSprite;

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
    [SerializeField] private bool isDead = false;
    private bool isUmbrella = false;

    [Header("Player_additional")]
    private ParticleSystem walking_particles_Instance;
    [SerializeField] private ParticleSystem walking_particles;
    private ParticleSystem Death_particles_Instance;
    [SerializeField] private ParticleSystem Death_particles;

    [SerializeField] private AgeState ageState;
    public AgeState CurrentAge
    {
        get => ageState;
        set
        {
            if (ageState == value) return;
            ageState = value;
            UpdateColliderParameters();
            UpdatePlayerVisual();
        }
    }

    [Header("Colliders")]
    public BoxCollider2D playerCollider => cachedCollider;
    private BoxCollider2D cachedCollider;
    private Vector2 babyOffset;
    private Vector2 babySize;

    [Header ("UI_Elements")]
    [SerializeField] private Image F_Image;
    [SerializeField] private float Current_Alpha_Value = 1;



    //                                              Unity functions

    private void Awake() { Resume(); }

    void Start()
    {
        animator = GetComponent<Animator>();
        F_Image = GameObject.FindWithTag("Fading_Screen").GetComponent<Image>();
        Player_body = GetComponent<Rigidbody2D>();
        Player_model = GetComponent<SpriteRenderer>();
        ExtraJump = ExtraJumpValue;
        F_Image.color = new Color(0, 0, 0, 1);
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
        // Moving
        
        
        targetInput = 0f;
        if (Keyboard.current != null)
        {
            if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed)

            {
                targetInput = 1f;
                Spawn_Particles();
            }
            else if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed)
            {
                targetInput = -1f;
                Spawn_Particles();
            }
        }
        SetAnimation(targetInput);
        smoothedInput = Mathf.MoveTowards(smoothedInput, targetInput, smoothing * Time.deltaTime);
        Player_body.linearVelocity = new Vector2(maxSpeed * smoothedInput, Player_body.linearVelocity.y);
        // Fliping the sprite
        if (targetInput < 0f)
        {
            Player_model.flipX = true;
        }
        else
        {
            Player_model.flipX = false;
        }
        if ((CurrentAge == AgeState.Ded))
        {
            if (Keyboard.current != null && Keyboard.current.tabKey.wasPressedThisFrame && isUmbrella == false)
            {
                Player_body.gravityScale = 0.1f;
                Player_body.linearVelocity = new Vector2(Player_body.linearVelocity.x, 0.3f);
                isUmbrella = true;
            }
            else if ((Keyboard.current != null && Keyboard.current.tabKey.wasPressedThisFrame && isUmbrella == true))
            {
                Player_body.gravityScale = 1f;
                isUmbrella = false;
            }
        }
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
            //Debug.Log("fff");
            if (Keyboard.current != null && Keyboard.current.spaceKey.wasPressedThisFrame)
            {
                Player_body.linearVelocity = new Vector2(Player_body.linearVelocity.x, jumpForce);
                ExtraJump -= 1;
            }
        }
        if (Is_near_to_Box  && CurrentAge == AgeState.MidAge) 
        {
            Player_body.mass = 1000f;
            if ((Box_Check.transform.position.y > BB.transform.position.y + 1))
            {
                return;
            }
            if ((Keyboard.current != null && Keyboard.current.eKey.isPressed))
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
    }

    void FixedUpdate()
    {
        Is_Grounded = Physics2D.OverlapCircle(Ground_Check.position, Ground_radius, Ground_Layer);
        Is_near_to_Box = Physics2D.OverlapCircle(Box_Check.position, Box_radius, Box_Layer);
        if (F_Image.color.a != 0 && isDead == false)
        {
            Current_Alpha_Value -= Time.deltaTime;
            F_Image.color = new Color(0, 0, 0, Current_Alpha_Value);
        }
        else if (isDead == true)
        {
            Current_Alpha_Value += Time.deltaTime;
            F_Image.color = new Color(0, 0, 0, Current_Alpha_Value);
        }
        

        // Jumping
        //if (Is_Grounded)
        //{
        //    ExtraJump = ExtraJumpValue;
        //    if (Keyboard.current != null && Keyboard.current.spaceKey.wasPressedThisFrame)
        //    {
        //        Player_body.linearVelocity = new Vector2(Player_body.linearVelocity.x, jumpForce);
        //    }
        //}
        //if ((ExtraJump != 0) && (Is_Grounded == false))
        //{
        //    Debug.Log("fff");
        //    if (Keyboard.current != null && Keyboard.current.spaceKey.wasPressedThisFrame)
        //    {
        //        Player_body.linearVelocity = new Vector2(Player_body.linearVelocity.x, jumpForce);
        //        ExtraJump -= 1;
        //    }
        //}

        // Umbrella
        
        



        
        if (transform.position.y < -20)
        {
            Kill("Fell Through the World");
        }
    }

    private void OnTriggerEnter2D(Collider2D other) // changes current "Main" box
    {
        if (other.CompareTag("Box") && other.transform.parent != null)
        {
            BB = other.transform.parent.gameObject;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Damage_Pike"))
        {
            isDead = true;
            Death_particles_Instance = Instantiate(Death_particles, Ground_Check.transform.position, Quaternion.identity);
            //Destroy(gameObject);
        }//Kill("Was Pierced by Thorns");
    }


    //                                              Custom functions


    private void SetAnimation(float targetInput)
    {
        string age = CurrentAge == AgeState.Baby ? "Kid" : "Parent";

        string animName = (Is_Grounded, targetInput == 0, Player_body.linearVelocityY > 0) switch
        {
            (true, true, _)   => $"{age}_Idle0_Animation",
            (false, _, true)  => $"{age}_Jump_Animation",
            (true, false, _)  => $"{age}_Run_Animation",
            (false, _, false) => $"{age}_Fall_Animation"
        };

        animator.Play(animName);
    }

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
                cachedCollider.offset = new Vector2(babyOffset.x, babyOffset.y - (midHeight - babySize.y) / 2f);
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
        walking_particles_Instance = Instantiate(walking_particles, Ground_Check.transform.position, Quaternion.identity);
        
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
        LoadScene("Game_Jam_");
        // Coming Soon: death animation
    }
    public void LoadScene(string sceneName = null)
    {
        if (string.IsNullOrEmpty(sceneName))
        {
            sceneName = SceneManager.GetActiveScene().name;
        }

        if (!Application.CanStreamedLevelBeLoaded(sceneName))
        {
            Debug.LogError($"Error: Scene '{sceneName}' isn't found! Add it to Build Settings..");
            return;
        }

        SceneManager.LoadScene(sceneName);
    }

    public void Pause()
    {
        Time.timeScale = 0f;
        // Coming Soon: turn off sounds
    }

    public void Resume()
    {
        Time.timeScale = 1f;
        // Coming Soon: turn on sounds
    }
    private void UpdatePlayerVisual()
    {
        
        Player_model.sprite = ageState switch
        {
            AgeState.Baby => babySprite,
            AgeState.MidAge => midAgeSprite,
            AgeState.Ded => dedSprite,
            _ => Player_model.sprite
        };
    }
}