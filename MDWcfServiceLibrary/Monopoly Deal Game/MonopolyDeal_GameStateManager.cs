using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MDWcfServiceLibrary
{
    /// <summary>
    /// Replaces old GameStateManager
    /// </summary>
    internal class MonopolyDeal_GameStateManager
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
    }
}