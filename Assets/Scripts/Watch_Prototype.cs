using UnityEngine;



//This is the prototype of Watch
//02.08 only changes the mass of the player to move Boxes
//03.08 changes the parameter Is_Mid_Age




public class AgingWatch : MonoBehaviour
{
    // You touch the clock and you become older
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Player player = collision.gameObject.GetComponent<Player>();
            if (player == null) return;

            if (player.CurrentAge == AgeState.Baby)
            {
                player.CurrentAge = AgeState.MidAge;
            }
            else if (player.CurrentAge == AgeState.MidAge)
            {
                player.CurrentAge = AgeState.Ded;
            }
            else if (player.CurrentAge == AgeState.Ded)
            {
                player.Kill("Senescence");
            }
            //player.Player_model.color = new Color(Color.red.r, Color.red.g, Color.red.b, player.Player_model.color.a);
            player.Player_model.sprite = player.sp;
            Destroy(gameObject);
        }
    }
}
public class RejuvenatingWatch : MonoBehaviour
{
    // You touch the clock and you become younger
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Player player = collision.gameObject.GetComponent<Player>();
            if (player == null) return;

            if (player.CurrentAge == AgeState.Ded)
            {
                player.CurrentAge = AgeState.MidAge;
            }
            else if (player.CurrentAge == AgeState.MidAge)
            {
                player.CurrentAge = AgeState.Baby;
            }
            else if (player.CurrentAge == AgeState.Baby)
            {
                player.Kill("Chronological Regression");
            }

            //player.Player_model.color = new Color(Color.red.r, Color.red.g, Color.red.b, player.Player_model.color.a);
            player.Player_model.sprite = player.sp;
            Destroy(gameObject);
        }
    }
}