using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MDWcfServiceLibrary
{
    public class CardIDSetGuid
    {
        public int cardID;
        public Guid setGuid;

        public CardIDSetGuid(int cardIDP, Guid setGuidP)
        {
            cardID = cardIDP;
            setGuid = setGuidP;
        }
    }
}