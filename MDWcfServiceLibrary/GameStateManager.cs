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
                //turn action is for this playfieldmodel
            }
            throw new NotImplementedException();
        }

        private bool checkIfActionIsForThisState(TurnActionModel ta, PlayFieldModel pfm)
        {
            throw new NotImplementedException();
        }
    }
}