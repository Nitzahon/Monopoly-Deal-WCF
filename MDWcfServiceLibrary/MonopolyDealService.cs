using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using MonopolyDealLibrary;

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
        private static List<IMonopolyDealCallback> _callbackList = new List<IMonopolyDealCallback>();
        private static int id = 2;
        private static List<WCFPlayer> wcfPlayers = new List<WCFPlayer>();
        private static List<Player> players = new List<Player>();
        //game contains state
        private static WCFGame game;
        private static bool gameCreated = false;
        private static bool isStarted = false;
        private static int NUMBER_OF_DECKS = 1;
        private static Deck deck = new Deck(NUMBER_OF_DECKS);

        private static String serverLog = "";
        private int i = 0;

        public string GetData(int value)
        {
            throw new NotImplementedException();
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            throw new NotImplementedException();
        }

        //Create new game
        private void createGame()
        {
            //Create Game if one does not exist
            if (!gameCreated)
            {
                game = new WCFGame(players, deck);
                gameCreated = true;
            }
        }

        private void createPlayer(string name)
        {
            // Subscribe the guest
            IMonopolyDealCallback guest = OperationContext.Current.GetCallbackChannel<IMonopolyDealCallback>();

            if (!_callbackList.Contains(guest))
            {
                _callbackList.Add(guest);
            }
            //Create new WCFPLayer for client
            WCFPlayer player = new WCFPlayer(name, guest);
            //Add player to list
            wcfPlayers.Add(player);
            //call back
            createPlayerCallback(player.getCallback(), player.getName(), player.getID());
        }

        private void createPlayerCallback(IMonopolyDealCallback playerCallback, string name, int id)
        {
            //CallBack one
            playerCallback.testOperationReturn2("Your Player Name:" + name + " ID:" + id);

            //Assign id to player
            playerCallback.recieveID(id);

            //Tell all players
            addToClientsLogs("Welcome Player:" + name);
            /*
            //CallBack all
            _callbackList.ForEach(
                delegate(IMonopolyDealCallback callback)
                { callback.testOperationReturn2("Welcome Player:" + name); });
             * */
        }

        private void addToClientsLogs(String description)
        {
            //CallBack all
            _callbackList.ForEach(
                delegate(IMonopolyDealCallback callback)
                { callback.addToLog(description); });
        }

        public void connect(string name)
        {
            //Create game
            createGame();
            //Future Only Add players if game has not started
            createPlayer(name);
        }

        public void testOperation(int id)
        {
        }

        private void testCallback(string name)
        {
        }

        public void startGame(int id)
        {
            wcfPlayers[id].setIfReady(true);
            bool allReady = true;
            foreach (WCFPlayer p in wcfPlayers)
            {
                if (!p.getIfReady())
                {
                    allReady = false;
                    addToClientsLogs("Player " + p.getName() + " is not Ready");
                }
                else
                {
                    addToClientsLogs("Player " + p.getName() + " is Ready");
                }
            }
            if (allReady && !game.getIfGameStarted())
            {
                game.startGame();
                addToClientsLogs("Game Started");
            }
        }

        public void chatToAll(string chat)
        {
            //CallBack all
            _callbackList.ForEach(
                delegate(IMonopolyDealCallback callback)
                { callback.recieveChat(chat); });
        }
    }
}