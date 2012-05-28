using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace MDWcfServiceLibrary
{
    public class ActionCardEvent
    {
        public static int nextID = 0;

        public static int generateID()
        {
            int id = nextID;
            nextID++;
            return id;
        }

        [DataMember]
        public int actionCardEventID;
        [DataMember]
        public int originalActionCardId;
        [DataMember]
        public TurnActionTypes actionTypeTaken;
        [DataMember]
        public ActionCardAction actionCardTypeUsed;
        [DataMember]
        public Guid playerWhoPerformedActionOnTurn;
        [DataMember]
        public Guid playerAffectedByAction;
        [DataMember]
        public List<int> bankedCardsTakenFromPlayer;
        [DataMember]
        public int propertySetTakenFromPlayer;
        [DataMember]
        public int[][] propertyCardsTakenFromPlayerAndSetTheCardWasIn;// [x][y] where x is the propertyCard id and y is the propertySet id
        [DataMember]
        public bool actionJustSayNoUsedByAffectedPlayer;
        [DataMember]
        public bool actionJustSayNoUsedByOnTurnPlayer;
        [DataMember]
        public int debtAmount;
        [DataMember]
        public bool doubleTheRentCardUsed;
    }
}