using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MDWcfServiceLibrary
{
    /// <summary>
    /// Represents a Game of MonopolyDeal
    /// </summary>
    internal class MonopolyDeal:IDisposable
    {
        public readonly Guid MONOPOLY_DEAL_GAME_GUID;

        public static readonly int MAX_PLAYERS = 5;
        public static readonly int MIN_PLAYERS = 2;
        public static readonly int NUMBER_OF_DECKS = 1;
        public static readonly int NEW_TURN_NUMBER_OF_CARDS_PLAYABLE = 3;
        public static readonly int NUMBER_OF_FULL_SETS_REQUIRED_TO_WIN = 3;
        public IMonopolyDeal_GameStateManager gameStateManager;
        public MessageManager messageManager;
        internal List<PlayerModel> players;
        internal List<PlayFieldModel> gameStates = new List<PlayFieldModel>();
        internal List<Guid> playerIdLookup = new List<Guid>();

        internal PlayFieldModel initialState;
        internal PlayFieldModel currentState;
        internal PlayPile initialPlayPile;

        internal int FIRST_PLAYER = 0;
        internal bool gameOver = false;
        //Move To playfieldmodel State
        int currentPlayerTurn;

        public Deck deck;
        public Guid gameModelGuid;
        public bool useMoveManager = true;
        internal ILobby lobby;

        #region Constructor

        /// <summary>
        /// Creates a new MonopolyDeal Game
        /// </summary>
        /// <param name="playersP">List of PlayerModels</param>
        public MonopolyDeal(List<PlayerModel> playersP, Guid thisGameGuidP, ILobby lobby)
        {
            this.lobby = lobby;
            //Assign Guid to this game of Monopoly Deal
            MONOPOLY_DEAL_GAME_GUID = thisGameGuidP;
            gameModelGuid = thisGameGuidP;
            //Assign Players to this game of Monopoly Deal
            players = playersP;

            gameStateManager = new GameStateManagerToMoveAdapter(this);

            addPlayersToIDLookup(playersP);
            initialState = createInitialState(players);
            gameStates.Add(initialState);
            currentState = initialState;
            //State added to gameStates list, notify all players, wait for responses
        }

        internal void addPlayersToIDLookup(List<PlayerModel> players)
        {
            foreach (PlayerModel p in players)
            {
                playerIdLookup.Add(p.guid);
            }
        }

        public IMonopolyDeal_GameStateManager getMonopolyDealGameStateManager()
        {
            return gameStateManager;
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

        #region From GameModel

        private PlayFieldModel createInitialState(List<PlayerModel> players)
        {
            //guid for initial state
            Guid playFieldModelGuid = PlayFieldModel.generateplayFieldModelGuid();
            //no cards to be shown as played in playpile
            List<Card> emptyTopPlayPile = new List<Card>();
            //setFirstPlayerToHaveTurn(players);
            //set player 0 to be first to play
            //Guid firstPlayerGuid = players.ElementAt(FIRST_PLAYER).guid;
            //currentPlayerTurn = FIRST_PLAYER;
            Guid firstPlayerGuid = setFirstPlayerToHaveTurn(players);
            //no players can be affected by actioncards as none have been played
            List<Guid> noPlayersAffectedByActionCard = new List<Guid>();
            //No actions have been taken
            List<TurnActionTypes> actionsAllowable = new List<TurnActionTypes>();
            actionsAllowable.Add(TurnActionTypes.drawTwoCardsAtStartOfTurn);

            //Set players Allowable actions
            foreach (PlayerModel p in players)
            {
                p.actionsCurrentlyAllowed = new List<TurnActionTypes>();
                if (p.guid.CompareTo(firstPlayerGuid) == 0)
                {
                    //Player whos turn it is must draw TwoCards
                    p.actionsCurrentlyAllowed.Add(TurnActionTypes.drawTwoCardsAtStartOfTurn);
                }
            }

            //
            TurnActionModel noActionsPlayedFirstPlayerToDraw = new TurnActionModel(this.playerIdLookup, this.gameModelGuid, playFieldModelGuid, generateTurnActionGuid(), actionsAllowable, TurnActionTypes.gameStarted, false);
            //create empty playpile
            initialPlayPile = new PlayPile();
            //fill  new drawpile
            DrawPile initialDrawPile = generateInitialDrawPile(initialPlayPile);
            //Deal players thier first five cards
            dealPlayersInitialFiveCards(players, initialDrawPile);
            //It is the start of a players turn
            bool turnStart = true;

            //put it all into the intial state
            PlayFieldModel state = new PlayFieldModel(playFieldModelGuid, players, emptyTopPlayPile, firstPlayerGuid, noPlayersAffectedByActionCard,
                null, noActionsPlayedFirstPlayerToDraw, initialDrawPile, initialPlayPile, NEW_TURN_NUMBER_OF_CARDS_PLAYABLE, turnStart, Statephase.Turn_Started_Draw_2_Cards, deck);
            //stateCreated
            currentState = state;
            return state;
        }

        private DrawPile generateInitialDrawPile(PlayPile pp)
        {
            //ShuffleDeck
            deck = new Deck(NUMBER_OF_DECKS);
            DrawPile dp = new DrawPile(deck.getDeck(), pp);
            return dp;
        }

        private void dealPlayersInitialFiveCards(List<PlayerModel> players, DrawPile drawPile)
        {
            //PreCondition drawPile is Full and each player.hand contains no Cards
            //PostCondition each player.hand contains 5 Cards
            int numberOfCardsDrawn = 0;
            int forloop = 0;
            for (int i = 0; i < 5; i++)
            {
                foreach (PlayerModel player in players)
                {
                    Card drawnCard = drawPile.drawcard();
                    player.hand.addCardToHand(drawnCard);
                    numberOfCardsDrawn++;
                }
                forloop++;
            }
        }

        private Guid setFirstPlayerToHaveTurn(List<PlayerModel> players)
        {
            //set player 0 to be first to play
            Guid firstPlayerGuid = players.ElementAt(FIRST_PLAYER).guid;
            players.ElementAt(FIRST_PLAYER).isThisPlayersTurn = true;
            currentPlayerTurn = FIRST_PLAYER;
            return firstPlayerGuid;
        }

        private Guid generateTurnActionGuid()
        {
            return Guid.NewGuid();
        }

        #endregion From GameModel

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}