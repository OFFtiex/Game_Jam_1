using UnityEngine;

public class Key : MonoBehaviour
{
    public Door Door;


    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //
            Destroy(gameObject);
        }
    }
}
