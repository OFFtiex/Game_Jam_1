//using UnityEngine;

//public class DoorOpening : MonoBehaviour
//{
//    [SerializeField] public SpriteRenderer Door_model;
//    [SerializeField] public Sprite old_sprite;
//    [SerializeField] public Sprite new_sprite;
//    [SerializeField] public BoxCollider2D boxCollider;
//    public GameObject DD;

//    void Start(){
//    boxCollider = GetComponent<BoxCollider2D>();
//    Door_model = GetComponent<SpriteRenderer>();
//    DD = GameObject.FindWithTag("SILENTCHECK");
//    }

//    void Update()
//    {
//        if (DD == null)
//        {
//            Door_model.sprite = new_sprite;
//        }
//    }

//    // Door_model.sprite = new_sprite;
//    // boxCollider.size = new Vector2(0,0);
//}
