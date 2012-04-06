using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace MDWcfServiceLibrary.Extensions
{
    [DataContract]
    internal class ResponseBox
    {
        [DataMember]
        public bool success;
        [DataMember]
        public string message;

        public ResponseBox(bool successP)
        {
            success = successP;
        }

        public ResponseBox(bool successP, string messageP)
        {
            success = successP;
            message = messageP;
        }
    }
}