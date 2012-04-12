using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace MDWcfServiceLibrary
{
    [DataContract]
    [KnownType(typeof(ActionCard))]
    public class RentStandard : ActionCard
    {
        [DataMember]
        public PropertyColor colorUp;
        [DataMember]
        public PropertyColor colorDown;

        public RentStandard(ActionCardAction actionType, int value, PropertyColor colorUp, PropertyColor colorDown)
            : base(actionType, value, "RENT " + colorUp + "/" + colorDown, "All players pay you rent for properties you own in one of these colours. Play into centre to use")
        {
            //Collect rent from all players on one of the propery groups they hold with one of these colors
            this.colorDown = colorDown;
            this.colorUp = colorUp;
        }

        public RentStandard(RentStandard rentStandard)
            : base(rentStandard.actionType, rentStandard.cardValue, rentStandard.cardName, rentStandard.cardText, rentStandard.cardID, rentStandard.cardGuid, rentStandard.description, rentStandard.MonetaryValueOnly)
        {
            this.colorDown = rentStandard.colorDown;
            this.colorUp = rentStandard.colorUp;
        }

        public override Card clone()
        {
            return new RentStandard(this);
        }
    }
}