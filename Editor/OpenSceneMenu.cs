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
    //[MenuItem("OpenScene/Refresh")]
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
	[MenuItem("OpenScene/Credits")]
	public static void OpenScene_Assets_Credits()
	{
		EditorSceneManager.OpenScene("Assets/Credits.unity");
	}
	[MenuItem("OpenScene/Levels/World1/Level1")]
	public static void OpenScene_Assets_Levels_World1_Level1()
	{
		EditorSceneManager.OpenScene("Assets/Levels/World1/Level1.unity");
	}
	[MenuItem("OpenScene/Levels/World1/Level2")]
	public static void OpenScene_Assets_Levels_World1_Level2()
	{
		EditorSceneManager.OpenScene("Assets/Levels/World1/Level2.unity");
	}
	[MenuItem("OpenScene/Levels/World1/Level3")]
	public static void OpenScene_Assets_Levels_World1_Level3()
	{
		EditorSceneManager.OpenScene("Assets/Levels/World1/Level3.unity");
	}
	[MenuItem("OpenScene/Levels/World1/Level4")]
	public static void OpenScene_Assets_Levels_World1_Level4()
	{
		EditorSceneManager.OpenScene("Assets/Levels/World1/Level4.unity");
	}
	[MenuItem("OpenScene/Levels/World1/Level5")]
	public static void OpenScene_Assets_Levels_World1_Level5()
	{
		EditorSceneManager.OpenScene("Assets/Levels/World1/Level5.unity");
	}
	[MenuItem("OpenScene/Levels/World1/Level6")]
	public static void OpenScene_Assets_Levels_World1_Level6()
	{
		EditorSceneManager.OpenScene("Assets/Levels/World1/Level6.unity");
	}
	[MenuItem("OpenScene/Levels/World1/Level7")]
	public static void OpenScene_Assets_Levels_World1_Level7()
	{
		EditorSceneManager.OpenScene("Assets/Levels/World1/Level7.unity");
	}
	[MenuItem("OpenScene/Levels/World1/Level8")]
	public static void OpenScene_Assets_Levels_World1_Level8()
	{
		EditorSceneManager.OpenScene("Assets/Levels/World1/Level8.unity");
	}
	[MenuItem("OpenScene/Levels/World1/Level9")]
	public static void OpenScene_Assets_Levels_World1_Level9()
	{
		EditorSceneManager.OpenScene("Assets/Levels/World1/Level9.unity");
	}
	[MenuItem("OpenScene/Levels/World2/Level1")]
	public static void OpenScene_Assets_Levels_World2_Level1()
	{
		EditorSceneManager.OpenScene("Assets/Levels/World2/Level1.unity");
	}
	[MenuItem("OpenScene/Levels/World2/Level2")]
	public static void OpenScene_Assets_Levels_World2_Level2()
	{
		EditorSceneManager.OpenScene("Assets/Levels/World2/Level2.unity");
	}
	[MenuItem("OpenScene/Levels/World2/Level3")]
	public static void OpenScene_Assets_Levels_World2_Level3()
	{
		EditorSceneManager.OpenScene("Assets/Levels/World2/Level3.unity");
	}
	[MenuItem("OpenScene/Levels/World2/Level4")]
	public static void OpenScene_Assets_Levels_World2_Level4()
	{
		EditorSceneManager.OpenScene("Assets/Levels/World2/Level4.unity");
	}
	[MenuItem("OpenScene/Levels/World2/Level5")]
	public static void OpenScene_Assets_Levels_World2_Level5()
	{
		EditorSceneManager.OpenScene("Assets/Levels/World2/Level5.unity");
	}
	[MenuItem("OpenScene/Levels/World2/Level6")]
	public static void OpenScene_Assets_Levels_World2_Level6()
	{
		EditorSceneManager.OpenScene("Assets/Levels/World2/Level6.unity");
	}
	[MenuItem("OpenScene/Levels/World2/Level7")]
	public static void OpenScene_Assets_Levels_World2_Level7()
	{
		EditorSceneManager.OpenScene("Assets/Levels/World2/Level7.unity");
	}
	[MenuItem("OpenScene/Levels/World2/Level8")]
	public static void OpenScene_Assets_Levels_World2_Level8()
	{
		EditorSceneManager.OpenScene("Assets/Levels/World2/Level8.unity");
	}
	[MenuItem("OpenScene/Levels/World2/Level9")]
	public static void OpenScene_Assets_Levels_World2_Level9()
	{
		EditorSceneManager.OpenScene("Assets/Levels/World2/Level9.unity");
	}
	[MenuItem("OpenScene/LevelSelection")]
	public static void OpenScene_Assets_LevelSelection()
	{
		EditorSceneManager.OpenScene("Assets/LevelSelection.unity");
	}
	[MenuItem("OpenScene/MainMenu")]
	public static void OpenScene_Assets_MainMenu()
	{
		EditorSceneManager.OpenScene("Assets/MainMenu.unity");
	}
	[MenuItem("OpenScene/Settings")]
	public static void OpenScene_Assets_Settings()
	{
		EditorSceneManager.OpenScene("Assets/Settings.unity");
	}
    #endregion
}
