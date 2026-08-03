using UnityEngine;

public class DoorOpening : MonoBehaviour
{   
    public SpriteRenderer Door_model;
    public Sprite new_sprite; 

    private BoxCollider2D boxCollider;

    void Start(){
    boxCollider = GetComponent<BoxCollider2D>();
    Player player = GetComponent<Player>();
    Door_model = GetComponent<SpriteRenderer>();
    }

    private void OnCollisionEnter2D(Collision2D collider){
        if (collider.gameObject.CompareTag("Player") && Input.GetKeyDown(KeyCode.Space)){
            Door_model.sprite = new_sprite;
            boxCollider.size = new Vector3(0,0,-1);
        }
    }
}
