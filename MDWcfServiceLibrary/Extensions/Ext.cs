using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MDWcfServiceLibrary
{
    public static class MyExtensions
    {
        public static GuidBox boxGuid(this Guid guid)
        {
            GuidBox gb = new GuidBox();
            gb.guid = guid;
            return gb;
        }

        public static Guid cloneGuid(this Guid guid)
        {
            Guid gClone = new Guid(guid.ToString());
            return gClone;
        }

        public static List<Guid> cloneListGuids(this List<Guid> guidsListOld)
        {
            if (guidsListOld != null)
            {
                List<Guid> clonedGuidList = new List<Guid>();
                foreach (Guid g in guidsListOld)
                {
                    Guid gc = g.cloneGuid();
                    clonedGuidList.Add(gc);
                }
                return clonedGuidList;
            }
            return null;//empty list
        }

        public static List<TurnActionTypes> cloneListTurnActionTypes(this List<TurnActionTypes> ListOld)
        {
            if (ListOld != null)
            {
                List<TurnActionTypes> clonedList = new List<TurnActionTypes>();
                TurnActionTypes[] allTurnActionTypes = (TurnActionTypes[])Enum.GetValues(typeof(TurnActionTypes)).Cast<TurnActionTypes>();

                foreach (TurnActionTypes t in ListOld)
                {
                    foreach (TurnActionTypes tn in allTurnActionTypes)
                    {
                        if (t.CompareTo(tn) == 0)
                        {
                            clonedList.Add(tn);
                            break;
                        }
                    }
                }
                return clonedList;
            }
            return new List<TurnActionTypes>();//list old null
        }

        public static List<PropertyColor> cloneListPropertyColor(this List<PropertyColor> ListOld)
        {
            if (ListOld != null)
            {
                List<PropertyColor> clonedList = new List<PropertyColor>();
                PropertyColor[] allPropertyColor = (PropertyColor[])Enum.GetValues(typeof(PropertyColor)).Cast<PropertyColor>();

                foreach (PropertyColor t in ListOld)
                {
                    foreach (PropertyColor tn in allPropertyColor)
                    {
                        if (t.CompareTo(tn) == 0)
                        {
                            clonedList.Add(tn);
                            break;
                        }
                    }
                }
                return clonedList;
            }
            return new List<PropertyColor>();//list old null
        }

        public static List<Card> cloneListCard(this List<Card> ListOld, Deck deck)
        {
            //Property Cards will be inconsistant if deck is not cloned for each state
            List<Card> clonedList = new List<Card>();

            foreach (Card t in ListOld)
            {
                clonedList.Add(deck.getCardByID(t.cardID));
            }
            return clonedList;
        }

        public static LinkedList<PropertyCard> cloneLinkedListPropertyCard(this LinkedList<PropertyCard> ListOld, Deck deck)
        {
            //Property Cards will be inconsistant if deck is not cloned for each state
            LinkedList<PropertyCard> clonedList = new LinkedList<PropertyCard>();

            foreach (PropertyCard t in ListOld)
            {
                clonedList.AddLast(deck.getCardByID(t.cardID) as PropertyCard);
            }
            return clonedList;
        }

        public static List<PropertyCardSet> cloneListPropertyCardSet(this List<PropertyCardSet> ListOld, PlayFieldModel state)
        {
            //Property Cards will be inconsistant if deck is not cloned for each state
            List<PropertyCardSet> clonedList = new List<PropertyCardSet>();
            Deck deck = state.deck;
            foreach (PropertyCardSet pcs in ListOld)
            {
                clonedList.Add(new PropertyCardSet(pcs, state));
            }
            return clonedList;
        }

        public static bool cloneBool(this bool oldBool)
        {
            if (oldBool)
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