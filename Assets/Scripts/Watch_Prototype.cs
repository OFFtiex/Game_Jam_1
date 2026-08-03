using UnityEngine;



//This is the prototype of Watch
//02.08 only changes the mass of the player to move Boxes





public class Boost_for_Boxes : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            // You pick up the watch and get older so You become stronger
            Player player = collision.gameObject.GetComponent<Player>();
            player.Player_body.mass = 1000f;
            player.collider.size = new Vector2(player.collider.size.x , player.collider.size.y  * 1.5f);
            //player.Player_model.color = new Color(Color.red.r, Color.red.g, Color.red.g, player.Player_model.color.a);
            player.Player_model.sprite = player.sp;
            Destroy(gameObject);
        }
    }
}
