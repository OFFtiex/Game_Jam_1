using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    [SerializeField] private float timeToReturn = 2.0f;
    private Dictionary<GameObject, Coroutine> activeTimers = new Dictionary<GameObject, Coroutine>();

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!gameObject.activeInHierarchy) return;

        GameObject obj = other.gameObject;

        if (activeTimers.ContainsKey(obj)) return;

        Coroutine timerCoroutine = StartCoroutine(CountdownToDestroy(obj));
        activeTimers.Add(obj, timerCoroutine);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        GameObject obj = other.gameObject;

        if (activeTimers.TryGetValue(obj, out Coroutine timerCoroutine))
        {
            if (gameObject.activeInHierarchy && timerCoroutine != null)
            {
                StopCoroutine(timerCoroutine);
            }
            activeTimers.Remove(obj);
        }
    }

    private IEnumerator CountdownToDestroy(GameObject obj)
    {
        yield return new WaitForSeconds(timeToReturn);

        if (obj != null)
        {
            if (obj.TryGetComponent<Player>(out Player player))
            {
                player.Kill("Fell beyond the boundaries of the world");
            }
            else
            {
                Destroy(obj);
            }
        }

        activeTimers.Remove(obj);
    }

    private void OnDestroy()
    {
        activeTimers.Clear();
    }
}
