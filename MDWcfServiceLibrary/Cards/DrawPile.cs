using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MDWcfServiceLibrary
{
    public class DrawPile
    {
        internal List<Card> drawPile;
        internal PlayPile playPile;

        public DrawPile(List<Card> cards, PlayPile playPile)
        {
            drawPile = cards;
            this.playPile = playPile;
        }

        public Card drawcard()
        {
            //Refills Draw Pile if empty
            if (drawPileIsEmpty())
            {
                if (!playPile.refillDrawpile(drawPile))
                {
                    throw new Exception("Out of Cards to draw");
                }
            }
            //Gets card from top of pile

            Card drawnCard = drawPile.ElementAt(drawPile.Count - 1);
            //removes card from pile
            drawPile.RemoveAt(drawPile.Count - 1);
            //returns card drawn
            return drawnCard;
        }

        public void discardCard(Card card)
        {
            //discard card to bottom of draw pile
            drawPile.Insert(0, card);
        }

        //check if drawpile is empty
        public bool drawPileIsEmpty()
        {
            if (drawPile.Count == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        internal DrawPile clone(Deck deck)
        {
            return new DrawPile(drawPile.cloneListCard(deck), playPile);
            throw new NotImplementedException();
        }
    }
}