using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace MDWcfServiceLibrary
{
    [DataContract]
    public class PlayerBank
    {
        [DataMember]//Cards this player has
        public List<Card> cardsInBank = new List<Card>();
        [DataMember]//Guid of the player whoes hand this is
        public Guid playerGuid;

        public PlayerBank(Guid playerP, List<Card> cardsP)
        {
            playerGuid = playerP;
            cardsInBank = cardsP;
        }

        public void addCardToBank(Card card)
        {
            cardsInBank.Add(card);
        }

        public PlayerBank(PlayerBank bank, PlayFieldModel state)
        {
            this.playerGuid = bank.playerGuid;
            this.cardsInBank = bank.cardsInBank.cloneListCard(state.deck);
        }

        public Card removeCardFromBank(Card card)
        {
            foreach (Card bankedCard in cardsInBank)
            {
                if (bankedCard.cardID.CompareTo(card.cardID) == 0)
                {
                    if (cardsInBank.Remove(bankedCard))
                    {
                        return bankedCard;
                    }
                    else
                    {
                        throw new Exception("Card is in bank but is unable to be removed. Error");
                    }
                }
            }
            return null;
        }
    }
}