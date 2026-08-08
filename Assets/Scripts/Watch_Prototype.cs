using UnityEngine;



//This is the prototype of Watch
//02.08 only changes the mass of the player to move Boxes
//03.08 changes the parameter Is_Mid_Age




public class Boost_for_Boxes : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            // You pick up the watch and get older so You become stronger
            Player player = collision.gameObject.GetComponent<Player>();
            player.Player_body.mass = 1000f;

            player.CurrentAge = AgeState.Baby;

            player.CurrentAge = AgeState.MidAge;

            player.CurrentAge = AgeState.MidAge;

            

            player.collider.size = new Vector2(player.collider.size.x , player.collider.size.y  * 1.5f);
            player.collider.offset = new Vector2(player.original_Collider_Offset.x, player.original_Collider_Offset.y - 0.25f);
            //player.Player_model.color = new Color(Color.red.r, Color.red.g, Color.red.g, player.Player_model.color.a);
            player.Player_model.sprite = player.sp;
            Destroy(gameObject);
        }
    }
}
