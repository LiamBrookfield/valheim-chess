using BepInEx;
using Jotunn.Configs;
using Jotunn.Entities;
using Jotunn.Managers;
using Jotunn.Utils;
using UnityEngine;

namespace Chessboard
{
    [BepInPlugin(PluginGUID, PluginName, PluginVersion)]
    [BepInDependency(Jotunn.Main.ModGuid)]
    //[NetworkCompatibility(CompatibilityLevel.EveryoneMustHaveMod, VersionStrictness.Minor)]
    internal class ChessboardPlugin : BaseUnityPlugin
    {
        public const string PluginGUID = "com.liam.chessboard";
        public const string PluginName = "Chessboard";
        public const string PluginVersion = "0.1.0";
        
        // Use this class to add your own localization to the game
        // https://valheim-modding.github.io/Jotunn/tutorials/localization.html
        public static CustomLocalization Localization = LocalizationManager.Instance.GetLocalization();

        private void Awake()
        {
            Jotunn.Logger.LogInfo("Chessboard prepared, all scholarly vikings ready yourselves");

            // Load the embedded AssetBundle.
            var pieceBundle = AssetUtils.LoadAssetBundleFromResources("chessboardassets");
            Jotunn.Logger.LogInfo("Chessboard: Loading embedded AssetBundle");

            if (pieceBundle == null)
            {
                Jotunn.Logger.LogError("Could not load chessboardassets AssetBundle.");
                return;
            }

            // Load the prefab named Cube from the bundle.
            GameObject cubePrefab = pieceBundle.LoadAsset<GameObject>("Cube");
            Jotunn.Logger.LogInfo("Chessboard: Loading cube prefab");

            if (cubePrefab == null)
            {
                Jotunn.Logger.LogError("Could not find Cube prefab in AssetBundle.");
                return;
            }

            // Add the Valheim Piece component if the prefab doesn't have one.
            if (cubePrefab.GetComponent<Piece>() == null)
            {
                cubePrefab.AddComponent<Piece>();
            }

            if (cubePrefab.GetComponent<ZNetView>() == null)
            {
                cubePrefab.AddComponent<ZNetView>();
            }

            Jotunn.Logger.LogInfo("Now configuring chessboard piece.");
            // Configure the buildable piece.
            PieceConfig cubeConfig = new PieceConfig();
            cubeConfig.Name = "Chessboard Cube";
            cubeConfig.Description = "A simple test cube.";
            cubeConfig.PieceTable = PieceTables.Hammer;
            // Temporary test icon: a solid green square.
            Texture2D testIconTexture = new Texture2D(1, 1);
            testIconTexture.SetPixel(0, 0, Color.green);
            testIconTexture.Apply();

            cubeConfig.Icon = Sprite.Create(
                testIconTexture,
                new Rect(0, 0, 1, 1),
                new Vector2(0.5f, 0.5f)
            );

            Jotunn.Logger.LogInfo("Chessboard: Temporary icon created");

            cubeConfig.AddRequirement("Wood", 1);
            Jotunn.Logger.LogInfo("cubeConfig.AddRequirement");
            // Register it with Jötunn.
            PieceManager.Instance.AddPiece(
                new CustomPiece(cubePrefab, false, cubeConfig)
            );

            Jotunn.Logger.LogInfo("Chessboard Cube registered.");
        }


        // To learn more about Jotunn's features, go to
        // https://valheim-modding.github.io/Jotunn/tutorials/overview.html
    }
    }