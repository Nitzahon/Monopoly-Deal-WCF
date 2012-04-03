using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MDWcfServiceLibrary
{
    public static class MyExtensions
    {
        public static GuidBox boxGuid(this Guid guid)
        {
            GuidBox gb = new GuidBox();
            gb.guid = guid;
            return gb;
        }
    }
}