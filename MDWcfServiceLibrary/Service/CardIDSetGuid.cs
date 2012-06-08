using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace MDWcfServiceLibrary
{
    [DataContract]
    public class CardIDSetGuid
    {
        [DataMember]
        public int cardID;
        [DataMember]
        public Guid setGuid;

        public CardIDSetGuid(int cardIDP, Guid setGuidP)
        {
            cardID = cardIDP;
            setGuid = setGuidP;
        }
    }
}