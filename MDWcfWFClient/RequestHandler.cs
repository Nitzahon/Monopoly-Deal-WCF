using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace MDWcfWFClient
{
    // Specify for the callback to NOT use the current synchronization context
    [CallbackBehavior(ConcurrencyMode = ConcurrencyMode.Single, UseSynchronizationContext = false)]
    internal class RequestHandler : MonopolyDealServiceReference.IMonopolyDealCallback
    {
        SynchronizationContext uiSync;
        MonopolyDealServiceReference.MonopolyDealClient monopolyDealService;
        Form1 mainForm;
        ClientInfo clientInfo;
        ClientMessageHandler clientMessageHandler;

        public RequestHandler(SynchronizationContext uiSyncP, Form1 form1P)
        {
            uiSync = uiSyncP;
            mainForm = form1P;
        }

        //Service calling methods
        public void connect()
        {
            //Opens Connection to service
            try
            {
                // The client callback interface must be hosted for the server to invoke the callback
                // Open a connection to the Monopoly Deal service via the proxy
                monopolyDealService = new MonopolyDealServiceReference.MonopolyDealClient(new InstanceContext(this), "TcpBinding");
                monopolyDealService.Open();
                //End

                //Connect to service with player name
                monopolyDealService.connect(mainForm.textBoxPlayerName.Text);

                //Disable Connect button
                mainForm.buttonConnect.Enabled = false;
            }
            catch (Exception r)
            {
                MessageBox.Show(r.ToString());
                mainForm.buttonConnect.Enabled = true;
            }
        }

        public void startGame()
        {
            //Tell the service this player is ready and to start game if all players are ready
            if (clientInfo != null)
            {
                monopolyDealService.startGame(clientInfo.getGuidID());
            }
            else
            {
                MessageBox.Show("Have not recieved guid");
            }
        }

        public void pollState()
        {
            //Incomplete
            //MonopolyDealServiceReference.PollForFieldUpdateMessage pffum = new MonopolyDealServiceReference.PollForFieldUpdateMessage();
            // pffum.thisMessageGuid = Guid.NewGuid();
            //pffum.playerSendingMessage = clientInfo.getGuidID();
            //monopolyDealService.pollState(pffum);
            MonopolyDealServiceReference.PollForFieldUpdateMessage message = new MonopolyDealServiceReference.PollForFieldUpdateMessage();
            message.messageType = MonopolyDealServiceReference.MessageType.pollForFieldUpdate;
            message.thisMessageGuid = Guid.NewGuid();
            message.playerSendingMessage = clientInfo.guid;
            monopolyDealService.pollState(message);

            //monopolyDealService.pollState(null);
        }

        //UI Calling
        public void dotetxt(String description)
        {
            SendOrPostCallback callback =
                delegate(object state)
                { mainForm.updateChatTextBox(state.ToString()); };

            uiSync.Post(callback, description);
        }

        //Callback methods
        public void testOperationReturn()
        {
            throw new NotImplementedException();
        }

        public void testOperationReturn2(string text)
        {
            MessageBox.Show(text);
        }

        public void recieveGuid(Guid id)
        {
            //Creates clientInfo object which holds the local players information
            clientInfo = new ClientInfo(id);
            clientMessageHandler = new ClientMessageHandler(uiSync, mainForm, clientInfo.getGuidID());
            addToLog(clientInfo.guid + "");
            setGuid(id);
        }

        public void setGuid(Guid guid)
        {
            // The UI thread won't be handling the callback, but it is the only one allowed to update the controls.
            // So, we will dispatch the UI update back to the UI sync context.

            //Create a SendOrPostCallback delegate with an anon method which recieves an Object called state and runs code in the SychronisationContext it is marshalled to using Post
            SendOrPostCallback callback =
                delegate(object state)
                {
                    //state is a string object.
                    mainForm.setGuid((Guid)state);
                };
            //Post takes a delagate and a State object and runs the delegate(State object) in the context Post is called on.
            uiSync.Post(callback, guid);
        }

        public void addToLog(string description)
        {
            // The UI thread won't be handling the callback, but it is the only one allowed to update the controls.
            // So, we will dispatch the UI update back to the UI sync context.

            //Create a SendOrPostCallback delegate with an anon method which recieves an Object called state and runs code in the SychronisationContext it is marshalled to using Post
            SendOrPostCallback callback =
                delegate(object state)
                {
                    //state is a string object.
                    mainForm.updateTextBoxLog(state.ToString());
                };
            //Post takes a delagate and a State object and runs the delegate(State object) in the context Post is called on.
            uiSync.Post(callback, description);
        }

        public void recieveChat(string description)
        {
            throw new NotImplementedException();
        }

        public void getName()
        {
            throw new NotImplementedException();
        }

        public void displayLookAtPlayedCardsOptions(MonopolyDealServiceReference.Player currentPlayer, MonopolyDealServiceReference.Player[] players)
        {
            throw new NotImplementedException();
        }

        public void displayBankedCards(MonopolyDealServiceReference.Player player)
        {
            throw new NotImplementedException();
        }

        public void displayPlayedProperties(MonopolyDealServiceReference.Player player)
        {
            throw new NotImplementedException();
        }

        public void displayLast3PlayedActionCards()
        {
            throw new NotImplementedException();
        }

        public void displayCardsPlayedThisTurn()
        {
            throw new NotImplementedException();
        }

        public void displayNumberOfCardsInPlayersHand()
        {
            throw new NotImplementedException();
        }

        public void displayPlayerHand(MonopolyDealServiceReference.Player player)
        {
            throw new NotImplementedException();
        }

        public void displayCard(MonopolyDealServiceReference.Card[] cards)
        {
            throw new NotImplementedException();
        }

        public void askIfUsingJustSayNo(string text)
        {
            throw new NotImplementedException();
        }

        public void displayListOfPlayersWithId()
        {
            throw new NotImplementedException();
        }

        public void askWhoToDebtCollect()
        {
            throw new NotImplementedException();
        }

        public void askWhoToRent()
        {
            throw new NotImplementedException();
        }

        public void askWhichSetToDealBreak()
        {
            throw new NotImplementedException();
        }

        public void notifyTurnStarted()
        {
            throw new NotImplementedException();
        }

        public void notifyOtherPlayerTurnStarted(MonopolyDealServiceReference.Player p)
        {
            throw new NotImplementedException();
        }

        public void playTurnAction()
        {
            throw new NotImplementedException();
        }

        public void askWhatSetToAddHouseTo(MonopolyDealServiceReference.Player p, MonopolyDealServiceReference.Card c)
        {
            throw new NotImplementedException();
        }

        public void askWhatSetToAddHotelTo(MonopolyDealServiceReference.Player p, MonopolyDealServiceReference.Card c)
        {
            throw new NotImplementedException();
        }

        public void recieveMessage(MonopolyDealServiceReference.Message message)
        {
            addToLog("Message Recieved guid" + message.thisMessageGuid);
            clientMessageHandler.processMessage(message);
        }

        public void recieveID(int id)
        {
            clientInfo.id = id;
        }
    }
}