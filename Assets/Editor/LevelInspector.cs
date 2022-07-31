using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using System;

[CustomEditor(typeof(Level))]
public class LevelInspector : Editor {

    public override void OnInspectorGUI() {
        base.OnInspectorGUI();
        Level level = (Level)target;
        if (level.tiles == null || level.tiles.Length != level.width * level.height) {
            level.tiles = new int[level.width * level.height];
            Array.Fill(level.tiles, -1);
        }

        for (int i = 0; i < level.height; i++) {

            EditorGUILayout.BeginHorizontal();
            for (int j = 0; j < level.width; j++) {
                int pos = i * level.width + j;
                string value = EditorGUILayout.TextField("", level.tiles[pos].ToString(), GUILayout.Width(20));
                level.tiles[pos] = int.Parse(value);

            }
            EditorGUILayout.EndHorizontal();
        }

        if (GUILayout.Button("Save")) {
            EditorUtility.SetDirty(level);
            AssetDatabase.SaveAssets();
        }
    }
}
