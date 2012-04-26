﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MDWcfServiceLibrary
{
    internal class Move
    {
        private MonopolyDeal monopolyDeal;
        private int playerTurnCounter = 0;

        public Move(MonopolyDeal monopolyDeal)
        {
            // TODO: Complete member initialization
            this.monopolyDeal = monopolyDeal;
        }

        private Guid generateGuidForNextState()
        {
            return Guid.NewGuid();
        }

        /// <summary>
        /// Gets the PlayerModel from a state given the PlayerModels Guid
        /// </summary>
        /// <param name="playerGuid"></param>
        /// <param name="pfm"></param>
        /// <returns></returns>
        public PlayerModel getPlayerModel(Guid playerGuid, PlayFieldModel pfm)
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

        /// <summary>
        /// Returns a list of the actions a player not on their turn can take when they are affected by an ActionCard
        /// </summary>
        /// <param name="listToSet"></param>
        /// <param name="newState"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Returns a list of the actions a player can take when they are not on their turn and are in debt
        /// </summary>
        /// <param name="listToSet"></param>
        /// <param name="newState"></param>
        /// <returns></returns>
        private List<TurnActionTypes> setAllowableActionsNotOnTurnInDebt(List<TurnActionTypes> listToSet, PlayFieldModel newState)
        {
            if (newState.currentPhase.CompareTo(Statephase.Turn_Started_Cards_Drawn_0_Cards_Played_Ask_Just_Say_No) == 0 || newState.currentPhase.CompareTo(Statephase.Turn_Started_Cards_Drawn_1_Cards_Played_Ask_Just_Say_No) == 0 || newState.currentPhase.CompareTo(Statephase.Turn_Started_Cards_Drawn_2_Cards_Played_Ask_Just_Say_No) == 0 || newState.currentPhase.CompareTo(Statephase.Turn_Started_Cards_Drawn_0_Cards_Played) == 0 || newState.currentPhase.CompareTo(Statephase.Turn_Started_Cards_Drawn_1_Cards_Played) == 0 || newState.currentPhase.CompareTo(Statephase.Turn_Started_Cards_Drawn_2_Cards_Played) == 0)
            {
                listToSet.Add(TurnActionTypes.PlayJustSayNo);
                listToSet.Add(TurnActionTypes.PayDebt);
                return listToSet;
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Returns a list of the actions a player on their turn whose actionCard has been Just Say No'd can take
        /// </summary>
        /// <param name="listToSet"></param>
        /// <param name="newState"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Sets the actions a player can take on their turn given the new state
        /// </summary>
        /// <param name="listToSet"></param>
        /// <param name="newState"></param>
        /// <returns></returns>
        private List<TurnActionTypes> setAllowableActionsOnTurn(List<TurnActionTypes> listToSet, PlayFieldModel newState)
        {
            if (newState.currentPhase.CompareTo(Statephase.Turn_Started_Cards_Drawn_0_Cards_Played) == 0 || newState.currentPhase.CompareTo(Statephase.Turn_Started_Cards_Drawn_1_Cards_Played) == 0 || newState.currentPhase.CompareTo(Statephase.Turn_Started_Cards_Drawn_2_Cards_Played) == 0)
            {
                listToSet.Add(TurnActionTypes.BankActionCard);
                listToSet.Add(TurnActionTypes.BankMoneyCard);
                listToSet.Add(TurnActionTypes.PlayCard);
                listToSet.Add(TurnActionTypes.PlayActionCard);
                listToSet.Add(TurnActionTypes.PlayPropertyCardFromHand);
                listToSet.Add(TurnActionTypes.PlayPropertyCard_New_Set);
                listToSet.Add(TurnActionTypes.SwitchAroundPlayedProperties);
                listToSet.Add(TurnActionTypes.MovePropertyCard);
                listToSet.Add(TurnActionTypes.EndTurn);
                return listToSet;
            }
            else if (newState.currentPhase.CompareTo(Statephase.Turn_Started_Cards_Drawn_3_Cards_Played_Swap_Properties_Or_End_Turn_Only) == 0)
            {
                listToSet.Add(TurnActionTypes.EndTurn);
                listToSet.Add(TurnActionTypes.MovePropertyCard);
                listToSet.Add(TurnActionTypes.SwitchAroundPlayedProperties);
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
            else if (newState.currentPhase.CompareTo(Statephase.Turn_Started_Cards_Drawn_0_Cards_Played_Ask_Just_Say_No) == 0)
            {
                return listToSet;
            }
            else if (newState.currentPhase.CompareTo(Statephase.Turn_Started_Cards_Drawn_2_Cards_Played_Ask_Just_Say_No) == 0)
            {
                return listToSet;
            }
            else if (newState.currentPhase.CompareTo(Statephase.Turn_Started_Cards_Drawn_2_Cards_Played_Just_Say_No_Used_By_Oppostion_Ask_Player_On_Turn_Just_Say_No) == 0)
            {
                return listToSet;
            }
            else if (newState.currentPhase.CompareTo(Statephase.Turn_Started_Cards_Drawn_0_Cards_Played_Just_Say_No_Used_By_Oppostion_Ask_Player_On_Turn_Just_Say_No) == 0)
            {
                listToSet.Add(TurnActionTypes.PlayJustSayNo);
                listToSet.Add(TurnActionTypes.Dont_Play_Just_Say_No);
                return listToSet;
            }
            else if (newState.currentPhase.CompareTo(Statephase.Turn_Started_Cards_Drawn_1_Cards_Played_Just_Say_No_Used_By_Oppostion_Ask_Player_On_Turn_Just_Say_No) == 0)
            {
                listToSet.Add(TurnActionTypes.PlayJustSayNo);
                listToSet.Add(TurnActionTypes.Dont_Play_Just_Say_No);
                return listToSet;
            }
            else if (newState.currentPhase.CompareTo(Statephase.Turn_Started_Cards_Drawn_2_Cards_Played_Just_Say_No_Used_By_Oppostion_Ask_Player_On_Turn_Just_Say_No) == 0)
            {
                listToSet.Add(TurnActionTypes.PlayJustSayNo);
                listToSet.Add(TurnActionTypes.Dont_Play_Just_Say_No);
                return listToSet;
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Removes a Card from a players Hand and returns the card
        /// </summary>
        /// <param name="card"></param>
        /// <param name="pm"></param>
        /// <returns></returns>
        private Card removeCardFromHand(Card card, PlayerModel pm)
        {
            Card cardInHand = checkIfCardInHand(card, pm);
            if (cardInHand != null && pm.hand.cardsInHand.Remove(cardInHand))
            {
                return cardInHand;
            }
            return null;
        }

        /// <summary>
        /// Determines if a Card is in a players hand and returns the Card from the players hand
        /// </summary>
        /// <param name="card"></param>
        /// <param name="pm"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Sets the allowable actions for each player
        /// </summary>
        /// <param name="state"></param>
        /// <param name="allowedForPlayersNotOnTurn"></param>
        /// <param name="allowedForPlayerOnTurn"></param>
        /// <param name="playerOnTurnGuid"></param>
        /// <param name="playerWhoPerformedActionGuid"></param>
        private void updateAllowableStates(PlayFieldModel state, List<TurnActionTypes> allowedForPlayersNotOnTurn, List<TurnActionTypes> allowedForPlayerOnTurn, Guid playerOnTurnGuid, Guid playerWhoPerformedActionGuid)
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

        /// <summary>
        /// Sets the actions each player can take after a debt has been played by a player
        /// </summary>
        /// <param name="state"></param>
        /// <param name="allowedForPlayersWhoDontHaveToPay"></param>
        /// <param name="allowedForPlayerOnTurn"></param>
        /// <param name="playerOnTurnGuid"></param>
        /// <param name="allowedForPlayersWhoDoHaveToPay"></param>
        private void updateAllowableStatesDebtPaid(PlayFieldModel state, List<TurnActionTypes> allowedForPlayersWhoDontHaveToPay, List<TurnActionTypes> allowedForPlayerOnTurn, Guid playerOnTurnGuid, List<TurnActionTypes> allowedForPlayersWhoDoHaveToPay)
        {
            foreach (PlayerModel p in state.playerModels)
            {
                if (p.guid.CompareTo(playerOnTurnGuid) == 0)
                //if (p.isThisPlayersTurn)
                {
                    //its p's turn
                    p.actionsCurrentlyAllowed = allowedForPlayerOnTurn;
                }
                else if (p.owesAnotherPlayer)
                {
                    p.actionsCurrentlyAllowed = allowedForPlayersWhoDoHaveToPay;
                }
                else
                {
                    p.actionsCurrentlyAllowed = allowedForPlayersWhoDontHaveToPay;
                }
            }
        }

        /// <summary>
        /// Allows a player to Draw 2 Cards at the start of their turn
        /// </summary>
        /// <param name="currentState"></param>
        /// <param name="nextState"></param>
        /// <param name="playerPerformingAction"></param>
        /// <param name="nextStatePhaseIfSuccessful"></param>
        /// <returns></returns>
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
            updateAllowableStates(nextState, notOnTurn, onTurn, nextState.guidOfPlayerWhosTurnItIs, nextState.guidOfPlayerWhosTurnItIs);
            //change the current state to the next state
            addNextState(nextState);
            return new BoolResponseBox(true, "Player:" + playerPerformingAction.name + " Has drawn 2 cards");
        }

        /// <summary>
        /// Allows a player to Draw 5 Cards at the start of their turn
        /// </summary>
        /// <param name="currentState"></param>
        /// <param name="nextState"></param>
        /// <param name="playerPerformingAction"></param>
        /// <param name="nextStatePhaseIfSuccessful"></param>
        /// <returns></returns>
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
            updateAllowableStates(nextState, notOnTurn, onTurn, nextState.guidOfPlayerWhosTurnItIs, nextState.guidOfPlayerWhosTurnItIs);

            //change the current state to the next state
            addNextState(nextState);
            return new BoolResponseBox(true, "Player:" + playerPerformingAction.name + " Has drawn 5 cards");
        }

        /// <summary>
        /// Allows a player on their turn to play a Pass Go card
        /// </summary>
        /// <param name="currentState"></param>
        /// <param name="nextState"></param>
        /// <param name="playerPerformingAction"></param>
        /// <param name="nextStatePhaseIfSuccessful"></param>
        /// <param name="passGoInfo"></param>
        /// <returns></returns>
        public BoolResponseBox playActionCardPassGo(PlayFieldModel currentState, PlayFieldModel nextState, PlayerModel playerPerformingAction, Statephase nextStatePhaseIfSuccessful, MoveInfo passGoInfo)
        {
            //Check
            //Perform action in next state
            //Clone the current state to create next state then draws 5 cards in the next state
            nextState = currentState.clone(generateGuidForNextState());
            PlayerModel playerModelForPlayer = getPlayerModel(playerPerformingAction.guid, nextState);

            Card cardInHandToBePlayed = nextState.deck.getCardByID(passGoInfo.idOfCardBeingUsed);
            //Get the reference to the players playerModel in the current PlayFieldModel

            PlayerModel player = getPlayerModel(passGoInfo.playerMakingMove, nextState);
            //Get the reference to the Card in the current PlayFieldModel
            if (cardInHandToBePlayed != null && cardInHandToBePlayed is ActionCard && ((ActionCard)cardInHandToBePlayed).actionType.CompareTo(ActionCardAction.PassGo) == 0)
            {
                Card card = removeCardFromHand(cardInHandToBePlayed, player);
                if (card != null)
                {
                    ActionCard actionCard = card as ActionCard;
                    player.hand.addCardToHand(nextState.drawPile.drawcard());
                    player.hand.addCardToHand(nextState.drawPile.drawcard());
                    //Change state on success
                    //has been performed, advance the phase of the game
                    nextState.currentPhase = nextStatePhaseIfSuccessful;
                    //Put card in discard pile
                    nextState.playpile.playCardOnPile(card);
                    List<TurnActionTypes> notOnTurn = new List<TurnActionTypes>();
                    List<TurnActionTypes> onTurn = new List<TurnActionTypes>();
                    onTurn = setAllowableActionsOnTurn(onTurn, nextState);

                    updateAllowableStates(nextState, notOnTurn, onTurn, nextState.guidOfPlayerWhosTurnItIs, nextState.guidOfPlayerWhosTurnItIs);
                    //change the current state to the next state
                    addNextState(nextState);
                    return new BoolResponseBox(true, "Player:" + playerPerformingAction.name + " Has passed go and drawn 2 cards");
                }
                return new BoolResponseBox(false, "Card is not in hand.");
            }
            return new BoolResponseBox(false, "Card is not in hand or is not a pass go card");
        }

        /// <summary>
        /// Sets the next player to have their turn.
        /// </summary>
        /// <param name="pfm"></param>
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
                        updateAllowableStates(nextState, notOnTurn, onTurn, nextState.guidOfPlayerWhosTurnItIs, nextState.guidOfPlayerWhosTurnItIs);
                        //change the current state to the next state
                        addNextState(nextState);
                        return new BoolResponseBox(true, "Player:" + playerPerformingAction.name + " Ended their turn with 8 cards and must discard 1 card");
                    }
                case 9:
                    {
                        nextState.currentPhase = Statephase.Turn_Ended_9_Cards_In_Hand_Discard_2_Cards;
                        onTurn.Add(TurnActionTypes.Discard_1_Card);
                        //Update the moves for players
                        updateAllowableStates(nextState, notOnTurn, onTurn, nextState.guidOfPlayerWhosTurnItIs, nextState.guidOfPlayerWhosTurnItIs);
                        //change the current state to the next state
                        addNextState(nextState);
                        return new BoolResponseBox(true, "Player:" + playerPerformingAction.name + " Ended their turn with 9 cards and must discard 2 cards");
                    }
                case 10:
                    {
                        nextState.currentPhase = Statephase.Turn_Ended_10_Cards_In_Hand_Discard_3_Cards;
                        onTurn.Add(TurnActionTypes.Discard_1_Card);
                        //Update the moves for players
                        updateAllowableStates(nextState, notOnTurn, onTurn, nextState.guidOfPlayerWhosTurnItIs, nextState.guidOfPlayerWhosTurnItIs);
                        //change the current state to the next state
                        addNextState(nextState);
                        return new BoolResponseBox(true, "Player:" + playerPerformingAction.name + " Ended their turn with 10 cards and must discard 3 cards");
                    }
                case 11:
                    {
                        nextState.currentPhase = Statephase.Turn_Ended_11_Cards_In_Hand_Discard_4_Cards;
                        onTurn.Add(TurnActionTypes.Discard_1_Card);
                        //Update the moves for players
                        updateAllowableStates(nextState, notOnTurn, onTurn, nextState.guidOfPlayerWhosTurnItIs, nextState.guidOfPlayerWhosTurnItIs);
                        //change the current state to the next state
                        addNextState(nextState);
                        return new BoolResponseBox(true, "Player:" + playerPerformingAction.name + " Ended their turn with 11 cards and must discard 4 cards");
                    }
                case 12:
                    {
                        nextState.currentPhase = Statephase.Turn_Ended_12_Cards_In_Hand_Discard_5_Cards;
                        onTurn.Add(TurnActionTypes.Discard_1_Card);
                        //Update the moves for players
                        updateAllowableStates(nextState, notOnTurn, onTurn, nextState.guidOfPlayerWhosTurnItIs, nextState.guidOfPlayerWhosTurnItIs);
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
                            updateAllowableStates(nextState, notOnTurn, onTurn, nextState.guidOfPlayerWhosTurnItIs, nextState.guidOfPlayerWhosTurnItIs);
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
                            updateAllowableStates(nextState, notOnTurn, onTurn, nextState.guidOfPlayerWhosTurnItIs, nextState.guidOfPlayerWhosTurnItIs);
                            //change the current state to the next state
                            addNextState(nextState);
                            return new BoolResponseBox(true, "Player:" + playerPerformingAction.name + " Ended their turn." + "Player:" + getPlayerModel(nextState.guidOfPlayerWhosTurnItIs, nextState).name + "\'s Turn. Draw 2 Cards.");
                        }
                    }
            }
        }

        /// <summary>
        /// Determines the type of action to perform and calls the correct method to perform it
        /// </summary>
        /// <param name="lastState"></param>
        /// <param name="currentState"></param>
        /// <param name="nextState"></param>
        /// <param name="playerPerformingAction"></param>
        /// <param name="typeOfActionToPerform"></param>
        /// <param name="justSayNoAble"></param>
        /// <param name="notJustSayNoAble"></param>
        /// <param name="rearrangeProperties"></param>
        /// <param name="drawCardsAtTurnStart"></param>
        /// <param name="JustSayNoUsedByOpposition"></param>
        /// <param name="moveInformation"></param>
        /// <param name="discard"></param>
        /// <returns></returns>
        private BoolResponseBox doAppropriateAction(PlayFieldModel lastState, PlayFieldModel currentState, PlayFieldModel nextState, PlayerModel playerPerformingAction, TurnActionTypes typeOfActionToPerform, Statephase justSayNoAble, Statephase notJustSayNoAble, Statephase rearrangeProperties, Statephase drawCardsAtTurnStart, Statephase JustSayNoUsedByOpposition, MoveInfo moveInformation, Statephase discard)
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
            else if (typeOfActionToPerform.CompareTo(TurnActionTypes.MovePropertyCard) == 0)
            {
                return movePropertyCard(currentState, nextState, playerPerformingAction, moveInformation, rearrangeProperties);
            }
            else if (typeOfActionToPerform.CompareTo(TurnActionTypes.PlayPropertyCardFromHand) == 0)
            {
                return playPropertyCardFromHand(currentState, nextState, playerPerformingAction, moveInformation, notJustSayNoAble);
            }
            else if (typeOfActionToPerform.CompareTo(TurnActionTypes.PlayPropertyCard_New_Set) == 0)
            {
                return playPropertyCardFromHandToNewSet(currentState, nextState, playerPerformingAction, moveInformation, notJustSayNoAble);
            }
            else if (typeOfActionToPerform.CompareTo(TurnActionTypes.BankActionCard) == 0)
            {
                return playCardFromHandToBank(currentState, nextState, playerPerformingAction, moveInformation, notJustSayNoAble);
            }
            else if (typeOfActionToPerform.CompareTo(TurnActionTypes.BankMoneyCard) == 0)
            {
                return playCardFromHandToBank(currentState, nextState, playerPerformingAction, moveInformation, notJustSayNoAble);
            }
            else if (typeOfActionToPerform.CompareTo(TurnActionTypes.Discard_1_Card) == 0)
            {
                return discard1Card(currentState, nextState, playerPerformingAction, moveInformation, discard);
            }
            else if (typeOfActionToPerform.CompareTo(TurnActionTypes.PayDebt) == 0)
            {
                return payDebt(currentState, nextState, playerPerformingAction, moveInformation, justSayNoAble, notJustSayNoAble);
            }
            //ActionCards
            else if (typeOfActionToPerform.CompareTo(TurnActionTypes.PlayActionCard) == 0 || typeOfActionToPerform.CompareTo(TurnActionTypes.PlayCard) == 0)
            {
                if (moveInformation.actionCardActionType.CompareTo(ActionCardAction.PassGo) == 0)
                {
                    Statephase nextStatePhase = notJustSayNoAble;
                    return playActionCardPassGo(currentState, nextState, playerPerformingAction, nextStatePhase, moveInformation);
                }
                else if (moveInformation.actionCardActionType.CompareTo(ActionCardAction.DebtCollector) == 0)
                {
                    Statephase nextStatePhase = justSayNoAble;
                    return playActionCardDebtCollector(currentState, nextState, playerPerformingAction, nextStatePhase, moveInformation);
                }
                else if (moveInformation.actionCardActionType.CompareTo(ActionCardAction.ItsMyBirthday) == 0)
                {
                    Statephase nextStatePhase = justSayNoAble;
                    return playActionCardItsMyBirthday(currentState, nextState, playerPerformingAction, nextStatePhase, moveInformation);
                }
                else if (moveInformation.actionCardActionType.CompareTo(ActionCardAction.JustSayNo) == 0)
                {
                    Statephase nextStatePhase = notJustSayNoAble;
                    return playActionCardJustSayNo(currentState, nextState, playerPerformingAction, nextStatePhase, justSayNoAble, JustSayNoUsedByOpposition, moveInformation);
                }
                throw new NotImplementedException();
            }
            else if (typeOfActionToPerform.CompareTo(TurnActionTypes.PlayJustSayNo) == 0)
            {
                throw new NotImplementedException();
            }
            else
            {
                return new BoolResponseBox(false, "Unsupported action:" + typeOfActionToPerform.ToString());
            }
        }

        private BoolResponseBox playActionCardJustSayNo(PlayFieldModel currentState, PlayFieldModel nextState, PlayerModel playerPerformingAction, Statephase nextStatePhase, Statephase justSayNoAble, Statephase JustSayNoUsedByOpposition, MoveInfo moveInformation)
        {
            #region Player not on turn canceling effect of ActionCard being played against them

            if (currentState.actionCardEvent.actionJustSayNoUsedByAffectedPlayer == false)
            {
                #region Just Say No used against debt incurring card

                if (currentState.actionCardEvent.actionCardTypeUsed.CompareTo(ActionCardAction.DebtCollector) == 0 || currentState.actionCardEvent.actionCardTypeUsed.CompareTo(ActionCardAction.DoubleTheRent) == 0 || currentState.actionCardEvent.actionCardTypeUsed.CompareTo(ActionCardAction.ItsMyBirthday) == 0 || currentState.actionCardEvent.actionCardTypeUsed.CompareTo(ActionCardAction.RentMultiColor) == 0 || currentState.actionCardEvent.actionCardTypeUsed.CompareTo(ActionCardAction.RentStandard) == 0)
                {
                    //Money events are just say noable before action is taken so rollback is unneccessary
                    //Check
                    //Perform action in next state
                    //Clone the current state to create next state
                    nextState = currentState.clone(generateGuidForNextState());

                    //Player Paying Debt
                    PlayerModel playerModelForPlayerPaying = getPlayerModel(playerPerformingAction.guid, nextState);

                    //Player Being paid debt
                    PlayerModel playerModelForPlayerToBePaid = getPlayerModel(moveInformation.guidOfPlayerToPayDebtTo, nextState);
                    //Discard Just say no card
                    Card cardInHandToBePlayed = nextState.deck.getCardByID(moveInformation.idOfCardBeingUsed);
                    //Get the reference to the players playerModel in the current PlayFieldModel

                    //Get the reference to the Card in the current PlayFieldModel
                    if (cardInHandToBePlayed != null && cardInHandToBePlayed is ActionCard && ((ActionCard)cardInHandToBePlayed).actionType.CompareTo(ActionCardAction.JustSayNo) == 0)
                    {
                        Card card = removeCardFromHand(cardInHandToBePlayed, playerModelForPlayerPaying);
                        if (card != null)
                        {
                            ActionCard actionCard = card as ActionCard;
                            //Do action

                            bool otherPlayersPaying = false;
                            //Get if player paying is the only player who has to pay
                            foreach (PlayerModel player in nextState.playerModels)
                            {
                                if (player.guid.CompareTo(playerModelForPlayerPaying.guid) != 0 && player.owesAnotherPlayer)
                                {
                                    //Another player owes money
                                    otherPlayersPaying = true;
                                    break;
                                }
                            }
                            Statephase nextStatePhaseIfSuccessful;
                            if (otherPlayersPaying)
                            {
                                //Other players must pay so states phase does not change
                                nextStatePhaseIfSuccessful = JustSayNoUsedByOpposition;
                            }
                            else
                            {
                                //No other players have to pay debt
                                nextStatePhaseIfSuccessful = JustSayNoUsedByOpposition;
                            }
                            //Generate action card event to give the chance to just say no
                            ActionCardEvent justSayNoUsedAgainstDebt = new ActionCardEvent();
                            justSayNoUsedAgainstDebt.actionCardTypeUsed = currentState.actionCardEvent.actionCardTypeUsed;//Will be a debt incurring card
                            justSayNoUsedAgainstDebt.actionJustSayNoUsedByAffectedPlayer = true;//The player affected by the debt inccuring card has used a just say no to cancel the debt incurring card
                            justSayNoUsedAgainstDebt.debtAmount = playerModelForPlayerPaying.amountOwedToAnotherPlayer;//The amount the player would owe if a just say no is played against thier just say no
                            justSayNoUsedAgainstDebt.actionTypeTaken = TurnActionTypes.PlayJustSayNo;//The action type taken
                            justSayNoUsedAgainstDebt.playerAffectedByAction = playerModelForPlayerPaying.guid;//The player using a just say no to cancel the debt incurring card
                            justSayNoUsedAgainstDebt.playerWhoPerformedActionOnTurn = playerModelForPlayerToBePaid.guid;//The player who played a debt inccuring card who can play a just say not against the cancelling players just say no
                            nextState.actionCardEvent = justSayNoUsedAgainstDebt;

                            //Player has now paid debt
                            playerModelForPlayerPaying.owesAnotherPlayer = false;
                            playerModelForPlayerPaying.amountOwedToAnotherPlayer = 0;
                            //Update the state

                            //Change state on success
                            //has been performed, advance the phase of the game
                            nextState.currentPhase = nextStatePhaseIfSuccessful;
                            List<TurnActionTypes> notInDebt = new List<TurnActionTypes>();
                            List<TurnActionTypes> onTurn = new List<TurnActionTypes>();
                            List<TurnActionTypes> inDebt = new List<TurnActionTypes>();
                            onTurn = setAllowableActionsOnTurn(onTurn, nextState);
                            inDebt = setAllowableActionsNotOnTurnInDebt(inDebt, nextState);
                            updateAllowableStatesDebtPaid(nextState, notInDebt, onTurn, nextState.guidOfPlayerWhosTurnItIs, inDebt);

                            //change the current state to the next state
                            addNextState(nextState);
                            return new BoolResponseBox(true, "Player:" + playerPerformingAction.name + " Has used a Just Say No to avoid paying.");
                        }
                    }
                }

                #endregion Just Say No used against debt incurring card
            }

            #endregion Player not on turn canceling effect of ActionCard being played against them

            #region Player is playing a Just Say No to Cancel the Effect of another ActionCard(Excluding Just Say No) against them

            else if (currentState.actionCardEvent.actionJustSayNoUsedByAffectedPlayer == true)
            {
                //The on turn player has played a just say no to cancel a just say no
                if (currentState.actionCardEvent.actionCardTypeUsed.CompareTo(ActionCardAction.DebtCollector) == 0 || currentState.actionCardEvent.actionCardTypeUsed.CompareTo(ActionCardAction.DoubleTheRent) == 0 || currentState.actionCardEvent.actionCardTypeUsed.CompareTo(ActionCardAction.ItsMyBirthday) == 0 || currentState.actionCardEvent.actionCardTypeUsed.CompareTo(ActionCardAction.RentMultiColor) == 0 || currentState.actionCardEvent.actionCardTypeUsed.CompareTo(ActionCardAction.RentStandard) == 0)
                {
                    //Perform action again
                    //Clone the current state to create next state
                    nextState = currentState.clone(generateGuidForNextState());

                    #region Debt Collector Redo

                    if (currentState.actionCardEvent.actionCardTypeUsed.CompareTo(ActionCardAction.DebtCollector) == 0)
                    {
                        //Card Played was a debt collector card
                        replayActionCardDebtCollector(currentState, nextState, getPlayerModel(moveInformation.guidOfPlayerToPayDebtTo, currentState), nextStatePhase, moveInformation);
                    }

                    #endregion Debt Collector Redo
                }
            }

            #endregion Player is playing a Just Say No to Cancel the Effect of another ActionCard(Excluding Just Say No) against them

            else
            {
                //RollBack neccessary
            }
            return new BoolResponseBox(false, "Not able to use a just say no card at this time");
        }

        /// <summary>
        /// Allows a Player to play an It's My Birthday Card on their turn
        /// </summary>
        /// <param name="currentState"></param>
        /// <param name="nextState"></param>
        /// <param name="playerPerformingAction"></param>
        /// <param name="nextStatePhase"></param>
        /// <param name="moveInformation"></param>
        /// <returns></returns>
        private BoolResponseBox playActionCardItsMyBirthday(PlayFieldModel currentState, PlayFieldModel nextState, PlayerModel playerPerformingAction, Statephase nextStatePhase, MoveInfo moveInformation)
        {
            //Check
            //Perform action in next state
            //Clone the current state to create next state then draws 5 cards in the next state
            nextState = currentState.clone(generateGuidForNextState());
            PlayerModel playerModelForPlayer = getPlayerModel(playerPerformingAction.guid, nextState);

            Card cardInHandToBePlayed = nextState.deck.getCardByID(moveInformation.idOfCardBeingUsed);
            //Get the reference to the players playerModel in the current PlayFieldModel

            //Get the reference to the Card in the current PlayFieldModel
            if (cardInHandToBePlayed != null && cardInHandToBePlayed is ActionCard && ((ActionCard)cardInHandToBePlayed).actionType.CompareTo(ActionCardAction.ItsMyBirthday) == 0)
            {
                Card card = removeCardFromHand(cardInHandToBePlayed, playerModelForPlayer);
                if (card != null)
                {
                    ActionCard actionCard = card as ActionCard;
                    //Do action

                    foreach (PlayerModel player in nextState.playerModels)
                    {
                        if (player.guid.CompareTo(playerModelForPlayer.guid) != 0)
                        {
                            player.owesAnotherPlayer = true;
                            player.amountOwedToAnotherPlayer = ActionCard.Its_My_Birthday_Value;
                        }
                    }

                    //Change state on success
                    //has been performed, advance the phase of the game
                    nextState.currentPhase = nextStatePhase;
                    //Put card in discard pile
                    nextState.playpile.playCardOnPile(actionCard);
                    List<TurnActionTypes> notOnTurn = new List<TurnActionTypes>();
                    List<TurnActionTypes> onTurn = new List<TurnActionTypes>();
                    onTurn = setAllowableActionsOnTurn(onTurn, nextState);
                    updateAllowableStatesDebtPaid(nextState, notOnTurn, onTurn, nextState.guidOfPlayerWhosTurnItIs, setAllowableActionsNotOnTurnInDebt(new List<TurnActionTypes>(), nextState));
                    //change the current state to the next state
                    addNextState(nextState);
                    return new BoolResponseBox(true, "Player:" + playerPerformingAction.name + " Has used a It's My Birthday");
                }

                return new BoolResponseBox(false, "Card is not in hand.");
            }
            return new BoolResponseBox(false, "Card is not in hand or is not a It's my birthday card");
        }

        /// <summary>
        /// Determines if a Player has enough cards to pay Debt
        /// </summary>
        /// <param name="player"></param>
        /// <param name="amountOwed"></param>
        /// <returns></returns>
        private bool hasPlayerEnoughCardsToPayFullValue(PlayerModel player, int amountOwed)
        {
            int playersTotalValue = 0;
            //Player has insufficent cards selected to pay debt
            //Find players net worth to see if they have payed everthing they have
            foreach (Card c in player.bank.cardsInBank)
            {
                playersTotalValue += c.cardValue;
            }
            foreach (PropertyCardSet ps in player.propertySets.playersPropertySets)
            {
                foreach (Card c in ps.properties)
                {
                    playersTotalValue += c.cardValue;
                }
                if (ps.hasHouse)
                {
                    playersTotalValue += ps.house.cardValue;
                    if (ps.hasHotel)
                    {
                        playersTotalValue += ps.hotel.cardValue;
                    }
                }
            }
            if (playersTotalValue >= amountOwed)
            {
                //Player has enough cards to pay
                return true;
            }
            else
            {
                //Player does not have enough cards to pay
                return false;
            }
        }

        /// <summary>
        /// If a Player is unable to pay a debt as they do not have cards at least equal to the debt this method takes all the players cards.
        /// </summary>
        /// <param name="player">Player to take Cards from</param>
        /// <returns>List of Cards taken</returns>
        private List<Card> takeAllCardsFromPlayer(PlayerModel player)
        {
            List<Card> cardsTaken = new List<Card>();
            //Find players net worth to see if they have payed everthing they have
            foreach (Card c in player.bank.cardsInBank)
            {
                Card card = player.bank.removeCardFromBank(c);
                if (card != null)
                {
                    cardsTaken.Add(card);
                }
            }
            foreach (PropertyCardSet ps in player.propertySets.playersPropertySets)
            {
                foreach (Card c in ps.properties)
                {
                    cardsTaken.Add(c);
                }
                if (ps.hasHouse)
                {
                    cardsTaken.Add(ps.removeHouse());
                    if (ps.hasHotel)
                    {
                        cardsTaken.Add(ps.removeHotel());
                    }
                }
                //player property set now empty remove the set from the player
                player.propertySets.removeEmptySet(ps);
            }
            return cardsTaken;
        }

        /// <summary>
        /// Allows a player to pay thier debt owing from rent, birthday or debt collector cards
        /// </summary>
        /// <param name="currentState"></param>
        /// <param name="nextState"></param>
        /// <param name="playerPerformingAction"></param>
        /// <param name="moveInformation"></param>
        /// <param name="justSayNoAble"></param>
        /// <param name="notJustSayNoAble"></param>
        /// <returns></returns>
        private BoolResponseBox payDebt(PlayFieldModel currentState, PlayFieldModel nextState, PlayerModel playerPerformingAction, MoveInfo moveInformation, Statephase justSayNoAble, Statephase notJustSayNoAble)
        {
            //Check
            //Perform action in next state
            //Clone the current state to create next state then draws 5 cards in the next state
            nextState = currentState.clone(generateGuidForNextState());

            //Player Paying Debt
            PlayerModel playerModelForPlayerPaying = getPlayerModel(playerPerformingAction.guid, nextState);

            //Player Being paid debt
            PlayerModel playerModelForPlayerToBePaid = getPlayerModel(moveInformation.guidOfPlayerToPayDebtTo, nextState);

            //Get all the cards the player intends to pay with
            List<Card> cardsToUseToPayDebt = new List<Card>();
            foreach (int i in moveInformation.listOfIDsOfCardsBeingUsedToPayDebt)
            {
                Card card = nextState.deck.getCardByID(i);
                if (card is PropertyCard)
                {
                    PropertyCard property = (PropertyCard)card;
                    if (playerModelForPlayerPaying.removePropertyCardFromPlayersPropertySets(property) != null)
                    {
                        //Property was in set and has been removed
                        cardsToUseToPayDebt.Add(property);
                    }
                }
                else if (playerModelForPlayerPaying.bank.removeCardFromBank(card) != null)
                {
                    //not a propertyCard
                    cardsToUseToPayDebt.Add(card);
                }
            }
            bool otherPlayersPaying = false;
            //Get if player paying is the only player who has to pay
            foreach (PlayerModel player in nextState.playerModels)
            {
                if (player.guid.CompareTo(playerModelForPlayerPaying.guid) != 0 && player.owesAnotherPlayer)
                {
                    //Another player owes money
                    otherPlayersPaying = true;
                    break;
                }
            }

            //Determine if payment is sufficent
            //Value of all cards must be at least the same as the debt owed or must be every property, house, hotel, banked card the player has
            int valueOfPayment = 0;
            foreach (Card c in cardsToUseToPayDebt)
            {
                valueOfPayment += c.cardValue;
            }
            if (valueOfPayment < moveInformation.amountOwed)
            {
                if (hasPlayerEnoughCardsToPayFullValue(playerModelForPlayerPaying, moveInformation.amountOwed))
                {
                    return new BoolResponseBox(false, "Player has enough played cards to pay debt but has not selected enough to pay.");
                }
                else
                {
                    //Player does not have enough to pay. Take all players played cards
                    cardsToUseToPayDebt = takeAllCardsFromPlayer(playerModelForPlayerPaying);
                }
            }
            //put properties used as payment into new sets for player being paid and put money and actioncards into player being paids bank
            foreach (Card card in cardsToUseToPayDebt)
            {
                if (card is PropertyCard)
                {
                    playerModelForPlayerPaying.removePropertyCardFromPlayersPropertySets((PropertyCard)card);
                    playerModelForPlayerToBePaid.propertySets.addSet(new PropertyCardSet((PropertyCard)card));
                }
                else
                {
                    removeCardFromHand(card, playerModelForPlayerPaying);
                    playerModelForPlayerToBePaid.bank.addCardToBank(card);
                }
            }
            //Player has now paid debt
            playerModelForPlayerPaying.owesAnotherPlayer = false;
            playerModelForPlayerPaying.amountOwedToAnotherPlayer = 0;
            //Update the state

            //Change state on success
            //has been performed, advance the phase of the game
            Statephase nextStatePhaseIfSuccessful;
            if (otherPlayersPaying)
            {
                //Other players must pay so states phase does not change
                nextStatePhaseIfSuccessful = nextState.currentPhase;
            }
            else
            {
                //No other players have to pay debt
                nextStatePhaseIfSuccessful = notJustSayNoAble;
            }
            nextState.currentPhase = nextStatePhaseIfSuccessful;
            if (nextStatePhaseIfSuccessful.CompareTo(Statephase.Turn_Started_Cards_Drawn_1_Cards_Played) == 0 || nextStatePhaseIfSuccessful.CompareTo(Statephase.Turn_Started_Cards_Drawn_2_Cards_Played) == 0 || nextStatePhaseIfSuccessful.CompareTo(Statephase.Turn_Started_Cards_Drawn_3_Cards_Played_Swap_Properties_Or_End_Turn_Only) == 0)
            {
                List<TurnActionTypes> notOnTurn = new List<TurnActionTypes>();
                List<TurnActionTypes> onTurn = new List<TurnActionTypes>();
                onTurn = setAllowableActionsOnTurn(onTurn, nextState);

                updateAllowableStates(nextState, notOnTurn, onTurn, nextState.guidOfPlayerWhosTurnItIs, nextState.guidOfPlayerWhosTurnItIs);
            }
            else
            {
                List<TurnActionTypes> notInDebt = new List<TurnActionTypes>();
                List<TurnActionTypes> onTurn = new List<TurnActionTypes>();
                List<TurnActionTypes> inDebt = new List<TurnActionTypes>();
                onTurn = setAllowableActionsOnTurn(onTurn, nextState);
                inDebt = setAllowableActionsNotOnTurnInDebt(inDebt, nextState);

                updateAllowableStatesDebtPaid(nextState, notInDebt, onTurn, nextState.guidOfPlayerWhosTurnItIs, inDebt);
            }
            //change the current state to the next state
            addNextState(nextState);
            return new BoolResponseBox(true, "Player:" + playerPerformingAction.name + " Has paid");
        }

        /// <summary>
        /// Allows a player on thier turn to Play A Debt Collector Action Card
        /// </summary>
        /// <param name="currentState"></param>
        /// <param name="nextState"></param>
        /// <param name="playerPerformingAction"></param>
        /// <param name="nextStatePhaseIfSuccessful"></param>
        /// <param name="debtCollectorInfo"></param>
        /// <returns></returns>
        private BoolResponseBox playActionCardDebtCollector(PlayFieldModel currentState, PlayFieldModel nextState, PlayerModel playerPerformingAction, Statephase nextStatePhaseIfSuccessful, MoveInfo debtCollectorInfo)
        {
            //Check
            //Perform action in next state
            //Clone the current state to create next state then draws 5 cards in the next state
            nextState = currentState.clone(generateGuidForNextState());
            PlayerModel playerModelForPlayer = getPlayerModel(playerPerformingAction.guid, nextState);
            PlayerModel playerModelForPlayerToDebtCollect = getPlayerModel(debtCollectorInfo.guidOfPlayerBeingDebtCollected, nextState);
            Card cardInHandToBePlayed = nextState.deck.getCardByID(debtCollectorInfo.idOfCardBeingUsed);
            //Get the reference to the players playerModel in the current PlayFieldModel

            //Get the reference to the Card in the current PlayFieldModel
            if (cardInHandToBePlayed != null && cardInHandToBePlayed is ActionCard && ((ActionCard)cardInHandToBePlayed).actionType.CompareTo(ActionCardAction.DebtCollector) == 0)
            {
                Card card = removeCardFromHand(cardInHandToBePlayed, playerModelForPlayer);
                if (card != null)
                {
                    ActionCard actionCard = card as ActionCard;
                    //Do action
                    playerModelForPlayerToDebtCollect.owesAnotherPlayer = true;
                    playerModelForPlayerToDebtCollect.amountOwedToAnotherPlayer = ActionCard.Debt_Collector_Value;

                    //Change state on success
                    //has been performed, advance the phase of the game
                    nextState.currentPhase = nextStatePhaseIfSuccessful;
                    //Put card in discard pile
                    nextState.playpile.playCardOnPile(actionCard);
                    List<TurnActionTypes> notOnTurn = new List<TurnActionTypes>();
                    List<TurnActionTypes> onTurn = new List<TurnActionTypes>();
                    onTurn = setAllowableActionsOnTurn(onTurn, nextState);
                    updateAllowableStatesDebtPaid(nextState, notOnTurn, onTurn, nextState.guidOfPlayerWhosTurnItIs, setAllowableActionsNotOnTurnInDebt(new List<TurnActionTypes>(), nextState));

                    //change the current state to the next state
                    addNextState(nextState);
                    return new BoolResponseBox(true, "Player:" + playerPerformingAction.name + " Has used a Debt Collector");
                }

                return new BoolResponseBox(false, "Card is not in hand.");
            }
            return new BoolResponseBox(false, "Card is not in hand or is not a Debt Collector card");
        }

        /// <summary>
        /// Plays a debt collector cards effect again after it being canceled then uncanceled by Just Say No Cards
        /// </summary>
        /// <param name="currentState"></param>
        /// <param name="nextState"></param>
        /// <param name="playerPerformingAction"></param>
        /// <param name="nextStatePhaseIfSuccessful"></param>
        /// <param name="debtCollectorInfo"></param>
        /// <returns></returns>
        private BoolResponseBox replayActionCardDebtCollector(PlayFieldModel currentState, PlayFieldModel nextState, PlayerModel playerPerformingAction, Statephase nextStatePhaseIfSuccessful, MoveInfo debtCollectorInfo)
        {
            //Check
            //Perform action in next state
            //Clone the current state to create next state then draws 5 cards in the next state
            nextState = currentState.clone(generateGuidForNextState());
            PlayerModel playerModelForPlayer = getPlayerModel(playerPerformingAction.guid, nextState);
            PlayerModel playerModelForPlayerToDebtCollect = getPlayerModel(debtCollectorInfo.guidOfPlayerBeingDebtCollected, nextState);
            //Get the reference to the players playerModel in the current PlayFieldModel

            //Get the reference to the Card in the current PlayFieldModel
            //Do action
            playerModelForPlayerToDebtCollect.owesAnotherPlayer = true;
            playerModelForPlayerToDebtCollect.amountOwedToAnotherPlayer = ActionCard.Debt_Collector_Value;

            //Change state on success
            //has been performed, advance the phase of the game
            nextState.currentPhase = nextStatePhaseIfSuccessful;

            List<TurnActionTypes> notOnTurn = new List<TurnActionTypes>();
            List<TurnActionTypes> onTurn = new List<TurnActionTypes>();
            onTurn = setAllowableActionsOnTurn(onTurn, nextState);
            updateAllowableStatesDebtPaid(nextState, notOnTurn, onTurn, nextState.guidOfPlayerWhosTurnItIs, setAllowableActionsNotOnTurnInDebt(new List<TurnActionTypes>(), nextState));

            //change the current state to the next state
            addNextState(nextState);
            return new BoolResponseBox(true, "Debt Collector replayed after Just Say No");
        }

        /// <summary>
        /// Allows a Player to discard one card from their Hand when they have finished thier turn with more than the max allowable cards in hand
        /// </summary>
        /// <param name="currentState"></param>
        /// <param name="nextState"></param>
        /// <param name="playerPerformingAction"></param>
        /// <param name="moveInformation"></param>
        /// <param name="notJustSayNoAble"></param>
        /// <returns></returns>
        private BoolResponseBox discard1Card(PlayFieldModel currentState, PlayFieldModel nextState, PlayerModel playerPerformingAction, MoveInfo moveInformation, Statephase notJustSayNoAble)
        {
            //Clone the current state to create next state then draws two cards in the next state
            nextState = currentState.clone(generateGuidForNextState());
            Card cardInHandToBeDiscarded = nextState.deck.getCardByID(moveInformation.idOfCardBeingUsed);
            //Get the reference to the players playerModel in the current PlayFieldModel
            PlayerModel playerWhoIsDiscardingCard = getPlayerModel(playerPerformingAction.guid, nextState);
            //Get the reference to the Card in the current PlayFieldModel

            Card card = removeCardFromHand(cardInHandToBeDiscarded, playerWhoIsDiscardingCard);
            if (card != null)
            {
                nextState.drawPile.discardCard(card);

                //action is valid for player at this time
                List<TurnActionTypes> notOnTurn = new List<TurnActionTypes>();
                List<TurnActionTypes> onTurn = new List<TurnActionTypes>();
                //Could perform the action here instead, for now just change the phase of the state
                //Not an action so cant be just say no'd
                //Change phase
                switch (playerWhoIsDiscardingCard.hand.cardsInHand.Count)
                {
                    case 8:
                        {
                            nextState.currentPhase = Statephase.Turn_Ended_8_Cards_In_Hand_Discard_1_Card;
                            onTurn.Add(TurnActionTypes.Discard_1_Card);
                            //Update the moves for players
                            updateAllowableStates(nextState, notOnTurn, onTurn, nextState.guidOfPlayerWhosTurnItIs, nextState.guidOfPlayerWhosTurnItIs);
                            //change the current state to the next state
                            addNextState(nextState);
                            return new BoolResponseBox(true, "Player:" + playerPerformingAction.name + " Ended their turn with 8 cards and must discard 1 card");
                        }
                    case 9:
                        {
                            nextState.currentPhase = Statephase.Turn_Ended_9_Cards_In_Hand_Discard_2_Cards;
                            onTurn.Add(TurnActionTypes.Discard_1_Card);
                            //Update the moves for players
                            updateAllowableStates(nextState, notOnTurn, onTurn, nextState.guidOfPlayerWhosTurnItIs, nextState.guidOfPlayerWhosTurnItIs);
                            //change the current state to the next state
                            addNextState(nextState);
                            return new BoolResponseBox(true, "Player:" + playerPerformingAction.name + " Ended their turn with 9 cards and must discard 2 cards");
                        }
                    case 10:
                        {
                            nextState.currentPhase = Statephase.Turn_Ended_10_Cards_In_Hand_Discard_3_Cards;
                            onTurn.Add(TurnActionTypes.Discard_1_Card);
                            //Update the moves for players
                            updateAllowableStates(nextState, notOnTurn, onTurn, nextState.guidOfPlayerWhosTurnItIs, nextState.guidOfPlayerWhosTurnItIs);
                            //change the current state to the next state
                            addNextState(nextState);
                            return new BoolResponseBox(true, "Player:" + playerPerformingAction.name + " Ended their turn with 10 cards and must discard 3 cards");
                        }
                    case 11:
                        {
                            nextState.currentPhase = Statephase.Turn_Ended_11_Cards_In_Hand_Discard_4_Cards;
                            onTurn.Add(TurnActionTypes.Discard_1_Card);
                            //Update the moves for players
                            updateAllowableStates(nextState, notOnTurn, onTurn, nextState.guidOfPlayerWhosTurnItIs, nextState.guidOfPlayerWhosTurnItIs);
                            //change the current state to the next state
                            addNextState(nextState);
                            return new BoolResponseBox(true, "Player:" + playerPerformingAction.name + " Ended their turn with 11 cards and must discard 4 cards");
                        }
                    case 12:
                        {
                            nextState.currentPhase = Statephase.Turn_Ended_12_Cards_In_Hand_Discard_5_Cards;
                            onTurn.Add(TurnActionTypes.Discard_1_Card);
                            //Update the moves for players
                            updateAllowableStates(nextState, notOnTurn, onTurn, nextState.guidOfPlayerWhosTurnItIs, nextState.guidOfPlayerWhosTurnItIs);
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
                                updateAllowableStates(nextState, notOnTurn, onTurn, nextState.guidOfPlayerWhosTurnItIs, nextState.guidOfPlayerWhosTurnItIs);
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
                                updateAllowableStates(nextState, notOnTurn, onTurn, nextState.guidOfPlayerWhosTurnItIs, nextState.guidOfPlayerWhosTurnItIs);
                                //change the current state to the next state
                                addNextState(nextState);
                                return new BoolResponseBox(true, "Player:" + playerPerformingAction.name + " Ended their turn." + "Player:" + getPlayerModel(nextState.guidOfPlayerWhosTurnItIs, nextState).name + "\'s Turn. Draw 2 Cards.");
                            }
                        }
                }
            }
            else
            {
                //Card not in players hand, can't be discarded
                return new BoolResponseBox(false, "Card not in hand");
            }
        }

        /// <summary>
        /// Plays a Card from a Players Hand on their turn to their Bank
        /// </summary>
        /// <param name="currentState"></param>
        /// <param name="nextState"></param>
        /// <param name="playerPerformingAction"></param>
        /// <param name="moveInformation"></param>
        /// <param name="notJustSayNoAble"></param>
        /// <returns></returns>
        private BoolResponseBox playCardFromHandToBank(PlayFieldModel currentState, PlayFieldModel nextState, PlayerModel playerPerformingAction, MoveInfo moveInformation, Statephase notJustSayNoAble)
        {
            //Check
            //Perform action in next state
            //Clone the current state to create next state
            nextState = currentState.clone(generateGuidForNextState());
            PlayerModel playerModelForPlayer = getPlayerModel(playerPerformingAction.guid, nextState);

            Card cardInHandToBePlayed = currentState.deck.getCardByID(moveInformation.idOfCardBeingUsed);
            //Get the reference to the players playerModel in the current PlayFieldModel

            PlayerModel player = getPlayerModel(moveInformation.playerMakingMove, nextState);
            //Get the reference to the Card in the current PlayFieldModel
            if (cardInHandToBePlayed != null)
            {
                Card card = removeCardFromHand(cardInHandToBePlayed, player);
                if (card != null)
                {
                    player.bank.addCardToBank(card);
                    //Change state on success
                    //has been performed, advance the phase of the game
                    nextState.currentPhase = notJustSayNoAble;
                    List<TurnActionTypes> notOnTurn = new List<TurnActionTypes>();
                    List<TurnActionTypes> onTurn = new List<TurnActionTypes>();
                    onTurn = setAllowableActionsOnTurn(onTurn, nextState);

                    updateAllowableStates(nextState, notOnTurn, onTurn, nextState.guidOfPlayerWhosTurnItIs, nextState.guidOfPlayerWhosTurnItIs);
                    //change the current state to the next state
                    addNextState(nextState);
                    return new BoolResponseBox(true, "Player:" + playerPerformingAction.name + " Has banked " + card.cardName);
                }
                return new BoolResponseBox(false, "Card is not in hand.");
            }
            return new BoolResponseBox(false, "Card id does not exist");
        }

        /// <summary>
        /// Plays a Property Card from a Players Hand on their turn to a new Property Set
        /// </summary>
        /// <param name="currentState"></param>
        /// <param name="nextState"></param>
        /// <param name="playerPerformingAction"></param>
        /// <param name="moveInformation"></param>
        /// <param name="notJustSayNoAble"></param>
        /// <returns></returns>
        private BoolResponseBox playPropertyCardFromHandToNewSet(PlayFieldModel currentState, PlayFieldModel nextState, PlayerModel playerPerformingAction, MoveInfo moveInformation, Statephase notJustSayNoAble)
        {
            //Clone the current state to create next state
            nextState = currentState.clone(generateGuidForNextState());
            //Get CurrentPlayFieldModelState
            Card cardInHandToBePlayed = nextState.deck.getCardByID(moveInformation.idOfCardBeingUsed);
            PlayerModel player = getPlayerModel(playerPerformingAction.guid, nextState);
            Card card = removeCardFromHand(cardInHandToBePlayed, player);
            if (card != null)
            {
                PropertyCard cP = cardInHandToBePlayed as PropertyCard;
                cP.setPropertyColor(moveInformation.isPropertyToPlayOrientedUp);
                PropertyCardSet ps = new PropertyCardSet(cP);

                player.propertySets.addSet(ps);
                //Change state on success
                //has been performed, advance the phase of the game
                nextState.currentPhase = notJustSayNoAble;
                List<TurnActionTypes> notOnTurn = new List<TurnActionTypes>();
                List<TurnActionTypes> onTurn = new List<TurnActionTypes>();
                onTurn = setAllowableActionsOnTurn(onTurn, nextState);

                updateAllowableStates(nextState, notOnTurn, onTurn, nextState.guidOfPlayerWhosTurnItIs, nextState.guidOfPlayerWhosTurnItIs);
                //change the current state to the next state
                addNextState(nextState);
                return new BoolResponseBox(true, "Player:" + playerPerformingAction.name + " Has played to a new set " + card.cardName);
            }
            return new BoolResponseBox(false, "Card is not in hand.");
        }

        /// <summary>
        /// Plays a Property Card from a Players Hand on their turn to an existing Property Set
        /// </summary>
        /// <param name="currentState"></param>
        /// <param name="nextState"></param>
        /// <param name="playerPerformingAction"></param>
        /// <param name="moveInformation"></param>
        /// <param name="notJustSayNoAble"></param>
        /// <returns></returns>
        private BoolResponseBox playPropertyCardFromHand(PlayFieldModel currentState, PlayFieldModel nextState, PlayerModel playerPerformingAction, MoveInfo moveInformation, Statephase notJustSayNoAble)
        {
            if (moveInformation.guidOfExistingSetToPlayPropertyTo.CompareTo(new Guid()) != 0)
            {
                //Clone the current state to create next state
                nextState = currentState.clone(generateGuidForNextState());
                PropertyColor oldColour = ((PropertyCard)currentState.deck.getCardByID(moveInformation.idOfCardBeingUsed)).getPropertyColor();
                bool oldOrientation = ((PropertyCard)currentState.deck.getCardByID(moveInformation.idOfCardBeingUsed)).isCardUp;
                ((PropertyCard)currentState.deck.getCardByID(moveInformation.idOfCardBeingUsed)).setPropertyColor(moveInformation.isPropertyToPlayOrientedUp);
                if ((getPropertyCardSet(getPlayerModel(playerPerformingAction.guid, nextState).propertySets, moveInformation.guidOfExistingSetToPlayPropertyTo)).propertySetColor.CompareTo(((PropertyCard)nextState.deck.getCardByID(moveInformation.idOfCardBeingUsed)).currentPropertyColor) == 0)
                {
                    //Get CurrentPlayFieldModelState
                    Card cardInHandToBePlayed = nextState.deck.getCardByID(moveInformation.idOfCardBeingUsed);
                    PlayerModel player = getPlayerModel(playerPerformingAction.guid, nextState);
                    Card card = removeCardFromHand(cardInHandToBePlayed, player);
                    if (card != null)
                    {
                        PropertyCard cP = cardInHandToBePlayed as PropertyCard;
                        cP.setPropertyColor(moveInformation.isPropertyToPlayOrientedUp);
                        PropertyCardSet ps = getPropertyCardSet(player.propertySets, moveInformation.guidOfExistingSetToPlayPropertyTo);
                        ps.addProperty(cP);
                        //Change state on success
                        //has been performed, advance the phase of the game
                        nextState.currentPhase = notJustSayNoAble;
                        List<TurnActionTypes> notOnTurn = new List<TurnActionTypes>();
                        List<TurnActionTypes> onTurn = new List<TurnActionTypes>();
                        onTurn = setAllowableActionsOnTurn(onTurn, nextState);

                        updateAllowableStates(nextState, notOnTurn, onTurn, nextState.guidOfPlayerWhosTurnItIs, nextState.guidOfPlayerWhosTurnItIs);
                        //change the current state to the next state
                        addNextState(nextState);
                        return new BoolResponseBox(true, "Player:" + playerPerformingAction.name + " Has played to a existing set " + card.cardName);
                    }
                    return new BoolResponseBox(false, "Card is not in hand.");
                }
                else
                {
                    nextState = null;
                    ((PropertyCard)currentState.deck.getCardByID(moveInformation.idOfCardBeingUsed)).setPropertyColor(oldOrientation);//change back the orientation of the property
                    return new BoolResponseBox(false, "Property Card is not the correct colour to be added to this set");
                }
            }
            else
            {
                return playPropertyCardFromHandToNewSet(currentState, nextState, playerPerformingAction, moveInformation, notJustSayNoAble);
            }
        }

        /// <summary>
        /// Gets a Property Set given a list of a Players PropertySet's and the Guid of the set
        /// </summary>
        /// <param name="pps"></param>
        /// <param name="guidOfSet"></param>
        /// <returns></returns>
        private PropertyCardSet getPropertyCardSet(PlayerPropertySets pps, Guid guidOfSet)
        {
            foreach (PropertyCardSet ps in pps.playersPropertySets)
            {
                if (ps.guid.CompareTo(guidOfSet) == 0)
                {
                    return ps;
                }
            }
            return null;
        }

        /// <summary>
        /// Moves a property card that has been played to a set to a new set
        /// </summary>
        /// <param name="currentState"></param>
        /// <param name="nextState"></param>
        /// <param name="playerPerformingAction"></param>
        /// <param name="moveInformation"></param>
        /// <param name="rearrangeProperties"></param>
        /// <returns></returns>
        private BoolResponseBox movePropertyCard(PlayFieldModel currentState, PlayFieldModel nextState, PlayerModel playerPerformingAction, MoveInfo moveInformation, Statephase rearrangeProperties)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Determines if a move is valid for the current player and state
        /// </summary>
        /// <param name="turnActionToDo"></param>
        /// <param name="player"></param>
        /// <param name="currentState"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Adds a state to the list of states in the game and sets it as the current state.
        /// Can be modified to set the state to be the current state only once all players have recieved the current state.
        /// </summary>
        /// <param name="nextPlayfieldModel"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Determines what state the game is currently in and sets the appropriate possible next states for the move being played and calls doAppropriateAction to play the move
        /// </summary>
        /// <param name="lastState"></param>
        /// <param name="currentState"></param>
        /// <param name="nextState"></param>
        /// <param name="playerPerformingAction"></param>
        /// <param name="typeOfActionToPerform"></param>
        /// <param name="cardsAndPlayersInvolved"></param>
        /// <returns></returns>
        public BoolResponseBox evaluateMove(PlayFieldModel lastState, PlayFieldModel currentState, PlayFieldModel nextState, PlayerModel playerPerformingAction, TurnActionTypes typeOfActionToPerform, MoveInfo cardsAndPlayersInvolved)
        {
            BoolResponseBox result;

            #region Draw2State

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
                Statephase discard = Statephase.Turn_Started_Draw_2_Cards;
                BoolResponseBox isMoveTypeAllowableAtCurrentPhase = checkIfMoveAllowedAtThisState(typeOfActionToPerform, playerPerformingAction, currentState);
                if (isMoveTypeAllowableAtCurrentPhase.success)
                {
                    //type of move is allowable at this point for this player. Check if move is doable ex is a set stealable
                    result = doAppropriateAction(lastState, currentState, nextState, playerPerformingAction, typeOfActionToPerform, justSayNoAble, notJustSayNoAble, rearrangeProperties, drawCardsAtTurnStart, JustSayNoUsedByOpposition, cardsAndPlayersInvolved, discard);

                    return result;//True if action performed, False if not
                }
                else
                {
                    //No other actionTypes are allowable in this state
                    return new BoolResponseBox(false, "Player:" + playerPerformingAction.name + " is not able to perform " + typeOfActionToPerform + " at this state:" + currentState.currentPhase.ToString());
                }
            }

            #endregion Draw2State

            #region Draw5State

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
                Statephase discard = Statephase.Turn_Started_Draw_5_Cards;
                BoolResponseBox isMoveTypeAllowableAtCurrentPhase = checkIfMoveAllowedAtThisState(typeOfActionToPerform, playerPerformingAction, currentState);
                if (isMoveTypeAllowableAtCurrentPhase.success)
                {
                    //type of move is allowable at this point for this player. Check if move is doable ex is a set stealable
                    result = doAppropriateAction(lastState, currentState, nextState, playerPerformingAction, typeOfActionToPerform, justSayNoAble, notJustSayNoAble, rearrangeProperties, drawCardsAtTurnStart, JustSayNoUsedByOpposition, cardsAndPlayersInvolved, discard);

                    return result;//True if action performed, False if not
                }
                else
                {
                    //No other actionTypes are allowable in this state
                    return new BoolResponseBox(false, "Player:" + playerPerformingAction.name + " is not able to perform " + typeOfActionToPerform + " at this state:" + currentState.currentPhase.ToString());
                }
            }

            #endregion Draw5State

            #region Turn Started Cards Drawn 0 Cards Played

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
                Statephase discard = Statephase.Turn_Started_Cards_Drawn_0_Cards_Played;
                Statephase drawCardsAtTurnStart = Statephase.Turn_Started_Cards_Drawn_0_Cards_Played;//Only action allowable at this phase
                BoolResponseBox isMoveTypeAllowableAtCurrentPhase = checkIfMoveAllowedAtThisState(typeOfActionToPerform, playerPerformingAction, currentState);
                if (isMoveTypeAllowableAtCurrentPhase.success)
                {
                    //type of move is allowable at this point for this player. Check if move is doable ex is a set stealable
                    result = doAppropriateAction(lastState, currentState, nextState, playerPerformingAction, typeOfActionToPerform, justSayNoAble, notJustSayNoAble, rearrangeProperties, drawCardsAtTurnStart, JustSayNoUsedByOpposition, cardsAndPlayersInvolved, discard);

                    return result;//True if action performed, False if not
                }
                else
                {
                    //No other actionTypes are allowable in this state
                    return new BoolResponseBox(false, "Player:" + playerPerformingAction.name + " is not able to perform " + typeOfActionToPerform + " at this state:" + currentState.currentPhase.ToString());
                }
            }

            #endregion Turn Started Cards Drawn 0 Cards Played

            #region 0 Cards Played Just Say Noable Card Played

            else if (currentState.currentPhase.CompareTo(Statephase.Turn_Started_Cards_Drawn_0_Cards_Played_Ask_Just_Say_No) == 0)
            {
                Statephase discard = Statephase.Invalid_Action_For_Turn;

                //Player used Just Say No
                Statephase JustSayNoUsedByOpposition = Statephase.Turn_Started_Cards_Drawn_0_Cards_Played_Just_Say_No_Used_By_Oppostion_Ask_Player_On_Turn_Just_Say_No;
                /*
                //Player did not play just say no and is dealbreakered or sly dealed or forced dealed
                Statephase JustSayNoNotUsed = Statephase.Turn_Started_Cards_Drawn_1_Cards_Played;

                //Player has to pay rent or Birthday or Debt Collector
                Statephase PayDebt = Statephase.Turn_Started_Cards_Drawn_1_Cards_Play
                */
                //Action card that can be just say no carded next state
                Statephase justSayNoAble = Statephase.Turn_Started_Cards_Drawn_0_Cards_Played_Ask_Just_Say_No;//Action cards not playable at this phase
                //Any move that plays a card excluding justSayNo move next state
                Statephase notJustSayNoAble = Statephase.Turn_Started_Cards_Drawn_1_Cards_Played;//cards playable at this phase

                //Property card rearranging next state
                Statephase rearrangeProperties = Statephase.Invalid_Action_For_Turn;//cards can be rearranged at this phase
                //Draw
                Statephase drawCardsAtTurnStart = Statephase.Invalid_Action_For_Turn;//not allowable at this phase
                BoolResponseBox isMoveTypeAllowableAtCurrentPhase = checkIfMoveAllowedAtThisState(typeOfActionToPerform, playerPerformingAction, currentState);
                //In this state players may be required to pay rent, birthday or debt collector money or use a just say no
                if (isMoveTypeAllowableAtCurrentPhase.success)
                {
                    //type of move is allowable at this point for this player. Check if move is doable ex is a set stealable
                    result = doAppropriateAction(lastState, currentState, nextState, playerPerformingAction, typeOfActionToPerform, justSayNoAble, notJustSayNoAble, rearrangeProperties, drawCardsAtTurnStart, JustSayNoUsedByOpposition, cardsAndPlayersInvolved, discard);

                    return result;//True if action performed, False if not
                }
                else
                {
                    //No other actionTypes are allowable in this state
                    return new BoolResponseBox(false, "Player:" + playerPerformingAction.name + " is not able to perform " + typeOfActionToPerform + " at this state:" + currentState.currentPhase.ToString());
                }
            }

            #endregion 0 Cards Played Just Say Noable Card Played

            #region 0 Cards Played Just Say Noable Card Just Say No'd

            else if (currentState.currentPhase.CompareTo(Statephase.Turn_Started_Cards_Drawn_0_Cards_Played_Just_Say_No_Used_By_Oppostion_Ask_Player_On_Turn_Just_Say_No) == 0)
            {
                Statephase discard = Statephase.Invalid_Action_For_Turn;

                //Player used Just Say No
                Statephase JustSayNoUsedByOpposition = Statephase.Invalid_Action_For_Turn;
                /*
                //Player did not play just say no and is dealbreakered or sly dealed or forced dealed
                Statephase JustSayNoNotUsed = Statephase.Turn_Started_Cards_Drawn_1_Cards_Played;

                //Player has to pay rent or Birthday or Debt Collector
                Statephase PayDebt = Statephase.Turn_Started_Cards_Drawn_1_Cards_Play
                */
                //Action card that can be just say no carded next state
                Statephase justSayNoAble = Statephase.Turn_Started_Cards_Drawn_0_Cards_Played_Ask_Just_Say_No;//Action cards not playable at this phase except just say no
                //Any move that plays a card excluding justSayNo move next state
                Statephase notJustSayNoAble = Statephase.Turn_Started_Cards_Drawn_1_Cards_Played;//cards playable at this phase

                //Property card rearranging next state
                Statephase rearrangeProperties = Statephase.Invalid_Action_For_Turn;//cards can be rearranged at this phase
                //Draw
                Statephase drawCardsAtTurnStart = Statephase.Invalid_Action_For_Turn;//not allowable at this phase
                BoolResponseBox isMoveTypeAllowableAtCurrentPhase = checkIfMoveAllowedAtThisState(typeOfActionToPerform, playerPerformingAction, currentState);
                //In this state players may be required to pay rent, birthday or debt collector money or use a just say no
                if (isMoveTypeAllowableAtCurrentPhase.success)
                {
                    //type of move is allowable at this point for this player. Check if move is doable ex is a set stealable
                    result = doAppropriateAction(lastState, currentState, nextState, playerPerformingAction, typeOfActionToPerform, justSayNoAble, notJustSayNoAble, rearrangeProperties, drawCardsAtTurnStart, JustSayNoUsedByOpposition, cardsAndPlayersInvolved, discard);

                    return result;//True if action performed, False if not
                }
                else
                {
                    //No other actionTypes are allowable in this state
                    return new BoolResponseBox(false, "Player:" + playerPerformingAction.name + " is not able to perform " + typeOfActionToPerform + " at this state:" + currentState.currentPhase.ToString());
                }
            }

            #endregion 0 Cards Played Just Say Noable Card Just Say No'd

            #region 1 Card Played State

            else if (currentState.currentPhase.CompareTo(Statephase.Turn_Started_Cards_Drawn_1_Cards_Played) == 0)
            {
                //Action card that can be just say no carded next state
                Statephase justSayNoAble = Statephase.Turn_Started_Cards_Drawn_1_Cards_Played_Ask_Just_Say_No;//Action cards not playable at this phase
                //Any move that plays a card excluding justSayNo move next state
                Statephase notJustSayNoAble = Statephase.Turn_Started_Cards_Drawn_2_Cards_Played;//cards playable at this phase
                //Just Say No card played by off turn player
                Statephase JustSayNoUsedByOpposition = Statephase.Turn_Started_Cards_Drawn_1_Cards_Played_Just_Say_No_Used_By_Oppostion_Ask_Player_On_Turn_Just_Say_No;
                //Property card rearranging next state
                Statephase rearrangeProperties = Statephase.Turn_Started_Cards_Drawn_1_Cards_Played;//cards can be rearranged at this phase
                //Draw
                Statephase drawCardsAtTurnStart = Statephase.Turn_Started_Cards_Drawn_1_Cards_Played;//not allowable at this phase
                BoolResponseBox isMoveTypeAllowableAtCurrentPhase = checkIfMoveAllowedAtThisState(typeOfActionToPerform, playerPerformingAction, currentState);
                Statephase discard = Statephase.Turn_Started_Cards_Drawn_1_Cards_Played;
                if (isMoveTypeAllowableAtCurrentPhase.success)
                {
                    //type of move is allowable at this point for this player. Check if move is doable ex is a set stealable
                    result = doAppropriateAction(lastState, currentState, nextState, playerPerformingAction, typeOfActionToPerform, justSayNoAble, notJustSayNoAble, rearrangeProperties, drawCardsAtTurnStart, JustSayNoUsedByOpposition, cardsAndPlayersInvolved, discard);

                    return result;//True if action performed, False if not
                }
                else
                {
                    //No other actionTypes are allowable in this state
                    return new BoolResponseBox(false, "Player:" + playerPerformingAction.name + " is not able to perform " + typeOfActionToPerform + " at this state:" + currentState.currentPhase.ToString());
                }
            }

            #endregion 1 Card Played State

            #region 1 Card Played Just Say Noable Card Played

            else if (currentState.currentPhase.CompareTo(Statephase.Turn_Started_Cards_Drawn_1_Cards_Played_Ask_Just_Say_No) == 0)
            {
                Statephase discard = Statephase.Invalid_Action_For_Turn;

                //Player used Just Say No
                Statephase JustSayNoUsedByOpposition = Statephase.Turn_Started_Cards_Drawn_1_Cards_Played_Just_Say_No_Used_By_Oppostion_Ask_Player_On_Turn_Just_Say_No;
                /*
                //Player did not play just say no and is dealbreakered or sly dealed or forced dealed
                Statephase JustSayNoNotUsed = Statephase.Turn_Started_Cards_Drawn_1_Cards_Played;

                //Player has to pay rent or Birthday or Debt Collector
                Statephase PayDebt = Statephase.Turn_Started_Cards_Drawn_1_Cards_Play
                */
                //Action card that can be just say no carded next state
                Statephase justSayNoAble = Statephase.Turn_Started_Cards_Drawn_1_Cards_Played_Ask_Just_Say_No;//Action cards not playable at this phase
                //Any move that plays a card excluding justSayNo move next state
                Statephase notJustSayNoAble = Statephase.Turn_Started_Cards_Drawn_2_Cards_Played;//cards playable at this phase

                //Property card rearranging next state
                Statephase rearrangeProperties = Statephase.Invalid_Action_For_Turn;//cards can be rearranged at this phase
                //Draw
                Statephase drawCardsAtTurnStart = Statephase.Invalid_Action_For_Turn;//not allowable at this phase
                BoolResponseBox isMoveTypeAllowableAtCurrentPhase = checkIfMoveAllowedAtThisState(typeOfActionToPerform, playerPerformingAction, currentState);
                //In this state players may be required to pay rent, birthday or debt collector money or use a just say no
                if (isMoveTypeAllowableAtCurrentPhase.success)
                {
                    //type of move is allowable at this point for this player. Check if move is doable ex is a set stealable
                    result = doAppropriateAction(lastState, currentState, nextState, playerPerformingAction, typeOfActionToPerform, justSayNoAble, notJustSayNoAble, rearrangeProperties, drawCardsAtTurnStart, JustSayNoUsedByOpposition, cardsAndPlayersInvolved, discard);

                    return result;//True if action performed, False if not
                }
                else
                {
                    //No other actionTypes are allowable in this state
                    return new BoolResponseBox(false, "Player:" + playerPerformingAction.name + " is not able to perform " + typeOfActionToPerform + " at this state:" + currentState.currentPhase.ToString());
                }
            }

            #endregion 1 Card Played Just Say Noable Card Played

            #region 1 Card Played Just Say Noable Card Just Say No'd

            else if (currentState.currentPhase.CompareTo(Statephase.Turn_Started_Cards_Drawn_1_Cards_Played_Just_Say_No_Used_By_Oppostion_Ask_Player_On_Turn_Just_Say_No) == 0)
            {
                Statephase discard = Statephase.Invalid_Action_For_Turn;

                //Player used Just Say No
                Statephase JustSayNoUsedByOpposition = Statephase.Invalid_Action_For_Turn;
                /*
                //Player did not play just say no and is dealbreakered or sly dealed or forced dealed
                Statephase JustSayNoNotUsed = Statephase.Turn_Started_Cards_Drawn_1_Cards_Played;

                //Player has to pay rent or Birthday or Debt Collector
                Statephase PayDebt = Statephase.Turn_Started_Cards_Drawn_1_Cards_Play
                */
                //Action card that can be just say no carded next state
                Statephase justSayNoAble = Statephase.Turn_Started_Cards_Drawn_1_Cards_Played_Ask_Just_Say_No;//Action cards not playable at this phase
                //Any move that plays a card excluding justSayNo move next state
                Statephase notJustSayNoAble = Statephase.Turn_Started_Cards_Drawn_2_Cards_Played;//cards playable at this phase

                //Property card rearranging next state
                Statephase rearrangeProperties = Statephase.Invalid_Action_For_Turn;//cards can be rearranged at this phase
                //Draw
                Statephase drawCardsAtTurnStart = Statephase.Invalid_Action_For_Turn;//not allowable at this phase
                BoolResponseBox isMoveTypeAllowableAtCurrentPhase = checkIfMoveAllowedAtThisState(typeOfActionToPerform, playerPerformingAction, currentState);
                //In this state players may be required to pay rent, birthday or debt collector money or use a just say no
                if (isMoveTypeAllowableAtCurrentPhase.success)
                {
                    //type of move is allowable at this point for this player. Check if move is doable ex is a set stealable
                    result = doAppropriateAction(lastState, currentState, nextState, playerPerformingAction, typeOfActionToPerform, justSayNoAble, notJustSayNoAble, rearrangeProperties, drawCardsAtTurnStart, JustSayNoUsedByOpposition, cardsAndPlayersInvolved, discard);

                    return result;//True if action performed, False if not
                }
                else
                {
                    //No other actionTypes are allowable in this state
                    return new BoolResponseBox(false, "Player:" + playerPerformingAction.name + " is not able to perform " + typeOfActionToPerform + " at this state:" + currentState.currentPhase.ToString());
                }
            }

            #endregion 1 Card Played Just Say Noable Card Just Say No'd

            #region 2 Cards Played State

            else if (currentState.currentPhase.CompareTo(Statephase.Turn_Started_Cards_Drawn_2_Cards_Played) == 0)
            {
                //Action card that can be just say no carded next state
                Statephase justSayNoAble = Statephase.Turn_Started_Cards_Drawn_2_Cards_Played_Ask_Just_Say_No;//Action cards playable at this phase
                //Any move that plays a card excluding justSayNo move next state
                Statephase notJustSayNoAble = Statephase.Turn_Started_Cards_Drawn_3_Cards_Played_Swap_Properties_Or_End_Turn_Only;//cards playable at this phase
                //Just Say No card played by off turn player
                Statephase JustSayNoUsedByOpposition = Statephase.Turn_Started_Cards_Drawn_2_Cards_Played_Just_Say_No_Used_By_Oppostion_Ask_Player_On_Turn_Just_Say_No;
                //Property card rearranging next state
                Statephase rearrangeProperties = Statephase.Turn_Started_Cards_Drawn_2_Cards_Played;//cards can be rearranged at this phase
                //Draw
                Statephase drawCardsAtTurnStart = Statephase.Turn_Started_Cards_Drawn_2_Cards_Played;//not allowable at this phase
                BoolResponseBox isMoveTypeAllowableAtCurrentPhase = checkIfMoveAllowedAtThisState(typeOfActionToPerform, playerPerformingAction, currentState);

                Statephase discard = Statephase.Turn_Started_Cards_Drawn_2_Cards_Played;
                if (isMoveTypeAllowableAtCurrentPhase.success)
                {
                    //type of move is allowable at this point for this player. Check if move is doable ex is a set stealable
                    result = doAppropriateAction(lastState, currentState, nextState, playerPerformingAction, typeOfActionToPerform, justSayNoAble, notJustSayNoAble, rearrangeProperties, drawCardsAtTurnStart, JustSayNoUsedByOpposition, cardsAndPlayersInvolved, discard);

                    return result;//True if action performed, False if not
                }
                else
                {
                    //No other actionTypes are allowable in this state
                    return new BoolResponseBox(false, "Player:" + playerPerformingAction.name + " is not able to perform " + typeOfActionToPerform + " at this state:" + currentState.currentPhase.ToString());
                }
            }

            #endregion 2 Cards Played State

            #region 2 Cards Played State Just Say Noable Card Played

            else if (currentState.currentPhase.CompareTo(Statephase.Turn_Started_Cards_Drawn_2_Cards_Played_Ask_Just_Say_No) == 0)
            {
                Statephase discard = Statephase.Invalid_Action_For_Turn;

                //Player used Just Say No
                Statephase JustSayNoUsedByOpposition = Statephase.Turn_Started_Cards_Drawn_2_Cards_Played_Just_Say_No_Used_By_Oppostion_Ask_Player_On_Turn_Just_Say_No;
                /*
                //Player did not play just say no and is dealbreakered or sly dealed or forced dealed
                Statephase JustSayNoNotUsed = Statephase.Turn_Started_Cards_Drawn_1_Cards_Played;

                //Player has to pay rent or Birthday or Debt Collector
                Statephase PayDebt = Statephase.Turn_Started_Cards_Drawn_1_Cards_Play
                */
                //Action card that can be just say no carded next state
                Statephase justSayNoAble = Statephase.Turn_Started_Cards_Drawn_2_Cards_Played_Ask_Just_Say_No;//Action cards not playable at this phase
                //Any move that plays a card excluding justSayNo move next state
                Statephase notJustSayNoAble = Statephase.Turn_Started_Cards_Drawn_3_Cards_Played_Swap_Properties_Or_End_Turn_Only;//cards playable at this phase

                //Property card rearranging next state
                Statephase rearrangeProperties = Statephase.Invalid_Action_For_Turn;//cards can be rearranged at this phase
                //Draw
                Statephase drawCardsAtTurnStart = Statephase.Invalid_Action_For_Turn;//not allowable at this phase
                BoolResponseBox isMoveTypeAllowableAtCurrentPhase = checkIfMoveAllowedAtThisState(typeOfActionToPerform, playerPerformingAction, currentState);
                //In this state players may be required to pay rent, birthday or debt collector money or use a just say no
                if (isMoveTypeAllowableAtCurrentPhase.success)
                {
                    //type of move is allowable at this point for this player. Check if move is doable ex is a set stealable
                    result = doAppropriateAction(lastState, currentState, nextState, playerPerformingAction, typeOfActionToPerform, justSayNoAble, notJustSayNoAble, rearrangeProperties, drawCardsAtTurnStart, JustSayNoUsedByOpposition, cardsAndPlayersInvolved, discard);

                    return result;//True if action performed, False if not
                }
                else
                {
                    //No other actionTypes are allowable in this state
                    return new BoolResponseBox(false, "Player:" + playerPerformingAction.name + " is not able to perform " + typeOfActionToPerform + " at this state:" + currentState.currentPhase.ToString());
                }
            }

            #endregion 2 Cards Played State Just Say Noable Card Played

            #region 2 Cards Played State Just Say Noable Card just Say No'd

            else if (currentState.currentPhase.CompareTo(Statephase.Turn_Started_Cards_Drawn_2_Cards_Played_Just_Say_No_Used_By_Oppostion_Ask_Player_On_Turn_Just_Say_No) == 0)
            {
                Statephase discard = Statephase.Invalid_Action_For_Turn;

                //Player used Just Say No
                Statephase JustSayNoUsedByOpposition = Statephase.Invalid_Action_For_Turn;
                /*
                //Player did not play just say no and is dealbreakered or sly dealed or forced dealed
                Statephase JustSayNoNotUsed = Statephase.Turn_Started_Cards_Drawn_1_Cards_Played;

                //Player has to pay rent or Birthday or Debt Collector
                Statephase PayDebt = Statephase.Turn_Started_Cards_Drawn_1_Cards_Play
                */
                //Action card that can be just say no carded next state
                Statephase justSayNoAble = Statephase.Turn_Started_Cards_Drawn_2_Cards_Played_Ask_Just_Say_No;//Action cards not playable at this phase except just say no
                //Any move that plays a card excluding justSayNo move next state
                Statephase notJustSayNoAble = Statephase.Turn_Started_Cards_Drawn_3_Cards_Played_Swap_Properties_Or_End_Turn_Only;//cards playable at this phase

                //Property card rearranging next state
                Statephase rearrangeProperties = Statephase.Invalid_Action_For_Turn;//cards can be rearranged at this phase
                //Draw
                Statephase drawCardsAtTurnStart = Statephase.Invalid_Action_For_Turn;//not allowable at this phase
                BoolResponseBox isMoveTypeAllowableAtCurrentPhase = checkIfMoveAllowedAtThisState(typeOfActionToPerform, playerPerformingAction, currentState);
                //In this state players may be required to pay rent, birthday or debt collector money or use a just say no
                if (isMoveTypeAllowableAtCurrentPhase.success)
                {
                    //type of move is allowable at this point for this player. Check if move is doable ex is a set stealable
                    result = doAppropriateAction(lastState, currentState, nextState, playerPerformingAction, typeOfActionToPerform, justSayNoAble, notJustSayNoAble, rearrangeProperties, drawCardsAtTurnStart, JustSayNoUsedByOpposition, cardsAndPlayersInvolved, discard);

                    return result;//True if action performed, False if not
                }
                else
                {
                    //No other actionTypes are allowable in this state
                    return new BoolResponseBox(false, "Player:" + playerPerformingAction.name + " is not able to perform " + typeOfActionToPerform + " at this state:" + currentState.currentPhase.ToString());
                }
            }

            #endregion 2 Cards Played State Just Say Noable Card just Say No'd

            #region 3 Cards Played State

            else if (currentState.currentPhase.CompareTo(Statephase.Turn_Started_Cards_Drawn_3_Cards_Played_Swap_Properties_Or_End_Turn_Only) == 0)
            {
                //Action card that can be just say no carded next state
                Statephase justSayNoAble = Statephase.Invalid_Action_For_Turn;//Action cards not playable at this phase
                //Any move that plays a card excluding justSayNo move next state
                Statephase notJustSayNoAble = Statephase.Invalid_Action_For_Turn;//cards not playable at this phase
                //Just Say No card played by off turn player
                Statephase JustSayNoUsedByOpposition = Statephase.Invalid_Action_For_Turn;
                //Property card rearranging next state
                Statephase rearrangeProperties = Statephase.Turn_Started_Cards_Drawn_3_Cards_Played_Swap_Properties_Or_End_Turn_Only;//cards can be rearranged at this phase
                //Draw
                Statephase drawCardsAtTurnStart = Statephase.Invalid_Action_For_Turn;//not allowable at this phase

                Statephase discard = Statephase.Turn_Started_Cards_Drawn_3_Cards_Played_Swap_Properties_Or_End_Turn_Only;
                BoolResponseBox isMoveTypeAllowableAtCurrentPhase = checkIfMoveAllowedAtThisState(typeOfActionToPerform, playerPerformingAction, currentState);
                if (isMoveTypeAllowableAtCurrentPhase.success)
                {
                    //type of move is allowable at this point for this player. Check if move is doable ex is a set stealable
                    result = doAppropriateAction(lastState, currentState, nextState, playerPerformingAction, typeOfActionToPerform, justSayNoAble, notJustSayNoAble, rearrangeProperties, drawCardsAtTurnStart, JustSayNoUsedByOpposition, cardsAndPlayersInvolved, discard);

                    return result;//True if action performed, False if not
                }
                else
                {
                    //No other actionTypes are allowable in this state
                    return new BoolResponseBox(false, "Player:" + playerPerformingAction.name + " is not able to perform " + typeOfActionToPerform + " at this state:" + currentState.currentPhase.ToString());
                }
            }

            #endregion 3 Cards Played State

            #region Too many cards in hand at end of turn states

            else if (currentState.currentPhase.CompareTo(Statephase.Turn_Ended_12_Cards_In_Hand_Discard_5_Cards) == 0)
            {
                //Action card that can be just say no carded next state
                Statephase justSayNoAble = Statephase.Turn_Ended_12_Cards_In_Hand_Discard_5_Cards;
                //Any move that plays a card excluding justSayNo move next state
                Statephase notJustSayNoAble = Statephase.Turn_Ended_12_Cards_In_Hand_Discard_5_Cards;
                //Just Say No card played by off turn player
                Statephase JustSayNoUsedByOpposition = Statephase.Turn_Ended_12_Cards_In_Hand_Discard_5_Cards;
                //Property card rearranging next state
                Statephase rearrangeProperties = Statephase.Turn_Ended_12_Cards_In_Hand_Discard_5_Cards;
                //Draw
                Statephase drawCardsAtTurnStart = Statephase.Turn_Ended_12_Cards_In_Hand_Discard_5_Cards;//not allowable at this phase
                //Discard
                Statephase discard = Statephase.Turn_Ended_11_Cards_In_Hand_Discard_4_Cards;

                BoolResponseBox isMoveTypeAllowableAtCurrentPhase = checkIfMoveAllowedAtThisState(typeOfActionToPerform, playerPerformingAction, currentState);
                if (isMoveTypeAllowableAtCurrentPhase.success)
                {
                    //type of move is allowable at this point for this player. Check if move is doable ex is a set stealable
                    result = doAppropriateAction(lastState, currentState, nextState, playerPerformingAction, typeOfActionToPerform, justSayNoAble, notJustSayNoAble, rearrangeProperties, drawCardsAtTurnStart, JustSayNoUsedByOpposition, cardsAndPlayersInvolved, discard);

                    return result;//True if action performed, False if not
                }
                else
                {
                    //No other actionTypes are allowable in this state
                    return new BoolResponseBox(false, "Player:" + playerPerformingAction.name + " is not able to perform " + typeOfActionToPerform + " at this state:" + currentState.currentPhase.ToString());
                }
            }
            else if (currentState.currentPhase.CompareTo(Statephase.Turn_Ended_11_Cards_In_Hand_Discard_4_Cards) == 0)
            {
                //Action card that can be just say no carded next state
                Statephase justSayNoAble = Statephase.Turn_Ended_11_Cards_In_Hand_Discard_4_Cards;
                //Any move that plays a card excluding justSayNo move next state
                Statephase notJustSayNoAble = Statephase.Turn_Ended_11_Cards_In_Hand_Discard_4_Cards;
                //Just Say No card played by off turn player
                Statephase JustSayNoUsedByOpposition = Statephase.Turn_Ended_11_Cards_In_Hand_Discard_4_Cards;
                //Property card rearranging next state
                Statephase rearrangeProperties = Statephase.Turn_Ended_11_Cards_In_Hand_Discard_4_Cards;
                //Draw
                Statephase drawCardsAtTurnStart = Statephase.Turn_Ended_11_Cards_In_Hand_Discard_4_Cards;//not allowable at this phase
                //Discard
                Statephase discard = Statephase.Turn_Ended_10_Cards_In_Hand_Discard_3_Cards;

                BoolResponseBox isMoveTypeAllowableAtCurrentPhase = checkIfMoveAllowedAtThisState(typeOfActionToPerform, playerPerformingAction, currentState);
                if (isMoveTypeAllowableAtCurrentPhase.success)
                {
                    //type of move is allowable at this point for this player. Check if move is doable ex is a set stealable
                    result = doAppropriateAction(lastState, currentState, nextState, playerPerformingAction, typeOfActionToPerform, justSayNoAble, notJustSayNoAble, rearrangeProperties, drawCardsAtTurnStart, JustSayNoUsedByOpposition, cardsAndPlayersInvolved, discard);

                    return result;//True if action performed, False if not
                }
                else
                {
                    //No other actionTypes are allowable in this state
                    return new BoolResponseBox(false, "Player:" + playerPerformingAction.name + " is not able to perform " + typeOfActionToPerform + " at this state:" + currentState.currentPhase.ToString());
                }
            }
            else if (currentState.currentPhase.CompareTo(Statephase.Turn_Ended_10_Cards_In_Hand_Discard_3_Cards) == 0)
            {
                //Action card that can be just say no carded next state
                Statephase justSayNoAble = Statephase.Turn_Ended_10_Cards_In_Hand_Discard_3_Cards;
                //Any move that plays a card excluding justSayNo move next state
                Statephase notJustSayNoAble = Statephase.Turn_Ended_10_Cards_In_Hand_Discard_3_Cards;
                //Just Say No card played by off turn player
                Statephase JustSayNoUsedByOpposition = Statephase.Turn_Ended_10_Cards_In_Hand_Discard_3_Cards;
                //Property card rearranging next state
                Statephase rearrangeProperties = Statephase.Turn_Ended_10_Cards_In_Hand_Discard_3_Cards;
                //Draw
                Statephase drawCardsAtTurnStart = Statephase.Turn_Ended_10_Cards_In_Hand_Discard_3_Cards;//not allowable at this phase
                //Discard
                Statephase discard = Statephase.Turn_Ended_9_Cards_In_Hand_Discard_2_Cards;

                BoolResponseBox isMoveTypeAllowableAtCurrentPhase = checkIfMoveAllowedAtThisState(typeOfActionToPerform, playerPerformingAction, currentState);
                if (isMoveTypeAllowableAtCurrentPhase.success)
                {
                    //type of move is allowable at this point for this player. Check if move is doable ex is a set stealable
                    result = doAppropriateAction(lastState, currentState, nextState, playerPerformingAction, typeOfActionToPerform, justSayNoAble, notJustSayNoAble, rearrangeProperties, drawCardsAtTurnStart, JustSayNoUsedByOpposition, cardsAndPlayersInvolved, discard);

                    return result;//True if action performed, False if not
                }
                else
                {
                    //No other actionTypes are allowable in this state
                    return new BoolResponseBox(false, "Player:" + playerPerformingAction.name + " is not able to perform " + typeOfActionToPerform + " at this state:" + currentState.currentPhase.ToString());
                }
            }
            else if (currentState.currentPhase.CompareTo(Statephase.Turn_Ended_9_Cards_In_Hand_Discard_2_Cards) == 0)
            {
                //Action card that can be just say no carded next state
                Statephase justSayNoAble = Statephase.Turn_Ended_9_Cards_In_Hand_Discard_2_Cards;
                //Any move that plays a card excluding justSayNo move next state
                Statephase notJustSayNoAble = Statephase.Turn_Ended_9_Cards_In_Hand_Discard_2_Cards;
                //Just Say No card played by off turn player
                Statephase JustSayNoUsedByOpposition = Statephase.Turn_Ended_9_Cards_In_Hand_Discard_2_Cards;
                //Property card rearranging next state
                Statephase rearrangeProperties = Statephase.Turn_Ended_9_Cards_In_Hand_Discard_2_Cards;
                //Draw
                Statephase drawCardsAtTurnStart = Statephase.Turn_Ended_9_Cards_In_Hand_Discard_2_Cards;//not allowable at this phase
                //Discard
                Statephase discard = Statephase.Turn_Ended_8_Cards_In_Hand_Discard_1_Card;

                BoolResponseBox isMoveTypeAllowableAtCurrentPhase = checkIfMoveAllowedAtThisState(typeOfActionToPerform, playerPerformingAction, currentState);
                if (isMoveTypeAllowableAtCurrentPhase.success)
                {
                    //type of move is allowable at this point for this player. Check if move is doable ex is a set stealable
                    result = doAppropriateAction(lastState, currentState, nextState, playerPerformingAction, typeOfActionToPerform, justSayNoAble, notJustSayNoAble, rearrangeProperties, drawCardsAtTurnStart, JustSayNoUsedByOpposition, cardsAndPlayersInvolved, discard);

                    return result;//True if action performed, False if not
                }
                else
                {
                    //No other actionTypes are allowable in this state
                    return new BoolResponseBox(false, "Player:" + playerPerformingAction.name + " is not able to perform " + typeOfActionToPerform + " at this state:" + currentState.currentPhase.ToString());
                }
            }
            else if (currentState.currentPhase.CompareTo(Statephase.Turn_Ended_8_Cards_In_Hand_Discard_1_Card) == 0)
            {
                //Action card that can be just say no carded next state
                Statephase justSayNoAble = Statephase.Turn_Ended_9_Cards_In_Hand_Discard_2_Cards;
                //Any move that plays a card excluding justSayNo move next state
                Statephase notJustSayNoAble = Statephase.Turn_Ended_9_Cards_In_Hand_Discard_2_Cards;
                //Just Say No card played by off turn player
                Statephase JustSayNoUsedByOpposition = Statephase.Turn_Ended_9_Cards_In_Hand_Discard_2_Cards;
                //Property card rearranging next state
                Statephase rearrangeProperties = Statephase.Turn_Ended_9_Cards_In_Hand_Discard_2_Cards;
                //Draw
                Statephase drawCardsAtTurnStart = Statephase.Turn_Ended_9_Cards_In_Hand_Discard_2_Cards;//not allowable at this phase
                //Discard
                Statephase discard = Statephase.Turn_Ended_7_Or_Less_Cards_In_Hand_Setup_NextPlayer;

                BoolResponseBox isMoveTypeAllowableAtCurrentPhase = checkIfMoveAllowedAtThisState(typeOfActionToPerform, playerPerformingAction, currentState);
                if (isMoveTypeAllowableAtCurrentPhase.success)
                {
                    //type of move is allowable at this point for this player. Check if move is doable ex is a set stealable
                    result = doAppropriateAction(lastState, currentState, nextState, playerPerformingAction, typeOfActionToPerform, justSayNoAble, notJustSayNoAble, rearrangeProperties, drawCardsAtTurnStart, JustSayNoUsedByOpposition, cardsAndPlayersInvolved, discard);

                    return result;//True if action performed, False if not
                }
                else
                {
                    //No other actionTypes are allowable in this state
                    return new BoolResponseBox(false, "Player:" + playerPerformingAction.name + " is not able to perform " + typeOfActionToPerform + " at this state:" + currentState.currentPhase.ToString());
                }
            }

            #endregion Too many cards in hand at end of turn states

            else
            {
                return new BoolResponseBox(false, "Game is in an invalid state");
            }
        }
    }
}