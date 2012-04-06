using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MDWcfServiceLibrary
{
    /// <summary>
    /// Represents a Game of MonopolyDeal
    /// </summary>
    internal class MonopolyDeal : IMonopolyDealGame
    {
        private readonly Guid MONOPOLY_DEAL_GAME_GUID;

        private MonopolyDeal_GameStateManager gameStateManger;
        private List<PlayerModel> players;
        private List<PlayFieldModel> gameStates = new List<PlayFieldModel>();

        private PlayFieldModel initialState;
        private PlayFieldModel currentState;
        private PlayPile initialPlayPile;

        int FIRST_PLAYER = 0;
        bool gameOver = false;
        //Move To playfieldmodel State
        int currentPlayerTurn;

        public Deck deck;
        public Guid gameModelGuid;

        /// <summary>
        /// Creates a new MonopolyDeal Game
        /// </summary>
        /// <param name="playersP">List of PlayerModels</param>
        public MonopolyDeal(List<PlayerModel> playersP, Guid thisGameGuid)
        {
            //Assign Guid to this game of Monopoly Deal
            MONOPOLY_DEAL_GAME_GUID = thisGameGuid;
        }

        internal static int getMinPlayers()
        {
            throw new NotImplementedException();
        }

        internal static int getMaxPlayers()
        {
            throw new NotImplementedException();
        }
    }
}