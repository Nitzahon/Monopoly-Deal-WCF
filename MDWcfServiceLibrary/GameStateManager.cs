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

        public bool doAction(TurnActionModel turnActionToDo)
        {
            ///Returns false if action not carried out
            ///
            //Get CurrentPlayFieldModelState
            currentPlayFieldModel = gameModel.gameStates[(gameModel.gameStates.Count - 1)];
            if (checkIfActionIsForThisState(turnActionToDo, currentPlayFieldModel))
            {
                if (turnActionToDo.actionTaken && turnActionToDo.typeOfActionToTake.CompareTo(TurnActionModel.TurnActionTypes.drawTwoCardsAtStartOfTurn) == 0)
                {
                    drawTwoCards(gameModel.players[gameModel.playerIdLookup.IndexOf(turnActionToDo.playerGuids[0])]);
                }
                //turn action is for this playfieldmodel
            }
            return true;
            throw new NotImplementedException();
        }

        public bool checkIfMoveLegal(Guid guidOfPlayerMakingMove, TurnActionModel.TurnActionTypes typeOfActionAttempted)
        {
            throw new NotImplementedException();
        }

        public void drawTwoCards(PlayerModel player)
        {
            //draws two cards to players hand Unsafe
            player.hand.addCardToHand(currentPlayFieldModel.drawPile.drawcard());
            player.hand.addCardToHand(currentPlayFieldModel.drawPile.drawcard());
            actionPerformed();
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
                actionPerformed();
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
            TurnActionModel.TurnActionTypes tAT = turnActionToDo.typeOfActionToTake;
            PlayerModel playerAttemptingAction = getPlayerModel(playerGuid, gameModel.gameModelGuid, currentState.thisPlayFieldModelInstanceGuid);
            if (playerAttemptingAction != null)
            {
                foreach (TurnActionModel.TurnActionTypes t in playerAttemptingAction.actionsCurrentlyAllowed)
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

        private bool checkIfActionIsForThisState(TurnActionModel ta, PlayFieldModel pfm)
        {
            return true;
            throw new NotImplementedException();
        }

        private void actionPerformed()
        {
            //update game state model and turn action model
        }
    }
}