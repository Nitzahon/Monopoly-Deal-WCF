using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace MDWcfServiceLibrary
{
    [DataContract]
    public class TurnActionModel
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