using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(GridBuilder))]
public class GridBuilderEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        GridBuilder gridBuilder = (GridBuilder)target;

        EditorGUILayout.Space(15);
        EditorGUILayout.LabelField("LEVEL EDITOR", EditorStyles.boldLabel);

        EditorGUILayout.BeginHorizontal();
        GUI.backgroundColor = new Color(.3f, .7f, 1f);
        if (GUILayout.Button("Generate Grid", GUILayout.Height(35)))
            gridBuilder.GenerateGrid();

        GUI.backgroundColor = new Color(1f, .3f, .3f);
        if (GUILayout.Button("Clear Grid", GUILayout.Height(35)))
        {
            if (EditorUtility.DisplayDialog("Clear Grid", "Do you want to clear grid?", "Yes", "No"))
                gridBuilder.ClearGrid();
        }
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.Space(10);
        EditorGUILayout.BeginHorizontal();
        GUI.backgroundColor = new Color(.3f, 1f, .4f);
        if (GUILayout.Button("Save Grid", GUILayout.Height(35)))
            gridBuilder.SaveToLevelSO();

        GUI.backgroundColor = new Color(1f, .75f, .3f);
        if (GUILayout.Button("Load Grid", GUILayout.Height(35)))
            gridBuilder.LoadFromLevelSO();

        EditorGUILayout.EndHorizontal();
        GUI.backgroundColor = Color.white;
    }
}
