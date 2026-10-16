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

            ChessPieceBuilder pieceBuilder = new ChessPieceBuilder();

            bool registered = pieceBuilder.ConfigureAndRegister();

            if (!registered)
            {
                Jotunn.Logger.LogInfo("Chessboard piece setup failed");
                return;
            }
            Jotunn.Logger.LogInfo("Chessboard prepared, all scholarly vikings ready yourselves");

        }


        // To learn more about Jotunn's features, go to
        // https://valheim-modding.github.io/Jotunn/tutorials/overview.html
    }
    }