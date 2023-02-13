using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor;

public class CreateFolders : EditorWindow
{
    static string projectName = "PROJECT_NAME";
    static string assetsPath = "Assets/";

    [MenuItem("Assets/Create Default Folders")]
    static void SetupFolders()
    {
        CreateFolders window = ScriptableObject.CreateInstance<CreateFolders>();
        window.position = new Rect(Screen.width / 2, Screen.height / 2, 400f, 150f);
        window.ShowPopup();
    }

    static void CreateAllFolders()
    {
        List<string> folders = new List<string>()
        {
            "Animations",
            "Audio",
            "Editor",
            "Materials",
            "Meshes",
            "Particles",
            "Prefabs",
            "_Scripts",
            "Settings",
            "Scenes",
            "Shaders",
            "Textures",
            "Third Party",
            "UI",
        };

        foreach (string folder in folders)
        {
            if (!Directory.Exists("Assets/" + folder))
            {
                Directory.CreateDirectory(assetsPath + projectName + "/" + folder);
            }
        }



        List<string> uiFolders = new List<string>()
        {
            "Assets",
            "Fonts",
            "Icon",
        };

        foreach (string subFolder in uiFolders)
        {
            if (!Directory.Exists(assetsPath + projectName + "/UI/" + subFolder))
            {
                Directory.CreateDirectory(assetsPath + projectName + "/UI/" + subFolder);
            }
        }

        AssetDatabase.Refresh();
    }

    private void OnGUI()
    {
        EditorGUILayout.LabelField("Insert the Project name used as the root folder");
        projectName = EditorGUILayout.TextField("Project Name:", projectName);
        this.Repaint();
        GUILayout.Space(70);
        if (GUILayout.Button("Generate!"))
        {
            CreateAllFolders();
            this.Close();
        }
    }
}
