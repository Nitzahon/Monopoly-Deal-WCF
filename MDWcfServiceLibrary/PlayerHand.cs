using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace MDWcfServiceLibrary
{
    [DataContract]
    internal class PlayerHand
    {
        [DataMember]//Cards this player has
        public List<Card> cardsInHand = new List<Card>();
        [DataMember]//Guid of the player whoes hand this is
        public Guid playerGuid;

        public PlayerHand(Guid playerP, List<Card> cardsP)
        {
            playerGuid = playerP;
            cardsInHand = cardsP;
        }
    }
}