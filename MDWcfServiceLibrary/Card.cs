using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace MDWcfServiceLibrary
{
    [DataContract]
    public enum CardType
    {
        Action,
        Property,
        WildProperty,
        Money
    }

    [DataContract]
    public class Card
    {
        public static int nextCardId = 0;

        public static int getNextCardId()
        {
            nextCardId++;
            return nextCardId;
        }

        public static List<Guid> cardGuids = new List<Guid>();

        public static Guid generateGuid()
        {
            Guid newCardGuid = Guid.NewGuid();
            while (true)
            {
                bool existsAllready = false;
                foreach (Guid existingGuid in cardGuids)
                {
                    if (existingGuid.CompareTo(newCardGuid) == 0)
                    {
                        //guid exists
                        existsAllready = true;
                        break;
                    }
                }
                if (!existsAllready)
                {
                    return newCardGuid;
                }
                else
                {
                    newCardGuid = Guid.NewGuid();
                }
            }
        }

        [DataMember]
        public String cardName;
        [DataMember]
        public int cardID;
        [DataMember]
        public Guid cardGuid;
        [DataMember]
        public String cardText;
        [DataMember]
        public int cardValue;
        [DataMember]
        public CardType cardType;

        public Card(String cardName, String cardText, int cardValue, CardType cardType)
        {
            this.cardID = Card.getNextCardId();
            this.cardName = cardName;
            this.cardText = cardText;
            this.cardType = cardType;
            this.cardValue = cardValue;
        }

        public bool equals(Card checkAgainst)
        {
            if (cardID == checkAgainst.cardID)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public int getCardId()
        {
            return cardID;
        }
    }
}