using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MonopolyDealLibrary;

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

        public WCFGame(List<Player> players, Deck deck)
            : base(players, deck)
        {
            //Set that the game has started
        }

        public void startGame()
        {
        }
    }
}