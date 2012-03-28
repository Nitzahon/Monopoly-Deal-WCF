using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace MDWcfServiceLibrary
{
    [DataContract]
    internal class PlayerPropertySets
    {
        [DataMember]//Cards this player has
        public List<PropertyCardSet> playersPropertySets = new List<PropertyCardSet>();
        [DataMember]//Guid of the player whoes hand this is
        public Guid playerGuid;

        public PlayerPropertySets(Guid playerP, List<PropertyCardSet> cardsP)
        {
            playerGuid = playerP;
            playersPropertySets = cardsP;
        }
    }
}