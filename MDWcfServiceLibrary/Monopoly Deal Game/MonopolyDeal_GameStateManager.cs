using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MDWcfServiceLibrary
{
    /// <summary>
    /// Replaces old GameStateManager
    /// </summary>
    internal class MonopolyDeal_GameStateManager : IMonopolyDeal_GameStateManager
    {
        private MonopolyDeal monopolyDeal;

        public MonopolyDeal_GameStateManager(MonopolyDeal monopolyDeal)
        {
            // TODO: Complete member initialization
            this.monopolyDeal = monopolyDeal;
        }

        public PlayFieldModel getCurrentState()
        {
            return monopolyDeal.currentState;
        }

        #region From GameStateManager

        PlayFieldModel currentPlayFieldModel;
        PlayFieldModel nextPlayFieldModel; //Careful of justSayNo chains
        List<Acknowledgement> acknowledgementsRecieved = new List<Acknowledgement>();

        public PlayFieldModel getPlayFieldModelByGuid(Guid playFieldModelGuid)
        {
            return currentPlayFieldModel;
            throw new NotImplementedException();
        }

        public PlayFieldModel getCurrentPlayFieldModel()
        {
            //Get CurrentPlayFieldModelState
            return monopolyDeal.gameStates[(monopolyDeal.gameStates.Count - 1)];
        }

        public void endTurn(PlayerModel player)
        {
            updateState(TurnActionTypes.EndTurn, ActionCardAction.NotAnActionCard, getCurrentPlayFieldModel(), player.guid);
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
                if (actionType.CompareTo(TurnActionTypes.PlayPropertyCard_New_Set) == 0)
                {
                    //playPropertyCardToNewSet(getPlayerByGuid(playerGuid, currentPlayFieldModel));
                }
                //turn action is for this playfieldmodel
            }
            return true;
            throw new NotImplementedException();
        }

        public bool playPropertyCardToNewSet(Guid gameGuid, bool isOrientedUp, Guid playerGuid, Guid gameStateActionShouldBeAppliedOnGuid, TurnActionTypes actionType, int propertyCardID)
        {
            //Get CurrentPlayFieldModelState
            currentPlayFieldModel = getCurrentPlayFieldModel();
            PlayerModel player = getPlayerByGuid(playerGuid, currentPlayFieldModel);
            if (checkIfActionIsForThisState(actionType, gameStateActionShouldBeAppliedOnGuid, playerGuid, gameGuid))
            {
                if (actionType.CompareTo(TurnActionTypes.PlayPropertyCard_New_Set) == 0)
                {
                    foreach (Card c in player.hand.cardsInHand)
                    {
                        if (c.cardID == propertyCardID && c is PropertyCard)
                        {
                            Card card = removeCardFromHand(c, player);
                            if (card != null)
                            {
                                PropertyCard cP = c as PropertyCard;
                                PropertyCardSet ps = new PropertyCardSet(cP);
                                cP.setPropertyColor(isOrientedUp);
                                player.propertySets.addSet(ps);
                                updateState(TurnActionTypes.PlayPropertyCard_New_Set, ActionCardAction.NotAnActionCard, currentPlayFieldModel, player.guid);
                                return true;
                            }
                        }
                    }
                }
            }
            return false;
        }

        public void drawTwoCardsAtTurnStart(PlayerModel player)
        {
            //draws two cards to players hand Unsafe
            player.hand.addCardToHand(currentPlayFieldModel.drawPile.drawcard());
            player.hand.addCardToHand(currentPlayFieldModel.drawPile.drawcard());
            //actionPerformed();
            updateState(TurnActionTypes.drawTwoCardsAtStartOfTurn, ActionCardAction.NotAnActionCard, currentPlayFieldModel, player.guid);
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
            updateState(TurnActionTypes.drawFiveCardsAtStartOfTurn, ActionCardAction.NotAnActionCard, currentPlayFieldModel, player.guid);
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

        public bool bankCard(int playedCardID, Guid playerGuid, Guid serverGuid, Guid playfieldModelInstanceGuid)
        {
            Card cardInHandToBeBanked = monopolyDeal.deck.getCardByID(playedCardID);
            //Get the reference to the players playerModel in the current PlayFieldModel
            PlayerModel playerWhoIsBankingCard = getPlayerModel(playerGuid, serverGuid, playfieldModelInstanceGuid);
            //Get the reference to the Card in the current PlayFieldModel

            Card card = removeCardFromHand(cardInHandToBeBanked, playerWhoIsBankingCard);
            if (card != null)
            {
                playerWhoIsBankingCard.bank.addCardToBank(card);
                //Change state on success
                updateState(TurnActionTypes.BankActionCard, ActionCardAction.NotAnActionCard, getCurrentPlayFieldModel(), playerWhoIsBankingCard.guid);
                return true;
            }
            else
            {
                //Card not in players hand, can't be banked
                return false;
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

        public bool isActionAllowedForPlayer(TurnActionTypes turnActionToDo, Guid playerGuid, PlayFieldModel currentState)
        {
            TurnActionTypes tAT = turnActionToDo;
            PlayerModel playerAttemptingAction = getPlayerModel(playerGuid, monopolyDeal.MONOPOLY_DEAL_GAME_GUID, currentState.thisPlayFieldModelInstanceGuid);
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
            PlayerModel player = getPlayerModel(playerGuid, gameGuid, stateGuid);
            foreach (TurnActionTypes t in player.actionsCurrentlyAllowed)
            {
                if (t.CompareTo(ta) == 0)
                {
                    return true;
                }
            }
            return false;
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

            pfm.guidOfPlayerWhosTurnItIs = pfm.playerModels[nextPlayerID].guid;
            foreach (PlayerModel p in pfm.playerModels)
            {
                if (p.guid.CompareTo(pfm.guidOfPlayerWhosTurnItIs) == 0)
                {
                    if (p.isThisPlayersTurn)
                    {
                        throw new Exception("player allready on turn");
                    }
                    p.isThisPlayersTurn = true;
                }
                else
                {
                    p.isThisPlayersTurn = false;
                }
            }
            pfm.guidOfPlayerWhosTurnItIs = pfm.playerModels.ElementAt(nextPlayerID).guid;
        }

        private void updateStateForPropertyPlayedToSet(TurnActionTypes actionToAttemptToPerform, PlayFieldModel currentState, Guid playerWhoPerformedActionGuid, PlayerModel playerWhoPerformedAction, PlayFieldModel newState)
        {
            throw new NotImplementedException();
        }

        private List<TurnActionTypes> setAllowableActionsOnTurn(List<TurnActionTypes> listToSet, PlayFieldModel newState)
        {
            if (newState.currentPhase.CompareTo(Statephase.Turn_Started_Cards_Drawn_0_Cards_Played) == 0 || newState.currentPhase.CompareTo(Statephase.Turn_Started_Cards_Drawn_1_Cards_Played) == 0 || newState.currentPhase.CompareTo(Statephase.Turn_Started_Cards_Drawn_2_Cards_Played) == 0)
            {
                listToSet.Add(TurnActionTypes.BankActionCard);
                listToSet.Add(TurnActionTypes.BankMoneyCard);
                listToSet.Add(TurnActionTypes.PlayCard);
                listToSet.Add(TurnActionTypes.PlayActionCard);
                listToSet.Add(TurnActionTypes.PlayPropertyCard_New_Set);
                listToSet.Add(TurnActionTypes.SwitchAroundPlayedProperties);
                listToSet.Add(TurnActionTypes.EndTurn);
                return listToSet;
            }
            else if (newState.currentPhase.CompareTo(Statephase.Turn_Ended_12_Cards_In_Hand_Discard_5_Cards) == 0)
            {
                listToSet.Add(TurnActionTypes.Discard_1_Card);
                return listToSet;
            }
            else if (newState.currentPhase.CompareTo(Statephase.Turn_Ended_11_Cards_In_Hand_Discard_4_Cards) == 0)
            {
                listToSet.Add(TurnActionTypes.Discard_1_Card);
                return listToSet;
            }
            else if (newState.currentPhase.CompareTo(Statephase.Turn_Ended_10_Cards_In_Hand_Discard_3_Cards) == 0)
            {
                listToSet.Add(TurnActionTypes.Discard_1_Card);
                return listToSet;
            }
            else if (newState.currentPhase.CompareTo(Statephase.Turn_Ended_9_Cards_In_Hand_Discard_2_Cards) == 0)
            {
                listToSet.Add(TurnActionTypes.Discard_1_Card);
                return listToSet;
            }
            else if (newState.currentPhase.CompareTo(Statephase.Turn_Ended_8_Cards_In_Hand_Discard_1_Card) == 0)
            {
                listToSet.Add(TurnActionTypes.Discard_1_Card);
                return listToSet;
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        private void updateStateActionCardPassGo(TurnActionTypes actionToAttemptToPerform, PlayFieldModel currentState, Guid playerWhoPerformedAction, PlayerModel player, PlayFieldModel newState)
        {
            //action is valid for player at this time
            List<TurnActionTypes> notOnTurn = new List<TurnActionTypes>();
            List<TurnActionTypes> onTurn = new List<TurnActionTypes>();

            onTurn = setAllowableActionsOnTurn(onTurn, newState);
            //updateStateForPropertyPlayedToSet(actionToAttemptToPerform, currentState, playerWhoPerformedAction, player, newState);
            //Change phase
            updateAllowableStates(newState, notOnTurn, onTurn, newState.guidOfPlayerWhosTurnItIs);
        }

        private void updateState(TurnActionTypes actionToAttemptToPerform, ActionCardAction actionCardType, PlayFieldModel currentState, Guid playerWhoPerformedAction)
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
                        onTurn = setAllowableActionsOnTurn(onTurn, newState);
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
                        onTurn = setAllowableActionsOnTurn(onTurn, newState);
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

                        //player has drawn their two cards, Now can play up to three cards on their turn on
                        List<TurnActionTypes> notOnTurn = new List<TurnActionTypes>();
                        List<TurnActionTypes> onTurn = new List<TurnActionTypes>();
                        onTurn = setAllowableActionsOnTurn(onTurn, newState);
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
                                    onTurn.Add(TurnActionTypes.Discard_1_Card);
                                    break;
                                }
                            case 10:
                                {
                                    newState.currentPhase = Statephase.Turn_Ended_10_Cards_In_Hand_Discard_3_Cards;
                                    onTurn.Add(TurnActionTypes.Discard_1_Card);
                                    break;
                                }
                            case 11:
                                {
                                    newState.currentPhase = Statephase.Turn_Ended_11_Cards_In_Hand_Discard_4_Cards;
                                    onTurn.Add(TurnActionTypes.Discard_1_Card);
                                    break;
                                }
                            case 12:
                                {
                                    newState.currentPhase = Statephase.Turn_Ended_12_Cards_In_Hand_Discard_5_Cards;
                                    onTurn.Add(TurnActionTypes.Discard_1_Card);
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

                #region playPropertyToNewSet

                else if (actionToAttemptToPerform.CompareTo(TurnActionTypes.PlayPropertyCard_New_Set) == 0)
                {
                    //Move was a valid move at current state
                    //Check if move is valid for player
                    if (isActionAllowedForPlayer(actionToAttemptToPerform, playerWhoPerformedAction, currentState))
                    {
                        //action is valid for player at this time
                        List<TurnActionTypes> notOnTurn = new List<TurnActionTypes>();
                        List<TurnActionTypes> onTurn = new List<TurnActionTypes>();

                        onTurn = setAllowableActionsOnTurn(onTurn, newState);
                        //updateStateForPropertyPlayedToSet(actionToAttemptToPerform, currentState, playerWhoPerformedAction, player, newState);
                        //Change phase
                        newState.currentPhase = Statephase.Turn_Started_Cards_Drawn_1_Cards_Played;
                        updateAllowableStates(newState, notOnTurn, onTurn, newState.guidOfPlayerWhosTurnItIs);
                    }
                }

                #endregion playPropertyToNewSet

                //Play Action

                #region Actions unable to be just say no carded

                #region pass Go

                else if (actionToAttemptToPerform.CompareTo(TurnActionTypes.PlayActionCard) == 0 && actionCardType.CompareTo(ActionCardAction.PassGo) == 0)
                {
                    //Move was a valid move at current state
                    //Check if move is valid for player
                    if (isActionAllowedForPlayer(actionToAttemptToPerform, playerWhoPerformedAction, currentState))
                    {
                        newState.currentPhase = Statephase.Turn_Started_Cards_Drawn_1_Cards_Played;
                        updateStateActionCardPassGo(actionToAttemptToPerform, currentState, playerWhoPerformedAction, player, newState);
                    }
                }

                #endregion pass Go

                #endregion Actions unable to be just say no carded
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
                        onTurn = setAllowableActionsOnTurn(onTurn, newState);
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
                                    onTurn.Add(TurnActionTypes.Discard_1_Card);
                                    break;
                                }
                            case 10:
                                {
                                    newState.currentPhase = Statephase.Turn_Ended_10_Cards_In_Hand_Discard_3_Cards;
                                    onTurn.Add(TurnActionTypes.Discard_1_Card);
                                    break;
                                }
                            case 11:
                                {
                                    newState.currentPhase = Statephase.Turn_Ended_11_Cards_In_Hand_Discard_4_Cards;
                                    onTurn.Add(TurnActionTypes.Discard_1_Card);
                                    break;
                                }
                            case 12:
                                {
                                    newState.currentPhase = Statephase.Turn_Ended_12_Cards_In_Hand_Discard_5_Cards;
                                    onTurn.Add(TurnActionTypes.Discard_1_Card);
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

                #region playPropertyToNewSet

                else if (actionToAttemptToPerform.CompareTo(TurnActionTypes.PlayPropertyCard_New_Set) == 0)
                {
                    //Move was a valid move at current state
                    //Check if move is valid for player
                    if (isActionAllowedForPlayer(actionToAttemptToPerform, playerWhoPerformedAction, currentState))
                    {
                        //action is valid for player at this time
                        List<TurnActionTypes> notOnTurn = new List<TurnActionTypes>();
                        List<TurnActionTypes> onTurn = new List<TurnActionTypes>();

                        onTurn = setAllowableActionsOnTurn(onTurn, newState);
                        //updateStateForPropertyPlayedToSet(actionToAttemptToPerform, currentState, playerWhoPerformedAction, player, newState);
                        //Change phase
                        newState.currentPhase = Statephase.Turn_Started_Cards_Drawn_2_Cards_Played;
                        updateAllowableStates(newState, notOnTurn, onTurn, newState.guidOfPlayerWhosTurnItIs);
                    }
                }

                #endregion playPropertyToNewSet

                //Play Action

                #region Actions unable to be just say no carded

                #region pass Go

                else if (actionToAttemptToPerform.CompareTo(TurnActionTypes.PlayActionCard) == 0 && actionCardType.CompareTo(ActionCardAction.PassGo) == 0)
                {
                    //Move was a valid move at current state
                    //Check if move is valid for player
                    if (isActionAllowedForPlayer(actionToAttemptToPerform, playerWhoPerformedAction, currentState))
                    {
                        newState.currentPhase = Statephase.Turn_Started_Cards_Drawn_2_Cards_Played;
                        updateStateActionCardPassGo(actionToAttemptToPerform, currentState, playerWhoPerformedAction, player, newState);
                    }
                }

                #endregion pass Go

                #endregion Actions unable to be just say no carded
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
                                    onTurn.Add(TurnActionTypes.Discard_1_Card);
                                    break;
                                }
                            case 10:
                                {
                                    newState.currentPhase = Statephase.Turn_Ended_10_Cards_In_Hand_Discard_3_Cards;
                                    onTurn.Add(TurnActionTypes.Discard_1_Card);
                                    break;
                                }
                            case 11:
                                {
                                    newState.currentPhase = Statephase.Turn_Ended_11_Cards_In_Hand_Discard_4_Cards;
                                    onTurn.Add(TurnActionTypes.Discard_1_Card);
                                    break;
                                }
                            case 12:
                                {
                                    newState.currentPhase = Statephase.Turn_Ended_12_Cards_In_Hand_Discard_5_Cards;
                                    onTurn.Add(TurnActionTypes.Discard_1_Card);
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

                #region playPropertyToNewSet

                else if (actionToAttemptToPerform.CompareTo(TurnActionTypes.PlayPropertyCard_New_Set) == 0)
                {
                    //Move was a valid move at current state
                    //Check if move is valid for player
                    if (isActionAllowedForPlayer(actionToAttemptToPerform, playerWhoPerformedAction, currentState))
                    {
                        //action is valid for player at this time
                        List<TurnActionTypes> notOnTurn = new List<TurnActionTypes>();
                        List<TurnActionTypes> onTurn = new List<TurnActionTypes>();
                        onTurn.Add(TurnActionTypes.SwitchAroundPlayedProperties);
                        onTurn.Add(TurnActionTypes.EndTurn);
                        //onTurn = setAllowableActionsOnTurn(onTurn, newState);
                        //updateStateForPropertyPlayedToSet(actionToAttemptToPerform, currentState, playerWhoPerformedAction, player, newState);
                        //Change phase
                        newState.currentPhase = Statephase.Turn_Started_Cards_Drawn_3_Cards_Played_Swap_Properties_Or_End_Turn_Only;
                        updateAllowableStates(newState, notOnTurn, onTurn, newState.guidOfPlayerWhosTurnItIs);
                    }
                }

                #endregion playPropertyToNewSet

                //Play Action

                #region Actions unable to be just say no carded

                #region pass Go

                else if (actionToAttemptToPerform.CompareTo(TurnActionTypes.PlayActionCard) == 0 && actionCardType.CompareTo(ActionCardAction.PassGo) == 0)
                {
                    //Move was a valid move at current state
                    //Check if move is valid for player
                    if (isActionAllowedForPlayer(actionToAttemptToPerform, playerWhoPerformedAction, currentState))
                    {
                        List<TurnActionTypes> notOnTurn = new List<TurnActionTypes>();
                        List<TurnActionTypes> onTurn = new List<TurnActionTypes>();
                        onTurn.Add(TurnActionTypes.SwitchAroundPlayedProperties);
                        onTurn.Add(TurnActionTypes.EndTurn);
                        newState.currentPhase = Statephase.Turn_Started_Cards_Drawn_3_Cards_Played_Swap_Properties_Or_End_Turn_Only;
                        updateAllowableStates(newState, notOnTurn, onTurn, newState.guidOfPlayerWhosTurnItIs);
                    }
                }

                #endregion pass Go

                #endregion Actions unable to be just say no carded
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
                                    onTurn.Add(TurnActionTypes.Discard_1_Card); //Replace with Discard_2_Cards when discarding 2 cards at a time is supported
                                    break;
                                }
                            case 10:
                                {
                                    newState.currentPhase = Statephase.Turn_Ended_10_Cards_In_Hand_Discard_3_Cards;
                                    onTurn.Add(TurnActionTypes.Discard_1_Card);//Replace with Discard_3_Cards when discarding 2 cards at a time is supported
                                    break;
                                }
                            case 11:
                                {
                                    newState.currentPhase = Statephase.Turn_Ended_11_Cards_In_Hand_Discard_4_Cards;
                                    onTurn.Add(TurnActionTypes.Discard_1_Card);//Replace with Discard_4_Cards when discarding 2 cards at a time is supported
                                    break;
                                }
                            case 12:
                                {
                                    newState.currentPhase = Statephase.Turn_Ended_12_Cards_In_Hand_Discard_5_Cards;
                                    onTurn.Add(TurnActionTypes.Discard_1_Card);//Replace with Discard_5_Cards when discarding 2 cards at a time is supported
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

            #region Turn_Ended_12_Cards_In_Hand_Discard_5_Cards

            else if (currentState.currentPhase.CompareTo(Statephase.Turn_Ended_12_Cards_In_Hand_Discard_5_Cards) == 0)
            {
                #region discard1Card

                if (actionToAttemptToPerform.CompareTo(TurnActionTypes.Discard_1_Card) == 0)
                {
                    //Move was a valid move at current state
                    //Check if move is valid for player
                    if (isActionAllowedForPlayer(actionToAttemptToPerform, playerWhoPerformedAction, currentState))
                    {
                        //action is valid for player at this time

                        //Could perform the action here instead, for now just change the phase of the state
                        //Not an action so cant be just say no'd
                        //Change phase
                        newState.currentPhase = Statephase.Turn_Ended_11_Cards_In_Hand_Discard_4_Cards;
                        //player has drawn their two cards, Now can play up to three cards on their turn
                        List<TurnActionTypes> notOnTurn = new List<TurnActionTypes>();
                        List<TurnActionTypes> onTurn = new List<TurnActionTypes>();
                        onTurn = setAllowableActionsOnTurn(onTurn, newState);
                        updateAllowableStates(newState, notOnTurn, onTurn, newState.guidOfPlayerWhosTurnItIs);
                    }
                }

                #endregion discard1Card
            }

            #endregion Turn_Ended_12_Cards_In_Hand_Discard_5_Cards

            #region Turn_Ended_11_Cards_In_Hand_Discard_4_Cards

            else if (currentState.currentPhase.CompareTo(Statephase.Turn_Ended_11_Cards_In_Hand_Discard_4_Cards) == 0)
            {
                #region discard1Card

                if (actionToAttemptToPerform.CompareTo(TurnActionTypes.Discard_1_Card) == 0)
                {
                    //Move was a valid move at current state
                    //Check if move is valid for player
                    if (isActionAllowedForPlayer(actionToAttemptToPerform, playerWhoPerformedAction, currentState))
                    {
                        //action is valid for player at this time

                        //Could perform the action here instead, for now just change the phase of the state
                        //Not an action so cant be just say no'd
                        //Change phase
                        newState.currentPhase = Statephase.Turn_Ended_10_Cards_In_Hand_Discard_3_Cards;

                        //player has drawn their two cards, Now can play up to three cards on their turn
                        List<TurnActionTypes> notOnTurn = new List<TurnActionTypes>();
                        List<TurnActionTypes> onTurn = new List<TurnActionTypes>();
                        onTurn = setAllowableActionsOnTurn(onTurn, newState);
                        updateAllowableStates(newState, notOnTurn, onTurn, newState.guidOfPlayerWhosTurnItIs);
                    }
                }

                #endregion discard1Card
            }

            #endregion Turn_Ended_11_Cards_In_Hand_Discard_4_Cards

            #region Turn_Ended_10_Cards_In_Hand_Discard_3_Cards

            else if (currentState.currentPhase.CompareTo(Statephase.Turn_Ended_10_Cards_In_Hand_Discard_3_Cards) == 0)
            {
                #region discard1Card

                if (actionToAttemptToPerform.CompareTo(TurnActionTypes.Discard_1_Card) == 0)
                {
                    //Move was a valid move at current state
                    //Check if move is valid for player
                    if (isActionAllowedForPlayer(actionToAttemptToPerform, playerWhoPerformedAction, currentState))
                    {
                        //action is valid for player at this time

                        //Could perform the action here instead, for now just change the phase of the state
                        //Not an action so cant be just say no'd
                        //Change phase
                        newState.currentPhase = Statephase.Turn_Ended_9_Cards_In_Hand_Discard_2_Cards;

                        //player has drawn their two cards, Now can play up to three cards on their turn
                        List<TurnActionTypes> notOnTurn = new List<TurnActionTypes>();
                        List<TurnActionTypes> onTurn = new List<TurnActionTypes>();
                        onTurn = setAllowableActionsOnTurn(onTurn, newState);
                        updateAllowableStates(newState, notOnTurn, onTurn, newState.guidOfPlayerWhosTurnItIs);
                    }
                }

                #endregion discard1Card
            }

            #endregion Turn_Ended_10_Cards_In_Hand_Discard_3_Cards

            #region Turn_Ended_9_Cards_In_Hand_Discard_2_Cards

            else if (currentState.currentPhase.CompareTo(Statephase.Turn_Ended_9_Cards_In_Hand_Discard_2_Cards) == 0)
            {
                #region discard1Card

                if (actionToAttemptToPerform.CompareTo(TurnActionTypes.Discard_1_Card) == 0)
                {
                    //Move was a valid move at current state
                    //Check if move is valid for player
                    if (isActionAllowedForPlayer(actionToAttemptToPerform, playerWhoPerformedAction, currentState))
                    {
                        //action is valid for player at this time

                        //Could perform the action here instead, for now just change the phase of the state
                        //Not an action so cant be just say no'd
                        //Change phase
                        newState.currentPhase = Statephase.Turn_Ended_8_Cards_In_Hand_Discard_1_Card;

                        //player has drawn their two cards, Now can play up to three cards on their turn
                        List<TurnActionTypes> notOnTurn = new List<TurnActionTypes>();
                        List<TurnActionTypes> onTurn = new List<TurnActionTypes>();
                        onTurn = setAllowableActionsOnTurn(onTurn, newState);
                        updateAllowableStates(newState, notOnTurn, onTurn, newState.guidOfPlayerWhosTurnItIs);
                    }
                }

                #endregion discard1Card
            }

            #endregion Turn_Ended_9_Cards_In_Hand_Discard_2_Cards

            #region Turn_Ended_8_Cards_In_Hand_Discard_1_Card

            else if (currentState.currentPhase.CompareTo(Statephase.Turn_Ended_8_Cards_In_Hand_Discard_1_Card) == 0)
            {
                #region discard1Card

                if (actionToAttemptToPerform.CompareTo(TurnActionTypes.Discard_1_Card) == 0)
                {
                    //Move was a valid move at current state
                    //Check if move is valid for player
                    List<TurnActionTypes> notOnTurn = new List<TurnActionTypes>();
                    List<TurnActionTypes> onTurn = new List<TurnActionTypes>();
                    if (isActionAllowedForPlayer(actionToAttemptToPerform, playerWhoPerformedAction, currentState))
                    {
                        //action is valid for player at this time

                        //Could perform the action here instead, for now just change the phase of the state
                        //Not an action so cant be just say no'd
                        //Change phase
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

                        //player has drawn their two cards, Now can play up to three cards on their turn

                        //onTurn = setAllowableActionsOnTurn(onTurn, newState);
                        updateAllowableStates(newState, notOnTurn, onTurn, newState.guidOfPlayerWhosTurnItIs);
                    }
                }

                #endregion discard1Card
            }

            #endregion Turn_Ended_8_Cards_In_Hand_Discard_1_Card
        }

        public bool discard(int cardsToDiscardID, Guid playerGuid, Guid serverGuid, Guid playfieldModelInstanceGuid)
        {
            Card cardInHandToBeDiscarded = monopolyDeal.deck.getCardByID(cardsToDiscardID);
            //Get the reference to the players playerModel in the current PlayFieldModel
            PlayerModel playerWhoIsDiscardingCard = getPlayerModel(playerGuid, serverGuid, playfieldModelInstanceGuid);
            //Get the reference to the Card in the current PlayFieldModel

            Card card = removeCardFromHand(cardInHandToBeDiscarded, playerWhoIsDiscardingCard);
            if (card != null)
            {
                getCurrentPlayFieldModel().drawPile.discardCard(card);
                //Change state on success
                updateState(TurnActionTypes.Discard_1_Card, ActionCardAction.NotAnActionCard, getCurrentPlayFieldModel(), playerWhoIsDiscardingCard.guid);
                return true;
            }
            else
            {
                //Card not in players hand, can't be discarded
                return false;
            }
        }

        public bool playActionCardPassGo(int passGoCardID, Guid serverGuid, Guid playerGuid, Guid playfieldModelInstanceGuid, TurnActionTypes turnActionTypes)
        {
            Card cardInHandToBePlayed = monopolyDeal.deck.getCardByID(passGoCardID);
            //Get the reference to the players playerModel in the current PlayFieldModel

            PlayerModel player = getPlayerModel(playerGuid, serverGuid, playfieldModelInstanceGuid);
            //Get the reference to the Card in the current PlayFieldModel
            if (cardInHandToBePlayed != null && cardInHandToBePlayed is ActionCard && ((ActionCard)cardInHandToBePlayed).actionType.CompareTo(ActionCardAction.PassGo) == 0)
            {
                Card card = removeCardFromHand(cardInHandToBePlayed, player);
                if (card != null)
                {
                    ActionCard actionCard = card as ActionCard;
                    player.hand.addCardToHand(currentPlayFieldModel.drawPile.drawcard());
                    player.hand.addCardToHand(currentPlayFieldModel.drawPile.drawcard());
                    //Change state on success

                    //Put card in discard pile
                    getCurrentPlayFieldModel().playpile.playCardOnPile(card);
                    updateState(TurnActionTypes.PlayActionCard, ActionCardAction.PassGo, getCurrentPlayFieldModel(), player.guid);
                    return true;
                }
                return false;
            }
            else
            {
                //Card not in players hand, can't be discarded not an actioncard
                return false;
            }
        }

        #endregion From GameStateManager

        public PropertyCardSet getPropertySet(Guid setGuid, Guid playerGuid, Guid gameLobbyGuid, Guid stateGuid)
        {
            PlayFieldModel pfm = getCurrentState();
            PlayerModel pm = getPlayerModel(playerGuid, gameLobbyGuid, stateGuid);
            foreach (PropertyCardSet ps in pm.propertySets.playersPropertySets)
            {
                if (ps.guid.CompareTo(setGuid) == 0)
                {
                    return ps;
                }
            }
            return null;
        }

        public bool playPropertyCardToExistingSet(Card playedCard, PropertyCardSet setToPlayPropertyTo, Guid gameLobbyGuid, Guid playerGuid, Guid playfieldModelInstanceGuid)
        {
            ///Check if Card is in hand
            bool cardIsInHand = false;
            int cardId = playedCard.cardID;
            PlayerModel pm = getPlayerModel(playerGuid, gameLobbyGuid, playfieldModelInstanceGuid);
            PropertyCardSet pset = getPropertySet(setToPlayPropertyTo.guid, playerGuid, gameLobbyGuid, playfieldModelInstanceGuid);
            Card card = null;
            foreach (Card c in pm.hand.cardsInHand)
            {
                if (c.cardID == cardId)
                {
                    cardIsInHand = true;
                    card = c;
                    break;
                }
            }

            if (cardIsInHand)
            {
                //Check if card is a property card
                PropertyCard pc = card as PropertyCard;
                if (pc != null)
                {
                    //Card is a property card
                    if (pset.addProperty(pc))
                    {
                        removeCardFromHand(pc, pm);
                        updateState(TurnActionTypes.PlayPropertyCard_New_Set, ActionCardAction.NotAnActionCard, currentPlayFieldModel, playerGuid);
                        return true;
                    }
                    else
                    {
                        //Is a property card in the players hand but cant be added to set
                        return false;
                    }
                }
            }
            return false;
        }

        public BoolResponseBox drawTwoCardsAtTurnStart(Guid player)
        {
            throw new NotImplementedException();
        }

        public BoolResponseBox drawFiveCards(Guid player)
        {
            throw new NotImplementedException();
        }
    }
}