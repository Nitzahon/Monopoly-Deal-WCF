using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace MDWcfServiceLibrary
{
    [DataContract]
    internal class BoolResponseBox
    {
        [DataMember]
        public bool success;
        [DataMember]
        public string message;

        public BoolResponseBox(bool successP)
        {
            success = successP;
        }

        public BoolResponseBox(bool successP, string messageP)
        {
            success = successP;
            message = messageP;
        }
    }
}