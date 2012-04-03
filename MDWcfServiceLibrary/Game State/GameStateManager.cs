using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace MDWcfServiceLibrary
{
    /// <summary>
    /// GameStateManager modifies the state of the game
    /// </summary>
    ///
    internal class GameStateManager
    {
        GameModel gameModel;

        PlayFieldModel currentPlayFieldModel;
        PlayFieldModel nextPlayFieldModel; //Careful of justSayNo chains
        List<Acknowledgement> acknowledgementsRecieved = new List<Acknowledgement>();

        public GameStateManager(GameModel gm)
        {
            gameModel = gm;
        }

        public PlayFieldModel getPlayFieldModelByGuid(Guid playFieldModelGuid)
        {
            return currentPlayFieldModel;
            throw new NotImplementedException();
        }

        public PlayFieldModel getCurrentPlayFieldModel()
        {
            //Get CurrentPlayFieldModelState
            return gameModel.gameStates[(gameModel.gameStates.Count - 1)];
        }

        public void endTurn(PlayerModel player)
        {
            updateState(TurnActionTypes.EndTurn, getCurrentPlayFieldModel(), player.guid);
        }

        public bool doAction(Guid gameGuid, Guid playerGuid, Guid gameStateActionShouldBeAppliedOnGuid, TurnActionTypes actionType)
        {
            ///Returns false if action not carried out
            ///
            //Get CurrentPlayFieldModelState
            currentPlayFieldModel = getCurrentPlayFieldModel();
            if (checkIfActionIsForThisState(actionType, gameStateActionShouldBeAppliedOnGuid, playerGuid, gameGuid))
            {
                if (actionType.CompareTo(TurnActionTypes.drawTwoCardsAtStartOfTurn) == 0)
                {
                    drawTwoCardsAtTurnStart(getPlayerByGuid(playerGuid, currentPlayFieldModel));
                }
                if (actionType.CompareTo(TurnActionTypes.drawFiveCardsAtStartOfTurn) == 0)
                {
                    drawFiveCards(getPlayerByGuid(playerGuid, currentPlayFieldModel));
                }
                if (actionType.CompareTo(TurnActionTypes.EndTurn) == 0)
                {
                    endTurn(getPlayerByGuid(playerGuid, currentPlayFieldModel));
                }
                //turn action is for this playfieldmodel
            }
            return true;
            throw new NotImplementedException();
        }

        /*
        public bool doActionOld(TurnActionModel turnActionToDo)
        {
            ///Returns false if action not carried out
            ///
            //Get CurrentPlayFieldModelState
            currentPlayFieldModel = gameModel.gameStates[(gameModel.gameStates.Count - 1)];
            if (checkIfActionIsForThisState(turnActionToDo, currentPlayFieldModel))
            {
                if (turnActionToDo.actionTaken && turnActionToDo.typeOfActionToTake.CompareTo(TurnActionTypes.drawTwoCardsAtStartOfTurn) == 0)
                {
                    drawTwoCardsAtTurnStart(gameModel.players[gameModel.playerIdLookup.IndexOf(turnActionToDo.playerGuids[0])]);
                }
                //turn action is for this playfieldmodel
            }
            return true;
            throw new NotImplementedException();
        }
         * */

        public bool checkIfMoveLegal(Guid guidOfPlayerMakingMove, TurnActionTypes typeOfActionAttempted)
        {
            throw new NotImplementedException();
        }

        public void drawTwoCardsAtTurnStart(PlayerModel player)
        {
            //draws two cards to players hand Unsafe
            player.hand.addCardToHand(currentPlayFieldModel.drawPile.drawcard());
            player.hand.addCardToHand(currentPlayFieldModel.drawPile.drawcard());
            //actionPerformed();
            updateState(TurnActionTypes.drawTwoCardsAtStartOfTurn, currentPlayFieldModel, player.guid);
        }

        public void drawFiveCards(PlayerModel player)
        {
            //draws five cards to players hand Unsafe
            player.hand.addCardToHand(currentPlayFieldModel.drawPile.drawcard());
            player.hand.addCardToHand(currentPlayFieldModel.drawPile.drawcard());
            player.hand.addCardToHand(currentPlayFieldModel.drawPile.drawcard());
            player.hand.addCardToHand(currentPlayFieldModel.drawPile.drawcard());
            player.hand.addCardToHand(currentPlayFieldModel.drawPile.drawcard());
            //actionPerformed();
            updateState(TurnActionTypes.drawFiveCardsAtStartOfTurn, currentPlayFieldModel, player.guid);
        }

        public Card checkIfCardInHand(Card card, PlayerModel pm)
        {
            foreach (Card c in pm.hand.cardsInHand)
            {
                if (c.cardID == card.cardID)
                {
                    return c;
                }
            }
            return null;
        }

        private Card removeCardFromHand(Card card, PlayerModel pm)
        {
            Card cardInHand = checkIfCardInHand(card, pm);
            if (cardInHand != null && pm.hand.cardsInHand.Remove(cardInHand))
            {
                return cardInHand;
            }
            return null;
        }

        public PlayerModel getPlayerFromGuid(PlayerModel pmP)
        {
            foreach (PlayerModel p in gameModel.players)
            {
                if (p.guid.CompareTo(pmP.guid) == 0)
                {
                    return p;
                }
            }
            return null;
        }

        public void bankCard(Card cardInHandToBeBanked, PlayerModel playerWhoIsBankingCard)
        {
            //Get the reference to the players playerModel in the current PlayFieldModel
            PlayerModel pm = getPlayerFromGuid(playerWhoIsBankingCard);
            //Get the reference to the Card in the current PlayFieldModel

            Card card = removeCardFromHand(cardInHandToBeBanked, pm);
            if (card != null)
            {
                playerWhoIsBankingCard.bank.addCardToBank(card);
                //Change state on success
                updateState(TurnActionTypes.BankActionCard, currentPlayFieldModel, pm.guid);
            }
            else
            {
                //Card not in players hand, can't be banked
            }
        }

        public PlayerModel getPlayerModel(Guid playerGuid, Guid gameGuid, Guid currentPlayFieldModelGuid)
        {
            //Modify for multiple games on service
            foreach (PlayerModel p in currentPlayFieldModel.playerModels)
            {
                if (p.guid.CompareTo(playerGuid) == 0)
                {
                    return p;
                }
            }
            return null;
        }

        public bool isActionAllowedForPlayer(TurnActionModel turnActionToDo, Guid playerGuid, PlayFieldModel currentState)
        {
            TurnActionTypes tAT = turnActionToDo.typeOfActionToTake;
            PlayerModel playerAttemptingAction = getPlayerModel(playerGuid, gameModel.gameModelGuid, currentState.thisPlayFieldModelInstanceGuid);
            if (playerAttemptingAction != null)
            {
                foreach (TurnActionTypes t in playerAttemptingAction.actionsCurrentlyAllowed)
                {
                    if (t.CompareTo(tAT) == 0)
                    {
                        //Action is in allowable list for player
                        return true;
                    }
                }
            }
            return false; //Action not allowable
        }

        public bool isActionAllowedForPlayer(TurnActionTypes turnActionToDo, Guid playerGuid, PlayFieldModel currentState)
        {
            TurnActionTypes tAT = turnActionToDo;
            PlayerModel playerAttemptingAction = getPlayerModel(playerGuid, gameModel.gameModelGuid, currentState.thisPlayFieldModelInstanceGuid);
            if (playerAttemptingAction != null)
            {
                foreach (TurnActionTypes t in playerAttemptingAction.actionsCurrentlyAllowed)
                {
                    if (t.CompareTo(tAT) == 0)
                    {
                        //Action is in allowable list for player
                        return true;
                    }
                }
            }
            return false; //Action not allowable
        }

        public bool haveAllPlayersAcknowledgedCurrentState(Guid gameServiceInstanceGuid, Guid currentStateGuidP, List<PlayerModel> playersInGame)
        {
            //Current State Guid
            foreach (PlayerModel pm in playersInGame)
            {
                bool recievedFromThisPlayer = false;
                foreach (Acknowledgement a in acknowledgementsRecieved)
                {
                    if (a.equal(pm.guid, gameServiceInstanceGuid, currentStateGuidP))
                    {
                        recievedFromThisPlayer = true;
                        break;
                    }
                }
                if (!recievedFromThisPlayer)
                {
                    return false;
                }
            }
            return true;
        }

        private bool checkIfActionIsForThisState(TurnActionTypes ta, Guid stateGuid, Guid playerGuid, Guid gameGuid)
        {
            return true;
            throw new NotImplementedException();
        }

        private void actionPerformed()
        {
            //update game state model and turn action model
        }

        public PlayerModel getPlayerByGuid(Guid player, PlayFieldModel pfm)
        {
            foreach (PlayerModel p in pfm.playerModels)
            {
                if (p.guid.CompareTo(player) == 0)
                {
                    return p;
                }
            }
            return null;
        }

        public PlayFieldModel copyPlayFieldModel(PlayFieldModel currentPlayFieldModel)
        {
            //not implemented
            return currentPlayFieldModel;
        }

        private void updateAllowableStates(PlayFieldModel state, List<TurnActionTypes> allowedForPlayersNotOnTurn, List<TurnActionTypes> allowedForPlayerOnTurn, Guid playerOnTurnGuid)
        {
            foreach (PlayerModel p in state.playerModels)
            {
                if (p.guid.CompareTo(playerOnTurnGuid) == 0)
                //if (p.isThisPlayersTurn)
                {
                    //its p's turn
                    p.actionsCurrentlyAllowed = allowedForPlayerOnTurn;
                }
                else
                {
                    p.actionsCurrentlyAllowed = allowedForPlayersNotOnTurn;
                }
            }
        }

        private int playerTurnCounter = 0;

        private void setNextPlayerOnTurn(PlayFieldModel pfm)
        {
            playerTurnCounter++;
            if (playerTurnCounter >= pfm.playerModels.Count)
            {
                playerTurnCounter = 0;
            }

            int nextPlayerID = playerTurnCounter;
            foreach (PlayerModel p in pfm.playerModels)
            {
                if (p.guid.CompareTo(pfm.guidOfPlayerWhosTurnItIs) == 0)
                {
                    p.isThisPlayersTurn = true;
                }
                else
                {
                    p.isThisPlayersTurn = false;
                }
            }
            pfm.guidOfPlayerWhosTurnItIs = pfm.playerModels.ElementAt(nextPlayerID).guid;
            pfm.playerModels.ElementAt(nextPlayerID).isThisPlayersTurn = true;
        }

        private void updateState(TurnActionTypes actionToAttemptToPerform, PlayFieldModel currentState, Guid playerWhoPerformedAction)
        {
            PlayerModel player = getPlayerByGuid(playerWhoPerformedAction, currentState);

            PlayFieldModel newState = copyPlayFieldModel(currentState);

            //List<TurnActionTypes> notOnTurn = new List<TurnActionTypes>();
            //List<TurnActionTypes> onTurn = new List<TurnActionTypes>();

            #region draw2state

            //draw 2 on turn start state
            if (currentState.currentPhase.CompareTo(Statephase.Turn_Started_Draw_2_Cards) == 0)
            {
                if (actionToAttemptToPerform.CompareTo(TurnActionTypes.drawTwoCardsAtStartOfTurn) == 0)
                {
                    //Move was a valid move at current state
                    //Check if move is valid for player
                    if (isActionAllowedForPlayer(actionToAttemptToPerform, playerWhoPerformedAction, currentState))
                    {
                        //action is valid for player at this time

                        //Could perform the action here instead, for now just change the phase of the state
                        newState.currentPhase = Statephase.Turn_Started_Cards_Drawn_0_Cards_Played;

                        //player has drawn their two cards, Now can play up to three cards on their turn
                        List<TurnActionTypes> notOnTurn = new List<TurnActionTypes>();
                        List<TurnActionTypes> onTurn = new List<TurnActionTypes>();
                        onTurn.Add(TurnActionTypes.BankActionCard);
                        onTurn.Add(TurnActionTypes.BankMoneyCard);
                        onTurn.Add(TurnActionTypes.PlayCard);
                        onTurn.Add(TurnActionTypes.PlayPropertyCard);
                        onTurn.Add(TurnActionTypes.SwitchAroundPlayedProperties);
                        onTurn.Add(TurnActionTypes.EndTurn);
                        updateAllowableStates(newState, notOnTurn, onTurn, newState.guidOfPlayerWhosTurnItIs);
                    }
                }
            }

            #endregion draw2state

            #region draw5state

            //draw 5 on turn start state
            else if (currentState.currentPhase.CompareTo(Statephase.Turn_Started_Draw_5_Cards) == 0)
            {
                if (actionToAttemptToPerform.CompareTo(TurnActionTypes.drawFiveCardsAtStartOfTurn) == 0)
                {
                    //Move was a valid move at current state
                    //Check if move is valid for player
                    if (isActionAllowedForPlayer(actionToAttemptToPerform, playerWhoPerformedAction, currentState))
                    {
                        //action is valid for player at this time

                        //Could perform the action here instead, for now just change the phase of the state
                        newState.currentPhase = Statephase.Turn_Started_Cards_Drawn_0_Cards_Played;
                        //player has drawn their five cards as they started the turn with zero cards, Now can play up to three cards on their turn
                        List<TurnActionTypes> notOnTurn = new List<TurnActionTypes>();
                        List<TurnActionTypes> onTurn = new List<TurnActionTypes>();
                        onTurn.Add(TurnActionTypes.BankActionCard);
                        onTurn.Add(TurnActionTypes.BankMoneyCard);
                        onTurn.Add(TurnActionTypes.PlayCard);
                        onTurn.Add(TurnActionTypes.PlayPropertyCard);
                        onTurn.Add(TurnActionTypes.SwitchAroundPlayedProperties);
                        onTurn.Add(TurnActionTypes.EndTurn);
                        updateAllowableStates(newState, notOnTurn, onTurn, newState.guidOfPlayerWhosTurnItIs);
                    }
                }
            }

            #endregion draw5state

            #region Turn_Started_Cards_Drawn_0_Cards_Played

            //draw 2 on turn start state
            else if (currentState.currentPhase.CompareTo(Statephase.Turn_Started_Cards_Drawn_0_Cards_Played) == 0)
            {
                #region bankActionCard

                if (actionToAttemptToPerform.CompareTo(TurnActionTypes.BankActionCard) == 0)
                {
                    //Move was a valid move at current state
                    //Check if move is valid for player
                    if (isActionAllowedForPlayer(actionToAttemptToPerform, playerWhoPerformedAction, currentState))
                    {
                        //action is valid for player at this time

                        //Could perform the action here instead, for now just change the phase of the state
                        //Not an action so cant be just say no'd
                        //Change phase
                        newState.currentPhase = Statephase.Turn_Started_Cards_Drawn_1_Cards_Played;

                        //player has drawn their two cards, Now can play up to three cards on their turn
                        List<TurnActionTypes> notOnTurn = new List<TurnActionTypes>();
                        List<TurnActionTypes> onTurn = new List<TurnActionTypes>();
                        onTurn.Add(TurnActionTypes.BankActionCard);
                        onTurn.Add(TurnActionTypes.BankMoneyCard);
                        onTurn.Add(TurnActionTypes.PlayCard);
                        onTurn.Add(TurnActionTypes.PlayPropertyCard);
                        onTurn.Add(TurnActionTypes.SwitchAroundPlayedProperties);
                        onTurn.Add(TurnActionTypes.EndTurn);
                        updateAllowableStates(newState, notOnTurn, onTurn, newState.guidOfPlayerWhosTurnItIs);
                    }
                }

                #endregion bankActionCard

                #region endTurn

                else if (actionToAttemptToPerform.CompareTo(TurnActionTypes.EndTurn) == 0)
                {
                    //Move was a valid move at current state
                    //Check if move is valid for player
                    if (isActionAllowedForPlayer(actionToAttemptToPerform, playerWhoPerformedAction, currentState))
                    {
                        //action is valid for player at this time
                        List<TurnActionTypes> notOnTurn = new List<TurnActionTypes>();
                        List<TurnActionTypes> onTurn = new List<TurnActionTypes>();
                        //Could perform the action here instead, for now just change the phase of the state
                        //Not an action so cant be just say no'd
                        //Change phase
                        switch (player.hand.cardsInHand.Count)
                        {
                            case 8:
                                {
                                    newState.currentPhase = Statephase.Turn_Ended_8_Cards_In_Hand_Discard_1_Card;
                                    onTurn.Add(TurnActionTypes.Discard_1_Card);
                                    break;
                                }
                            case 9:
                                {
                                    newState.currentPhase = Statephase.Turn_Ended_9_Cards_In_Hand_Discard_2_Cards;
                                    onTurn.Add(TurnActionTypes.Discard_2_Cards);
                                    break;
                                }
                            case 10:
                                {
                                    newState.currentPhase = Statephase.Turn_Ended_10_Cards_In_Hand_Discard_3_Cards;
                                    onTurn.Add(TurnActionTypes.Discard_3_Cards);
                                    break;
                                }
                            case 11:
                                {
                                    newState.currentPhase = Statephase.Turn_Ended_11_Cards_In_Hand_Discard_4_Cards;
                                    onTurn.Add(TurnActionTypes.Discard_4_Cards);
                                    break;
                                }
                            case 12:
                                {
                                    newState.currentPhase = Statephase.Turn_Ended_12_Cards_In_Hand_Discard_5_Cards;
                                    onTurn.Add(TurnActionTypes.Discard_5_Cards);
                                    break;
                                }
                            default:
                                {
                                    newState.currentPhase = Statephase.Turn_Ended_7_Or_Less_Cards_In_Hand_Setup_NextPlayer;
                                    setNextPlayerOnTurn(newState);
                                    if (getPlayerByGuid(newState.guidOfPlayerWhosTurnItIs, newState).hand.cardsInHand.Count == 0)
                                    {
                                        //Player has 0 cards draws 5 on turn start instead of 2
                                        newState.currentPhase = Statephase.Turn_Started_Draw_5_Cards;
                                        onTurn.Add(TurnActionTypes.drawFiveCardsAtStartOfTurn);
                                        updateAllowableStates(newState, notOnTurn, onTurn, newState.guidOfPlayerWhosTurnItIs);
                                    }
                                    else
                                    {
                                        newState.currentPhase = Statephase.Turn_Started_Draw_2_Cards;
                                        onTurn.Add(TurnActionTypes.drawTwoCardsAtStartOfTurn);
                                        updateAllowableStates(newState, notOnTurn, onTurn, newState.guidOfPlayerWhosTurnItIs);
                                    }

                                    break;
                                }
                        }

                        updateAllowableStates(newState, notOnTurn, onTurn, newState.guidOfPlayerWhosTurnItIs);
                    }
                }

                #endregion endTurn

                //Play Action
            }

            #endregion Turn_Started_Cards_Drawn_0_Cards_Played

            #region Turn_Started_Cards_Drawn_1_Cards_Played

            else if (currentState.currentPhase.CompareTo(Statephase.Turn_Started_Cards_Drawn_1_Cards_Played) == 0)
            {
                #region bankActionCard

                if (actionToAttemptToPerform.CompareTo(TurnActionTypes.BankActionCard) == 0)
                {
                    //Move was a valid move at current state
                    //Check if move is valid for player
                    if (isActionAllowedForPlayer(actionToAttemptToPerform, playerWhoPerformedAction, currentState))
                    {
                        //action is valid for player at this time

                        //Could perform the action here instead, for now just change the phase of the state
                        //Not an action so cant be just say no'd
                        //Change phase
                        newState.currentPhase = Statephase.Turn_Started_Cards_Drawn_2_Cards_Played;

                        //player has drawn their two cards, Now can play up to three cards on their turn
                        List<TurnActionTypes> notOnTurn = new List<TurnActionTypes>();
                        List<TurnActionTypes> onTurn = new List<TurnActionTypes>();
                        onTurn.Add(TurnActionTypes.BankActionCard);
                        onTurn.Add(TurnActionTypes.BankMoneyCard);
                        onTurn.Add(TurnActionTypes.PlayCard);
                        onTurn.Add(TurnActionTypes.PlayPropertyCard);
                        onTurn.Add(TurnActionTypes.SwitchAroundPlayedProperties);
                        onTurn.Add(TurnActionTypes.EndTurn);
                        updateAllowableStates(newState, notOnTurn, onTurn, newState.guidOfPlayerWhosTurnItIs);
                    }
                }

                #endregion bankActionCard

                #region endTurn

                else if (actionToAttemptToPerform.CompareTo(TurnActionTypes.EndTurn) == 0)
                {
                    //Move was a valid move at current state
                    //Check if move is valid for player
                    if (isActionAllowedForPlayer(actionToAttemptToPerform, playerWhoPerformedAction, currentState))
                    {
                        //action is valid for player at this time
                        List<TurnActionTypes> notOnTurn = new List<TurnActionTypes>();
                        List<TurnActionTypes> onTurn = new List<TurnActionTypes>();
                        //Could perform the action here instead, for now just change the phase of the state
                        //Not an action so cant be just say no'd
                        //Change phase
                        switch (player.hand.cardsInHand.Count)
                        {
                            case 8:
                                {
                                    newState.currentPhase = Statephase.Turn_Ended_8_Cards_In_Hand_Discard_1_Card;
                                    onTurn.Add(TurnActionTypes.Discard_1_Card);
                                    break;
                                }
                            case 9:
                                {
                                    newState.currentPhase = Statephase.Turn_Ended_9_Cards_In_Hand_Discard_2_Cards;
                                    onTurn.Add(TurnActionTypes.Discard_2_Cards);
                                    break;
                                }
                            case 10:
                                {
                                    newState.currentPhase = Statephase.Turn_Ended_10_Cards_In_Hand_Discard_3_Cards;
                                    onTurn.Add(TurnActionTypes.Discard_3_Cards);
                                    break;
                                }
                            case 11:
                                {
                                    newState.currentPhase = Statephase.Turn_Ended_11_Cards_In_Hand_Discard_4_Cards;
                                    onTurn.Add(TurnActionTypes.Discard_4_Cards);
                                    break;
                                }
                            case 12:
                                {
                                    newState.currentPhase = Statephase.Turn_Ended_12_Cards_In_Hand_Discard_5_Cards;
                                    onTurn.Add(TurnActionTypes.Discard_5_Cards);
                                    break;
                                }
                            default:
                                {
                                    newState.currentPhase = Statephase.Turn_Ended_7_Or_Less_Cards_In_Hand_Setup_NextPlayer;
                                    setNextPlayerOnTurn(newState);
                                    if (getPlayerByGuid(newState.guidOfPlayerWhosTurnItIs, newState).hand.cardsInHand.Count == 0)
                                    {
                                        //Player has 0 cards draws 5 on turn start instead of 2
                                        newState.currentPhase = Statephase.Turn_Started_Draw_5_Cards;
                                        onTurn.Add(TurnActionTypes.drawFiveCardsAtStartOfTurn);
                                    }
                                    else
                                    {
                                        newState.currentPhase = Statephase.Turn_Started_Draw_2_Cards;
                                        onTurn.Add(TurnActionTypes.drawTwoCardsAtStartOfTurn);
                                    }

                                    break;
                                }
                        }

                        updateAllowableStates(newState, notOnTurn, onTurn, newState.guidOfPlayerWhosTurnItIs);
                    }
                }

                #endregion endTurn

                //Play Action
            }

            #endregion Turn_Started_Cards_Drawn_1_Cards_Played

            #region Turn_Started_Cards_Drawn_2_Cards_Played

            else if (currentState.currentPhase.CompareTo(Statephase.Turn_Started_Cards_Drawn_2_Cards_Played) == 0)
            {
                //Actions that can be taken on this phase

                #region bankActionCard

                if (actionToAttemptToPerform.CompareTo(TurnActionTypes.BankActionCard) == 0)
                {
                    //Move was a valid move at current state
                    //Check if move is valid for player
                    if (isActionAllowedForPlayer(actionToAttemptToPerform, playerWhoPerformedAction, currentState))
                    {
                        //action is valid for player at this time

                        //Could perform the action here instead, for now just change the phase of the state
                        //Not an action so cant be just say no'd
                        //Change phase
                        newState.currentPhase = Statephase.Turn_Started_Cards_Drawn_3_Cards_Played_Swap_Properties_Or_End_Turn_Only;

                        //player has drawn their two cards, Now can play up to three cards on their turn
                        List<TurnActionTypes> notOnTurn = new List<TurnActionTypes>();
                        List<TurnActionTypes> onTurn = new List<TurnActionTypes>();
                        onTurn.Add(TurnActionTypes.SwitchAroundPlayedProperties);
                        onTurn.Add(TurnActionTypes.EndTurn);
                        updateAllowableStates(newState, notOnTurn, onTurn, newState.guidOfPlayerWhosTurnItIs);
                    }
                }

                #endregion bankActionCard

                #region endTurn

                else if (actionToAttemptToPerform.CompareTo(TurnActionTypes.EndTurn) == 0)
                {
                    //Move was a valid move at current state
                    //Check if move is valid for player
                    if (isActionAllowedForPlayer(actionToAttemptToPerform, playerWhoPerformedAction, currentState))
                    {
                        //action is valid for player at this time
                        List<TurnActionTypes> notOnTurn = new List<TurnActionTypes>();
                        List<TurnActionTypes> onTurn = new List<TurnActionTypes>();
                        //Could perform the action here instead, for now just change the phase of the state
                        //Not an action so cant be just say no'd
                        //Change phase
                        switch (player.hand.cardsInHand.Count)
                        {
                            case 8:
                                {
                                    newState.currentPhase = Statephase.Turn_Ended_8_Cards_In_Hand_Discard_1_Card;
                                    onTurn.Add(TurnActionTypes.Discard_1_Card);
                                    break;
                                }
                            case 9:
                                {
                                    newState.currentPhase = Statephase.Turn_Ended_9_Cards_In_Hand_Discard_2_Cards;
                                    onTurn.Add(TurnActionTypes.Discard_2_Cards);
                                    break;
                                }
                            case 10:
                                {
                                    newState.currentPhase = Statephase.Turn_Ended_10_Cards_In_Hand_Discard_3_Cards;
                                    onTurn.Add(TurnActionTypes.Discard_3_Cards);
                                    break;
                                }
                            case 11:
                                {
                                    newState.currentPhase = Statephase.Turn_Ended_11_Cards_In_Hand_Discard_4_Cards;
                                    onTurn.Add(TurnActionTypes.Discard_4_Cards);
                                    break;
                                }
                            case 12:
                                {
                                    newState.currentPhase = Statephase.Turn_Ended_12_Cards_In_Hand_Discard_5_Cards;
                                    onTurn.Add(TurnActionTypes.Discard_5_Cards);
                                    break;
                                }
                            default:
                                {
                                    newState.currentPhase = Statephase.Turn_Ended_7_Or_Less_Cards_In_Hand_Setup_NextPlayer;
                                    setNextPlayerOnTurn(newState);
                                    if (getPlayerByGuid(newState.guidOfPlayerWhosTurnItIs, newState).hand.cardsInHand.Count == 0)
                                    {
                                        //Player has 0 cards draws 5 on turn start instead of 2
                                        newState.currentPhase = Statephase.Turn_Started_Draw_5_Cards;
                                        onTurn.Add(TurnActionTypes.drawFiveCardsAtStartOfTurn);
                                    }
                                    else
                                    {
                                        newState.currentPhase = Statephase.Turn_Started_Draw_2_Cards;
                                        onTurn.Add(TurnActionTypes.drawTwoCardsAtStartOfTurn);
                                    }

                                    break;
                                }
                        }

                        updateAllowableStates(newState, notOnTurn, onTurn, newState.guidOfPlayerWhosTurnItIs);
                    }
                }

                #endregion endTurn

                //Play Action
            }

            #endregion Turn_Started_Cards_Drawn_2_Cards_Played

            #region Turn_Started_Cards_Drawn_3_Cards_Played_Swap_Properties_Or_End_Turn_Only

            //draw 2 on turn start state
            else if (currentState.currentPhase.CompareTo(Statephase.Turn_Started_Cards_Drawn_3_Cards_Played_Swap_Properties_Or_End_Turn_Only) == 0)
            {
                #region endTurn

                if (actionToAttemptToPerform.CompareTo(TurnActionTypes.EndTurn) == 0)
                {
                    //Move was a valid move at current state
                    //Check if move is valid for player
                    if (isActionAllowedForPlayer(actionToAttemptToPerform, playerWhoPerformedAction, currentState))
                    {
                        //action is valid for player at this time
                        List<TurnActionTypes> notOnTurn = new List<TurnActionTypes>();
                        List<TurnActionTypes> onTurn = new List<TurnActionTypes>();
                        //Could perform the action here instead, for now just change the phase of the state
                        //Not an action so cant be just say no'd
                        //Change phase
                        switch (player.hand.cardsInHand.Count)
                        {
                            case 8:
                                {
                                    newState.currentPhase = Statephase.Turn_Ended_8_Cards_In_Hand_Discard_1_Card;
                                    onTurn.Add(TurnActionTypes.Discard_1_Card);
                                    break;
                                }
                            case 9:
                                {
                                    newState.currentPhase = Statephase.Turn_Ended_9_Cards_In_Hand_Discard_2_Cards;
                                    onTurn.Add(TurnActionTypes.Discard_2_Cards);
                                    break;
                                }
                            case 10:
                                {
                                    newState.currentPhase = Statephase.Turn_Ended_10_Cards_In_Hand_Discard_3_Cards;
                                    onTurn.Add(TurnActionTypes.Discard_3_Cards);
                                    break;
                                }
                            case 11:
                                {
                                    newState.currentPhase = Statephase.Turn_Ended_11_Cards_In_Hand_Discard_4_Cards;
                                    onTurn.Add(TurnActionTypes.Discard_4_Cards);
                                    break;
                                }
                            case 12:
                                {
                                    newState.currentPhase = Statephase.Turn_Ended_12_Cards_In_Hand_Discard_5_Cards;
                                    onTurn.Add(TurnActionTypes.Discard_5_Cards);
                                    break;
                                }
                            default:
                                {
                                    newState.currentPhase = Statephase.Turn_Ended_7_Or_Less_Cards_In_Hand_Setup_NextPlayer;
                                    setNextPlayerOnTurn(newState);
                                    if (getPlayerByGuid(newState.guidOfPlayerWhosTurnItIs, newState).hand.cardsInHand.Count == 0)
                                    {
                                        //Player has 0 cards draws 5 on turn start instead of 2
                                        newState.currentPhase = Statephase.Turn_Started_Draw_5_Cards;
                                        onTurn.Add(TurnActionTypes.drawFiveCardsAtStartOfTurn);
                                    }
                                    else
                                    {
                                        newState.currentPhase = Statephase.Turn_Started_Draw_2_Cards;
                                        onTurn.Add(TurnActionTypes.drawTwoCardsAtStartOfTurn);
                                    }

                                    break;
                                }
                        }

                        updateAllowableStates(newState, notOnTurn, onTurn, newState.guidOfPlayerWhosTurnItIs);
                    }
                }

                #endregion endTurn

                //Play Action
            }

            #endregion Turn_Started_Cards_Drawn_3_Cards_Played_Swap_Properties_Or_End_Turn_Only

            //BAD REMOVE THIS
            foreach (PlayerModel p in newState.playerModels)
            {
                p.actionsCurrentlyAllowed = p.actionsCurrentlyAllowed;
                foreach (PlayerModel pp in newState.playerModels)
                {
                    if (p != pp && p.actionsCurrentlyAllowed == pp.actionsCurrentlyAllowed)
                    {
                        throw new Exception();
                    }
                }
            }
            //
        }
    }
}