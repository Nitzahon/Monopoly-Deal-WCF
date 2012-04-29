using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MDWcfServiceLibrary
{
    internal class GameStateManagerToMoveAdapter : IMonopolyDeal_GameStateManager
    {
        internal MonopolyDeal monopolyDeal;
        internal Move move;

        public GameStateManagerToMoveAdapter(MonopolyDeal md)
        {
            monopolyDeal = md;
            move = new Move(monopolyDeal);
        }

        private PlayFieldModel getCurrentState()
        {
            return monopolyDeal.gameStates[monopolyDeal.gameStates.Count - 1];
        }

        private PlayFieldModel getPreviousState()
        {
            try
            {
                return monopolyDeal.gameStates[monopolyDeal.gameStates.Count - 2];
            }
            catch (ArgumentOutOfRangeException)
            {
                return null;
            }
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

        public bool bankCard(int playedCardID, Guid playerGuid, Guid serverGuid, Guid playfieldModelInstanceGuid)
        {
            PlayFieldModel lastState = getPreviousState();
            PlayFieldModel currentState = getCurrentState();
            PlayFieldModel nextState = null;
            PlayerModel playerModelAtCurrentState = move.getPlayerModel(playerGuid, currentState);
            Card playedcard = monopolyDeal.deck.getCardByID(playedCardID);
            if (checkIfCardInHand(playedcard, playerModelAtCurrentState) != null)
            {
                if (playedcard is PropertyCard)
                {
                    return new BoolResponseBox(false, "Property Cards can not be Banked").success;
                }
                else
                {
                    MoveInfo bankCardInfo = new MoveInfo();
                    bankCardInfo.playerWhoseTurnItIs = playerGuid;
                    bankCardInfo.playerMakingMove = playerGuid;
                    bankCardInfo.moveBeingMade = TurnActionTypes.BankMoneyCard;
                    bankCardInfo.idOfCardBeingUsed = playedCardID;

                    BoolResponseBox result = move.evaluateMove(lastState, currentState, nextState, playerModelAtCurrentState, bankCardInfo.moveBeingMade, bankCardInfo);
                    return result.success;
                }
            }
            return new BoolResponseBox(false, "Selected Card is not in players hand.").success;
        }

        public PlayFieldModel copyPlayFieldModel(PlayFieldModel currentPlayFieldModel)
        {
            throw new NotImplementedException();
        }

        public bool doAction(Guid gameGuid, Guid playerGuid, Guid gameStateActionShouldBeAppliedOnGuid, TurnActionTypes actionType)
        {
            throw new NotSupportedException();
        }

        public BoolResponseBox drawFiveCards(Guid player, Guid state)
        {
            PlayFieldModel lastState = getPreviousState();
            PlayFieldModel currentState = getCurrentState();
            PlayFieldModel nextState = null;
            PlayerModel playerModelAtCurrentState = move.getPlayerModel(player, currentState);

            MoveInfo draw5CardsInfo = new MoveInfo();
            draw5CardsInfo.playerWhoseTurnItIs = player;
            draw5CardsInfo.playerMakingMove = player;
            draw5CardsInfo.moveBeingMade = TurnActionTypes.drawFiveCardsAtStartOfTurn;

            BoolResponseBox result = move.evaluateMove(lastState, currentState, nextState, playerModelAtCurrentState, draw5CardsInfo.moveBeingMade, draw5CardsInfo);
            return result;
        }

        public BoolResponseBox drawTwoCardsAtTurnStart(Guid player, Guid state)
        {
            PlayFieldModel lastState = getPreviousState();
            PlayFieldModel currentState = getCurrentState();
            PlayFieldModel nextState = null;
            PlayerModel playerModelAtCurrentState = move.getPlayerModel(player, currentState);

            MoveInfo draw2CardsInfo = new MoveInfo();
            draw2CardsInfo.playerWhoseTurnItIs = player;
            draw2CardsInfo.playerMakingMove = player;
            draw2CardsInfo.moveBeingMade = TurnActionTypes.drawTwoCardsAtStartOfTurn;

            BoolResponseBox result = move.evaluateMove(lastState, currentState, nextState, playerModelAtCurrentState, draw2CardsInfo.moveBeingMade, draw2CardsInfo);
            return result;
        }

        public BoolResponseBox endTurn(Guid playerGuid, Guid stateGuid)
        {
            PlayFieldModel lastState = getPreviousState();
            PlayFieldModel currentState = getCurrentState();
            PlayFieldModel nextState = null;
            PlayerModel playerModelAtCurrentState = move.getPlayerModel(playerGuid, currentState);

            MoveInfo endTurnInfo = new MoveInfo();
            endTurnInfo.playerWhoseTurnItIs = playerGuid;
            endTurnInfo.playerMakingMove = playerGuid;
            endTurnInfo.moveBeingMade = TurnActionTypes.EndTurn;

            BoolResponseBox result = move.evaluateMove(lastState, currentState, nextState, playerModelAtCurrentState, endTurnInfo.moveBeingMade, endTurnInfo);
            return result;
        }

        public PlayFieldModel getCurrentPlayFieldModel()
        {
            throw new NotImplementedException();
        }

        public PlayerModel getPlayerByGuid(Guid player, PlayFieldModel pfm)
        {
            throw new NotImplementedException();
        }

        public PlayerModel getPlayerModel(Guid playerGuid, Guid gameGuid, Guid currentPlayFieldModelGuid)
        {
            foreach (PlayerModel pm in getCurrentState().playerModels)
            {
                if (pm.guid.CompareTo(playerGuid) == 0)
                {
                    return pm;
                }
            }
            return null;
        }

        public PlayFieldModel getPlayFieldModelByGuid(Guid playFieldModelGuid)
        {
            throw new NotImplementedException();
        }

        public bool haveAllPlayersAcknowledgedCurrentState(Guid gameServiceInstanceGuid, Guid currentStateGuidP, List<PlayerModel> playersInGame)
        {
            throw new NotImplementedException();
        }

        public bool isActionAllowedForPlayer(TurnActionTypes turnActionToDo, Guid playerGuid, PlayFieldModel currentState)
        {
            throw new NotImplementedException();
        }

        public bool playPropertyCardToNewSet(Guid gameGuid, bool isOrientedUp, Guid playerGuid, Guid gameStateActionShouldBeAppliedOnGuid, TurnActionTypes actionType, int propertyCardID)
        {
            PlayFieldModel lastState = getPreviousState();
            PlayFieldModel currentState = getCurrentState();
            PlayFieldModel nextState = null;
            PlayerModel playerModelAtCurrentState = move.getPlayerModel(playerGuid, currentState);
            PropertyCard playedcard = monopolyDeal.deck.getCardByID(propertyCardID) as PropertyCard;
            if (playedcard != null && checkIfCardInHand(playedcard, playerModelAtCurrentState) != null)
            {
                MoveInfo playPropertyCardFromHand = new MoveInfo();
                playPropertyCardFromHand.playerWhoseTurnItIs = playerGuid;
                playPropertyCardFromHand.playerMakingMove = playerGuid;
                playPropertyCardFromHand.moveBeingMade = TurnActionTypes.PlayPropertyCardFromHand;
                playPropertyCardFromHand.idOfCardBeingUsed = propertyCardID;
                playPropertyCardFromHand.isPropertyToPlayOrientedUp = isOrientedUp;
                playPropertyCardFromHand.addPropertyToPlayToExistingSet = false;//Add to new set

                BoolResponseBox result = move.evaluateMove(lastState, currentState, nextState, playerModelAtCurrentState, playPropertyCardFromHand.moveBeingMade, playPropertyCardFromHand);
                return result.success;
            }
            return new BoolResponseBox(false, "Selected Card is not in players hand or is not a property card").success;
        }

        public bool discard(int cardsToDiscardID, Guid playerGuid, Guid serverGuid, Guid playfieldModelInstanceGuid)
        {
            PlayFieldModel lastState = getPreviousState();
            PlayFieldModel currentState = getCurrentState();
            PlayFieldModel nextState = null;
            PlayerModel playerModelAtCurrentState = move.getPlayerModel(playerGuid, currentState);
            Card playedcard = currentState.deck.getCardByID(cardsToDiscardID);
            if (playedcard != null && checkIfCardInHand(playedcard, playerModelAtCurrentState) != null)
            {
                MoveInfo discard1Card = new MoveInfo();
                discard1Card.moveBeingMade = TurnActionTypes.Discard_1_Card;
                discard1Card.idOfCardBeingUsed = cardsToDiscardID;

                BoolResponseBox result = move.evaluateMove(lastState, currentState, nextState, playerModelAtCurrentState, discard1Card.moveBeingMade, discard1Card);
                return result.success;
            }
            return new BoolResponseBox(false, "Selected Card is not in players hand").success;
        }

        public bool playActionCardPassGo(int passGoCardID, Guid serverGuid, Guid playerGuid, Guid playfieldModelInstanceGuid, TurnActionTypes turnActionTypes)
        {
            PlayFieldModel lastState = getPreviousState();
            PlayFieldModel currentState = getCurrentState();
            PlayFieldModel nextState = null;
            PlayerModel playerModelAtCurrentState = move.getPlayerModel(playerGuid, currentState);
            ActionCard playedPassGo = monopolyDeal.deck.getCardByID(passGoCardID) as ActionCard;
            if (playedPassGo != null && checkIfCardInHand(playedPassGo, playerModelAtCurrentState) != null)
            {
                MoveInfo playPassGo = new MoveInfo();
                playPassGo.playerWhoseTurnItIs = playerGuid;
                playPassGo.playerMakingMove = playerGuid;
                playPassGo.moveBeingMade = TurnActionTypes.PlayActionCard;
                playPassGo.idOfCardBeingUsed = passGoCardID;
                playPassGo.actionCardActionType = ActionCardAction.PassGo;

                BoolResponseBox result = move.evaluateMove(lastState, currentState, nextState, playerModelAtCurrentState, playPassGo.moveBeingMade, playPassGo);
                return result.success;
            }
            return new BoolResponseBox(false, "Selected Card is not in players hand or is not a Action Card").success;
        }

        public bool playPropertyCardToExistingSet(Card playedCard, PropertyCardSet setToPlayPropertyTo, Guid gameLobbyGuid, Guid playerGuid, Guid playfieldModelInstanceGuid)
        {
            PlayFieldModel lastState = getPreviousState();
            PlayFieldModel currentState = getCurrentState();
            PlayFieldModel nextState = null;
            PlayerModel playerModelAtCurrentState = move.getPlayerModel(playerGuid, currentState);
            PropertyCard playedPropertycard = monopolyDeal.deck.getCardByID(playedCard.cardID) as PropertyCard;
            if (playedPropertycard != null && checkIfCardInHand(playedPropertycard, playerModelAtCurrentState) != null)
            {
                MoveInfo playPropertyCardFromHand = new MoveInfo();
                playPropertyCardFromHand.playerMakingMove = playerGuid;
                playPropertyCardFromHand.moveBeingMade = TurnActionTypes.PlayPropertyCardFromHand;
                playPropertyCardFromHand.idOfCardBeingUsed = playedCard.cardID;

                bool isOrientedUp = (playedCard as PropertyCard).isCardUp;
                playPropertyCardFromHand.isPropertyToPlayOrientedUp = isOrientedUp;
                playPropertyCardFromHand.addPropertyToPlayToExistingSet = true;//Add to existing set
                playPropertyCardFromHand.guidOfExistingSetToPlayPropertyTo = setToPlayPropertyTo.guid;

                BoolResponseBox result = move.evaluateMove(lastState, currentState, nextState, playerModelAtCurrentState, playPropertyCardFromHand.moveBeingMade, playPropertyCardFromHand);
                return result.success;
            }
            return new BoolResponseBox(false, "Selected Card is not in players hand or is not a property card").success;
        }

        PlayFieldModel IMonopolyDeal_GameStateManager.getCurrentState()
        {
            return monopolyDeal.gameStates[monopolyDeal.gameStates.Count - 1];
        }

        public BoolResponseBox drawTwoCardsAtTurnStart(Guid player)
        {
            PlayFieldModel lastState = getPreviousState();
            PlayFieldModel currentState = getCurrentState();
            PlayFieldModel nextState = null;
            PlayerModel playerModelAtCurrentState = move.getPlayerModel(player, currentState);

            MoveInfo draw2CardsInfo = new MoveInfo();
            draw2CardsInfo.playerWhoseTurnItIs = player;
            draw2CardsInfo.playerMakingMove = player;
            draw2CardsInfo.moveBeingMade = TurnActionTypes.drawTwoCardsAtStartOfTurn;

            BoolResponseBox result = move.evaluateMove(lastState, currentState, nextState, playerModelAtCurrentState, draw2CardsInfo.moveBeingMade, draw2CardsInfo);
            return result;
        }

        public BoolResponseBox drawFiveCards(Guid player)
        {
            throw new NotImplementedException();
        }

        public bool playDebtCollector(int debtCollectorCardID, Guid targetedPlayerGuid, Guid playerGuid, Guid gameLobbyGuid, Guid playfieldModelInstanceGuid)
        {
            PlayFieldModel lastState = getPreviousState();
            PlayFieldModel currentState = getCurrentState();
            PlayFieldModel nextState = null;
            PlayerModel playerModelAtCurrentState = move.getPlayerModel(playerGuid, currentState);
            ActionCard playedActionCard = monopolyDeal.deck.getCardByID(debtCollectorCardID) as ActionCard;
            if (playedActionCard != null && checkIfCardInHand(playedActionCard, playerModelAtCurrentState) != null)
            {
                MoveInfo playDebtCollector = new MoveInfo();
                playDebtCollector.playerWhoseTurnItIs = currentState.guidOfPlayerWhosTurnItIs;
                playDebtCollector.playerMakingMove = playerGuid;
                playDebtCollector.moveBeingMade = TurnActionTypes.PlayActionCard;
                playDebtCollector.idOfCardBeingUsed = debtCollectorCardID;

                playDebtCollector.actionCardActionType = ActionCardAction.DebtCollector;

                playDebtCollector.guidOfPlayerBeingDebtCollected = targetedPlayerGuid;

                BoolResponseBox result = move.evaluateMove(lastState, currentState, nextState, playerModelAtCurrentState, playDebtCollector.moveBeingMade, playDebtCollector);
                return result.success;
            }
            return new BoolResponseBox(false, "Selected Card is not in players hand or is not a Action card").success;
        }

        public bool payDebt(List<int> idOfCardsToPayWith, Guid playerGuid, Guid gameLobbyGuid, Guid playfieldModelInstanceGuid)
        {
            PlayFieldModel lastState = getPreviousState();
            PlayFieldModel currentState = getCurrentState();
            PlayFieldModel nextState = null;
            PlayerModel playerModelAtCurrentState = move.getPlayerModel(playerGuid, currentState);
            MoveInfo payDebt = new MoveInfo();
            payDebt.playerMakingMove = playerGuid;
            payDebt.moveBeingMade = TurnActionTypes.PayDebt;
            payDebt.listOfIDsOfCardsBeingUsedToPayDebt = idOfCardsToPayWith;
            payDebt.guidOfPlayerToPayDebtTo = currentState.guidOfPlayerWhosTurnItIs;
            payDebt.amountOwed = playerModelAtCurrentState.amountOwedToAnotherPlayer;
            BoolResponseBox result = move.evaluateMove(lastState, currentState, nextState, playerModelAtCurrentState, payDebt.moveBeingMade, payDebt);
            return result.success;
        }

        public bool playActionCardItsMyBirthday(int myBirthdayCardID, Guid playerGuid, Guid gameLobbyGuid, Guid stateGuid)
        {
            PlayFieldModel lastState = getPreviousState();
            PlayFieldModel currentState = getCurrentState();
            PlayFieldModel nextState = null;
            PlayerModel playerModelAtCurrentState = move.getPlayerModel(playerGuid, currentState);
            ActionCard playedActionCard = monopolyDeal.deck.getCardByID(myBirthdayCardID) as ActionCard;
            if (playedActionCard != null && checkIfCardInHand(playedActionCard, playerModelAtCurrentState) != null)
            {
                MoveInfo playBirthday = new MoveInfo();
                playBirthday.playerWhoseTurnItIs = currentState.guidOfPlayerWhosTurnItIs;
                playBirthday.playerMakingMove = playerGuid;
                playBirthday.moveBeingMade = TurnActionTypes.PlayActionCard;
                playBirthday.idOfCardBeingUsed = myBirthdayCardID;

                playBirthday.actionCardActionType = ActionCardAction.ItsMyBirthday;

                BoolResponseBox result = move.evaluateMove(lastState, currentState, nextState, playerModelAtCurrentState, playBirthday.moveBeingMade, playBirthday);
                return result.success;
            }
            return new BoolResponseBox(false, "Selected Card is not in players hand or is not a Action card").success;
        }

        public bool playActionCardJustSayNo(int playedCard, Guid playerGuid, Guid gameLobbyGuid, Guid playfieldModelInstanceGuid)
        {
            PlayFieldModel lastState = getPreviousState();
            PlayFieldModel currentState = getCurrentState();
            PlayFieldModel nextState = null;
            PlayerModel playerModelAtCurrentState = move.getPlayerModel(playerGuid, currentState);
            MoveInfo playJustSayNo = new MoveInfo();
            playJustSayNo.playerMakingMove = playerGuid;
            playJustSayNo.moveBeingMade = TurnActionTypes.PlayJustSayNo;
            playJustSayNo.idOfCardBeingUsed = playedCard;
            BoolResponseBox result = move.evaluateMove(lastState, currentState, nextState, playerModelAtCurrentState, playJustSayNo.moveBeingMade, playJustSayNo);
            return result.success;
        }

        public BoolResponseBox movePropertyCard(int propertyCardToMoveID, bool isCardUp, bool moveToNewEmptySet,
            Guid oldSetGuid, Guid setToPlayPropertyToGuid, Guid playerGuid, Guid gameLobbyGuid, Guid playfieldModelInstanceGuid)
        {
            //Gets the last,current and a reference for the next state
            PlayFieldModel lastState = getPreviousState();
            PlayFieldModel currentState = getCurrentState();
            PlayFieldModel nextState = null;
            //Gets the PlayerModel of the Player making the move.
            PlayerModel playerModelAtCurrentState = move.getPlayerModel(playerGuid, currentState);
            //The property card to move between sets
            PropertyCard propertyCardToMove = monopolyDeal.deck.getCardByID(propertyCardToMoveID) as PropertyCard;
            if (propertyCardToMove != null && checkIfCardInHand(propertyCardToMove, playerModelAtCurrentState) != null)
            {
                //Initialize MoveInfo for moving a property card between sets
                MoveInfo movePropertyCardToNewSet = new MoveInfo();
                movePropertyCardToNewSet.playerMakingMove = playerGuid;
                movePropertyCardToNewSet.moveBeingMade = TurnActionTypes.MovePropertyCard;
                movePropertyCardToNewSet.guidOfSetPropertyToMoveIsIn = oldSetGuid;
                movePropertyCardToNewSet.guidOfPropertyToMove = propertyCardToMove.cardGuid;
                movePropertyCardToNewSet.isPropertyToMoveOrientedUp = isCardUp;
                movePropertyCardToNewSet.addPropertyToMoveToExistingSet = moveToNewEmptySet;
                movePropertyCardToNewSet.guidOfExistingSetToMovePropertyTo = setToPlayPropertyToGuid;
                movePropertyCardToNewSet.idOfCardBeingUsed = propertyCardToMoveID;
                BoolResponseBox result = move.evaluateMove(lastState, currentState, nextState, playerModelAtCurrentState, movePropertyCardToNewSet.moveBeingMade, movePropertyCardToNewSet);
                return result;
            }
            return new BoolResponseBox(false, "Selected Card is not in players property card sets or is not a property card");
        }
    }
}