
using System.IO;
using UnityEditor;
using UnityEngine;

public static class BuildAssetBundles
{
    [MenuItem("Chessboard/Build AssetBundles")]
    private static void BuildAllAssetBundles()
    {
        const string outputPath = "AssetBundles";

        Directory.CreateDirectory(outputPath);

        AssetBundleManifest manifest =
            BuildPipeline.BuildAssetBundles(
                outputPath,
                BuildAssetBundleOptions.None,
                BuildTarget.StandaloneWindows64
            );

        if (manifest == null)
        {
            Debug.LogError("Chessboard AssetBundle build failed.");
            return;
        }

        Debug.Log(
            "Chessboard AssetBundles built to: " +
            Path.GetFullPath(outputPath)
        );
    }
}