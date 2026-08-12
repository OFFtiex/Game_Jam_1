using UnityEngine;

public class Key : MonoBehaviour
{
    [SerializeField] private Door _door;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            _door.FullOpen();
            Destroy(gameObject);
        }
    }
}
