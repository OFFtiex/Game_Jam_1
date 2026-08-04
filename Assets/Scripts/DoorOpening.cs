// using UnityEngine;

// public class DoorOpening : MonoBehaviour
// {   
//     public SpriteRenderer Door_model;
//     public Sprite new_sprite; 

//     private BoxCollider2D boxCollider;

//     void Start(){
//     boxCollider = GetComponent<BoxCollider2D>();

//     Door_model = GetComponent<SpriteRenderer>();
//     }

//     private void OnCollisionEnter2D(Collision2D collider){
//         HasKey isKey = collider.gameObject.GetComponent<HasKey>();
//         if (isKey == null) return;
//         if (collider.gameObject.CompareTag("Player") && isKey.hasKey){
//             switch (isKey.color)
//             {   
//                 case "Red":
//                     Door_model.sprite = new_sprite;
//                     boxCollider.size = new Vector2(0,0);
//                     break;
                
//                 default: return;
//             }
//         }
//     }
// }
using UnityEngine;

public class DoorOpening : MonoBehaviour
{   
    public SpriteRenderer Door_model;
    public Sprite new_sprite; 
    public BoxCollider2D boxCollider;

    void Start(){
    boxCollider = GetComponent<BoxCollider2D>();
    Door_model = GetComponent<SpriteRenderer>();
    }

    // Door_model.sprite = new_sprite;
    // boxCollider.size = new Vector2(0,0);
}
