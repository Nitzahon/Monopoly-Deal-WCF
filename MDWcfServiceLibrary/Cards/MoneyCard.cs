using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace MDWcfServiceLibrary
{
    //Represents Money cards
    [DataContract]
    [KnownType(typeof(Card))]
    public class MoneyCard : Card
    {
        public MoneyCard(int value)
            : base("$" + value + "M", "$" + value + "M", value, CardType.Money)
        {
            //Done
        }
    }
}