using UnityEngine;



//This is the prototype of Watch
//02.08 only changes the mass of the player to move Boxes





public class Boost_for_Boxes : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Player player = collision.gameObject.GetComponent<Player>();
            player.Player_body.mass = 1000f;
            Destroy(gameObject);
        }
    }
}
