using UnityEditor;
using UnityEditor.Formats.Fbx.Exporter;
using UnityEngine;
using System.IO;

public class BatchFbxExporterBinary : EditorWindow
{
    private DefaultAsset inputFolder;
    private string outputFolder = "Converted-Meshes";

    [MenuItem("Tools/Batch FBX Exporter (Binary)")]
    public static void ShowWindow()
    {
        GetWindow(typeof(BatchFbxExporterBinary), false, "FBX Exporter (Binary)");
    }

    void OnGUI()
    {
        GUILayout.Label("Batch FBX Export (Binary)", EditorStyles.boldLabel);

        inputFolder = (DefaultAsset)EditorGUILayout.ObjectField("Input Folder", inputFolder, typeof(DefaultAsset), false);
        outputFolder = EditorGUILayout.TextField("Output Folder (relative to Assets/)", outputFolder);

        if (GUILayout.Button("Export All Prefabs"))
        {
            if (inputFolder == null)
            {
                Debug.LogError("Please assign an input folder.");
                return;
            }

            string inputPath = AssetDatabase.GetAssetPath(inputFolder);
            string fullOutputPath = Path.Combine(Application.dataPath, outputFolder);

            if (!Directory.Exists(fullOutputPath))
                Directory.CreateDirectory(fullOutputPath);

            ExportPrefabs(inputPath, fullOutputPath);
        }
    }

    void ExportPrefabs(string inputPath, string outputPath)
    {
        string[] guids = AssetDatabase.FindAssets("t:GameObject", new[] { inputPath });

        foreach (var guid in guids)
        {
            string assetPath = AssetDatabase.GUIDToAssetPath(guid);
            GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(assetPath);
            if (prefab == null)
                continue;

            string safeName = prefab.name.Replace(" ", "_");
            string outputFilePath = Path.Combine(outputPath, safeName + ".fbx");

            Debug.Log($"Exporting {prefab.name} to: {outputFilePath}");

            // Use ModelExporter with basic parameters
            ModelExporter.ExportObject(outputFilePath, prefab);
        }

        AssetDatabase.Refresh();
    }
}
