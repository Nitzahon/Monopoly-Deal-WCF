using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MDWcfServiceLibrary
{
    internal class TurnActionModel
    {
        public enum TurnActionTypes
        {
            gameStarted,
            drawTwoCardsAtStartOfTurn
        }

        public TurnActionModel()
        {
        }
    }
}