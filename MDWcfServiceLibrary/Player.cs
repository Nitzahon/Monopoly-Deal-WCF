using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace MDWcfServiceLibrary
{
    [DataContract]
    public class Player
    {
        //A player has a ID Number
        int id;
        //A player has a name
        String playerName;
        //A player has a hand of cards
        List<Card> cardsInHand;
        //A player has a collection of Property Cards
        List<PropertyCard> propertyCollection;
        //A player has a collection of sets of Property Cards
        List<PropertyCardSet> propertySetCollection;
        //A player has a collection of Cards in Bank;
        List<Card> bankPile;
        //A Player may be on their turn
        bool isPlayersTurn;
        //A Player may play up to three cards on their turn
        //int remainingCardPlays;
        //A Player may have an action played against them:
        //rent, forced deal, sly deal, dealbreaker, just say no, it's my birthday, debt collector
        //bool actionAgainst;
        private static int ID = 0;

        //A player has actions it can take
        //IPlayerActions iAction;
        //A player can be notifed of a change
        //IRequestInput iRequestInput;
        //A player can be displayed information
        //IPlayerDisplay iDisplay;

        /*public IPlayerActions getIPlayerActions()
        {
            return iAction;
        }

        public IPlayerDisplay getIPlayerDisplay()
        {
            return iDisplay;
        }

        public IRequestInput getIRequestInput()
        {
            return iRequestInput;
        }

         */

        private void generatePlayerID()
        {
            id = ID;
            ID++;
        }

        public void constructPlayer(String playerName)
        {
            generatePlayerID();
            this.playerName = playerName;
            cardsInHand = new List<Card>();
            propertyCollection = new List<PropertyCard>();
            propertySetCollection = new List<PropertyCardSet>();
            bankPile = new List<Card>();
            isPlayersTurn = false;
            //remainingCardPlays = 0;
            //actionAgainst = false;
        }

        public List<Card> getCardsInHand()
        {
            return cardsInHand;
        }

        public List<Card> getCardsInBank()
        {
            return bankPile;
        }

        public List<PropertyCard> getProperties()
        {
            return propertyCollection;
        }

        public List<PropertyCardSet> getPropertySets()
        {
            return propertySetCollection;
        }

        public bool playPropertyCardFromHand(PropertyCard card, PropertyCardSet setToPlaceCardIn)
        {
            if (setToPlaceCardIn.addProperty(card))
            {
                cardsInHand.Remove(card);
                return true;
            }
            else
            {
                return false;
            }
        }

        public void setTurn(Boolean isTurn)
        {
            isPlayersTurn = isTurn;
        }

        public Boolean getTurn()
        {
            return isPlayersTurn;
        }

        public void addCardToHand(Card card)
        {
            cardsInHand.Add(card);
        }

        public void recieveCardsAsPayment(List<Card> cardsRecieved)
        {
            foreach (Card card in cardsRecieved)
            {
                if (card is MoneyCard)
                {
                    bankPile.Add(card);
                }
                else if (card is ActionCard)
                {
                    bankPile.Add(card);
                }
                else if (card is PropertyCard)
                {
                    propertySetCollection.Add(new PropertyCardSet((PropertyCard)card));
                }
            }
        }

        public string getName()
        {
            return playerName;
        }

        public int getID()
        {
            return id;
        }

        //IMonopolyDealCallback callbackInterface;

        bool ready = false;

        Guid playerGuid;

        public Player(String playerName/*, IMonopolyDealCallback cbi*/)
        {
            playerGuid = Guid.NewGuid();

            //callbackInterface = cbi;
            constructPlayer(playerName);
        }

        //public IMonopolyDealCallback getCallback()
        // {
        // return callbackInterface;
        // }

        public bool getIfReady()
        {
            return ready;
        }

        public void setIfReady(bool isReady)
        {
            ready = isReady;
        }

        public static List<Guid> playerGuids = new List<Guid>();

        private static Guid generatePlayerGuid()
        {
            Guid newPlayerGuid = Guid.NewGuid();
            while (true)
            {
                bool existsAllready = false;
                foreach (Guid existingGuid in playerGuids)
                {
                    if (existingGuid.CompareTo(newPlayerGuid) == 0)
                    {
                        //guid exists
                        existsAllready = true;
                        break;
                    }
                }
                if (!existsAllready)
                {
                    return newPlayerGuid;
                }
                else
                {
                    newPlayerGuid = Guid.NewGuid();
                }
            }
        }

        public Guid getPlayerGuid()
        {
            return playerGuid;
        }
    }
}