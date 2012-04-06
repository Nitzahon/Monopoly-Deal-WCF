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
    //[CallbackBehavior(ConcurrencyMode = ConcurrencyMode.Single, UseSynchronizationContext = false)]
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
                //monopolyDealService.Open();
                getServiceReady();
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
                monopolyDealService.Close();
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
            getServiceReady();
            try
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
            catch (Exception ex)
            {
                addToLog(ex.ToString());
                //monopolyDealService.Close();
            }
        }

        public void drawTwoAtTurnStart()
        {
            getServiceReady();
            try
            {
                MonopolyDealServiceReference.TurnActionModel ta = CurrentPlayFieldModel.currentTurnActionModel;

                monopolyDealService.draw2AtStartOfTurn(thisClientGuid.boxGuid(), ta.serverGuid.boxGuid(), ta.currentPlayFieldModelGuid.boxGuid(), ta.thisTurnactionGuid.boxGuid());
            }
            catch (Exception ex)
            {
                addToLog(ex.ToString());
                //monopolyDealService.Close();
            }
        }

        public void pollState()
        {
            getServiceReady();
            try
            {
                //MonopolyDealServiceReference.PollForFieldUpdateMessage message = new MonopolyDealServiceReference.PollForFieldUpdateMessage();
                //message.messageType = MonopolyDealServiceReference.MessageType.pollForFieldUpdate;
                //message.thisMessageGuid = Guid.NewGuid();
                //message.playerSendingMessage = thisClientGuid;
                //monopolyDealService.pollState(message);
                MonopolyDealServiceReference.GuidBox thisClientGuidB = new MonopolyDealServiceReference.GuidBox();
                thisClientGuidB.guid = thisClientGuid;
                MonopolyDealServiceReference.GuidBox gameOnServiceGuidB = new MonopolyDealServiceReference.GuidBox();
                gameOnServiceGuidB.guid = gameOnServiceGuid;
                MonopolyDealServiceReference.PlayFieldModel pfm = monopolyDealService.pollState(thisClientGuidB, gameOnServiceGuidB);
                CurrentPlayFieldModel = pfm;
                mainForm.drawField(CurrentPlayFieldModel);
                addToLog(pfm.currentPhase.ToString());
                foreach (MonopolyDealServiceReference.PlayerModel p in pfm.playerModels)
                {
                    if (p.isThisPlayersTurn)
                    {
                        addToLog("Player: " + p.name + "'s Turn");
                    }
                }
            }
            catch (Exception ex)
            {
                addToLog(ex.ToString());
                //monopolyDealService.Close();
            }
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

        public bool getServiceReady()
        {
            bool closedSuccess = false;
            if (monopolyDealService.State == CommunicationState.Opened)
            {
                return true;
            }
            else if (monopolyDealService.State == CommunicationState.Faulted || monopolyDealService.State == CommunicationState.Closed || monopolyDealService.State == CommunicationState.Closing)
            {
                try
                {
                    monopolyDealService.Close();
                    closedSuccess = true;
                }
                catch (Exception eC)
                {
                    addToLog(eC.ToString());
                    monopolyDealService.Abort();
                    closedSuccess = true;
                }
            }
            if (closedSuccess)
            {
                try
                {
                    monopolyDealService = new MonopolyDealServiceReference.MonopolyDealClient("HttpBinding");
                    monopolyDealService.Open();
                    return true;
                }
                catch (Exception eO)
                {
                    addToLog(eO.ToString());
                    return false;
                }
            }
            return false;
        }

        internal void bankCard(int cardToBankID)
        {
            getServiceReady();
            try
            {
                bool success = monopolyDealService.playCardFromHandToBank(cardToBankID, thisClientGuid.boxGuid(), gameOnServiceGuid.boxGuid(), CurrentPlayFieldModel.thisPlayFieldModelInstanceGuid.boxGuid(), CurrentPlayFieldModel.currentTurnActionModel.thisTurnactionGuid.boxGuid());
                addToLog(success.ToString());
            }
            catch (Exception ex)
            {
                addToLog(ex.ToString());
                try
                {
                    monopolyDealService.Close();
                }
                catch (Exception exC)
                {
                    monopolyDealService.Abort();
                    addToLog(exC.ToString());
                }
            }
        }

        internal bool checkHasGameStarted()
        {
            if (mainForm.buttonConnect.Enabled)
            {
                monopolyDealService = new MonopolyDealServiceReference.MonopolyDealClient("HttpBinding");
            }
            getServiceReady();
            try
            {
                return monopolyDealService.hasGameStarted(thisClientGuid.boxGuid(), gameOnServiceGuid.boxGuid());
            }
            catch (Exception ex)
            {
                addToLog(ex.ToString());
                return false;
            }
        }

        internal void playPropertyToNewSet(int cardIDOfPropertyToPlay)
        {
            if (monopolyDealService.playPropertyCardNewSet(cardIDOfPropertyToPlay, thisClientGuid.boxGuid(), gameOnServiceGuid.boxGuid(), CurrentPlayFieldModel.thisPlayFieldModelInstanceGuid.boxGuid()))
            {
                addToLog("Property played to new set");
            }
            else
            {
                addToLog("Error, card not played");
            }
        }

        internal void endTurn()
        {
            getServiceReady();
            try
            {
                monopolyDealService.endTurn(thisClientGuid.boxGuid(), gameOnServiceGuid.boxGuid(), CurrentPlayFieldModel.thisPlayFieldModelInstanceGuid.boxGuid());
            }
            catch (Exception ex)
            {
                addToLog(ex.ToString());
                monopolyDealService.Close();
            }
        }

        internal void drawFiveAtTurnStart()
        {
            getServiceReady();
            try
            {
                monopolyDealService.draw5AtStartOfTurn(thisClientGuid.boxGuid(), gameOnServiceGuid.boxGuid(), CurrentPlayFieldModel.thisPlayFieldModelInstanceGuid.boxGuid());
            }
            catch (Exception ex)
            {
                addToLog(ex.ToString());
                monopolyDealService.Close();
            }
        }

        internal void discard1Card(int p)
        {
            getServiceReady();
            try
            {
                monopolyDealService.discard(p, thisClientGuid.boxGuid(), gameOnServiceGuid.boxGuid(), CurrentPlayFieldModel.thisPlayFieldModelInstanceGuid.boxGuid());
            }
            catch (Exception ex)
            {
                addToLog(ex.ToString());
                monopolyDealService.Close();
            }
        }

        internal bool passGo(int p)
        {
            getServiceReady();
            try
            {
                return monopolyDealService.playActionCardPassGo(p, thisClientGuid.boxGuid(), gameOnServiceGuid.boxGuid(), CurrentPlayFieldModel.thisPlayFieldModelInstanceGuid.boxGuid());
            }
            catch (Exception ex)
            {
                addToLog(ex.ToString());
                monopolyDealService.Close();
            }
            return false;
        }
    }
}