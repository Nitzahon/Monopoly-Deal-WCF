using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MDWcfServiceLibrary
{
    internal class MonopolyDealGameGen
    {
        private List<MonopolyDeal> games = new List<MonopolyDeal>();
        public static readonly int MAX_PLAYERS_PER_GAME = 5;
        public static readonly int MIN_PLAYERS_PER_GAME = 2;

        public MonopolyDealGameGen(List<MonopolyDeal> monopolyDealGames)
        {
            games = monopolyDealGames;
        }

        public MonopolyDeal createGame(List<PlayerModel> players, Guid guidForGame)
        {
            MonopolyDeal md = new MonopolyDeal(players, guidForGame);
            return md;
        }

        internal bool startNewGame(List<LobbyClient> list)
        {
            throw new NotImplementedException();
        }
    }
}