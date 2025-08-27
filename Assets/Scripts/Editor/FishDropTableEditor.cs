#if UNITY_EDITOR // This ensures this script is only included in the editor and not in builds.
using UnityEngine;
using UnityEditor;
using System.Linq;

/// <summary>
/// Custom editor for the FishLootTable ScriptableObject.
/// Renders the list of weight entries in a clean, table-like format.
/// </summary>
[CustomEditor(typeof(FishDropTable))]
public class FishDropTableEditor : Editor
{
    private SerializedProperty weightEntriesProperty;

    private void OnEnable()
    {
        weightEntriesProperty = serializedObject.FindProperty("weightEntries");
    }

    public override void OnInspectorGUI()
    {
        // Update the serializedObject to reflect the latest state of the object.
        serializedObject.Update();

        // --- TABLE HEADER ---
        EditorGUILayout.LabelField("Fish Weight Over Time", EditorStyles.boldLabel);
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Time", GUILayout.Width(70));
        EditorGUILayout.LabelField("Small", GUILayout.MinWidth(30));
        EditorGUILayout.LabelField("Normal", GUILayout.MinWidth(30));
        EditorGUILayout.LabelField("Big", GUILayout.MinWidth(30));
        EditorGUILayout.LabelField("Huge", GUILayout.MinWidth(30));
        EditorGUILayout.LabelField("Actions", GUILayout.Width(50));
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.Space();

        // --- TABLE ROWS ---
        for (int i = 0; i < weightEntriesProperty.arraySize; i++)
        {
            SerializedProperty entry = weightEntriesProperty.GetArrayElementAtIndex(i);

            EditorGUILayout.BeginHorizontal();

            // Draw fields for each property in the FishWeightEntry
            EditorGUILayout.PropertyField(entry.FindPropertyRelative("Time"), GUIContent.none, GUILayout.Width(70));
            EditorGUILayout.PropertyField(entry.FindPropertyRelative("SmallFish"), GUIContent.none, GUILayout.MinWidth(30));
            EditorGUILayout.PropertyField(entry.FindPropertyRelative("Fish"), GUIContent.none, GUILayout.MinWidth(30));
            EditorGUILayout.PropertyField(entry.FindPropertyRelative("BigFish"), GUIContent.none, GUILayout.MinWidth(30));
            EditorGUILayout.PropertyField(entry.FindPropertyRelative("HugeFish"), GUIContent.none, GUILayout.MinWidth(30));

            // "Remove" button
            if (GUILayout.Button("X", GUILayout.Width(50)))
            {
                weightEntriesProperty.DeleteArrayElementAtIndex(i);
                break;
            }

            EditorGUILayout.EndHorizontal();
        }

        EditorGUILayout.Space();

        // --- ADD NEW ROW BUTTON ---
        if (GUILayout.Button("Add New Weight Entry"))
        {
            weightEntriesProperty.InsertArrayElementAtIndex(weightEntriesProperty.arraySize);
        }

        SortEntriesByTime();

        // Apply any changes made in the GUI to the actual object.
        serializedObject.ApplyModifiedProperties();
    }

    private void SortEntriesByTime()
    {
        FishDropTable lootTable = (FishDropTable)target;
        if (lootTable.weightEntries != null)
        {
            lootTable.weightEntries = lootTable.weightEntries.OrderBy(entry => entry.Time).ToList();
        }
    }
}
#endif
