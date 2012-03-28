using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MDWcfServiceLibrary
{
    public class Game
    {
        //Represents Game
        //A game has a Deck of 110 cards
        Deck cardDeck;
        //A game has a Draw Pile
        public DrawPile drawPile;
        //A game has a Play Pile
        public PlayPile playPile;
        int numberOfPlayers;
        //A game has 2-5 players with one deck
        List<Player> players;
        //Number of decks, 1 if less than 6 people, two if more than 6 people
        //int numberOfDecks;
        //A game may have a winner
        Player winner;
        //A game may be won
        bool gameWon;
        //A game may be over
        bool gameOver;
        //A game has gameplay

        public Game(List<Player> players, Deck deck)
        {
            //Game logic

            //Game Start
            //Get list of players
            this.players = players;
            //Get number of players
            this.numberOfPlayers = this.players.Count;
            //build playpile
            setUpPlaypile();
            //build DrawPile
            setUpDrawPile(deck, playPile);
            //deal initial 5 cards to eachPlayer
            dealCards(players);
            gameOver = false;
        }

        public PlayPile setUpPlaypile()
        {
            playPile = new PlayPile();
            return playPile;
        }

        private DrawPile setUpDrawPile(Deck deck, PlayPile playPile)
        {
            drawPile = new DrawPile(deck.getDeck(), playPile);
            return drawPile;
        }

        private void dealCards(List<Player> players)
        {
            Console.WriteLine("Dealing Cards");
            for (int card = 0; card < 5; card++)
            {
                foreach (Player p in players)
                {
                    p.addCardToHand(drawPile.drawcard());
                }
            }

            Console.WriteLine("Cards Dealed");
        }

        public List<Player> getListOfPlayers()
        {
            return players;
        }

        public void setGameOver(bool over)
        {
            gameOver = over;
        }

        public bool getIsGameOver()
        {
            return gameOver;
        }
    }
}