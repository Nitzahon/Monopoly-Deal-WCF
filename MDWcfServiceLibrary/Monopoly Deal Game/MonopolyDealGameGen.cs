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

        public bool createGame(List<LobbyClient> clients, Guid guidForGame, ILobby lobby)
        {
            try
            {
                List<PlayerModel> playersMD = new List<PlayerModel>();
                foreach (LobbyClient lc in clients)
                {
                    PlayerModel p = new PlayerModel(lc.getName());
                    p.guid = lc.getGuid();
                    p.isReadyToStartGame = true;
                    playersMD.Add(p);
                }
                MonopolyDeal game = new MonopolyDeal(playersMD, guidForGame, lobby);
                games.Add(game);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public BoolResponseBox cleanUpGame(Guid guidForGame, ILobby lobby)
        {
            MonopolyDeal GameToCleanUp = null;
            foreach (MonopolyDeal md in games)
            {
                if (md.MONOPOLY_DEAL_GAME_GUID.CompareTo(guidForGame) == 0)
                {
                    GameToCleanUp = md;
                    break;
                }
            }
            GameToCleanUp.Dispose();

            return new BoolResponseBox(false, "Game not cleaned up");
        }
    }
}