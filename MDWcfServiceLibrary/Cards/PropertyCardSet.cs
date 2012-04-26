using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace MDWcfServiceLibrary
{
    [DataContract]
    public struct SwapPropertiesBetweenSets
    {
        [DataMember]
        public PropertyCard cardBeingSwapped;
        [DataMember]
        public PropertyCardSet setToRemoveCardFrom;
        [DataMember]
        public PropertyCardSet setToPutCardIn;
    }

    [DataContract]
    public class PropertyCardSet
    {
        //A set contains up to 4 property cards, some of which may be wild.
        //A full set may contain up to 1 house card
        //A full set may contain up to 1 hotel card if it has a house card.
        //If a full set is broken any houses and hotels are added to the bottom of the draw pile
        [DataMember]
        public static int ID = 0;

        private static int generateID()
        {
            ID += 1;
            return ID;
        }

        [DataMember]
        public PropertyColor propertySetColor;
        [DataMember]
        public LinkedList<PropertyCard> properties;
        [DataMember]
        public int id;
        [DataMember]
        public bool hasHouse;
        [DataMember]
        public bool hasHotel;
        [DataMember]
        public Card house;
        [DataMember]
        public Card hotel;
        [DataMember]
        public Guid guid;

        public Guid generateGuid()
        {
            return Guid.NewGuid();
        }

        public PropertyCardSet(PropertyCard propertyCard)
        {
            id = generateID();
            guid = generateGuid();
            properties = new LinkedList<PropertyCard>();
            properties.AddFirst(propertyCard);
            propertySetColor = propertyCard.currentPropertyColor;
        }

        public PropertyCardSet(PropertyCardSet pcs, PlayFieldModel state)
        {
            id = pcs.id;
            guid = pcs.guid;
            hasHouse = pcs.hasHouse;
            hasHotel = pcs.hasHotel;
            if (pcs.hotel != null)
            {
                pcs.hotel = state.deck.getCardByID(pcs.hotel.cardID);
            }
            if (pcs.house != null)
            {
                pcs.house = state.deck.getCardByID(pcs.house.cardID);
            }
            properties = pcs.properties.cloneLinkedListPropertyCard(state.deck);
            propertySetColor = pcs.propertySetColor;
        }

        public bool isFullSet()
        {
            //Should be static method
            return new PropertySetInfo(propertySetColor).getIsFullSet(properties);
        }

        public bool addHouse(Card houseCard)
        {
            if (isFullSet() && !hasHouse)
            {
                hasHouse = true;
                house = houseCard;
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool addHotel(Card hotelCard)
        {
            if (isFullSet() && hasHouse && !hasHotel)
            {
                hasHotel = true;
                hotel = hotelCard;
                return true;
            }
            else
            {
                return false;
            }
        }

        public Card removeHotel()
        {
            if (hasHotel)
            {
                hasHotel = false;
                return hotel;
            }
            return null;
        }

        public Card removeHouse()
        {
            if (hasHotel || !hasHouse)
            {
                //Not allowed to remove house without removing hotel first
                return null;
            }
            else
            {
                return house;
            }
        }

        public bool removeProperty(PropertyCard cardToRemove)
        {
            foreach (PropertyCard p in properties)
            {
                if (p.cardID.CompareTo(cardToRemove.cardID) == 0)
                {
                    properties.Remove(p);
                    return true;
                }
            }
            return false;
            /*
            if (properties.Contains(cardToRemove))
            {
                properties.Remove(cardToRemove);
                //FIX if set was full and had house or hotel cards these should be moved to the bank pile.
                return true;
            }
            return false;
             * */
        }

        public bool addProperty(PropertyCard newCard)
        {
            //Returns true if property card is added to set
            //Returns false if property card is not added to set

            if (isFullSet())
            {
                //Set full
                return false;
            }
            if (properties.Count == 0)
            {
                //Card is first Card in set
                properties.AddLast(newCard);
                propertySetColor = newCard.getPropertyColor();
                return true;
            }
            if (properties.Count > 0 && !isFullSet())
            {
                //used to track wether property card is up or down
                bool isUp = false;
                foreach (PropertyColor newCardColor in newCard.propertyColors)
                {
                    if (newCardColor == getPropertySetColor())
                    {
                        isUp = !isUp;
                        newCard.isCardUp = isUp;
                        newCard.currentPropertyColor = newCardColor;
                        properties.AddLast(newCard);
                        //Card is correct colour
                        return true;
                    }
                }
            }
            //Card is not correct colour
            return false;
        }

        public PropertyColor getPropertySetColor()
        {
            foreach (PropertyCard card in properties)
            {
                //If neccessary to allow sets with multicolour wild cards to be set to the correct color
                if (card.getPropertyColor() != PropertyColor.Wild_MultiColored)
                {
                    return card.getPropertyColor();
                }
            }
            return PropertyColor.Wild_MultiColored;
        }
    }
}