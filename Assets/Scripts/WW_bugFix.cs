using UnityEngine;

public class WW_bugFix : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Box1")
        {
            Destroy(collision.gameObject.GetComponent<Transform>().GetChild(0).gameObject);
        }    
    }
}
