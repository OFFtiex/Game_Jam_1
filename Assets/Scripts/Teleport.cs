using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Teleport : MonoBehaviour
{
    private static float nextTeleportTime;

    [SerializeReference]
    private ITeleportLogic logic;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && logic != null && Time.time >= nextTeleportTime)
        {
            nextTeleportTime = Time.time + 0.5f;

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


// ==========================================
// CUSTOM INSPECTOR (Will only work in the editor)
// ==========================================

#if UNITY_EDITOR

[CustomEditor(typeof(Teleport))]
public class TeleportEditor : Editor
{
    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        SerializedProperty logicProperty = serializedObject.FindProperty("logic");

        int currentIndex = logicProperty.managedReferenceValue switch
        {
            NextLevelTeleport => 2,
            LocalTeleport => 1,
            _ => 0
        };

        string[] options = { "Not selected", "Doubles", "Next level" };

        EditorGUI.BeginChangeCheck();
        int newIndex = EditorGUILayout.Popup("Teleport type", currentIndex, options);

        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(target, "Change Teleport Type");

            if (newIndex == 0)
            {
                logicProperty.managedReferenceValue = null;
            }
            else
            {
                System.Type targetType = newIndex == 2 ? typeof(NextLevelTeleport) : typeof(LocalTeleport);

                object newInstance = System.Activator.CreateInstance(targetType);
                logicProperty.managedReferenceValue = newInstance;
            }

            EditorUtility.SetDirty(target);
        }

        if (newIndex > 0 && logicProperty.managedReferenceValue != null)
        {
            EditorGUILayout.Space(5);
            EditorGUILayout.LabelField("Teleport Settings", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(logicProperty, GUIContent.none, true);
        }

        serializedObject.ApplyModifiedProperties();
    }
}
#endif