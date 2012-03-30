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
    public class MonopolyDealServiceOld : IMonopolyDealDuplex
    {
        // NOTE: The variables for storing callbacks and beer inventory are static.
        //       This is necessary since the service is using PerCall instancing.
        //       An instance of the service will be created each time a service method is invoked by a client.
        //       Consequently, the state must be persisted somewhere in between calls.
        private static List<IMonopolyDealCallback> _callbackList = new List<IMonopolyDealCallback>();
        private static int id = 2;
        //private static List<Player> wcfPlayers = new List<Player>();
        //private static List<Player> players = new List<Player>();
        //game contains state
        private static GameModel gameModel;
        private static List<PlayerModel> playerModels = new List<PlayerModel>();
        private static List<Guid> playerIdLookup = new List<Guid>();
        //private static WCFGame game;
        private static bool gameCreated = false;
        private static bool isStarted = false;
        private static int NUMBER_OF_DECKS = 1;
        private static Deck deck = new Deck(NUMBER_OF_DECKS);
        private static int MAX_PLAYERS_PER_GAME = 5;
        private static int numberOfPlayers = 0;
        private static String serverLog = "";
        private int i = 0;
        private static GameStateManager gameStateManager;

        private static MessageManager messageManager;

        public string GetData(int value)
        {
            throw new NotImplementedException();
        }

        //public CompositeType GetDataUsingDataContract(CompositeType composite)
        // {
        //    throw new NotImplementedException();
        //}

        //Create new game
        private void createGame()
        {
            //Create Game if one does not exist
            if (!gameCreated)
            {
                gameModel = new GameModel(playerModels, messageManager);
                messageManager = new MessageManager(gameModel);
                gameStateManager = new GameStateManager(gameModel);
                gameCreated = true;
            }
        }

        private void createPlayer(string name)
        {
            numberOfPlayers++;
            //WCF Model suitable
            // Subscribe the guest
            IMonopolyDealCallback guest = OperationContext.Current.GetCallbackChannel<IMonopolyDealCallback>();

            if (!_callbackList.Contains(guest))
            {
                _callbackList.Add(guest);
            }
            //Create new PlayerModel for client
            PlayerModel player = new PlayerModel(guest, name);
            //Add player to list
            playerModels.Add(player);
            playerIdLookup.Add(player.guid);
            //call back and tell player what number they are
            createPlayerCallback(player.ICallBack, player.name, playerModels.IndexOf(player), player.guid);
        }

        private void createPlayerCallback(IMonopolyDealCallback playerCallback, string name, int id, Guid guidP)
        {
            //CallBack one
            //playerCallback.testOperationReturn2("Your Player Name:" + name + " ID:" + id);
            playerCallback.addToLog("Your Player Name:" + name + " ID:" + id);
            //Assign guid to player
            playerCallback.recieveGuid(guidP);

            //Tell all players
            addToClientsLogs("Welcome Player:" + name);
            /*
            //CallBack all
            _callbackList.ForEach(
                delegate(IMonopolyDealCallback callback)
                { callback.testOperationReturn2("Welcome Player:" + name); });
             * */
        }

        public void addToClientsLogs(String description)
        {
            //CallBack all
            _callbackList.ForEach(
                delegate(IMonopolyDealCallback callback)
                { callback.addToLog(description); });
        }

        public void connect(string name)
        {
            //WCFMODELSUIT
            //Create a playermodel for client and add to list
            if (!isStarted && numberOfPlayers < MAX_PLAYERS_PER_GAME)
            {
                createPlayer(name);
            }
            //reject player as full
        }

        public void testOperation(int id)
        {
        }

        private void testCallback(string name)
        {
        }

        private PlayerModel getPlayerModelByGuid(Guid g)
        {
            //WCFMODELSUIT
            int id = playerIdLookup.IndexOf(g);
            return playerModels.ElementAt(id);
        }

        public void startGame(Guid guid)
        {
            //WCFMODELSUIT
            getPlayerModelByGuid(guid).isReadyToStartGame = true;
            bool allReady = true;
            foreach (PlayerModel p in playerModels)
            {
                if (!p.isReadyToStartGame)
                {
                    allReady = false;
                    addToClientsLogs("Player " + p.name + " is not Ready");
                }
                else
                {
                    addToClientsLogs("Player " + p.name + " is Ready");
                }
            }
            if (allReady)
            {
                //Create game
                createGame();
                isStarted = true;
                addToClientsLogs("Game Started");
                /*
                foreach (PlayerModel pm in playerModels)
                {
                    messageManager.respondToFieldUpdate(new PollForFieldUpdateMessage(pm.guid, Guid.NewGuid(), Guid.NewGuid(), gameModel.gameModelGuid, gameModel.gameStates.ElementAt(0), Guid.NewGuid(), new TurnActionModel()));
                }
                 * */
            }
        }

        public void chatToAll(string chat)
        {
            //CallBack all
            _callbackList.ForEach(
                delegate(IMonopolyDealCallback callback)
                { callback.recieveChat(chat); });
        }

        public void sendMessageToService(Message message)
        {
            messageManager.recieveNewMessage(message);
        }

        public void pollState(Message message)
        {
            addToClientsLogs("Polled");
            getPlayerModelByGuid(message.playerSendingMessage).ICallBack.addToLog("recieved poll");
            messageManager.recieveNewMessage(message);
        }

        public void referenceAllDataContracts(ActionCard ac, Card c, FieldUpdateMessage fum, Message msg, MoneyCard mc, PlayerBank pb, PlayerHand ph, PlayerModel pm, PlayerPropertySets pps, PlayFieldModel pfm, PlayPile pp, PollForFieldUpdateMessage pffum, PropertyCard pc, PropertyCardSet pcs, PropertySetInfo psi, RentStandard rs, TakeActionOnTurnMessage taotm, TurnActionModel tam)
        {
            throw new NotImplementedException();
        }

        public void draw2AtStartOfTurn(Guid playerGuid, Guid serverGuid, Guid playfieldModelInstanceGuid, Guid turnActionGuid)
        {
            List<Guid> guids = new List<Guid>();
            guids.Add(playerGuid);
            TurnActionModel tamDrawTwoST = new TurnActionModel(guids, serverGuid, playfieldModelInstanceGuid, turnActionGuid, new List<TurnActionModel.TurnActionTypes>(), TurnActionModel.TurnActionTypes.drawTwoCardsAtStartOfTurn, true);

            gameStateManager.doAction(tamDrawTwoST);
        }
    }
}