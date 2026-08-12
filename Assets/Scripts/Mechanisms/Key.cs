using UnityEngine;

public class Key : MonoBehaviour
{
    [SerializeField] private Door _Door;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            _Door.FullOpen();
            Destroy(gameObject);
        }
    }
}
