using UnityEditor;
using System.Reflection;
using System;
using System.IO;
using System.Linq;
using UnityEditor.SceneManagement;
using System.Text.RegularExpressions;
public class OpenSceneMenu {
    private static bool initialized = false;
    private static string thisScriptLocation;
    [MenuItem("OpenScene/Refresh")]
    public static void Refresh()
    {
        if (initialized) 
            LoadMenuItems();
        else
            Initialize();
    }
    void Awake() { Initialize(); }
    static void LoadMenuItems()
    {
        string[] allAssets = AssetDatabase.GetAllAssetPaths();
        ClearMenu();
        Array.Sort(allAssets);
        Array.Reverse(allAssets);
        //find All Scenes
        foreach (string asset in allAssets)
        {
            int indexUnity = asset.IndexOf(".unity");
            if (indexUnity != -1)
            {
                AddSceneMenuItem(asset);
            }
        }

        AssetDatabase.ImportAsset(thisScriptLocation);
    }
    static void Initialize()
    {
        if (initialized) { return; }

        string[] allAssets = AssetDatabase.GetAllAssetPaths();
        string thisFileName = MethodBase.GetCurrentMethod().DeclaringType.ToString();
        foreach (string asset in allAssets)
        {
            if (asset.Contains(thisFileName))
            { 
                thisScriptLocation = asset;
                break;
            }
        }

        LoadMenuItems();
        initialized = true;
    }
    public static void AddSceneMenuItem(string sceneFullName)
    {
        string functionName= sceneFullName.Substring(0, sceneFullName.Length-6).Replace('/', '_').Replace(" ", "").Trim();
        //replace special characters with _
        functionName = Regex.Replace(functionName, @"[^0-9a-zA-Z]+", "_");
        //remove the "Assets\" part of the path
        string menuPath = sceneFullName.Substring(7);
        menuPath = menuPath.Replace(".unity", "");
        string functionBody = "\r\n\tpublic static void OpenScene_" + functionName + "()" +
                              "\r\n\t{\r\n\t\tEditorSceneManager.OpenScene(\"" + sceneFullName.Trim() + "\");\r\n\t}";
        AddMenuItem(menuPath, functionBody);

    }
    public static void ClearMenu()
    {
        var txtLines = File.ReadAllLines(thisScriptLocation).ToList();
        int startIndex = txtLines.FindLastIndex(s => s.Contains("#region Scenes Menu Functions")) + 1;
        int endIndex = txtLines.FindLastIndex(s => s.Contains("#endregion"));
        txtLines.RemoveRange(startIndex, endIndex - startIndex );
        File.WriteAllLines(thisScriptLocation, txtLines.ToArray());
    }
    public static void AddMenuItem(string path,string functionBody)
    {
        var txtLines = File.ReadAllLines(thisScriptLocation).ToList();
        int startIndex = txtLines.FindLastIndex(s => s.Contains("#region Scenes Menu Functions")) + 1;
        string attribute = string.Format("\t[MenuItem(\"OpenScene/{0}\")]", path);
        txtLines.Insert(startIndex, attribute  + functionBody );
        File.WriteAllLines(thisScriptLocation, txtLines.ToArray());
    }
    
    #region Scenes Menu Functions
    #endregion
}
