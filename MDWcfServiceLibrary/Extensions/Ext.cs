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

        public static Guid cloneGuid(this Guid guid)
        {
            Guid gClone = new Guid(guid.ToString());
            return gClone;
        }

        public static List<Guid> cloneListGuids(this List<Guid> guidsListOld)
        {
            List<Guid> clonedGuidList = new List<Guid>();
            foreach (Guid g in guidsListOld)
            {
                Guid gc = g.cloneGuid();
                clonedGuidList.Add(gc);
            }
            return clonedGuidList;
        }

        public static bool cloneBool(this bool oldBool)
        {
            if (oldBool)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}