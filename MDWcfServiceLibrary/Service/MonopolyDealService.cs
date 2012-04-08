using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace MDWcfServiceLibrary
{
    //MonopolyDealService implements IMonopolyDeal interface using Duplex mode
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Single, InstanceContextMode = InstanceContextMode.PerCall)]
    public class MonopolyDealService : IMonopolyDeal
    {
        // NOTE: The variables for storing callbacks and beer inventory are static.
        //       This is necessary since the service is using PerCall instancing.
        //       An instance of the service will be created each time a service method is invoked by a client.
        //       Consequently, the state must be persisted somewhere in between calls.

        //private static int id = 2;

        //game contains state
        private static GameModel gameModel;
        private static List<PlayerModel> playerModels = new List<PlayerModel>();
        private static List<Guid> playerIdLookup = new List<Guid>();
        //private static WCFGame game;
        private static bool gameCreated = false;
        private static bool isStarted = false;
        private static int NUMBER_OF_DECKS = 1;
        //private static Deck deck;// = new Deck(NUMBER_OF_DECKS);
        public static readonly int MAX_PLAYERS_PER_GAME = 5;
        public static readonly int MIN_PLAYERS_PER_GAME = 2;
        private static int numberOfPlayers = 0;
        //private static String serverLog = "";
        private int i = 0;
        private static GameStateManager gameStateManager;

        private static Guid gameGuid;
        private static bool gameGuidSet = false;

        private static MessageManager messageManager;
        //internal static List<MonopolyDeal> gamesOnService = new List<MonopolyDeal>();
        private static List<MonopolyDeal> monopolyDealGamesOnService = new List<MonopolyDeal>();
        internal static MonopolyDealGameGen mdGen = new MonopolyDealGameGen(monopolyDealGamesOnService);

        //public static MonopolyDeal gameInterface = new MonopolyDeal(null, new Guid()); //for getting game info only
        private static ILobby lobby = new Lobby(mdGen);

        //Create new game
        private void createGame()
        {
            //Create Game if one does not exist
            if (!gameCreated)
            {
                gameModel = new GameModel(playerModels, messageManager, gameGuid);
                messageManager = new MessageManager(gameModel);
                gameStateManager = new GameStateManager(gameModel);
                gameCreated = true;
            }
        }

        private PlayerModel getPlayerModelByGuid(Guid g)
        {
            //WCFMODELSUIT
            int id = playerIdLookup.IndexOf(g);
            return playerModels.ElementAt(id);
        }

        private Guid createPlayer(string name)
        {
            numberOfPlayers++;
            //WCF Model suitable

            //Create new PlayerModel for client
            PlayerModel player = new PlayerModel(name);
            //Add player to list
            playerModels.Add(player);
            playerIdLookup.Add(player.guid);
            //call back and tell player what number they are
            return player.guid;
        }

        //
        public Guid connectToService(string name)
        {
            //WCFMODELSUIT
            //Create a playermodel for client and add to list
            if (!isStarted && numberOfPlayers < MAX_PLAYERS_PER_GAME)
            {
                if (!gameGuidSet)
                {
                    gameGuid = Guid.NewGuid();
                }
                return createPlayer(name);
            }
            //reject player as full
            return new Guid();
        }

        public GuidBox startGame(GuidBox guidBoxed)
        {
            Guid guid = guidBoxed.guid;
            //WCFMODELSUIT
            getPlayerModelByGuid(guid).isReadyToStartGame = true;
            bool allReady = true;
            foreach (PlayerModel p in playerModels)
            {
                if (!p.isReadyToStartGame)
                {
                    allReady = false;
                    //addToClientsLogs("Player " + p.name + " is not Ready");
                }
                else
                {
                    //addToClientsLogs("Player " + p.name + " is Ready");
                }
                if (playerIdLookup.Count <= 1)
                {
                    allReady = false;
                }
            }
            if (allReady)
            {
                //Create game
                createGame();
                isStarted = true;
                GuidBox allreadyG = new GuidBox();
                allreadyG.bool1 = true;
                allreadyG.guid = gameModel.gameModelGuid;
                return allreadyG;
            }
            GuidBox notallreadyG = new GuidBox();
            notallreadyG.bool1 = false;
            notallreadyG.guid = gameGuid;
            return notallreadyG; //not all players ready
        }

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
            Card card = gameModel.deck.getCardByID(playedCardID);
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
            PlayerModel pm = getPlayerModelByGuid(playerGuid);
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
            foreach (PlayerModel p in playerModels)
            {
                if (!p.isReadyToStartGame)
                {
                    return false;
                    ///allReady = false;//
                }
            }
            if (playerIdLookup.Count <= 1)//minplayers
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

        public bool playActionCardPassGo(int passGoCardID, GuidBox playerGuid, GuidBox serverGuid, GuidBox playfieldModelInstanceGuid)
        {
            return gameStateManager.playActionCardPassGo(passGoCardID, serverGuid.guid, playerGuid.guid, playfieldModelInstanceGuid.guid, TurnActionTypes.PlayActionCard);
        }

        public void referenceAllDataContracts(ActionCard ac, Card c, MoneyCard mc, PlayerBank pb, PlayerHand ph, PlayerModel pm, PlayerPropertySets pps, PlayFieldModel pfm, PlayPile pp, PropertyCard pc, PropertyCardSet pcs, PropertySetInfo psi, RentStandard rs, TakeActionOnTurnMessage taotm, TurnActionModel tam)
        {
            throw new NotImplementedException();
        }

        #region gameLobbyMethods

        public GuidBox connectToLobby(string name)
        {
            try
            {
                GuidBox success = lobby.connectToLobby(name).boxGuid();
                return success;
            }
            catch (Exception ex)
            {
                //Replace with better handling for system.servicemodel exceptions
                return new GuidBox();
            }
        }

        public GameLobbyStatus getGameLobbyStatus(GuidBox gameLobbyGuidP)
        {
            try
            {
                return lobby.getGameLobbyStatus(gameLobbyGuidP.guid);
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }

        public List<GameLobby> getListOfAllGameLobbys()
        {
            try
            {
                return lobby.getListOfAllGameLobbys();
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }

        public bool joinExistingGameLobby(GuidBox gameLobbyGuidP, GuidBox clientGuidP)
        {
            try
            {
                return lobby.joinExistingGameLobby(gameLobbyGuidP.guid, clientGuidP.guid);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public GuidBox joinNewGameLobby(GuidBox clientGuidP)
        {
            try
            {
                return lobby.joinNewGameLobby(clientGuidP.guid).boxGuid();
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }

        public bool exitGameLobby(GuidBox clientGuidP)
        {
            try
            {
                return lobby.exitGameLobby(clientGuidP.guid);
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }

        public bool setLobbyClientReady(GuidBox gameLobbyGuidP, GuidBox clientGuidP, bool readyP)
        {
            try
            {
                return lobby.setLobbyClientReady(gameLobbyGuidP.guid, clientGuidP.guid, readyP);
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }

        public bool checkIfGameStarted(GuidBox gameLobbyGuidP)
        {
            try
            {
                return lobby.checkIfGameStarted(gameLobbyGuidP.guid);
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }

        #endregion gameLobbyMethods

        #region IGameMethods

        public int getMinPlayersPerGame()
        {
            return MonopolyDeal.getMinPlayers();
        }

        public int getMaxPlayersPerGame()
        {
            return MonopolyDeal.getMaxPlayers();
        }

        #endregion IGameMethods

        #region MonopolyDealGameMethods

        /// <summary>
        /// Creates a new instance of MonopolyDeal and adds it to the list of games on service
        /// </summary>
        /// <param name="clients"></param>
        /// <param name="guidForGame"></param>
        /// <returns></returns>
        public bool startNewMonopolyDealGame(List<LobbyClient> clients, Guid guidForGame)
        {
            try
            {
                List<PlayerModel> players = new List<PlayerModel>();
                foreach (LobbyClient lc in clients)
                {
                    PlayerModel p = new PlayerModel(lc.getName());
                    p.guid = lc.getGuid();
                    p.isReadyToStartGame = true;
                    players.Add(p);
                }
                MonopolyDeal game = new MonopolyDeal(players, guidForGame);
                monopolyDealGamesOnService.Add(game);
                return true;
            }
            catch (System.ServiceModel.CommunicationException ex)
            {
                throw new NotImplementedException(ex.Message);
            }
        }

        /// <summary>
        /// Gets the current state of a Monopoly Deal Game on service
        /// </summary>
        /// <param name="playerGuid">Guid of player</param>
        /// <param name="gameGuid">Guid of MonopolyDeal game instance</param>
        /// <returns>PlayFieldModel of current state</returns>
        public PlayFieldModel pollStateMonopolyDeal(GuidBox playerGuid, GuidBox gameGuid)
        {
            try
            {
                //Find MonopolyDealGame
                MonopolyDeal md = getMonopolyDeal(gameGuid.guid);
                if (md != null)
                {
                    //Ask GameStateManager for currentState
                    PlayFieldModel currentState = md.getMonopolyDealGameStateManager().getCurrentState();
                    //TODO:Current state should be filtered here so players cant see other players hands
                    //TODO:Could send Acknowledgement at this point but currently will have client send Acknowledgement
                    return currentState;
                }
                else
                {
                    return null;
                }
            }
            catch (System.ServiceModel.CommunicationException ex)
            {
                throw new FaultException(ex.Message);
            }
        }

        /// <summary>
        /// Gets an instance of MonopolyDeal from the list of MonopolyDeal games on service
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        internal MonopolyDeal getMonopolyDeal(Guid guid)
        {
            foreach (MonopolyDeal md in monopolyDealGamesOnService)
            {
                if (md.getGuid().CompareTo(guid) == 0)
                {
                    //Game found
                    return md;
                }
            }
            //Game not found
            return null;
        }

        public bool draw2AtStartOfTurnMD(GuidBox playerGuid, GuidBox gameLobbyGuid, GuidBox playfieldModelInstanceGuid, GuidBox turnActionGuid)
        {
            try
            {
                //Find MonopolyDealGame
                MonopolyDeal md = getMonopolyDeal(gameLobbyGuid.guid);
                if (md != null)
                {
                    return md.getMonopolyDealGameStateManager().doAction(gameLobbyGuid.guid, playerGuid.guid, playfieldModelInstanceGuid.guid, TurnActionTypes.drawTwoCardsAtStartOfTurn);
                }
                else
                {
                    return false;
                }
            }
            catch (System.ServiceModel.CommunicationException ex)
            {
                throw new FaultException(ex.Message);
            }
        }

        public bool playCardFromHandToBankMD(int playedCardID, GuidBox playerGuid, GuidBox gameLobbyGuid, GuidBox playfieldModelInstanceGuid, GuidBox turnActionGuid)
        {
            try
            {
                //Find MonopolyDealGame
                MonopolyDeal md = getMonopolyDeal(gameLobbyGuid.guid);
                if (md != null)
                {
                    Guid pg = playerGuid.guid;
                    Card card = md.deck.getCardByID(playedCardID);
                    bool result = md.bankCardValidityCheck(card, pg);
                    if (result)
                    {
                        md.getMonopolyDealGameStateManager().bankCard(playedCardID, playerGuid.guid, gameLobbyGuid.guid, playfieldModelInstanceGuid.guid);
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            catch (System.ServiceModel.CommunicationException ex)
            {
                throw new FaultException(ex.Message);
            }
        }

        public bool playActionCardOnTurnMD(PlayerModel player, Card playedCard, PlayerModel playerTargeted, List<Card> cardsTargeted, GuidBox playerGuid, GuidBox gameLobbyGuid, GuidBox playfieldModelInstanceGuid, GuidBox turnActionGuid)
        {
            throw new NotImplementedException();
        }

        public bool playWildRentActionCardOnTurnMD(PlayerModel player, Card playedCard, PlayerModel playerTargeted, PropertyCardSet setOfPropertiesToRentOn, GuidBox playerGuid, GuidBox gameLobbyGuid, GuidBox playfieldModelInstanceGuid, GuidBox turnActionGuid)
        {
            throw new NotImplementedException();
        }

        public bool playStandardRentActionCardOnTurnMD(PlayerModel player, Card playedCard, PropertyCardSet setOfPropertiesToRentOn, GuidBox playerGuid, GuidBox gameLobbyGuid, GuidBox playfieldModelInstanceGuid, GuidBox turnActionGuid)
        {
            throw new NotImplementedException();
        }

        public bool playJustSayNoMD(PlayerModel player, Card playedCard, GuidBox playerGuid, GuidBox gameLobbyGuid, GuidBox playfieldModelInstanceGuid, GuidBox turnActionGuid)
        {
            throw new NotImplementedException();
        }

        public bool playPropertyCardMD(PlayerModel player, Card playedCard, PropertyCardSet setToPlayPropertyTo, GuidBox playerGuid, GuidBox gameLobbyGuid, GuidBox playfieldModelInstanceGuid, GuidBox turnActionGuid)
        {
            try
            {
                //Find MonopolyDealGame
                MonopolyDeal md = getMonopolyDeal(gameLobbyGuid.guid);
                if (md != null)
                {
                    return md.getMonopolyDealGameStateManager().playPropertyCardToExistingSet(playedCard, setToPlayPropertyTo, gameLobbyGuid.guid, playerGuid.guid, playfieldModelInstanceGuid.guid);
                }
                else
                {
                    return false;
                }
            }
            catch (System.ServiceModel.CommunicationException ex)
            {
                throw new FaultException(ex.Message);
            }
        }

        public bool playPropertyCardNewSetMD(int playedCardID, GuidBox playerGuid, GuidBox gameLobbyGuid, GuidBox playfieldModelInstanceGuid)
        {
            try
            {
                //Find MonopolyDealGame
                MonopolyDeal md = getMonopolyDeal(gameLobbyGuid.guid);
                if (md != null)
                {
                    return md.getMonopolyDealGameStateManager().playPropertyCardToNewSet(gameLobbyGuid.guid, playerGuid.guid, playfieldModelInstanceGuid.guid, TurnActionTypes.PlayPropertyCard_New_Set, playedCardID);
                }
                else
                {
                    return false;
                }
            }
            catch (System.ServiceModel.CommunicationException ex)
            {
                throw new FaultException(ex.Message);
            }
        }

        public bool movePropertyCardMD(PlayerModel player, Card propertyCard, PropertyCardSet oldSet, PropertyCardSet setToPlayPropertyTo, GuidBox playerGuid, GuidBox gameLobbyGuid, GuidBox playfieldModelInstanceGuid, GuidBox turnActionGuid)
        {
            throw new NotImplementedException();
        }

        public bool payCardsMD(PlayerModel playerPaying, PlayerModel playerRecieving, List<Card> cards, GuidBox gameLobbyGuid, GuidBox serverGuid, GuidBox playfieldModelInstanceGuid, GuidBox turnActionGuid)
        {
            throw new NotImplementedException();
        }

        public bool endTurnMD(GuidBox playerGuid, GuidBox gameLobbyGuid, GuidBox playfieldModelInstanceGuid)
        {
            try
            {
                //Find MonopolyDealGame
                MonopolyDeal md = getMonopolyDeal(gameLobbyGuid.guid);
                if (md != null)
                {
                    return md.getMonopolyDealGameStateManager().doAction(gameLobbyGuid.guid, playerGuid.guid, playfieldModelInstanceGuid.guid, TurnActionTypes.EndTurn);
                }
                else
                {
                    return false;
                }
            }
            catch (System.ServiceModel.CommunicationException ex)
            {
                throw new FaultException(ex.Message);
            }
        }

        public bool discardMD(int cardsToDiscardIDs, GuidBox playerGuid, GuidBox gameLobbyGuid, GuidBox playfieldModelInstanceGuid)
        {
            try
            {
                //Find MonopolyDealGame
                MonopolyDeal md = getMonopolyDeal(gameLobbyGuid.guid);
                if (md != null)
                {
                    return md.getMonopolyDealGameStateManager().discard(cardsToDiscardIDs, playerGuid.guid, gameLobbyGuid.guid, playfieldModelInstanceGuid.guid);
                }
                else
                {
                    return false;
                }
            }
            catch (System.ServiceModel.CommunicationException ex)
            {
                throw new FaultException(ex.Message);
            }
        }

        public bool playActionCardPassGoMD(int passGoCardID, GuidBox playerGuid, GuidBox gameLobbyGuid, GuidBox playfieldModelInstanceGuid)
        {
            try
            {
                //Find MonopolyDealGame
                MonopolyDeal md = getMonopolyDeal(gameLobbyGuid.guid);
                if (md != null)
                {
                    return md.getMonopolyDealGameStateManager().playActionCardPassGo(passGoCardID, gameLobbyGuid.guid, playerGuid.guid, playfieldModelInstanceGuid.guid, TurnActionTypes.PlayActionCard);
                }
                else
                {
                    return false;
                }
            }
            catch (System.ServiceModel.CommunicationException ex)
            {
                throw new FaultException(ex.Message);
            }
        }

        public bool draw5AtStartOfTurnMD(GuidBox playerGuid, GuidBox gameLobbyGuid, GuidBox playfieldModelInstanceGuid)
        {
            try
            {
                //Find MonopolyDealGame
                MonopolyDeal md = getMonopolyDeal(gameLobbyGuid.guid);
                if (md != null)
                {
                    return md.getMonopolyDealGameStateManager().doAction(gameLobbyGuid.guid, playerGuid.guid, playfieldModelInstanceGuid.guid, TurnActionTypes.drawFiveCardsAtStartOfTurn);
                }
                else
                {
                    return false;
                }
            }
            catch (System.ServiceModel.CommunicationException ex)
            {
                throw new FaultException(ex.Message);
            }
        }

        public bool hasGameStartedMD(GuidBox playerGuid, GuidBox gameLobbyGuid)
        {
            throw new NotImplementedException();
        }

        #endregion MonopolyDealGameMethods
    }
}