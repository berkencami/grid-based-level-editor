#if UNITY_EDITOR
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace LevelEditor
{
    [CustomEditor(typeof(Level))]
    public class LevelEditor : Editor
    {
        private SerializedProperty row;
        private SerializedProperty column;
        private SerializedProperty itemPalette;
        private SerializedProperty levelData;
        private int selectedItem = 0;

        private void OnEnable()
        {
            row = serializedObject.FindProperty("row");
            column = serializedObject.FindProperty("column");
            itemPalette = serializedObject.FindProperty("itemPalette");
            levelData = serializedObject.FindProperty("data");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            itemPalette.objectReferenceValue = (ItemPalette)EditorGUILayout.ObjectField("Item Palette",
                itemPalette.objectReferenceValue, typeof(ItemPalette), false);
            ItemPalette newPalette = (ItemPalette)itemPalette.objectReferenceValue;

            EditorGUILayout.Space(20);

            if (newPalette == null)
            {
                EditorGUILayout.LabelField("Please assign a palette");
                return;
            }

            row.intValue = EditorGUILayout.IntSlider("Row", row.intValue, 1, 100);
            column.intValue = EditorGUILayout.IntSlider("Column", column.intValue, 1, 100);


            EditorGUILayout.Space(10);
            EditorGUILayout.LabelField("Items");
            EditorGUILayout.Space(20);
            EditorGUILayout.BeginHorizontal();
            for (int i = 0; i < newPalette.items.Count; i++)
            {
                if (GUILayout.Button(newPalette.items[i].icon, GUILayout.Width(35), GUILayout.Height(35)))
                {
                    selectedItem = i;
                }
            }

            if (GUILayout.Button("Erase", GUILayout.Width(50), GUILayout.Height(35)))
            {
                selectedItem = newPalette.items.Count + 1;
            }

            EditorGUILayout.EndHorizontal();
            EditorGUILayout.Space(20);

            LevelData[] data = GetLevelData(levelData.stringValue, row.intValue, column.intValue);

            Color defaultColor = GUI.backgroundColor;
            data = GenerateGrid(data, row.intValue, column.intValue);

            if (GUILayout.Button("Save Level", GUILayout.Width(100), GUILayout.Height(35)))
            {
                EditorUtility.SetDirty(this);
            }

            GUI.backgroundColor = defaultColor;
            levelData.stringValue = JsonExtension.SetToJson(data.ToArray());
            serializedObject.ApplyModifiedProperties();
        }

        private LevelData[] GetLevelData(string json, int sizeX, int sizeY)
        {
            List<LevelData> data = new List<LevelData>();
            if (!string.IsNullOrEmpty(json))
            {
                data = JsonExtension.GetFromJson<LevelData>(json).ToList();
            }

            if (sizeX * sizeY > 0 && data.Count == sizeX * sizeY)
            {
                return data.ToArray();
            }

            data.Clear();
            for (int x = 0; x < sizeX; x++)
            {
                for (int y = 0; y < sizeY; y++)
                {
                    data.Add(new LevelData
                    {
                        row = x,
                        column = y,
                        itemID = string.Empty
                    });
                }
            }

            return data.ToArray();
        }

        private LevelData[] GenerateGrid(LevelData[] data, int sizeX, int sizeY)
        {
            if (sizeX <= 0 || sizeY <= 0)
            {
                EditorGUILayout.EndFoldoutHeaderGroup();
                return data;
            }

            EditorGUILayout.Space(5);
            EditorGUILayout.BeginHorizontal();

            if (GUILayout.Button("Clear"))
            {
                data = GetLevelData(string.Empty, sizeX, sizeY);
            }

            EditorGUILayout.EndHorizontal();
            EditorGUILayout.Space(10);

            Color backgroundColor = GUI.backgroundColor;
            ItemPalette palette = this.itemPalette.objectReferenceValue as ItemPalette;

            for (int y = 0; y < sizeY; y++)
            {
                EditorGUILayout.BeginHorizontal();

                for (int x = 0; x < sizeX; x++)
                {
                    LevelData item = data.FirstOrDefault(fd => fd.row == x && fd.column == y);
                    string id = item.itemID ?? string.Empty;
                    Texture icon = string.IsNullOrEmpty(id)
                        ? default
                        : (palette.items.FirstOrDefault(fd => fd.id == id)?.icon ?? default);

                    bool button = GUILayout.Button(icon, GUILayout.Width(35), GUILayout.Height(35));
                    if (button)
                    {
                        item.itemID = selectedItem < 0 || selectedItem >= palette.items.Count
                            ? string.Empty
                            : palette.items[selectedItem].id;
                    }
                }

                EditorGUILayout.EndHorizontal();
            }

            EditorGUILayout.EndFoldoutHeaderGroup();
            GUI.backgroundColor = backgroundColor;
            EditorGUILayout.Space(30);
            return data;
        }
    }
}
#endif