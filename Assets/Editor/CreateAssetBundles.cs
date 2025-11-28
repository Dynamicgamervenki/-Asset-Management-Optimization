using UnityEngine;
using UnityEditor;

public class CreateAssetBundles 
{
    [MenuItem("Assets/Build AssetBundles")]
    private static void BuildAssetBundles()
    {
        string assetBundleDirectory = Application.dataPath + "/AssetBundles";
        try
        {
            BuildPipeline.BuildAssetBundles(assetBundleDirectory,
                BuildAssetBundleOptions.None,
               EditorUserBuildSettings.activeBuildTarget);
        }
        catch (System.Exception e)
        {
            Debug.LogError("Failed to create AssetBundles: " + e.Message);
        }
    }
}
