using UnityEngine;
using UnityEditor;

public class LevelsCreateWindow : EditorWindow
{
    private SerializedObject serializedObject;
    private SerializedProperty serializedProperty;

    protected LevelData[] levels;
    public LevelData newLevel;

    private void OnGUI() {
        serializedObject = new SerializedObject(newLevel);
        serializedProperty = serializedObject.GetIterator();
        serializedProperty.NextVisible(true);
        DrawProperties(serializedProperty);
        if (GUILayout.Button("save"))
        {
            levels = GetAllInstances<LevelData>();
            if (newLevel.LevelsName == null)
            {
                newLevel.LevelsName = "level" + (levels.Length + 1);
            }
            AssetDatabase.CreateAsset(newLevel, "Assets/Resources/Levels/Level_" + (levels.Length + 1) + ".asset");
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            Close();
        }

        Apply();
    }

    public static T[] GetAllInstances<T>() where T : LevelData
    {
        string[] guids = AssetDatabase.FindAssets("t:" + typeof(T).Name);
        T[] result = new T[guids.Length];
        for (int i = 0; i < guids.Length; i++)
        {
            string path = AssetDatabase.GUIDToAssetPath(guids[i]);
            result[i] = AssetDatabase.LoadAssetAtPath<T>(path);
        }

        return result;

    }

    protected void DrawProperties(SerializedProperty property)
    {
        while (property.NextVisible(false))
        {
            EditorGUILayout.PropertyField(property, true);
        }
    }

    protected void Apply()
    {
        serializedObject.ApplyModifiedProperties();
    }
}
