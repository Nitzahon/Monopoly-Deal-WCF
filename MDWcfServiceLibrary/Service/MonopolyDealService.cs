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

        private static int id = 2;

        //game contains state
        private static GameModel gameModel;
        private static List<PlayerModel> playerModels = new List<PlayerModel>();
        private static List<Guid> playerIdLookup = new List<Guid>();
        //private static WCFGame game;
        private static bool gameCreated = false;
        private static bool isStarted = false;
        private static int NUMBER_OF_DECKS = 1;
        //private static Deck deck;// = new Deck(NUMBER_OF_DECKS);
        private static int MAX_PLAYERS_PER_GAME = 5;
        private static int numberOfPlayers = 0;
        private static String serverLog = "";
        private int i = 0;
        private static GameStateManager gameStateManager;

        private static Guid gameGuid;
        private static bool gameGuidSet = false;

        private static MessageManager messageManager;

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

        public PlayFieldModel pollState(Guid playerGuid, Guid gameGuid)
        {
            //addToClientsLogs("Polled");
            //getPlayerModelByGuid(message.playerSendingMessage).ICallBack.addToLog("recieved poll");
            //messageManager.recieveNewMessage(message);
            throw new NotImplementedException();
        }

        public bool draw2AtStartOfTurn(GuidBox playerGuid, GuidBox gameGuid, GuidBox playfieldModelInstanceGuid, GuidBox turnActionGuid)
        {
            return gameStateManager.doAction(gameGuid.guid, playerGuid.guid, playfieldModelInstanceGuid.guid, TurnActionTypes.drawTwoCardsAtStartOfTurn);
        }

        /*
        public bool draw2AtStartOfTurnOld(GuidBox playerGuid, GuidBox serverGuid, GuidBox playfieldModelInstanceGuid, GuidBox turnActionGuid)
        {
            List<Guid> guids = new List<Guid>();
            guids.Add(playerGuid.guid);
            TurnActionModel tamDrawTwoST = new TurnActionModel(guids, serverGuid.guid, playfieldModelInstanceGuid.guid, turnActionGuid.guid, new List<TurnActionTypes>(), TurnActionTypes.drawTwoCardsAtStartOfTurn, true);
            return gameStateManager.doAction(tamDrawTwoST);
        }
         * */

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
                gameStateManager.bankCard(card, getPlayerModelByGuid(playerGuid.guid));
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

        public bool discard(PlayerModel player, Card[] cardsToDiscard, GuidBox playerGuid, GuidBox serverGuid, GuidBox playfieldModelInstanceGuid, GuidBox turnActionGuid)
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
            return true;
        }

        public bool playPropertyCardNewSet(int playedCardID, GuidBox playerGuid, GuidBox serverGuid, GuidBox playfieldModelInstanceGuid)
        {
            //Play property card to new set only
            throw new NotImplementedException();
        }

        public bool endTurn(GuidBox playerGuid, GuidBox gameGuid, GuidBox playfieldModelInstanceGuid)
        {
            return gameStateManager.doAction(gameGuid.guid, playerGuid.guid, playfieldModelInstanceGuid.guid, TurnActionTypes.EndTurn);
        }
    }
}