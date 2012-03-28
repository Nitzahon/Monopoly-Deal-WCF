using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MDWcfServiceLibrary
{
    public enum TurnActionType
    {
        EndTurn,
        SwitchAroundPlayedProperties,
        PlayCard,
        PlayPropertyCard,
        BankMoneyCard,
        PlayActionCard,
        BankActionCard
    }

    public class TurnOptions
    {
        //The player whose turn it is
        private Player player
        {
            get
            {
                return player;
            }
            set
            {
                player = value;
            }
        }

        //The card if any they are playing on this move of their turn
        private Card card
        {
            get { return card; }
            set { card = value; }
        }

        //The type of action they are taking
        private TurnActionType turnActionType
        {
            get { return turnActionType; }
            set { turnActionType = value; }
        }

        public TurnOptions(Player p, Card c, TurnActionType t)
        {
            player = p;
            card = c;
            turnActionType = t;
        }
    }
}