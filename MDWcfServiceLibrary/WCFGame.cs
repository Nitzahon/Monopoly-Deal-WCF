using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MDWcfServiceLibrary
{
    internal class WCFGame : Game
    {
        private static int check = 0;
        private bool isStarted = false;

        public bool getIfGameStarted()
        {
            return isStarted;
        }

        public WCFGame(List<Player> players, Deck deck, MonopolyDealService mds)
            : base(players, deck)
        {
            mds.addToClientsLogs("Game setup");
            mds.addToClientsLogs("Player 1 turn");
            //Set that the game has started
        }

        public void startGame()
        {
        }
    }
}