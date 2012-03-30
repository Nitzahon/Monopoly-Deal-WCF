using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MDWcfWFClient
{
    public static class MyExtensions
    {
        public static MonopolyDealServiceReference.GuidBox boxGuid(this Guid guid)
        {
            MonopolyDealServiceReference.GuidBox gb = new MonopolyDealServiceReference.GuidBox();
            gb.guid = guid;
            return gb;
        }
    }
}