using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace MDWcfServiceLibrary
{
    [DataContract]
    public class PlayerPropertySets
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

        public void addSet(PropertyCardSet ps)
        {
            playersPropertySets.Add(ps);
        }

        public PlayerPropertySets(PlayerPropertySets playerPropertySetCollection, PlayFieldModel state)
        {
            this.playerGuid = playerPropertySetCollection.playerGuid;

            this.playersPropertySets = playerPropertySetCollection.playersPropertySets.cloneListPropertyCardSet(state);
        }

        internal bool removeEmptySet(PropertyCardSet ps)
        {
            if (playersPropertySets.Remove(ps))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}