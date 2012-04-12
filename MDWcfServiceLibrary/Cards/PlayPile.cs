using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace MDWcfServiceLibrary
{
    [DataContract]
    public class PlayPile
    {
        [DataMember]
        List<Card> playPile;

        public PlayPile()
        {
            playPile = new List<Card>();
        }

        public void playCardOnPile(Card card)
        {
            playPile.Add(card);
        }

        public bool refillDrawpile(List<Card> drawPile)
        {
            drawPile = playPile;
            playPile = new List<Card>();
            if (drawPile.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        internal PlayPile clone(Deck deck)
        {
            PlayPile ppclone = new PlayPile();
            foreach (Card c in this.playPile)
            {
                ppclone.playCardOnPile(deck.getCardByID(c.cardID));
            }
            return ppclone;
        }
    }
}