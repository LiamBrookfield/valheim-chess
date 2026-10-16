using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Chessboard
{
    internal class Chessboard : MonoBehaviour, Interactable, Hoverable
    {
        private ChessGUI chessGUI = new ChessGUI();

        public string GetHoverName()
        {
            return "Chessboard";
        }

        public float GetHoverOffset()
        {
            return 0f;
        }

        public string GetHoverText()
        {
            return "[E] Use";
        }

        public bool Interact(Humanoid user, bool hold, bool alt)
        {
            MessageHud.instance.ShowMessage(MessageHud.MessageType.Center, "Chess interaction looking good");

            chessGUI.MakeChessPanel();

            return true;
        }

        public bool UseItem(Humanoid user, ItemDrop.ItemData item)
        {
            return false;
        }
    }
}
