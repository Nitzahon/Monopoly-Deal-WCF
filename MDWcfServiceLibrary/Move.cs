using System;
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

        private List<TurnActionTypes> setAllowableActionsNotOnTurnInDebt(List<TurnActionTypes> listToSet, PlayFieldModel newState)
        {
            if (newState.currentPhase.CompareTo(Statephase.Turn_Started_Cards_Drawn_0_Cards_Played_Ask_Just_Say_No) == 0 || newState.currentPhase.CompareTo(Statephase.Turn_Started_Cards_Drawn_1_Cards_Played_Ask_Just_Say_No) == 0 || newState.currentPhase.CompareTo(Statephase.Turn_Started_Cards_Drawn_2_Cards_Played_Ask_Just_Say_No) == 0)
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

        private Card removeCardFromHand(Card card, PlayerModel pm)
        {
            Card cardInHand = checkIfCardInHand(card, pm);
            if (cardInHand != null && pm.hand.cardsInHand.Remove(cardInHand))
            {
                return cardInHand;
            }
            return null;
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
                else if (card != null)
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
                    playerModelForPlayerToBePaid.propertySets.addSet(new PropertyCardSet((PropertyCard)card));
                }
                else
                {
                    playerModelForPlayerToBePaid.bank.addCardToBank(card);
                }
            }
            //Player has now paid debt
            playerModelForPlayerPaying.owesAnotherPlayer = false;
            //Update the state

            //Change state on success
            //has been performed, advance the phase of the game
            nextState.currentPhase = nextStatePhaseIfSuccessful;
            List<TurnActionTypes> notOnTurn = new List<TurnActionTypes>();
            List<TurnActionTypes> onTurn = new List<TurnActionTypes>();
            List<TurnActionTypes> inDebt = new List<TurnActionTypes>();
            onTurn = setAllowableActionsOnTurn(onTurn, nextState);
            inDebt = setAllowableActionsNotOnTurnInDebt(inDebt, nextState);
            updateAllowableStatesDebtPaid(nextState, notOnTurn, onTurn, nextState.guidOfPlayerWhosTurnItIs, inDebt);
            //change the current state to the next state
            addNextState(nextState);
            return new BoolResponseBox(true, "Player:" + playerPerformingAction.name + " Has paid");
        }

        private BoolResponseBox playActionCardDebtCollector(PlayFieldModel currentState, PlayFieldModel nextState, PlayerModel playerPerformingAction, Statephase nextStatePhaseIfSuccessful, MoveInfo debtCollectorInfo)
        {
            //Check
            //Perform action in next state
            //Clone the current state to create next state then draws 5 cards in the next state
            nextState = currentState.clone(generateGuidForNextState());
            PlayerModel playerModelForPlayer = getPlayerModel(playerPerformingAction.guid, nextState);

            Card cardInHandToBePlayed = nextState.deck.getCardByID(debtCollectorInfo.idOfCardBeingUsed);
            //Get the reference to the players playerModel in the current PlayFieldModel

            PlayerModel player = getPlayerModel(debtCollectorInfo.playerMakingMove, nextState);
            //Get the reference to the Card in the current PlayFieldModel
            if (cardInHandToBePlayed != null && cardInHandToBePlayed is ActionCard && ((ActionCard)cardInHandToBePlayed).actionType.CompareTo(ActionCardAction.DebtCollector) == 0)
            {
                Card card = removeCardFromHand(cardInHandToBePlayed, player);
                if (card != null)
                {
                    ActionCard actionCard = card as ActionCard;
                    //Do action
                    throw new NotImplementedException("Debt Collector Action Not implemented");

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
                    return new BoolResponseBox(true, "Player:" + playerPerformingAction.name + " Has used a Debt Collector");
                }

                return new BoolResponseBox(false, "Card is not in hand.");
            }
            return new BoolResponseBox(false, "Card is not in hand or is not a Debt Collector card");
        }

        #region implemented moves

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

        #endregion implemented moves

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

        private BoolResponseBox movePropertyCard(PlayFieldModel currentState, PlayFieldModel nextState, PlayerModel playerPerformingAction, MoveInfo moveInformation, Statephase rearrangeProperties)
        {
            throw new NotImplementedException();
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
            else if (currentState.currentPhase.CompareTo(Statephase.Turn_Started_Cards_Drawn_0_Cards_Played_Just_Say_No_Used_By_Oppostion_Ask_Player_On_Turn_Just_Say_No) == 0)
            {
                throw new NotImplementedException();
            }
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
            else if (currentState.currentPhase.CompareTo(Statephase.Turn_Started_Cards_Drawn_1_Cards_Played_Ask_Just_Say_No) == 0)
            {
            }
            else if (currentState.currentPhase.CompareTo(Statephase.Turn_Started_Cards_Drawn_1_Cards_Played_Just_Say_No_Used_By_Oppostion_Ask_Player_On_Turn_Just_Say_No) == 0)
            {
            }
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
            else if (currentState.currentPhase.CompareTo(Statephase.Turn_Started_Cards_Drawn_2_Cards_Played_Ask_Just_Say_No) == 0)
            {
            }
            else if (currentState.currentPhase.CompareTo(Statephase.Turn_Started_Cards_Drawn_2_Cards_Played_Just_Say_No_Used_By_Oppostion_Ask_Player_On_Turn_Just_Say_No) == 0)
            {
            }
            else if (currentState.currentPhase.CompareTo(Statephase.Turn_Started_Cards_Drawn_3_Cards_Played_Swap_Properties_Or_End_Turn_Only) == 0)
            {
                //Action card that can be just say no carded next state
                Statephase justSayNoAble = Statephase.Turn_Started_Cards_Drawn_3_Cards_Played_Swap_Properties_Or_End_Turn_Only;//Action cards not playable at this phase
                //Any move that plays a card excluding justSayNo move next state
                Statephase notJustSayNoAble = Statephase.Turn_Started_Cards_Drawn_3_Cards_Played_Swap_Properties_Or_End_Turn_Only;//cards not playable at this phase
                //Just Say No card played by off turn player
                Statephase JustSayNoUsedByOpposition = Statephase.Turn_Started_Cards_Drawn_3_Cards_Played_Swap_Properties_Or_End_Turn_Only;
                //Property card rearranging next state
                Statephase rearrangeProperties = Statephase.Turn_Started_Cards_Drawn_3_Cards_Played_Swap_Properties_Or_End_Turn_Only;//cards can be rearranged at this phase
                //Draw
                Statephase drawCardsAtTurnStart = Statephase.Turn_Started_Cards_Drawn_3_Cards_Played_Swap_Properties_Or_End_Turn_Only;//not allowable at this phase

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
            else if (currentState.currentPhase.CompareTo(Statephase.Game_Started) == 0)
            {
                throw new NotImplementedException();
            }
            else
            {
                return new BoolResponseBox(false, "Game is in an invalid state");
            }
            return new BoolResponseBox(false, "Game is in an invalid state");
        }
    }
}