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

        public void drawTwoCards(PlayerModel player)
        {
            //draws two cards to players hand BADCODE
            player.hand.addCardToHand(currentPlayFieldModel.drawPile.drawcard());
            player.hand.addCardToHand(currentPlayFieldModel.drawPile.drawcard());
            actionPerformed();
        }

        public void bankCard(Card cardInHandToBeBanked, PlayerModel playerWhoIsBankingCard)
        {
            playerWhoIsBankingCard.hand.cardsInHand.Remove(cardInHandToBeBanked);
            playerWhoIsBankingCard.bank.addCardToBank(cardInHandToBeBanked);
            actionPerformed();
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