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
        public readonly Guid MONOPOLY_DEAL_GAME_GUID;

        public static readonly int MAX_PLAYERS = 5;
        public static readonly int MIN_PLAYERS = 2;

        public MonopolyDeal_GameStateManager gameStateManger;
        internal List<PlayerModel> players;
        internal List<PlayFieldModel> gameStates = new List<PlayFieldModel>();

        internal PlayFieldModel initialState;
        internal PlayFieldModel currentState;
        internal PlayPile initialPlayPile;

        internal int FIRST_PLAYER = 0;
        bool gameOver = false;
        //Move To playfieldmodel State
        int currentPlayerTurn;

        public Deck deck;
        public Guid gameModelGuid;

        #region Constructor

        /// <summary>
        /// Creates a new MonopolyDeal Game
        /// </summary>
        /// <param name="playersP">List of PlayerModels</param>
        public MonopolyDeal(List<PlayerModel> playersP, Guid thisGameGuidP)
        {
            //Assign Guid to this game of Monopoly Deal
            MONOPOLY_DEAL_GAME_GUID = thisGameGuidP;
            //Assign Players to this game of Monopoly Deal
            players = playersP;
            gameStateManger = new MonopolyDeal_GameStateManager(this);
        }

        public MonopolyDeal_GameStateManager getMonopolyDealGameStateManager()
        {
            return gameStateManger;
        }

        #endregion Constructor

        #region getters

        /// <summary>
        /// Get a PlayerModel by the PlayerModel's Guid
        /// </summary>
        /// <param name="g">Guid of PlayerModel</param>
        /// <returns>PlayerModel with specified Guid</returns>
        private PlayerModel getPlayerModel(Guid g)
        {
            foreach (PlayerModel pm in getPlayers())
            {
                if (pm.guid.CompareTo(g) == 0)
                {
                    //Player Found
                    return pm;
                }
            }
            return null;
        }

        /// <summary>
        /// Returns a list of all PlayerModels in game.
        /// </summary>
        /// <returns></returns>
        private List<PlayerModel> getPlayers()
        {
            return players;
        }

        internal static int getMinPlayers()
        {
            return MIN_PLAYERS;
        }

        internal static int getMaxPlayers()
        {
            return MAX_PLAYERS;
        }

        #endregion getters

        internal Guid getGuid()
        {
            return MONOPOLY_DEAL_GAME_GUID;
        }

        internal int getMaxPlayersPerGame()
        {
            throw new NotImplementedException();
        }
    }
}