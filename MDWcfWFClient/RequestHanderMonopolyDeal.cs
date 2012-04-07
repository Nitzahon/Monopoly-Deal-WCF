using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace MDWcfWFClient
{
    internal class RequestHanderMonopolyDeal
    {
        private System.Threading.SynchronizationContext _uiSyncContext;
        private Form1 mainForm;
        //public Guid thisClientGuid;
        public Guid gameOnServiceGuid;
        public MonopolyDealServiceReference.PlayFieldModel CurrentPlayFieldModel;
        MonopolyDealServiceReference.MonopolyDealClient monopolyDealService;
        public Guid thisClientGuid;
        public Guid gameLobbyGuid;

        public RequestHanderMonopolyDeal(System.Threading.SynchronizationContext _uiSyncContext, Form1 form1)
        {
            // TODO: Complete member initialization
            this._uiSyncContext = _uiSyncContext;
            this.mainForm = form1;
        }

        #region Service Calls

        internal void connectToLobby(string name)
        {
            monopolyDealService = new MonopolyDealServiceReference.MonopolyDealClient("HttpBinding");
            try
            {
                getServiceReady();

                thisClientGuid = monopolyDealService.connectToLobby(name).guid;
                mainForm.buttonConnect.Enabled = false;
                mainForm.buttonStartGame.Enabled = true;
                addToLog("connected .guid" + thisClientGuid);
            }
            catch (Exception ex)
            {
                addToLog(ex.Message);
            }
        }

        public bool getServiceReady()
        {
            bool closedSuccess = false;
            if (monopolyDealService.State == CommunicationState.Created)
            {
                monopolyDealService.Open();
            }
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

        #endregion Service Calls

        #region Graphical Calls to Form

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
            _uiSyncContext.Post(callback, description);
        }

        public void displayLobbies(MonopolyDealServiceReference.GameLobby[] lobbies)
        {
            SendOrPostCallback callBack = delegate(Object state)
            {
                mainForm.displayLobbiesState(state as MonopolyDealServiceReference.GameLobby[]);
            };

            _uiSyncContext.Post(callBack, lobbies);
        }

        #endregion Graphical Calls to Form

        internal void iAmReady()
        {
            try
            {
                getServiceReady();

                bool success = monopolyDealService.setLobbyClientReady(gameLobbyGuid.boxGuid(), thisClientGuid.boxGuid(), true);
                if (success)
                {
                    addToLog("I am ready");
                }
                else
                {
                    addToLog("Try again");
                }
            }
            catch (Exception ex)
            {
                addToLog(ex.Message);
            }
        }

        internal void iAmNotReady()
        {
            try
            {
                getServiceReady();

                bool success = monopolyDealService.setLobbyClientReady(gameLobbyGuid.boxGuid(), thisClientGuid.boxGuid(), false);
                addToLog("I am not ready");
            }
            catch (Exception ex)
            {
                addToLog(ex.Message);
            }
        }

        internal void updateLobbies()
        {
            try
            {
                getServiceReady();
                MonopolyDealServiceReference.GameLobby[] gameLobbies = monopolyDealService.getListOfAllGameLobbys();
                displayLobbies(gameLobbies);
            }
            catch (Exception ex)
            {
                addToLog(ex.Message);
            }
        }

        internal void connectToNewLobby()
        {
            try
            {
                getServiceReady();
                gameLobbyGuid = monopolyDealService.joinNewGameLobby(thisClientGuid.boxGuid()).guid;
                addToLog("Connected to game. Guid:" + gameLobbyGuid.ToString());
            }
            catch (Exception ex)
            {
                addToLog(ex.Message);
            }
        }

        internal void connectToExistingLobby(Guid guid)
        {
            try
            {
                getServiceReady();
                bool success = monopolyDealService.joinExistingGameLobby(guid.boxGuid(), thisClientGuid.boxGuid());
                if (success)
                {
                    gameLobbyGuid = guid;
                    addToLog("Connected to game. Guid:" + gameLobbyGuid.ToString());
                }
                else
                {
                    addToLog("Failed to connect to game.");
                }
            }
            catch (Exception ex)
            {
                addToLog(ex.Message);
            }
        }
    }
}