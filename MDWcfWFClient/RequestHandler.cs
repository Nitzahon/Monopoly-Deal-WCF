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
    public class RequestHandler //: MonopolyDealServiceReference.IMonopolyDealCallback
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

        public Guid thisClientGuid;
        public Guid gameOnServiceGuid;
        public MonopolyDealServiceReference.PlayFieldModel CurrentPlayFieldModel;

        //Service calling methods
        public void connect()
        {
            //Opens Connection to service
            try
            {
                // Open a connection to the Monopoly Deal service via the proxy
                monopolyDealService = new MonopolyDealServiceReference.MonopolyDealClient("HttpBinding");
                //monopolyDealService = new MonopolyDealServiceReference.MonopolyDealClient(new InstanceContext(this), "TcpBinding");
                monopolyDealService.Open();
                //End

                //Connect to service with player name
                //monopolyDealService.connect(mainForm.textBoxPlayerName.Text);
                thisClientGuid = monopolyDealService.connectToService(mainForm.textBoxPlayerName.Text);
                //Disable Connect button
                mainForm.buttonConnect.Enabled = false;
                mainForm.buttonStartGame.Enabled = true;
                addToLog("connected .guid" + thisClientGuid);
            }
            catch (Exception r)
            {
                MessageBox.Show(r.ToString());
                mainForm.buttonConnect.Enabled = true;
            }
        }

        public MonopolyDealServiceReference.PlayerModel getPlayerModelByGuid(Guid pmG)
        {
            MonopolyDealServiceReference.PlayerModel[] pms = CurrentPlayFieldModel.playerModels;
            foreach (MonopolyDealServiceReference.PlayerModel pm in pms)
            {
                if (pmG.CompareTo(pm.guid) == 0)
                {
                    return pm;
                }
            }
            //Player not found
            return null;
        }

        public void startGame()
        {
            //Tell the service this player is ready and to start game if all players are ready
            if (thisClientGuid.CompareTo(new Guid()) != 0)
            {
                MonopolyDealServiceReference.GuidBox gb = new MonopolyDealServiceReference.GuidBox();
                gb.guid = thisClientGuid;
                gameOnServiceGuid = monopolyDealService.startGame(gb).guid;
                addToLog("Connected to " + gameOnServiceGuid);
                mainForm.buttonStartGame.Enabled = false;
            }
            else
            {
                MessageBox.Show("Have not recieved guid, not connected");
            }
        }

        public void drawTwoAtTurnStart()
        {
            MonopolyDealServiceReference.TurnActionModel ta = CurrentPlayFieldModel.currentTurnActionModel;

            monopolyDealService.draw2AtStartOfTurn(thisClientGuid.boxGuid(), ta.serverGuid.boxGuid(), ta.currentPlayFieldModelGuid.boxGuid(), ta.thisTurnactionGuid.boxGuid());
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
            message.playerSendingMessage = thisClientGuid;
            //monopolyDealService.pollState(message);
            MonopolyDealServiceReference.GuidBox thisClientGuidB = new MonopolyDealServiceReference.GuidBox();
            thisClientGuidB.guid = thisClientGuid;
            MonopolyDealServiceReference.GuidBox gameOnServiceGuidB = new MonopolyDealServiceReference.GuidBox();
            gameOnServiceGuidB.guid = gameOnServiceGuid;
            MonopolyDealServiceReference.PlayFieldModel pfm = monopolyDealService.pollState(thisClientGuidB, gameOnServiceGuidB);
            CurrentPlayFieldModel = pfm;
            mainForm.drawField(CurrentPlayFieldModel);
        }

        //UI Calling
        public void dotetxt(String description)
        {
            SendOrPostCallback callback =
                delegate(object state)
                { mainForm.updateChatTextBox(state.ToString()); };

            uiSync.Post(callback, description);
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

        public void recieveMessage(MonopolyDealServiceReference.Message message)
        {
            addToLog("Message Recieved guid" + message.thisMessageGuid);
            clientMessageHandler.processMessage(message);
        }

        public void recieveID(int id)
        {
            clientInfo.id = id;
        }

        internal void bankCard(int cardToBankID)
        {
            try
            {
                bool success = monopolyDealService.playCardFromHandToBank(cardToBankID, thisClientGuid.boxGuid(), gameOnServiceGuid.boxGuid(), CurrentPlayFieldModel.thisPlayFieldModelInstanceGuid.boxGuid(), CurrentPlayFieldModel.currentTurnActionModel.thisTurnactionGuid.boxGuid());
                addToLog(success.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}