
using Jotunn.GUI;
using Jotunn.Managers;
using UnityEngine;
using UnityEngine.UI;

namespace Chessboard
{
    internal class ChessGUI
    {
        private GameObject chessPanel;

        public void MakeChessPanel()
        {
            if (GUIManager.Instance == null ||
                GUIManager.CustomGUIFront == null)
            {
                Jotunn.Logger.LogError(
                    "GUIManager or CustomGUIFront is null");
                return;
            }

            // Don't create another panel if one already exists.
            if (chessPanel != null)
            {
                OpenChessPanel();
                return;
            }

            chessPanel = GUIManager.Instance.CreateWoodpanel(
                parent: GUIManager.CustomGUIFront.transform,
                anchorMin: new Vector2(0.5f, 0.5f),
                anchorMax: new Vector2(0.5f, 0.5f),
                position: Vector2.zero,
                width: 1200,
                height: 1200,
                draggable: false);

            chessPanel.AddComponent<DragWindowCntrl>();

            GUIManager.Instance.CreateText(
                text: "Finally, chess",
                parent: chessPanel.transform,
                anchorMin: new Vector2(0.5f, 1f),
                anchorMax: new Vector2(0.5f, 1f),
                position: new Vector2(0f, -50f),
                font: GUIManager.Instance.AveriaSerifBold,
                fontSize: 30,
                color: GUIManager.Instance.ValheimOrange,
                outline: true,
                outlineColor: Color.black,
                width: 350f,
                height: 40f,
                addContentSizeFitter: false);

            GameObject buttonObject = GUIManager.Instance.CreateButton(
                text: "Close Chess Panel",
                parent: chessPanel.transform,
                anchorMin: new Vector2(0.5f, 0.5f),
                anchorMax: new Vector2(0.5f, 0.5f),
                position: new Vector2(0, -250f),
                width: 250f,
                height: 60f);

            Button button = buttonObject.GetComponent<Button>();

            // This listener closes the existing panel.
            button.onClick.AddListener(CloseChessPanel);

            // Start hidden; open it through the explicit method.
            chessPanel.SetActive(false);
            OpenChessPanel();
        }

        public void OpenChessPanel()
        {
            if (chessPanel == null)
                return;

            chessPanel.SetActive(true);
            GUIManager.BlockInput(true);
        }

        public void CloseChessPanel()
        {
            if (chessPanel == null)
                return;

            chessPanel.SetActive(false);
            GUIManager.BlockInput(false);
        }
    }
}
