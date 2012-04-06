using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace MDWcfServiceLibrary.Extensions
{
    [DataContract]
    internal class ObjectResponseBox
    {
        [DataMember]
        public Object response;
        [DataMember]
        public string message;
        [DataMember]
        public Type objectType;

        public ObjectResponseBox(Object response)
        {
            this.response = response;
        }

        public ObjectResponseBox(Object response, string messageP)
        {
            this.response = response;
            this.message = messageP;
        }

        public ObjectResponseBox(Object response, string messageP, Type objectType)
        {
            this.response = response;
            this.message = messageP;
            this.objectType = objectType;
        }
    }
}