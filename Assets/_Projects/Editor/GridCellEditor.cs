using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(CellBuilder))]
[CanEditMultipleObjects]
public class GridCellEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        EditorGUILayout.Space(15);
        EditorGUILayout.LabelField("CHANGE CELL TYPE", EditorStyles.boldLabel);

        EditorGUILayout.BeginHorizontal();
        GUI.backgroundColor = new Color(.2f, .4f, .9f);
        if (GUILayout.Button("WALL CELL", GUILayout.Height(30)))
            SetTypeForSelected(CellType.Wall);


        GUI.backgroundColor = new Color(1f, .75f, .4f);
        if (GUILayout.Button("START CELL", GUILayout.Height(30)))
            SetTypeForSelected(CellType.Start);

        GUI.backgroundColor = Color.white;
        if (GUILayout.Button("EMPTY CELL", GUILayout.Height(30)))
            SetTypeForSelected(CellType.Empty);

        EditorGUILayout.EndHorizontal();
        GUI.backgroundColor = Color.white;
    }

    private void SetTypeForSelected(CellType cellType)
    {
        foreach (Object obj in targets)
        {
            if (obj is CellBuilder cell)
            {
                cell.SetType(cellType);
                EditorUtility.SetDirty(cell);
            }
        }
    }
}
