using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MDWcfServiceLibrary
{
    /// <summary>
    /// Represents a Game of MonopolyDeal
    /// </summary>
    internal class MonopolyDeal
    {
        public readonly Guid MONOPOLY_DEAL_GAME_GUID;

        public static readonly int MAX_PLAYERS = 5;
        public static readonly int MIN_PLAYERS = 2;
        public static readonly int NUMBER_OF_DECKS = 1;
        public static readonly int NEW_TURN_NUMBER_OF_CARDS_PLAYABLE = 3;
        public MonopolyDeal_GameStateManager gameStateManager;
        public MessageManager messageManager;
        internal List<PlayerModel> players;
        internal List<PlayFieldModel> gameStates = new List<PlayFieldModel>();
        internal List<Guid> playerIdLookup = new List<Guid>();

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
            gameStateManager = new MonopolyDeal_GameStateManager(this);
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

        public MonopolyDeal_GameStateManager getMonopolyDealGameStateManager()
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

        #region From Service

        public bool draw2AtStartOfTurn(GuidBox playerGuid, GuidBox gameGuid, GuidBox playfieldModelInstanceGuid, GuidBox turnActionGuid)
        {
            return gameStateManager.doAction(gameGuid.guid, playerGuid.guid, playfieldModelInstanceGuid.guid, TurnActionTypes.drawTwoCardsAtStartOfTurn);
        }

        public PlayFieldModel pollState(GuidBox playerGuid, GuidBox gameGuid)
        {
            PlayFieldModel pfm = messageManager.respondToPoll();
            return pfm;
        }

        public bool playCardFromHandToBank(int playedCardID, GuidBox playerGuid, GuidBox serverGuid, GuidBox playfieldModelInstanceGuid, GuidBox turnActionGuid)
        {
            Guid pg = playerGuid.guid;
            Card card = this.deck.getCardByID(playedCardID);
            bool result = bankCardValidityCheck(card, pg);
            if (result)
            {
                gameStateManager.bankCard(playedCardID, playerGuid.guid, serverGuid.guid, playfieldModelInstanceGuid.guid);
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool bankCardValidityCheck(Card card, Guid playerGuid)
        {
            //check if player has card in hand
            PlayerModel pm = getPlayerModel(playerGuid);
            foreach (Card c in pm.hand.cardsInHand)
            {
                if (c.cardID == card.cardID)
                {
                    return true;
                }
            }
            return false;
        }

        public bool playActionCardOnTurn(PlayerModel player, Card playedCard, PlayerModel playerTargeted, List<Card> cardsTargeted, GuidBox playerGuid, GuidBox serverGuid, GuidBox playfieldModelInstanceGuid, GuidBox turnActionGuid)
        {
            throw new NotImplementedException();
        }

        public bool playWildRentActionCardOnTurn(PlayerModel player, Card playedCard, PlayerModel playerTargeted, PropertyCardSet setOfPropertiesToRentOn, GuidBox playerGuid, GuidBox serverGuid, GuidBox playfieldModelInstanceGuid, GuidBox turnActionGuid)
        {
            throw new NotImplementedException();
        }

        public bool playStandardRentActionCardOnTurn(PlayerModel player, Card playedCard, PropertyCardSet setOfPropertiesToRentOn, GuidBox playerGuid, GuidBox serverGuid, GuidBox playfieldModelInstanceGuid, GuidBox turnActionGuid)
        {
            throw new NotImplementedException();
        }

        public bool playJustSayNo(PlayerModel player, Card playedCard, GuidBox playerGuid, GuidBox serverGuid, GuidBox playfieldModelInstanceGuid, GuidBox turnActionGuid)
        {
            throw new NotImplementedException();
        }

        public bool playPropertyCard(PlayerModel player, Card playedCard, PropertyCardSet setToPlayPropertyTo, GuidBox playerGuid, GuidBox serverGuid, GuidBox playfieldModelInstanceGuid, GuidBox turnActionGuid)
        {
            throw new NotImplementedException();
        }

        public bool movePropertyCard(PlayerModel player, Card propertyCard, PropertyCardSet oldSet, PropertyCardSet setToPlayPropertyTo, GuidBox playerGuid, GuidBox serverGuid, GuidBox playfieldModelInstanceGuid, GuidBox turnActionGuid)
        {
            throw new NotImplementedException();
        }

        public bool payCards(PlayerModel playerPaying, PlayerModel playerRecieving, List<Card> cards, GuidBox playerGuid, GuidBox serverGuid, GuidBox playfieldModelInstanceGuid, GuidBox turnActionGuid)
        {
            throw new NotImplementedException();
        }

        public void referenceAllDataContracts(ActionCard ac, Card c, FieldUpdateMessage fum, Message msg, MoneyCard mc, PlayerBank pb, PlayerHand ph, PlayerModel pm, PlayerPropertySets pps, PlayFieldModel pfm, PlayPile pp, PollForFieldUpdateMessage pffum, PropertyCard pc, PropertyCardSet pcs, PropertySetInfo psi, RentStandard rs, TakeActionOnTurnMessage taotm, TurnActionModel tam)
        {
            throw new NotImplementedException();
        }

        public bool hasGameStarted(GuidBox playerGuid, GuidBox serverGuid)
        {
            // bool allReady = true;
            foreach (PlayerModel p in players)
            {
                if (!p.isReadyToStartGame)
                {
                    return false;
                    ///allReady = false;//
                }
            }
            if (players.Count <= 1)//minplayers
            {
                return false;
            }
            return true;
        }

        public bool playPropertyCardNewSet(int playedCardID, GuidBox playerGuid, GuidBox serverGuid, GuidBox playfieldModelInstanceGuid)
        {
            //Play property card to new set only
            return gameStateManager.playPropertyCardToNewSet(serverGuid.guid, playerGuid.guid, playfieldModelInstanceGuid.guid, TurnActionTypes.PlayPropertyCard_New_Set, playedCardID);
        }

        public bool endTurn(GuidBox playerGuid, GuidBox gameGuid, GuidBox playfieldModelInstanceGuid)
        {
            return gameStateManager.doAction(gameGuid.guid, playerGuid.guid, playfieldModelInstanceGuid.guid, TurnActionTypes.EndTurn);
        }

        public bool discard(int cardsToDiscardID, GuidBox playerGuid, GuidBox serverGuid, GuidBox playfieldModelInstanceGuid)
        {
            return gameStateManager.discard(cardsToDiscardID, playerGuid.guid, serverGuid.guid, playfieldModelInstanceGuid.guid);
        }

        public bool draw5AtStartOfTurn(GuidBox playerGuid, GuidBox serverGuid, GuidBox playfieldModelInstanceGuid)
        {
            return gameStateManager.doAction(serverGuid.guid, playerGuid.guid, playfieldModelInstanceGuid.guid, TurnActionTypes.drawFiveCardsAtStartOfTurn);
        }

        #endregion From Service

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
                null, noActionsPlayedFirstPlayerToDraw, initialDrawPile, initialPlayPile, NEW_TURN_NUMBER_OF_CARDS_PLAYABLE, turnStart, Statephase.Turn_Started_Draw_2_Cards);
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
    }
}