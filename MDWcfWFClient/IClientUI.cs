using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MDWcfWFClient
{
    interface IClientUI
    {
        // public void displayThisClientHand(MonopolyDealServiceReference.);
        void drawField(MonopolyDealServiceReference.PlayFieldModel pfm);
    }
}