using UnityEngine;

public class Background : MonoBehaviour
{
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent<Player>(out Player player))
        {
            player.Kill("Fell beyond the boundaries of the world");
        }
        else Destroy(other.gameObject);
    }
}
