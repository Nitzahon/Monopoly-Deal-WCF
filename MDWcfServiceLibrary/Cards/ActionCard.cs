﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace MDWcfServiceLibrary
{
    [DataContract]
    public enum ActionCardAction
    {
        [EnumMember]
        NotAnActionCard,
        [EnumMember]
        SlyDeal,
        [EnumMember]
        ForcedDeal,
        [EnumMember]
        DealBreaker,
        [EnumMember]
        JustSayNo,
        [EnumMember]
        DebtCollector,
        [EnumMember]
        ItsMyBirthday,
        [EnumMember]
        RentMultiColor,
        [EnumMember]
        RentStandard,
        [EnumMember]
        DoubleTheRent,
        [EnumMember]
        House,
        [EnumMember]
        Hotel,
        [EnumMember]
        PassGo
    }

    [DataContract]
    [KnownType(typeof(Card))]
    public class ActionCard : Card
    {
        [DataMember]
        public ActionCardAction actionType;
        [DataMember]
        public bool MonetaryValueOnly = false;
        [DataMember]
        public const int Debt_Collector_Value = 5;
        [DataMember]
        public const int Its_My_Birthday_Value = 2;

        public ActionCard(ActionCardAction actionType, int value, String name, String text)
            : base(name, text, value, CardType.Action)
        {
            this.actionType = actionType;
        }

        public ActionCard(ActionCardAction actionType, int value, String name, String text, int id, Guid guid, String description, bool monetaryValueOnly)
            : base(name, text, value, CardType.Action, id, guid)
        {
            this.actionType = actionType;
        }

        public override Card clone()
        {
            ActionCard clone = new ActionCard(this.actionType, this.cardValue, this.cardName, this.cardText, this.cardID, this.cardGuid, this.description, this.MonetaryValueOnly);
            return clone;
        }
    }
}