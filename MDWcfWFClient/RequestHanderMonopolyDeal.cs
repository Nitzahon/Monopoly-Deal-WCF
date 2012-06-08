using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace MDWcfWFClient
{
    public class RequestHanderMonopolyDeal
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
                mainForm.buttonConnect1.Enabled = false;
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

        #region Lobby Service Calls

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
                gameOnServiceGuid = gameLobbyGuid;
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
                if (guid.CompareTo(new Guid()) != 0)
                {
                    bool success = monopolyDealService.joinExistingGameLobby(guid.boxGuid(), thisClientGuid.boxGuid());
                    if (success)
                    {
                        gameLobbyGuid = guid;
                        gameOnServiceGuid = guid;
                        addToLog("Connected to game. Guid:" + gameLobbyGuid.ToString());
                    }
                    else
                    {
                        addToLog("Failed to connect to game.");
                    }
                }
            }
            catch (Exception ex)
            {
                addToLog(ex.Message);
            }
        }

        internal void leaveGameLobby()
        {
            try
            {
                getServiceReady();
                bool success = monopolyDealService.exitGameLobby(thisClientGuid.boxGuid());
                if (success)
                {
                    addToLog("Disconnected from game.");
                }
                else
                {
                    addToLog("Unable to disconnect.");
                }
            }
            catch (Exception ex)
            {
                addToLog(ex.Message);
            }
        }

        #endregion Lobby Service Calls

        internal void pollState()
        {
            getServiceReady();
            try
            {
                MonopolyDealServiceReference.GuidBox thisClientGuidB = new MonopolyDealServiceReference.GuidBox();
                thisClientGuidB.guid = thisClientGuid;
                MonopolyDealServiceReference.GuidBox gameOnServiceGuidB = new MonopolyDealServiceReference.GuidBox();
                gameOnServiceGuidB.guid = gameOnServiceGuid;
                MonopolyDealServiceReference.PlayFieldModel pfm = monopolyDealService.pollStateMonopolyDeal(thisClientGuidB, gameOnServiceGuidB);
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

        internal void playPropertyToNewSet(int cardIDOfPropertyToPlay, bool isOrientedUp)
        {
            getServiceReady();
            try
            {
                if (monopolyDealService.playPropertyCardNewSetMD(cardIDOfPropertyToPlay, isOrientedUp, thisClientGuid.boxGuid(), gameOnServiceGuid.boxGuid(), CurrentPlayFieldModel.thisPlayFieldModelInstanceGuid.boxGuid()))
                {
                    addToLog("Property played to new set");
                }
                else
                {
                    addToLog("Error, card not played");
                }
            }
            catch (Exception ex)
            {
                addToLog(ex.ToString());
                monopolyDealService.Close();
            }
        }

        internal bool checkHasGameStarted()
        {
            getServiceReady();
            try
            {
                if (monopolyDealService.getGameLobbyStatus(gameLobbyGuid.boxGuid()).CompareTo(MonopolyDealServiceReference.GameLobbyStatus.Game_In_Progress) == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                addToLog(ex.ToString());
                monopolyDealService.Close();
                return false;
            }
        }

        internal bool passGo(int p)
        {
            getServiceReady();
            try
            {
                return monopolyDealService.playActionCardPassGoMD(p, thisClientGuid.boxGuid(), gameOnServiceGuid.boxGuid(), CurrentPlayFieldModel.thisPlayFieldModelInstanceGuid.boxGuid());
            }
            catch (Exception ex)
            {
                addToLog(ex.ToString());
                monopolyDealService.Close();
            }
            return false;
        }

        internal void endTurn()
        {
            getServiceReady();
            try
            {
                monopolyDealService.endTurnMD(thisClientGuid.boxGuid(), gameOnServiceGuid.boxGuid(), CurrentPlayFieldModel.thisPlayFieldModelInstanceGuid.boxGuid());
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
                monopolyDealService.draw5AtStartOfTurnMD(thisClientGuid.boxGuid(), gameOnServiceGuid.boxGuid(), CurrentPlayFieldModel.thisPlayFieldModelInstanceGuid.boxGuid());
            }
            catch (Exception ex)
            {
                addToLog(ex.ToString());
                monopolyDealService.Close();
            }
        }

        internal void drawTwoAtTurnStart()
        {
            getServiceReady();
            try
            {
                MonopolyDealServiceReference.TurnActionModel ta = CurrentPlayFieldModel.currentTurnActionModel;

                bool success = monopolyDealService.draw2AtStartOfTurnMD(thisClientGuid.boxGuid(), gameLobbyGuid.boxGuid(), ta.currentPlayFieldModelGuid.boxGuid(), ta.thisTurnactionGuid.boxGuid());
            }
            catch (Exception ex)
            {
                addToLog(ex.ToString());
                //monopolyDealService.Close();
            }
        }

        internal void bankCard(int cardIDOfCardToBank)
        {
            getServiceReady();
            try
            {
                bool success = monopolyDealService.playCardFromHandToBankMD(cardIDOfCardToBank, thisClientGuid.boxGuid(), gameOnServiceGuid.boxGuid(), CurrentPlayFieldModel.thisPlayFieldModelInstanceGuid.boxGuid(), CurrentPlayFieldModel.currentTurnActionModel.thisTurnactionGuid.boxGuid());
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

        internal void discard1Card(int p)
        {
            getServiceReady();
            try
            {
                monopolyDealService.discardMD(p, thisClientGuid.boxGuid(), gameOnServiceGuid.boxGuid(), CurrentPlayFieldModel.thisPlayFieldModelInstanceGuid.boxGuid());
            }
            catch (Exception ex)
            {
                addToLog(ex.ToString());
                monopolyDealService.Close();
            }
        }

        internal void playPropertyToSelectedSet(MonopolyDealServiceReference.PropertyCard pc, MonopolyDealServiceReference.PropertyCardSet pcs)
        {
            getServiceReady();
            try
            {
                if (monopolyDealService.playPropertyCardMD(null, pc, pcs, thisClientGuid.boxGuid(), gameLobbyGuid.boxGuid(), CurrentPlayFieldModel.thisPlayFieldModelInstanceGuid.boxGuid(), null))
                {
                    addToLog("Property played to new set");
                }
                else
                {
                    addToLog("Error, card not played");
                }
            }
            catch (Exception ex)
            {
                addToLog(ex.ToString());
                monopolyDealService.Close();
            }
        }

        internal void payDebt(List<MonopolyDealServiceReference.Card> list)
        {
            List<int> listCardsToPayWithIds = new List<int>();
            foreach (MonopolyDealServiceReference.Card c in list)
            {
                listCardsToPayWithIds.Add(c.cardID);
            }
            getServiceReady();
            try
            {
                if (monopolyDealService.payCardsMD(thisClientGuid.boxGuid(), listCardsToPayWithIds.ToArray(), gameLobbyGuid.boxGuid(), gameLobbyGuid.boxGuid(), CurrentPlayFieldModel.thisPlayFieldModelInstanceGuid.boxGuid(), null))
                {
                    addToLog("Player has paid debt");
                }
                else
                {
                    addToLog("Error, player has not paid debt");
                }
            }
            catch (Exception ex)
            {
                addToLog(ex.ToString());
                monopolyDealService.Close();
            }
        }

        internal bool debtCollector(int cardBeingUsedId, Guid targetPlayerGuid)
        {
            getServiceReady();
            try
            {
                return monopolyDealService.playActionCardDebtCollectorMD(cardBeingUsedId, targetPlayerGuid.boxGuid(), thisClientGuid.boxGuid(), gameOnServiceGuid.boxGuid(), CurrentPlayFieldModel.thisPlayFieldModelInstanceGuid.boxGuid());
            }
            catch (Exception ex)
            {
                addToLog(ex.ToString());
                monopolyDealService.Close();
            }
            return false;
        }

        internal bool itsMyBirthday(int cardBeingUsedId)
        {
            getServiceReady();
            try
            {
                return monopolyDealService.playActionCardItsMyBirthdayMD(cardBeingUsedId, thisClientGuid.boxGuid(), gameOnServiceGuid.boxGuid(), CurrentPlayFieldModel.thisPlayFieldModelInstanceGuid.boxGuid());
            }
            catch (Exception ex)
            {
                addToLog(ex.ToString());
                monopolyDealService.Close();
            }
            return false;
        }

        internal bool justSayNo(int cardIDOfCardToUse)
        {
            getServiceReady();
            try
            {
                return monopolyDealService.playJustSayNoMD(cardIDOfCardToUse, thisClientGuid.boxGuid(), gameOnServiceGuid.boxGuid(), CurrentPlayFieldModel.thisPlayFieldModelInstanceGuid.boxGuid());
            }
            catch (Exception ex)
            {
                addToLog(ex.ToString());
                monopolyDealService.Close();
            }
            return false;
        }

        internal bool moveProperty(int pickedPropertyToMove, Guid oldPropertySet, Guid newPropertySet, bool isCardUp, bool playToExistingSet)
        {
            getServiceReady();
            try
            {
                MonopolyDealServiceReference.BoolResponseBox response = monopolyDealService.movePropertyCardMD(
                    pickedPropertyToMove, isCardUp, playToExistingSet, oldPropertySet.boxGuid(), newPropertySet.boxGuid(),
                    thisClientGuid.boxGuid(), gameOnServiceGuid.boxGuid(), CurrentPlayFieldModel.thisPlayFieldModelInstanceGuid.boxGuid());

                addToLog(response.message);
                return response.success;
            }
            catch (Exception ex)
            {
                addToLog(ex.ToString());
                monopolyDealService.Close();
            }
            return false;
        }

        internal bool useRentCard(int RentCard, Guid selectedSet, bool usingDoubleTheRent, int doubleTheRentCard, bool isWildRentCard, Guid targetedPlayer)
        {
            getServiceReady();
            try
            {
                MonopolyDealServiceReference.BoolResponseBox response;
                if (isWildRentCard)
                {
                    response = monopolyDealService.playWildRentActionCardOnTurnMD(RentCard, targetedPlayer.boxGuid(), selectedSet.boxGuid(),
                        thisClientGuid.boxGuid(), gameOnServiceGuid.boxGuid(), CurrentPlayFieldModel.thisPlayFieldModelInstanceGuid.boxGuid());
                }
                else
                {
                    response = monopolyDealService.playStandardRentActionCardOnTurnMD(RentCard, selectedSet.boxGuid(),
                        thisClientGuid.boxGuid(), gameOnServiceGuid.boxGuid(), CurrentPlayFieldModel.thisPlayFieldModelInstanceGuid.boxGuid());
                }
                addToLog(response.message);
                return response.success;
            }
            catch (Exception ex)
            {
                addToLog(ex.ToString());
                monopolyDealService.Close();
            }
            return false;
        }

        internal bool dealBreaker(MonopolyDealServiceReference.Card DealBreakerCard, Guid targetedPlayer, MonopolyDealServiceReference.PropertyCardSet targetedSet)
        {
            getServiceReady();
            try
            {
                MonopolyDealServiceReference.BoolResponseBox response;
                response = monopolyDealService.playActionCardDealBreakerMD(DealBreakerCard.cardID, targetedPlayer.boxGuid(), targetedSet.guid.boxGuid(),
                       thisClientGuid.boxGuid(), gameOnServiceGuid.boxGuid(), CurrentPlayFieldModel.thisPlayFieldModelInstanceGuid.boxGuid());
                addToLog(response.message);
                return response.success;
            }
            catch (Exception ex)
            {
                addToLog(ex.ToString());
                monopolyDealService.Close();
            }
            return false;
        }

        internal bool slyDeal(MonopolyDealServiceReference.ActionCard Card, Guid targetedPlayer, MonopolyDealServiceReference.PropertyCardSet targetedSet, MonopolyDealServiceReference.Card targetedCard)
        {
            getServiceReady();
            try
            {
                MonopolyDealServiceReference.BoolResponseBox response;
                response = monopolyDealService.playActionCardSlyDealMD(Card.cardID, targetedPlayer.boxGuid(), targetedCard.cardID, targetedSet.guid.boxGuid(),
                       thisClientGuid.boxGuid(), gameOnServiceGuid.boxGuid(), CurrentPlayFieldModel.thisPlayFieldModelInstanceGuid.boxGuid());
                addToLog(response.message);
                return response.success;
            }
            catch (Exception ex)
            {
                addToLog(ex.ToString());
                monopolyDealService.Close();
            }
            return false;
        }

        internal bool house(MonopolyDealServiceReference.ActionCard Card, MonopolyDealServiceReference.PropertyCardSet targetedSet)
        {
            getServiceReady();
            try
            {
                MonopolyDealServiceReference.BoolResponseBox response;
                response = monopolyDealService.playHouseMD(Card.cardID, targetedSet.guid.boxGuid(),
                       thisClientGuid.boxGuid(), gameOnServiceGuid.boxGuid(), CurrentPlayFieldModel.thisPlayFieldModelInstanceGuid.boxGuid());
                addToLog(response.message);
                return response.success;
            }
            catch (Exception ex)
            {
                addToLog(ex.ToString());
                monopolyDealService.Close();
            }
            return false;
        }

        internal bool hotel(MonopolyDealServiceReference.ActionCard Card, MonopolyDealServiceReference.PropertyCardSet targetedSet)
        {
            getServiceReady();
            try
            {
                MonopolyDealServiceReference.BoolResponseBox response;
                response = monopolyDealService.playHotelMD(Card.cardID, targetedSet.guid.boxGuid(),
                       thisClientGuid.boxGuid(), gameOnServiceGuid.boxGuid(), CurrentPlayFieldModel.thisPlayFieldModelInstanceGuid.boxGuid());
                addToLog(response.message);
                return response.success;
            }
            catch (Exception ex)
            {
                addToLog(ex.ToString());
                monopolyDealService.Close();
            }
            return false;
        }

        internal bool doNotJustSayNo()
        {
            getServiceReady();
            try
            {
                MonopolyDealServiceReference.BoolResponseBox response;
                response = monopolyDealService.doNotPlayJustSayNoMD(
                       thisClientGuid.boxGuid(), gameOnServiceGuid.boxGuid(), CurrentPlayFieldModel.thisPlayFieldModelInstanceGuid.boxGuid());
                addToLog(response.message);
                return response.success;
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