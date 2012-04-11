using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MDWcfServiceLibrary
{
    internal class Move
    {
        private MonopolyDeal monopolyDeal;

        public Move(MonopolyDeal monopolyDeal)
        {
            // TODO: Complete member initialization
            this.monopolyDeal = monopolyDeal;
        }

        private Guid generateGuidForNextState()
        {
            return Guid.NewGuid();
        }

        private PlayerModel getPlayerModel(Guid playerGuid, PlayFieldModel pfm)
        {
            foreach (PlayerModel pm in pfm.playerModels)
            {
                if (pm.guid.CompareTo(playerGuid) == 0)
                {
                    return pm;
                }
            }
            return null;
        }

        private List<TurnActionTypes> setAllowableActionsNotOnTurnAffectedByActionCard(List<TurnActionTypes> listToSet, PlayFieldModel newState)
        {
            if (newState.currentPhase.CompareTo(Statephase.Turn_Started_Cards_Drawn_0_Cards_Played_Ask_Just_Say_No) == 0 || newState.currentPhase.CompareTo(Statephase.Turn_Started_Cards_Drawn_1_Cards_Played_Ask_Just_Say_No) == 0 || newState.currentPhase.CompareTo(Statephase.Turn_Started_Cards_Drawn_2_Cards_Played_Ask_Just_Say_No) == 0)
            {
                listToSet.Add(TurnActionTypes.PlayJustSayNo);
                return listToSet;
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        private List<TurnActionTypes> setAllowableActionsOnTurnJustSayNoUsedAgainst(List<TurnActionTypes> listToSet, PlayFieldModel newState)
        {
            if (newState.currentPhase.CompareTo(Statephase.Turn_Started_Cards_Drawn_0_Cards_Played_Just_Say_No_Used_By_Oppostion_Ask_Player_On_Turn_Just_Say_No) == 0 || newState.currentPhase.CompareTo(Statephase.Turn_Started_Cards_Drawn_1_Cards_Played_Just_Say_No_Used_By_Oppostion_Ask_Player_On_Turn_Just_Say_No) == 0 || newState.currentPhase.CompareTo(Statephase.Turn_Started_Cards_Drawn_2_Cards_Played_Just_Say_No_Used_By_Oppostion_Ask_Player_On_Turn_Just_Say_No) == 0)
            {
                listToSet.Add(TurnActionTypes.PlayJustSayNo);
                return listToSet;
            }
            else
            {
                throw new NotImplementedException();
            }
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

        private BoolResponseBox draw2CardsAtStartOfTurn(PlayFieldModel currentState, PlayFieldModel nextState, PlayerModel playerPerformingAction, Statephase nextStatePhaseIfSuccessful)
        {
            //Check
            //Perform action in next state
            //Clone the current state to create next state then draws two cards in the next state
            nextState = currentState.clone(generateGuidForNextState());
            PlayerModel playerModelForPlayer = getPlayerModel(playerPerformingAction.guid, nextState);

            //Perform action
            playerModelForPlayer.hand.addCardToHand(nextState.drawPile.drawcard());
            playerModelForPlayer.hand.addCardToHand(nextState.drawPile.drawcard());

            //drawtwocards has been performed, advance the phase of the game
            nextState.currentPhase = nextStatePhaseIfSuccessful;
            //Respond that the move has been performed

            //player has drawn their two cards, Now can play up to three cards on their turn
            //Set the allowable moves for all players for the next state
            List<TurnActionTypes> notOnTurn = new List<TurnActionTypes>();
            List<TurnActionTypes> onTurn = new List<TurnActionTypes>();
            onTurn = setAllowableActionsOnTurn(onTurn, nextState);
            updateAllowableStates(nextState, notOnTurn, onTurn, nextState.guidOfPlayerWhosTurnItIs);
            //change the current state to the next state
            addNextState(nextState);
            return new BoolResponseBox(true, "Player:" + playerPerformingAction.name + " Has drawn 2 cards");
        }

        private BoolResponseBox draw5CardsAtStartOfTurn(PlayFieldModel currentState, PlayFieldModel nextState, PlayerModel playerPerformingAction, Statephase nextStatePhaseIfSuccessful)
        {
            //Check
            //Perform action in next state
            //Clone the current state to create next state then draws 5 cards in the next state
            nextState = currentState.clone(generateGuidForNextState());
            PlayerModel playerModelForPlayer = getPlayerModel(playerPerformingAction.guid, nextState);

            //Perform action
            playerModelForPlayer.hand.addCardToHand(nextState.drawPile.drawcard());
            playerModelForPlayer.hand.addCardToHand(nextState.drawPile.drawcard());
            playerModelForPlayer.hand.addCardToHand(nextState.drawPile.drawcard());
            playerModelForPlayer.hand.addCardToHand(nextState.drawPile.drawcard());
            playerModelForPlayer.hand.addCardToHand(nextState.drawPile.drawcard());

            //drawtwocards has been performed, advance the phase of the game
            nextState.currentPhase = nextStatePhaseIfSuccessful;
            //Respond that the move has been performed

            //player has drawn their 5 cards, Now can play up to three cards on their turn
            //Set the allowable moves for all players for the next state
            List<TurnActionTypes> notOnTurn = new List<TurnActionTypes>();
            List<TurnActionTypes> onTurn = new List<TurnActionTypes>();
            onTurn = setAllowableActionsOnTurn(onTurn, nextState);
            updateAllowableStates(nextState, notOnTurn, onTurn, nextState.guidOfPlayerWhosTurnItIs);

            //change the current state to the next state
            addNextState(nextState);
            return new BoolResponseBox(true, "Player:" + playerPerformingAction.name + " Has drawn 5 cards");
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

        private BoolResponseBox endTurn(PlayFieldModel currentState, PlayFieldModel nextState, PlayerModel playerPerformingAction)
        {
            //Allowed to end turn
            //Clone the current state to create next state then draws two cards in the next state
            nextState = currentState.clone(generateGuidForNextState());
            PlayerModel playerModelForPlayer = getPlayerModel(playerPerformingAction.guid, nextState);

            //action is valid for player at this time
            List<TurnActionTypes> notOnTurn = new List<TurnActionTypes>();
            List<TurnActionTypes> onTurn = new List<TurnActionTypes>();
            //Could perform the action here instead, for now just change the phase of the state
            //Not an action so cant be just say no'd
            //Change phase
            switch (playerModelForPlayer.hand.cardsInHand.Count)
            {
                case 8:
                    {
                        nextState.currentPhase = Statephase.Turn_Ended_8_Cards_In_Hand_Discard_1_Card;
                        onTurn.Add(TurnActionTypes.Discard_1_Card);
                        //Update the moves for players
                        updateAllowableStates(nextState, notOnTurn, onTurn, nextState.guidOfPlayerWhosTurnItIs);
                        //change the current state to the next state
                        addNextState(nextState);
                        return new BoolResponseBox(true, "Player:" + playerPerformingAction.name + " Ended their turn with 8 cards and must discard 1 card");
                    }
                case 9:
                    {
                        nextState.currentPhase = Statephase.Turn_Ended_9_Cards_In_Hand_Discard_2_Cards;
                        onTurn.Add(TurnActionTypes.Discard_1_Card);
                        //Update the moves for players
                        updateAllowableStates(nextState, notOnTurn, onTurn, nextState.guidOfPlayerWhosTurnItIs);
                        //change the current state to the next state
                        addNextState(nextState);
                        return new BoolResponseBox(true, "Player:" + playerPerformingAction.name + " Ended their turn with 9 cards and must discard 2 cards");
                    }
                case 10:
                    {
                        nextState.currentPhase = Statephase.Turn_Ended_10_Cards_In_Hand_Discard_3_Cards;
                        onTurn.Add(TurnActionTypes.Discard_1_Card);
                        //Update the moves for players
                        updateAllowableStates(nextState, notOnTurn, onTurn, nextState.guidOfPlayerWhosTurnItIs);
                        //change the current state to the next state
                        addNextState(nextState);
                        return new BoolResponseBox(true, "Player:" + playerPerformingAction.name + " Ended their turn with 10 cards and must discard 3 cards");
                    }
                case 11:
                    {
                        nextState.currentPhase = Statephase.Turn_Ended_11_Cards_In_Hand_Discard_4_Cards;
                        onTurn.Add(TurnActionTypes.Discard_1_Card);
                        //Update the moves for players
                        updateAllowableStates(nextState, notOnTurn, onTurn, nextState.guidOfPlayerWhosTurnItIs);
                        //change the current state to the next state
                        addNextState(nextState);
                        return new BoolResponseBox(true, "Player:" + playerPerformingAction.name + " Ended their turn with 11 cards and must discard 4 cards");
                    }
                case 12:
                    {
                        nextState.currentPhase = Statephase.Turn_Ended_12_Cards_In_Hand_Discard_5_Cards;
                        onTurn.Add(TurnActionTypes.Discard_1_Card);
                        //Update the moves for players
                        updateAllowableStates(nextState, notOnTurn, onTurn, nextState.guidOfPlayerWhosTurnItIs);
                        //change the current state to the next state
                        addNextState(nextState);
                        return new BoolResponseBox(true, "Player:" + playerPerformingAction.name + " Ended their turn with 12 cards and must discard 5 cards");
                    }
                default:
                    {
                        nextState.currentPhase = Statephase.Turn_Ended_7_Or_Less_Cards_In_Hand_Setup_NextPlayer;
                        setNextPlayerOnTurn(nextState);
                        if (getPlayerModel(nextState.guidOfPlayerWhosTurnItIs, nextState).hand.cardsInHand.Count == 0)
                        {
                            //Player has 0 cards draws 5 on turn start instead of 2
                            nextState.currentPhase = Statephase.Turn_Started_Draw_5_Cards;
                            onTurn.Add(TurnActionTypes.drawFiveCardsAtStartOfTurn);
                            //Update the moves for players
                            updateAllowableStates(nextState, notOnTurn, onTurn, nextState.guidOfPlayerWhosTurnItIs);
                            //change the current state to the next state
                            addNextState(nextState);
                            return new BoolResponseBox(true, "Player:" + playerPerformingAction.name + " Ended their turn." + "Player:" + getPlayerModel(nextState.guidOfPlayerWhosTurnItIs, nextState).name + "\'s Turn. Draw 5 Cards.");
                        }
                        else
                        {
                            //Player has at least one and at most seven cards, draws 2
                            nextState.currentPhase = Statephase.Turn_Started_Draw_2_Cards;
                            onTurn.Add(TurnActionTypes.drawTwoCardsAtStartOfTurn);
                            //Update the moves for players
                            updateAllowableStates(nextState, notOnTurn, onTurn, nextState.guidOfPlayerWhosTurnItIs);
                            //change the current state to the next state
                            addNextState(nextState);
                            return new BoolResponseBox(true, "Player:" + playerPerformingAction.name + " Ended their turn." + "Player:" + getPlayerModel(nextState.guidOfPlayerWhosTurnItIs, nextState).name + "\'s Turn. Draw 2 Cards.");
                        }
                    }
            }
        }

        private BoolResponseBox doAppropriateAction(PlayFieldModel lastState, PlayFieldModel currentState, PlayFieldModel nextState, PlayerModel playerPerformingAction, TurnActionTypes typeOfActionToPerform, Statephase justSayNoAble, Statephase notJustSayNoAble, Statephase rearrangeProperties, Statephase drawCardsAtTurnStart, Statephase JustSayNoUsedByOpposition, MoveInfo cardsAndPlayersInvolved)
        {
            if (typeOfActionToPerform.CompareTo(TurnActionTypes.drawTwoCardsAtStartOfTurn) == 0)
            {
                return draw2CardsAtStartOfTurn(currentState, nextState, playerPerformingAction, drawCardsAtTurnStart);
            }
            else if (typeOfActionToPerform.CompareTo(TurnActionTypes.drawFiveCardsAtStartOfTurn) == 0)
            {
                return draw5CardsAtStartOfTurn(currentState, nextState, playerPerformingAction, drawCardsAtTurnStart);
            }
            else if (typeOfActionToPerform.CompareTo(TurnActionTypes.EndTurn) == 0)
            {
                return endTurn(currentState, nextState, playerPerformingAction);
            }
            else
            {
                return new BoolResponseBox(false, "Unsupported action");
            }
        }

        private BoolResponseBox checkIfMoveAllowedAtThisState(TurnActionTypes turnActionToDo, PlayerModel player, PlayFieldModel currentState)
        {
            TurnActionTypes tAT = turnActionToDo;
            PlayerModel playerAttemptingAction = player;
            if (playerAttemptingAction != null)
            {
                foreach (TurnActionTypes t in playerAttemptingAction.actionsCurrentlyAllowed)
                {
                    if (t.CompareTo(tAT) == 0)
                    {
                        //Action is in allowable list for player
                        return new BoolResponseBox(true, player.name + " is allowed to :" + turnActionToDo.ToString() + " at current phase");
                    }
                }
            }
            return new BoolResponseBox(false, player.name + " is not allowed to :" + turnActionToDo.ToString() + " at current phase"); ; //Action not allowable
        }

        public BoolResponseBox addNextState(PlayFieldModel nextPlayfieldModel)
        {
            //Add the next state to the list of states
            monopolyDeal.gameStates.Add(nextPlayfieldModel);
            //TODO If all players have adknowledged the current state then set the nextPlayfieldModel as the current state
            //For now set it straight away
            bool allAdknowleged = true;
            if (allAdknowleged)
            {
                monopolyDeal.currentState = nextPlayfieldModel;
                return new BoolResponseBox(true, "State added and set as current state");
            }
            else
            {
                return new BoolResponseBox(false, "Not all players have adknowledged recieving the current state, state added but not switched to");
            }
        }

        public BoolResponseBox evaluateMove(PlayFieldModel lastState, PlayFieldModel currentState, PlayFieldModel nextState, PlayerModel playerPerformingAction, TurnActionTypes typeOfActionToPerform, MoveInfo cardsAndPlayersInvolved)
        {
            BoolResponseBox result;
            if (currentState.currentPhase.CompareTo(Statephase.Turn_Started_Draw_2_Cards) == 0)
            {
                //Action card that can be just say no carded next state
                Statephase justSayNoAble = Statephase.Turn_Started_Cards_Drawn_0_Cards_Played;//Action cards not playable at this phase
                //Any move that plays a card excluding justSayNo move next state
                Statephase notJustSayNoAble = Statephase.Turn_Started_Cards_Drawn_0_Cards_Played;//No cards playable at this phase
                //Just Say No card played by off turn player
                Statephase JustSayNoUsedByOpposition = Statephase.Turn_Started_Cards_Drawn_0_Cards_Played;
                //Property card rearranging next state
                Statephase rearrangeProperties = Statephase.Turn_Started_Cards_Drawn_0_Cards_Played;//No cards can be rearranged at this phase
                //Draw
                Statephase drawCardsAtTurnStart = Statephase.Turn_Started_Cards_Drawn_0_Cards_Played;//Only action allowable at this phase
                BoolResponseBox isMoveTypeAllowableAtCurrentPhase = checkIfMoveAllowedAtThisState(typeOfActionToPerform, playerPerformingAction, currentState);
                if (isMoveTypeAllowableAtCurrentPhase.success)
                {
                    //type of move is allowable at this point for this player. Check if move is doable ex is a set stealable
                    result = doAppropriateAction(lastState, currentState, nextState, playerPerformingAction, typeOfActionToPerform, justSayNoAble, notJustSayNoAble, rearrangeProperties, drawCardsAtTurnStart, JustSayNoUsedByOpposition, cardsAndPlayersInvolved);

                    return result;//True if action performed, False if not
                }
                else
                {
                    //No other actionTypes are allowable in this state
                    return new BoolResponseBox(false, "Player:" + playerPerformingAction.name + " is not able to perform " + typeOfActionToPerform + " at this state:" + currentState.currentPhase.ToString());
                }
            }
            else if (currentState.currentPhase.CompareTo(Statephase.Turn_Started_Draw_5_Cards) == 0)
            {
                //Action card that can be just say no carded next state
                Statephase justSayNoAble = Statephase.Turn_Started_Cards_Drawn_0_Cards_Played;//Action cards not playable at this phase
                //Any move that plays a card excluding justSayNo move next state
                Statephase notJustSayNoAble = Statephase.Turn_Started_Cards_Drawn_0_Cards_Played;//No cards playable at this phase
                //Just Say No card played by off turn player
                Statephase JustSayNoUsedByOpposition = Statephase.Turn_Started_Cards_Drawn_0_Cards_Played;
                //Property card rearranging next state
                Statephase rearrangeProperties = Statephase.Turn_Started_Cards_Drawn_0_Cards_Played;//No cards can be rearranged at this phase
                //Draw
                Statephase drawCardsAtTurnStart = Statephase.Turn_Started_Cards_Drawn_0_Cards_Played;//Only action allowable at this phase
                BoolResponseBox isMoveTypeAllowableAtCurrentPhase = checkIfMoveAllowedAtThisState(typeOfActionToPerform, playerPerformingAction, currentState);
                if (isMoveTypeAllowableAtCurrentPhase.success)
                {
                    //type of move is allowable at this point for this player. Check if move is doable ex is a set stealable
                    result = doAppropriateAction(lastState, currentState, nextState, playerPerformingAction, typeOfActionToPerform, justSayNoAble, notJustSayNoAble, rearrangeProperties, drawCardsAtTurnStart, JustSayNoUsedByOpposition, cardsAndPlayersInvolved);

                    return result;//True if action performed, False if not
                }
                else
                {
                    //No other actionTypes are allowable in this state
                    return new BoolResponseBox(false, "Player:" + playerPerformingAction.name + " is not able to perform " + typeOfActionToPerform + " at this state:" + currentState.currentPhase.ToString());
                }
            }
            else if (currentState.currentPhase.CompareTo(Statephase.Turn_Started_Cards_Drawn_0_Cards_Played) == 0)
            {
                //Action card that can be just say no carded next state
                Statephase justSayNoAble = Statephase.Turn_Started_Cards_Drawn_0_Cards_Played_Ask_Just_Say_No;//Action cards not playable at this phase
                //Any move that plays a card excluding justSayNo move next state
                Statephase notJustSayNoAble = Statephase.Turn_Started_Cards_Drawn_1_Cards_Played;//No cards playable at this phase
                //Just Say No card played by off turn player
                Statephase JustSayNoUsedByOpposition = Statephase.Turn_Started_Cards_Drawn_0_Cards_Played_Just_Say_No_Used_By_Oppostion_Ask_Player_On_Turn_Just_Say_No;
                //Property card rearranging next state
                Statephase rearrangeProperties = Statephase.Turn_Started_Cards_Drawn_0_Cards_Played;//No cards can be rearranged at this phase
                //Draw
                Statephase drawCardsAtTurnStart = Statephase.Turn_Started_Cards_Drawn_0_Cards_Played;//Only action allowable at this phase
                BoolResponseBox isMoveTypeAllowableAtCurrentPhase = checkIfMoveAllowedAtThisState(typeOfActionToPerform, playerPerformingAction, currentState);
                if (isMoveTypeAllowableAtCurrentPhase.success)
                {
                    //type of move is allowable at this point for this player. Check if move is doable ex is a set stealable
                    result = doAppropriateAction(lastState, currentState, nextState, playerPerformingAction, typeOfActionToPerform, justSayNoAble, notJustSayNoAble, rearrangeProperties, drawCardsAtTurnStart, JustSayNoUsedByOpposition, cardsAndPlayersInvolved);

                    return result;//True if action performed, False if not
                }
                else
                {
                    //No other actionTypes are allowable in this state
                    return new BoolResponseBox(false, "Player:" + playerPerformingAction.name + " is not able to perform " + typeOfActionToPerform + " at this state:" + currentState.currentPhase.ToString());
                }
            }
            else if (currentState.currentPhase.CompareTo(Statephase.Turn_Started_Cards_Drawn_0_Cards_Played_Ask_Just_Say_No) == 0)
            {
                //Action card that can be just say no carded next state
                Statephase justSayNoAble = Statephase.Turn_Started_Cards_Drawn_0_Cards_Played_Ask_Just_Say_No;//Action cards not playable at this phase
                //Any move that plays a card excluding justSayNo move next state
                Statephase notJustSayNoAble = Statephase.Turn_Started_Cards_Drawn_1_Cards_Played;//No cards playable at this phase
                //Just Say No card played by off turn player
                Statephase JustSayNoUsedByOpposition = Statephase.Turn_Started_Cards_Drawn_0_Cards_Played_Just_Say_No_Used_By_Oppostion_Ask_Player_On_Turn_Just_Say_No;
                //Property card rearranging next state
                Statephase rearrangeProperties = Statephase.Turn_Started_Cards_Drawn_0_Cards_Played;//No cards can be rearranged at this phase
                //Draw
                Statephase drawCardsAtTurnStart = Statephase.Turn_Started_Cards_Drawn_0_Cards_Played;//Only action allowable at this phase
                BoolResponseBox isMoveTypeAllowableAtCurrentPhase = checkIfMoveAllowedAtThisState(typeOfActionToPerform, playerPerformingAction, currentState);
                if (isMoveTypeAllowableAtCurrentPhase.success)
                {
                    //type of move is allowable at this point for this player. Check if move is doable ex is a set stealable
                    result = doAppropriateAction(lastState, currentState, nextState, playerPerformingAction, typeOfActionToPerform, justSayNoAble, notJustSayNoAble, rearrangeProperties, drawCardsAtTurnStart, JustSayNoUsedByOpposition, cardsAndPlayersInvolved);

                    return result;//True if action performed, False if not
                }
                else
                {
                    //No other actionTypes are allowable in this state
                    return new BoolResponseBox(false, "Player:" + playerPerformingAction.name + " is not able to perform " + typeOfActionToPerform + " at this state:" + currentState.currentPhase.ToString());
                }
            }
            else if (currentState.currentPhase.CompareTo(Statephase.Turn_Started_Cards_Drawn_0_Cards_Played_Just_Say_No_Used_By_Oppostion_Ask_Player_On_Turn_Just_Say_No) == 0)
            {
            }
            else if (currentState.currentPhase.CompareTo(Statephase.Turn_Started_Cards_Drawn_1_Cards_Played) == 0)
            {
            }
            else if (currentState.currentPhase.CompareTo(Statephase.Turn_Started_Cards_Drawn_1_Cards_Played_Ask_Just_Say_No) == 0)
            {
            }
            else if (currentState.currentPhase.CompareTo(Statephase.Turn_Started_Cards_Drawn_1_Cards_Played_Just_Say_No_Used_By_Oppostion_Ask_Player_On_Turn_Just_Say_No) == 0)
            {
            }
            else if (currentState.currentPhase.CompareTo(Statephase.Turn_Started_Cards_Drawn_2_Cards_Played) == 0)
            {
            }
            else if (currentState.currentPhase.CompareTo(Statephase.Turn_Started_Cards_Drawn_2_Cards_Played_Ask_Just_Say_No) == 0)
            {
            }
            else if (currentState.currentPhase.CompareTo(Statephase.Turn_Started_Cards_Drawn_2_Cards_Played_Just_Say_No_Used_By_Oppostion_Ask_Player_On_Turn_Just_Say_No) == 0)
            {
            }
            else if (currentState.currentPhase.CompareTo(Statephase.Turn_Started_Cards_Drawn_3_Cards_Played_Swap_Properties_Or_End_Turn_Only) == 0)
            {
            }
            else if (currentState.currentPhase.CompareTo(Statephase.Turn_Ended_12_Cards_In_Hand_Discard_5_Cards) == 0)
            {
            }
            else if (currentState.currentPhase.CompareTo(Statephase.Turn_Ended_11_Cards_In_Hand_Discard_4_Cards) == 0)
            {
            }
            else if (currentState.currentPhase.CompareTo(Statephase.Turn_Ended_10_Cards_In_Hand_Discard_3_Cards) == 0)
            {
            }
            else if (currentState.currentPhase.CompareTo(Statephase.Turn_Ended_9_Cards_In_Hand_Discard_2_Cards) == 0)
            {
            }
            else if (currentState.currentPhase.CompareTo(Statephase.Turn_Ended_8_Or_More_Cards_In_Hand_Discard_Cards) == 0)
            {
            }
            else if (currentState.currentPhase.CompareTo(Statephase.Turn_Ended_7_Or_Less_Cards_In_Hand_Setup_NextPlayer) == 0)
            {
            }
            else if (currentState.currentPhase.CompareTo(Statephase.Game_Started) == 0)
            {
            }
            else
            {
                return new BoolResponseBox(false, "Game is in an invalid state");
            }
            return new BoolResponseBox(false, "Game is in an invalid state");
        }
    }
}