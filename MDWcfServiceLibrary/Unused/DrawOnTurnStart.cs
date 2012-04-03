using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace MDWcfServiceLibrary
{
    [DataContract]
    public class DrawOnTurnStart : TurnActionModel
    {
        [DataMember]
        public bool b;

        public DrawOnTurnStart(TurnActionModel ta, PlayFieldModel pfm)
            : base(ta)
        {
        }
    }
}