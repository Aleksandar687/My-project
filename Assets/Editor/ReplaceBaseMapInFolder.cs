using UnityEditor;
using UnityEngine;
using System.IO;

public class ReplaceBaseMapInFolder : EditorWindow
{
    Texture2D oldTexture;
    Texture2D newTexture;
    string folderPath = "Assets/Materials/FolderName"; // Change this to your target folder

    [MenuItem("Tools/Replace Base Map In Folder")]
    public static void ShowWindow()
    {
        GetWindow<ReplaceBaseMapInFolder>("Replace Base Map in Folder");
    }

    void OnGUI()
    {
        oldTexture = (Texture2D)EditorGUILayout.ObjectField("Old Texture", oldTexture, typeof(Texture2D), false);
        newTexture = (Texture2D)EditorGUILayout.ObjectField("New Texture", newTexture, typeof(Texture2D), false);
        folderPath = EditorGUILayout.TextField("Folder Path", folderPath);

        if (GUILayout.Button("Replace Base Map"))
        {
            ReplaceBaseMapsInFolder(folderPath, oldTexture, newTexture);
        }
    }

    static void ReplaceBaseMapsInFolder(string folder, Texture2D oldTex, Texture2D newTex)
    {
        if (!AssetDatabase.IsValidFolder(folder))
        {
            Debug.LogError($"Invalid folder path: {folder}");
            return;
        }

        string[] guids = AssetDatabase.FindAssets("t:Material", new[] { folder });
        int changedCount = 0;

        foreach (string guid in guids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            Material mat = AssetDatabase.LoadAssetAtPath<Material>(path);

            if (mat != null && mat.HasProperty("_BaseMap"))
            {
                Texture current = mat.GetTexture("_BaseMap");
                if (current == oldTex)
                {
                    mat.SetTexture("_BaseMap", newTex);
                    EditorUtility.SetDirty(mat);
                    changedCount++;
                }
            }
        }

        AssetDatabase.SaveAssets();
        Debug.Log($"Updated {changedCount} materials in folder '{folder}'.");
    }
}
