using UnityEngine;

public class Lever : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Lever_Check")
        {
            Debug.Log("A");
        }
    }
}
