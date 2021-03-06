﻿using System;
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
        public Guid playerWhoPerformedActionOffTurn;
        [DataMember]
        public bool playerOnTurnPerformingAction;//False if it is not on turn player making move e.x. using just say no, paying debt, not using just say no
        [DataMember]
        public Guid playerAffectedByAction;
        [DataMember]
        public List<int> bankedCardsTakenFromPlayer;
        [DataMember]
        public Guid propertySetTakenFromPlayer;
        [DataMember]
        public CardIDSetGuid propertyCardGivenUpInForcedDeal;
        [DataMember]
        public List<CardIDSetGuid> propertyCardsTakenFromPlayerAndSetTheCardWasIn;// [x][y] where x is the propertyCard id and y is the propertySet id
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