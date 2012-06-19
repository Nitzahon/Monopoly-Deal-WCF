using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MDWcfServiceLibrary
{
    public class Deck
    {
        public LinkedList<Card> cardDeck;

        int houseValue = 3;
        int hotelValue = 4;
        Card[] deck;
        //int nextCardID = 0;
        int numberOfDecks;

        public Deck(int numberOfDecks)
        {
            //empty newDeck
            cardDeck = new LinkedList<Card>();
            //Sets up deck for play pile
            for (int i = 0; i < numberOfDecks; i++)
            {
                fillDeck();
                //shuffle cards into newDeck
            }
            deck = cardDeck.ToArray();
            durstenfeldShuffle();
        }

        public Deck(Deck oldDeck)
        {
            numberOfDecks = oldDeck.numberOfDecks;
            List<Card> newDecksCards = new List<Card>();
            foreach (Card card in oldDeck.deck)
            {
                newDecksCards.Add(card.clone());
            }
            deck = newDecksCards.ToArray<Card>();
            cardDeck = oldDeck.cardDeck.cloneLinkedListCard();
        }

        public Deck cloneDeck()
        {
            return new Deck(this);
        }

        public Card getCardByID(int id)
        {
            return cardDeck.ElementAt(id);
        }

        private void durstenfeldShuffle()
        {
            Random random = new Random();
            for (int i = deck.Length - 1; i > 0; i--)
            {
                int j = random.Next(0, i);
                exchangeCards(j, i);
            }
        }

        private void exchangeCards(int j, int i)
        {
            Card temp = deck[j];
            deck[j] = deck[i];
            deck[i] = temp;
        }

        private void fillDeck()
        {
            //Adds all  cards to the standard card deck

            //Standard Properties

            #region StandardProperties

            //PropertyColor color, String name, int fullsetSize, int oneCardVal, int twoCardVal, int threeCardVal, int fourCardVal, int fiveCardVal,int bankVal)
            cardDeck.AddLast(new PropertyCard(PropertyColor.Brown, "WHITECHAPEL ROAD", 2, 1, 2, 2 + houseValue, 2 + houseValue + hotelValue, 2 + houseValue + hotelValue, 1));
            cardDeck.AddLast(new PropertyCard(PropertyColor.Brown, "OLD KENT ROAD", 2, 1, 2, 2 + houseValue, 2 + houseValue + hotelValue, 2 + houseValue + hotelValue, 1));

            cardDeck.AddLast(new PropertyCard(PropertyColor.LightBlue, "THE ANGEL, ISLINGTON", 3, 1, 2, 3, 3 + houseValue, 3 + houseValue + hotelValue, 1));
            cardDeck.AddLast(new PropertyCard(PropertyColor.LightBlue, "EUSTON ROAD", 3, 1, 2, 3, 3 + houseValue, 3 + houseValue + hotelValue, 1));
            cardDeck.AddLast(new PropertyCard(PropertyColor.LightBlue, "PENTONVILLE ROAD", 3, 1, 2, 3, 3 + houseValue, 3 + houseValue + hotelValue, 1));

            cardDeck.AddLast(new PropertyCard(PropertyColor.Pink, "NORTHUMBERLAND AVENUE", 3, 1, 2, 4, 4 + houseValue, 4 + hotelValue, 2));
            cardDeck.AddLast(new PropertyCard(PropertyColor.Pink, "PALL MALL", 3, 1, 2, 4, 4 + houseValue, 4 + hotelValue, 2));
            cardDeck.AddLast(new PropertyCard(PropertyColor.Pink, "WHITEHALL", 3, 1, 2, 4, 4 + houseValue, 4 + hotelValue, 2));

            cardDeck.AddLast(new PropertyCard(PropertyColor.Orange, "MARLBOROUGH STREET", 3, 1, 3, 5, 5 + houseValue, 5 + houseValue + hotelValue, 2));
            cardDeck.AddLast(new PropertyCard(PropertyColor.Orange, "VINE STREET", 3, 1, 3, 5, 5 + houseValue, 5 + houseValue + hotelValue, 2));
            cardDeck.AddLast(new PropertyCard(PropertyColor.Orange, "BOW STREET", 3, 1, 3, 5, 5 + houseValue, 5 + houseValue + hotelValue, 2));

            cardDeck.AddLast(new PropertyCard(PropertyColor.Red, "STRAND", 3, 2, 3, 6, 6 + houseValue, 6 + houseValue + hotelValue, 3));
            cardDeck.AddLast(new PropertyCard(PropertyColor.Red, "FLEET STREET", 3, 2, 3, 6, 6 + houseValue, 6 + houseValue + hotelValue, 3));
            cardDeck.AddLast(new PropertyCard(PropertyColor.Red, "TRAFALGAR SQUARE", 3, 2, 3, 6, 6 + houseValue, 6 + houseValue + hotelValue, 3));

            cardDeck.AddLast(new PropertyCard(PropertyColor.Yellow, "PICCADILLY", 3, 2, 4, 6, 6 + houseValue, 6 + houseValue + hotelValue, 3));
            cardDeck.AddLast(new PropertyCard(PropertyColor.Yellow, "COVENTRY STREET", 3, 2, 4, 6, 6 + houseValue, 6 + houseValue + hotelValue, 3));
            cardDeck.AddLast(new PropertyCard(PropertyColor.Yellow, "LEICESTER SQUARE", 3, 2, 4, 6, 6 + houseValue, 6 + houseValue + hotelValue, 3));

            cardDeck.AddLast(new PropertyCard(PropertyColor.Green, "OXFORD STREET", 3, 2, 4, 7, 7 + houseValue, 7 + houseValue + hotelValue, 4));
            cardDeck.AddLast(new PropertyCard(PropertyColor.Green, "REGENT STREET", 3, 2, 4, 7, 7 + houseValue, 7 + houseValue + hotelValue, 4));
            cardDeck.AddLast(new PropertyCard(PropertyColor.Green, "BOND STREET", 3, 2, 4, 7, 7 + houseValue, 7 + houseValue + hotelValue, 4));

            cardDeck.AddLast(new PropertyCard(PropertyColor.Blue, "MAYFAIR", 2, 3, 8, 8 + houseValue, 8 + houseValue + hotelValue, 8 + houseValue + hotelValue, 4));
            cardDeck.AddLast(new PropertyCard(PropertyColor.Blue, "PARK LANE", 2, 3, 8, 8 + houseValue, 8 + houseValue + hotelValue, 8 + houseValue + hotelValue, 4));

            cardDeck.AddLast(new PropertyCard(PropertyColor.Station, "MARYLEBONE STATION", 4, 1, 2, 3, 4, 4, 2));
            cardDeck.AddLast(new PropertyCard(PropertyColor.Station, "FENCHURCH ST. STATION", 4, 1, 2, 3, 4, 4, 2));
            cardDeck.AddLast(new PropertyCard(PropertyColor.Station, "KINGS CROSS STATION", 4, 1, 2, 3, 4, 4, 2));
            cardDeck.AddLast(new PropertyCard(PropertyColor.Station, "LIVERPOOL ST. STATION", 4, 1, 2, 3, 4, 4, 2));

            cardDeck.AddLast(new PropertyCard(PropertyColor.Utilities, "WATER WORKS", 2, 1, 2, 2, 2, 2, 2));
            cardDeck.AddLast(new PropertyCard(PropertyColor.Utilities, "ELECTRIC COMPANY", 2, 1, 2, 2, 2, 2, 2));

            #endregion StandardProperties

            //Property WildCards

            #region WildCardProperties

            cardDeck.AddLast(new PropertyCard(PropertyColor.Brown, PropertyColor.LightBlue, "PROPERTY WILD CARD BROWN", 2, 1, 2, 2 + houseValue, 2 + houseValue + hotelValue, 2 + houseValue + hotelValue,
                "PROPERTY WILD CARD LIGHT BLUE", 3, 1, 2, 3, 3 + houseValue, 3 + houseValue + hotelValue, 1));

            cardDeck.AddLast(new PropertyCard(PropertyColor.LightBlue, PropertyColor.Station, "PROPERTY WILD CARD LIGHT BLUE", 3, 1, 2, 3, 3 + houseValue, 3 + houseValue + hotelValue,
                "PROPERTY WILD CARD STATION", 4, 1, 2, 3, 4, 4, 4));

            cardDeck.AddLast(new PropertyCard(PropertyColor.Pink, PropertyColor.Orange, "PROPERTY WILD CARD PINK", 3, 1, 2, 4, 4 + houseValue, 4 + houseValue + hotelValue,
                "PROPERTY WILD CARD ORANGE", 3, 1, 3, 5, 5 + houseValue, 5 + houseValue + hotelValue, 2));

            cardDeck.AddLast(new PropertyCard(PropertyColor.Pink, PropertyColor.Orange, "PROPERTY WILD CARD PINK", 3, 1, 2, 4, 4 + houseValue, 4 + houseValue + hotelValue,
                "PROPERTY WILD CARD ORANGE", 3, 1, 3, 5, 5 + houseValue, 5 + houseValue + hotelValue, 2));

            cardDeck.AddLast(new PropertyCard(PropertyColor.Red, PropertyColor.Yellow, "PROPERTY WILD CARD RED", 3, 2, 3, 6, 6 + houseValue, 6 + hotelValue,
                "PROPERTY WILD CARD YELLOW", 3, 2, 4, 6, 6 + houseValue, 6 + houseValue + hotelValue, 3));

            cardDeck.AddLast(new PropertyCard(PropertyColor.Red, PropertyColor.Yellow, "PROPERTY WILD CARD RED", 3, 2, 3, 6, 6 + houseValue, 6 + hotelValue,
                "PROPERTY WILD CARD YELLOW", 3, 2, 4, 6, 6 + houseValue, 6 + houseValue + hotelValue, 3));

            cardDeck.AddLast(new PropertyCard(PropertyColor.Green, PropertyColor.Blue, "PROPERTY WILD CARD GREEN", 3, 2, 4, 7, 7 + houseValue, 7 + houseValue + hotelValue,
                "PROPERTY WILD CARD BLUE", 2, 3, 8, 8 + houseValue, 8 + hotelValue + houseValue, 8 + houseValue + hotelValue, 4));

            cardDeck.AddLast(new PropertyCard(PropertyColor.Green, PropertyColor.Station, "PROPERTY WILD CARD GREEN", 3, 2, 4, 7, 7 + houseValue, 7 + houseValue + hotelValue,
                "PROPERTY WILD CARD STATION", 4, 1, 2, 3, 4, 4, 4));

            cardDeck.AddLast(new PropertyCard(PropertyColor.Station, PropertyColor.Utilities, "PROPERTY WILD CARD STATION", 4, 1, 2, 3, 4, 4,
                "PROPERTY WILD CARD UTILITIES", 2, 1, 2, 2, 2, 2, 2));

            //MultiColored Property Wild Cards
            cardDeck.AddLast(new PropertyCard(PropertyColor.Wild_MultiColored));

            cardDeck.AddLast(new PropertyCard(PropertyColor.Wild_MultiColored));

            #endregion WildCardProperties

            //Money Cards

            #region MoneyCards

            cardDeck.AddLast(new MoneyCard(1));
            cardDeck.AddLast(new MoneyCard(1));
            cardDeck.AddLast(new MoneyCard(1));
            cardDeck.AddLast(new MoneyCard(1));
            cardDeck.AddLast(new MoneyCard(1));
            cardDeck.AddLast(new MoneyCard(1));

            cardDeck.AddLast(new MoneyCard(2));
            cardDeck.AddLast(new MoneyCard(2));
            cardDeck.AddLast(new MoneyCard(2));
            cardDeck.AddLast(new MoneyCard(2));
            cardDeck.AddLast(new MoneyCard(2));

            cardDeck.AddLast(new MoneyCard(3));
            cardDeck.AddLast(new MoneyCard(3));
            cardDeck.AddLast(new MoneyCard(3));

            cardDeck.AddLast(new MoneyCard(4));
            cardDeck.AddLast(new MoneyCard(4));
            cardDeck.AddLast(new MoneyCard(4));

            cardDeck.AddLast(new MoneyCard(5));
            cardDeck.AddLast(new MoneyCard(5));

            cardDeck.AddLast(new MoneyCard(10));

            #endregion MoneyCards

            //Action Cards

            #region ActionCards

            for (int i = 0; i < 2; i++)
            {
                cardDeck.AddLast(new ActionCard(ActionCardAction.DealBreaker, 5, "DEAL BREAKER",
                    "Steal a complete set of properties from any player. (Includes any buildings.) Play into the centre to use."));
            }
            for (int i = 0; i < 3; i++)
            {
                cardDeck.AddLast(new ActionCard(ActionCardAction.ForcedDeal, 3, "FORCED DEAL",
                    "Swap any property with another player. (Cannot be part of a full set.) Play into centre to use."));
            }
            for (int i = 0; i < 3; i++)
            {
                cardDeck.AddLast(new ActionCard(ActionCardAction.SlyDeal, 3, "SLY DEAL",
                    "Steal a property from the player of your choice. (Cannot be part of a full set.) Play into the centre to use."));
            }
            for (int i = 0; i < 3; i++)
            {
                cardDeck.AddLast(new ActionCard(ActionCardAction.JustSayNo, 4, "JUST SAY NO!", "Use any time when an action card is played against you. Play into centre to use"));
            }
            for (int i = 0; i < 3; i++)
            {
                cardDeck.AddLast(new ActionCard(ActionCardAction.DebtCollector, 3, "DEBT COLLECTOR", "Force any player to pay you $5M. Play into centre to use."));
            }
            for (int i = 0; i < 3; i++)
            {
                cardDeck.AddLast(new ActionCard(ActionCardAction.ItsMyBirthday, 2, "IT'S MY BIRTHDAY", "All players give you $2M as a \"gift\". Play into centre to use."));
            }
            for (int i = 0; i < 2; i++)
            {
                cardDeck.AddLast(new ActionCard(ActionCardAction.DoubleTheRent, 1, "DOUBLE THE RENT!", "Needs to be played with a rent card. Play into centre to use"));
            }
            for (int i = 0; i < 3; i++)
            {
                cardDeck.AddLast(new ActionCard(ActionCardAction.House, 3, "HOUSE", "Add onto any full set you own to add $3M to the rent value.(Except stations and utilties.)"));
            }
            for (int i = 0; i < 2; i++)
            {
                cardDeck.AddLast(new ActionCard(ActionCardAction.Hotel, 4, "HOTEL", "Add onto any full set you own to add $4M to the rent value.(Except stations and utilties.)"));
            }
            for (int i = 0; i < 10; i++)
            {
                cardDeck.AddLast(new ActionCard(ActionCardAction.PassGo, 1, "PASS GO", "Draw 2 extra cards. Play into centre to use."));
            }

            #endregion ActionCards

            //rent cards

            #region RentCards

            for (int i = 0; i < 2; i++)
            {
                cardDeck.AddLast(new RentStandard(ActionCardAction.RentStandard, 1, PropertyColor.LightBlue, PropertyColor.Brown));
            }
            for (int i = 0; i < 2; i++)
            {
                cardDeck.AddLast(new RentStandard(ActionCardAction.RentStandard, 1, PropertyColor.Pink, PropertyColor.Orange));
            }
            for (int i = 0; i < 2; i++)
            {
                cardDeck.AddLast(new RentStandard(ActionCardAction.RentStandard, 1, PropertyColor.Red, PropertyColor.Yellow));
            }
            for (int i = 0; i < 2; i++)
            {
                cardDeck.AddLast(new RentStandard(ActionCardAction.RentStandard, 1, PropertyColor.Blue, PropertyColor.Green));
            }
            for (int i = 0; i < 2; i++)
            {
                cardDeck.AddLast(new RentStandard(ActionCardAction.RentStandard, 1, PropertyColor.Station, PropertyColor.Utilities));
            }
            for (int i = 0; i < 3; i++)
            {
                cardDeck.AddLast(new ActionCard(ActionCardAction.RentMultiColor, 3, "RENT", "Force one player to pay you rent for the properties you own in one of these colours, Play into centre to use"));
            }

            #endregion RentCards
        }

        public List<Card> getDeck()
        {
            return deck.ToList();
        }
    }
}