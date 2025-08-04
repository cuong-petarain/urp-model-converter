using UnityEditor;
using UnityEditor.Formats.Fbx.Exporter;
using UnityEngine;
using System.IO;

public class CopilotFbxExporter : EditorWindow
{
    private string sourceFolderPath = "Assets/Furniture_Cute/Meshes2";
    private string exportFolderPath = "Assets/Converted-Meshes";

    [MenuItem("Tools/Batch FBX Exporter (Copilot)")]
    public static void ShowWindow()
    {
        GetWindow<CopilotFbxExporter>("Batch FBX Exporter");
    }

    private void OnGUI()
    {
        GUILayout.Label("Batch FBX Exporter", EditorStyles.boldLabel);

        // Source folder selection
        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Source Folder:");
        sourceFolderPath = EditorGUILayout.TextField(sourceFolderPath);
        if (GUILayout.Button("Browse"))
        {
            string selectedPath = EditorUtility.OpenFolderPanel("Select Source Folder", "Assets", "");
            if (!string.IsNullOrEmpty(selectedPath))
            {
                sourceFolderPath = ConvertToRelativePath(selectedPath);
            }
        }
        EditorGUILayout.EndHorizontal();

        // Export folder selection
        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Export Folder:");
        exportFolderPath = EditorGUILayout.TextField(exportFolderPath);
        if (GUILayout.Button("Browse"))
        {
            string selectedPath = EditorUtility.OpenFolderPanel("Select Export Folder", "Assets", "");
            if (!string.IsNullOrEmpty(selectedPath))
            {
                exportFolderPath = ConvertToRelativePath(selectedPath);
            }
        }
        EditorGUILayout.EndHorizontal();

        if (GUILayout.Button("Export All FBX"))
        {
            ExportAllFBXInFolder(sourceFolderPath, exportFolderPath);
        }
    }

    private void ExportAllFBXInFolder(string sourceFolderPath, string exportFolderPath)
    {
        if (!Directory.Exists(sourceFolderPath))
        {
            Debug.LogError($"Source folder path does not exist: {sourceFolderPath}");
            return;
        }

        if (!Directory.Exists(exportFolderPath))
        {
            Directory.CreateDirectory(exportFolderPath);
        }

        string[] guids = AssetDatabase.FindAssets("t:Model", new[] { sourceFolderPath });
        foreach (string guid in guids)
        {
            string assetPath = AssetDatabase.GUIDToAssetPath(guid);
            GameObject model = AssetDatabase.LoadAssetAtPath<GameObject>(assetPath);

            if (model != null)
            {
                string exportPath = Path.Combine(exportFolderPath, $"{model.name}.fbx");
                ExportFBX(model, exportPath);
            }
        }

        Debug.Log("FBX export completed.");
    }

    private void ExportFBX(GameObject model, string exportPath)
    {
        // Export the model to FBX (binary format is used by default)
        ModelExporter.ExportObject(exportPath, model);
        Debug.Log($"Exported {model.name} to {exportPath}");
    }

    private string ConvertToRelativePath(string absolutePath)
    {
        if (absolutePath.StartsWith(Application.dataPath))
        {
            return "Assets" + absolutePath.Substring(Application.dataPath.Length);
        }
        return absolutePath;
    }
}