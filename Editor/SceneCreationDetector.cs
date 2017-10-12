using UnityEngine;
using System.Collections;
using UnityEditor;

public class SceneCreationDetector : AssetPostprocessor
{

    static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
    {
        bool shouldReferesh = false;
        foreach (string str in importedAssets)
        {
            if(str.Contains(".unity"))
            {
                shouldReferesh = true;
            }
        }
        foreach (string str in deletedAssets)
        {
            if (str.Contains(".unity"))
            {
                shouldReferesh = true;
            }
        }

        for (int i = 0; i < movedAssets.Length; i++)
        {
           if (movedAssets[i].Contains(".unity"))
            {
                shouldReferesh = true;
            }
        }
        if (shouldReferesh)
            OpenSceneMenu.Refresh();
    }
}
