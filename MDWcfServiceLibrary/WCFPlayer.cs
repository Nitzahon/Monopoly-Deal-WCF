using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MonopolyDealLibrary;

namespace MDWcfServiceLibrary
{
    internal class WCFPlayer : MonopolyDealLibrary.Player
    {
        IMonopolyDealCallback callbackInterface;

        bool ready = false;

        public WCFPlayer(String playerName, IMonopolyDealCallback cbi)
            : base(playerName)
        {
            callbackInterface = cbi;
        }

        public IMonopolyDealCallback getCallback()
        {
            return callbackInterface;
        }

        public bool getIfReady()
        {
            return ready;
        }

        public void setIfReady(bool isReady)
        {
            ready = isReady;
        }
    }
}