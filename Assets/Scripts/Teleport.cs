using UnityEngine;

public class Teleport : MonoBehaviour
{
    public Transform TP_Point;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if (collision.gameObject.tag == "Player")
        //{
            collision.gameObject.transform.position = TP_Point.position;
        //}
    }
}
