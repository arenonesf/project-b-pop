using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class FindMaterialsUsingShader : EditorWindow
{
    private Shader customShader;
    private List<Material> foundMaterials = new List<Material>();

    [MenuItem("Tools/Find Materials Using Shader")]
    public static void ShowWindow()
    {
        GetWindow<FindMaterialsUsingShader>("Find Materials Using Shader");
    }

    private void OnGUI()
    {
        GUILayout.Label("Find Materials Using Custom Shader", EditorStyles.boldLabel);

        customShader = EditorGUILayout.ObjectField("Shader", customShader, typeof(Shader), false) as Shader;

        if (GUILayout.Button("Find Materials"))
        {
            FindMaterials();
        }

        if (foundMaterials.Count > 0)
        {
            GUILayout.Label("Materials using the shader:");
            foreach (var mat in foundMaterials)
            {
                EditorGUILayout.ObjectField(mat, typeof(Material), false);
            }
        }
    }

    private void FindMaterials()
    {
        foundMaterials.Clear();

        if (customShader == null)
        {
            Debug.LogError("Please assign a shader.");
            return;
        }

        string[] materialGUIDs = AssetDatabase.FindAssets("t:Material");
        foreach (string guid in materialGUIDs)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            Material mat = AssetDatabase.LoadAssetAtPath<Material>(path);

            if (mat != null && mat.shader == customShader)
            {
                foundMaterials.Add(mat);
            }
        }

        Debug.Log($"Found {foundMaterials.Count} materials using the shader {customShader.name}.");
    }
}
