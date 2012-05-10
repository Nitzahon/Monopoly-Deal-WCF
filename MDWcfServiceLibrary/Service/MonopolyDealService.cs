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
        // NOTE: The variables are  static.
        //       This is necessary since the service is using PerCall instancing.
        //       An instance of the service will be created each time a service method is invoked by a client.
        //       Consequently, the state must be persisted somewhere in between calls.

        private static List<PlayerModel> playerModels = new List<PlayerModel>();
        private static List<Guid> playerIdLookup = new List<Guid>();
        public static readonly int MAX_PLAYERS_PER_GAME = 5;
        public static readonly int MIN_PLAYERS_PER_GAME = 2;
        private static List<MonopolyDeal> monopolyDealGamesOnService = new List<MonopolyDeal>();
        internal static MonopolyDealGameGen mdGen = new MonopolyDealGameGen(monopolyDealGamesOnService);
        private static ILobby lobby = new Lobby(mdGen);

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
                return GameLobbyStatus.Full;
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

        /// <summary>
        /// Clients call this method to draw 2 cards at the start of their turn.
        /// </summary>
        /// <param name="playerGuid"></param>
        /// <param name="gameLobbyGuid"></param>
        /// <param name="playfieldModelInstanceGuid"></param>
        /// <param name="turnActionGuid"></param>
        /// <returns></returns>
        public bool draw2AtStartOfTurnMD(GuidBox playerGuid, GuidBox gameLobbyGuid, GuidBox playfieldModelInstanceGuid, GuidBox turnActionGuid)
        {
            try
            {
                //Find MonopolyDealGame
                MonopolyDeal md = getMonopolyDeal(gameLobbyGuid.guid);
                if (md != null)
                {
                    BoolResponseBox result = md.getMonopolyDealGameStateManager().drawTwoCardsAtTurnStart(playerGuid.guid, playfieldModelInstanceGuid.guid);
                    return result.success;
                }
                else return new BoolResponseBox(false, "Unable to find game with specified Guid").success;
            }
            catch (System.ServiceModel.CommunicationException ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Clients call this method to bank a card on their turn.
        /// </summary>
        /// <param name="playedCardID"></param>
        /// <param name="playerGuid"></param>
        /// <param name="gameLobbyGuid"></param>
        /// <param name="playfieldModelInstanceGuid"></param>
        /// <param name="turnActionGuid"></param>
        /// <returns></returns>
        public bool playCardFromHandToBankMD(int playedCardID, GuidBox playerGuid, GuidBox gameLobbyGuid, GuidBox playfieldModelInstanceGuid, GuidBox turnActionGuid)
        {
            try
            {
                //Find MonopolyDealGame
                MonopolyDeal md = getMonopolyDeal(gameLobbyGuid.guid);
                if (md != null)
                {
                    return md.getMonopolyDealGameStateManager().bankCard(playedCardID, playerGuid.guid, gameLobbyGuid.guid, playfieldModelInstanceGuid.guid);
                }
                else
                {
                    return false;
                }
            }
            catch (System.ServiceModel.CommunicationException ex)
            {
                throw ex;
            }
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

        public bool playPropertyCardNewSetMD(int playedCardID, bool isOrientedUp, GuidBox playerGuid, GuidBox gameLobbyGuid, GuidBox playfieldModelInstanceGuid)
        {
            try
            {
                //Find MonopolyDealGame
                MonopolyDeal md = getMonopolyDeal(gameLobbyGuid.guid);
                if (md != null)
                {
                    return md.getMonopolyDealGameStateManager().playPropertyCardToNewSet(gameLobbyGuid.guid, isOrientedUp, playerGuid.guid, playfieldModelInstanceGuid.guid, TurnActionTypes.PlayPropertyCard_New_Set, playedCardID);
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

        public bool endTurnMD(GuidBox playerGuid, GuidBox gameLobbyGuid, GuidBox playfieldModelInstanceGuid)
        {
            try
            {
                //Find MonopolyDealGame
                MonopolyDeal md = getMonopolyDeal(gameLobbyGuid.guid);
                if (md != null)
                {
                    return md.getMonopolyDealGameStateManager().endTurn(playerGuid.guid, playfieldModelInstanceGuid.guid).success;
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
                    return md.getMonopolyDealGameStateManager().drawFiveCards(playerGuid.guid, playfieldModelInstanceGuid.guid).success;
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

        public bool playActionCardDebtCollectorMD(int debtCollectorCardID, GuidBox targetedPlayerGuid, GuidBox playerGuid, GuidBox gameLobbyGuid, GuidBox playfieldModelInstanceGuid)
        {
            //Find MonopolyDealGame
            MonopolyDeal md = getMonopolyDeal(gameLobbyGuid.guid);
            if (md != null)
            {
                return md.getMonopolyDealGameStateManager().playDebtCollector(debtCollectorCardID, targetedPlayerGuid.guid, playerGuid.guid, gameLobbyGuid.guid, playfieldModelInstanceGuid.guid);
            }
            else
            {
                return false;
            }
        }

        public bool payCardsMD(GuidBox playerPaying, List<int> cardsToPayWith, GuidBox gameLobbyGuid, GuidBox serverGuid, GuidBox playfieldModelInstanceGuid, GuidBox turnActionGuid)
        {
            //Find MonopolyDealGame
            MonopolyDeal md = getMonopolyDeal(gameLobbyGuid.guid);
            if (md != null)
            {
                return md.getMonopolyDealGameStateManager().payDebt(cardsToPayWith, playerPaying.guid, gameLobbyGuid.guid, playfieldModelInstanceGuid.guid);
            }
            else
            {
                return false;
            }
        }

        public bool playActionCardItsMyBirthdayMD(int myBirthdayCardID, GuidBox playerGuid, GuidBox gameLobbyGuid, GuidBox playfieldModelInstanceGuid)
        {
            //Find MonopolyDealGame
            MonopolyDeal md = getMonopolyDeal(gameLobbyGuid.guid);
            if (md != null)
            {
                return md.getMonopolyDealGameStateManager().playActionCardItsMyBirthday(myBirthdayCardID, playerGuid.guid, gameLobbyGuid.guid, playfieldModelInstanceGuid.guid);
            }
            else
            {
                return false;
            }
        }

        public bool playJustSayNoMD(int playedCard, GuidBox playerGuid, GuidBox gameLobbyGuid, GuidBox playfieldModelInstanceGuid)
        {
            //Find MonopolyDealGame
            MonopolyDeal md = getMonopolyDeal(gameLobbyGuid.guid);
            if (md != null)
            {
                return md.getMonopolyDealGameStateManager().playActionCardJustSayNo(playedCard, playerGuid.guid, gameLobbyGuid.guid, playfieldModelInstanceGuid.guid);
            }
            else
            {
                return false;
            }
        }

        public BoolResponseBox movePropertyCardMD(int propertyCardToMoveID, bool isCardUp, bool moveToExistiongSet, GuidBox oldSetGuid,
            GuidBox setToPlayPropertyToGuid, GuidBox playerGuid, GuidBox gameLobbyGuid, GuidBox playfieldModelInstanceGuid)
        {
            //Find MonopolyDealGame
            MonopolyDeal md = getMonopolyDeal(gameLobbyGuid.guid);
            if (md != null)
            {
                return md.getMonopolyDealGameStateManager().movePropertyCard(propertyCardToMoveID, isCardUp, moveToExistiongSet, oldSetGuid.guid, setToPlayPropertyToGuid.guid,
                    playerGuid.guid, gameLobbyGuid.guid, playfieldModelInstanceGuid.guid);
            }
            else
            {
                return new BoolResponseBox(false, "Can not find game");
            }
        }

        public bool playWildRentActionCardOnTurnMD(PlayerModel player, Card playedCard, PlayerModel playerTargeted, PropertyCardSet setOfPropertiesToRentOn, GuidBox playerGuid, GuidBox gameLobbyGuid, GuidBox playfieldModelInstanceGuid, GuidBox turnActionGuid)
        {
            throw new NotImplementedException();
        }

        public bool playStandardRentActionCardOnTurnMD(PlayerModel player, Card playedCard, PropertyCardSet setOfPropertiesToRentOn, GuidBox playerGuid, GuidBox gameLobbyGuid, GuidBox playfieldModelInstanceGuid, GuidBox turnActionGuid)
        {
            throw new NotImplementedException();
        }
    }
}