using Jotunn.Configs;
using Jotunn.Entities;
using Jotunn.Managers;
using Jotunn.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Chessboard
{
    internal class ChessPieceBuilder
    {
        private GameObject chessPrefab;
        public bool ConfigureAndRegister() {

            // Load the embedded AssetBundle.
            var pieceBundle = AssetUtils.LoadAssetBundleFromResources("chessboardassets");
            Jotunn.Logger.LogInfo("Chessboard: Loading embedded AssetBundle");

            if (pieceBundle == null)
            {
                Jotunn.Logger.LogError("Could not load chessboardassets AssetBundle.");
                return false;
            }

            // Load the prefab named Cube from the bundle and provide neccesary components.
            GameObject chessPrefab = pieceBundle.LoadAsset<GameObject>("Cube");
            Jotunn.Logger.LogInfo("Chessboard: Loading cube prefab");

            if (chessPrefab == null)
            {
                Jotunn.Logger.LogError("Could not find Cube prefab in AssetBundle.");
                return false;
            }

            // Add the Valheim Piece component if the prefab doesn't have one.
            if (chessPrefab.GetComponent<Piece>() == null)
            {
                chessPrefab.AddComponent<Piece>();
            }

            // Game will crash if you place the chessboard without a ZNetView
            if (chessPrefab.GetComponent<ZNetView>() == null)
            {
                chessPrefab.AddComponent<ZNetView>();
            }

            //Add my own chessboard component
            if (chessPrefab.GetComponent<Chessboard>() == null)
            {
                chessPrefab.AddComponent<Chessboard>();
            }

            Jotunn.Logger.LogInfo("Now configuring chessboard piece.");
            // Configure the buildable piece.
            PieceConfig chessConfig = new PieceConfig();
            chessConfig.Name = "Chessboard Cube";
            chessConfig.Description = "The eventual chess board";
            chessConfig.PieceTable = PieceTables.Hammer;

            // Temporary test icon (a solid green square).
            Texture2D testIconTexture = new Texture2D(1, 1);
            testIconTexture.SetPixel(0, 0, Color.green);
            testIconTexture.Apply();

            chessConfig.Icon = Sprite.Create(
                testIconTexture,
                new Rect(0, 0, 1, 1),
                new Vector2(0.5f, 0.5f)
            );

            Jotunn.Logger.LogInfo("Chessboard: Temporary icon created");

            chessConfig.AddRequirement("Wood", 1);
            Jotunn.Logger.LogInfo("cubeConfig.AddRequirement");
            // Register it with Jötunn.
            PieceManager.Instance.AddPiece(
                new CustomPiece(chessPrefab, false, chessConfig)
            );

            Jotunn.Logger.LogInfo("Chessboard Cube registered.");
            return true;
        }


    }
}
