using UnityEngine.SceneManagement;
using UnityEngine;
using System;


public class Teleport : MonoBehaviour
{
    [SerializeReference]
    private ITeleportLogic logic;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && logic != null)
        {
            logic.Execute(other.gameObject);
        }
    }
}

public interface ITeleportLogic
{
    void Execute(GameObject player);
}

[Serializable]
public class LocalTeleport : ITeleportLogic
{
    public GameObject targetTeleport;

    public void Execute(GameObject player)
    {
        if (targetTeleport != null) player.transform.position = targetTeleport.transform.position;
    }
}

[Serializable]
public class NextLevelTeleport : ITeleportLogic
{
    public void Execute(GameObject player)
    {
        int nextScene = SceneManager.GetActiveScene().buildIndex + 1;
        if (nextScene < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextScene);
        }
    }
}