using UnityEngine;
using UnityEditor;

public class LevelsEditWindow : EditorWindow
{
    protected SerializedObject serializedObject;
    protected SerializedProperty serializedProperty;

    protected LevelData[] levels;
    protected string selectedPropertyPach;
    protected string selectedProperty;

    [MenuItem("Tile Unity/Levels Edit")]
    private static void ShowWindow() {
        var window = GetWindow<LevelsEditWindow>("Levels");
    }

    private void OnGUI()
    {
        levels = GetAllInstances<LevelData>();
        serializedObject = new SerializedObject(levels[0]);
        EditorGUILayout.BeginHorizontal();

        EditorGUILayout.BeginVertical("box", GUILayout.MaxWidth(100), GUILayout.ExpandHeight(true));

        DrawSliderBar(levels);

        EditorGUILayout.EndVertical();
        EditorGUILayout.BeginVertical("box", GUILayout.ExpandHeight(true));

        if (selectedProperty != null)
        {
            for (int i = 0; i < levels.Length; i++)
            {
                if (levels[i].LevelsName == selectedProperty)
                {
                    serializedObject = new SerializedObject(levels[i]);
                    serializedProperty = serializedObject.GetIterator();
                    serializedProperty.NextVisible(true);
                    DrawProperties(serializedProperty);
                }
            }
        }
        else
        {
            EditorGUILayout.LabelField("select an item from the list");
        }
        EditorGUILayout.EndVertical();
        EditorGUILayout.EndHorizontal();

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

    protected void DrawSliderBar(LevelData[] prop)
    {
        foreach (LevelData p in prop)
        {
            if (GUILayout.Button(p.LevelsName))
            {
                selectedPropertyPach = p.LevelsName;
            }
        }

        if (!string.IsNullOrEmpty(selectedPropertyPach))
        {
            selectedProperty = selectedPropertyPach;
        }

        if (GUILayout.Button("New"))
        {
            LevelData newLevel = ScriptableObject.CreateInstance<LevelData>();
            LevelsCreateWindow newLevelWindow = GetWindow<LevelsCreateWindow>("New Level");
            newLevelWindow.newLevel = newLevel;

        }
    }

    protected void Apply()
    {
        serializedObject.ApplyModifiedProperties();
    }
}
