using System;
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
        SlyDeal,
        ForcedDeal,
        DealBreaker,
        JustSayNo,
        DebtCollector,
        ItsMyBirthday,
        RentMultiColor,
        RentStandard,
        DoubleTheRent,
        House,
        Hotel,
        PassGo
    }

    [DataContract]
    public class ActionCard : Card
    {
        [DataMember]
        public ActionCardAction actionType;
        [DataMember]
        public bool MonetaryValueOnly = false;

        public ActionCard(ActionCardAction actionType, int value, String name, String text)
            : base(name, text, value, CardType.Action)
        {
            this.actionType = actionType;
        }
    }
}