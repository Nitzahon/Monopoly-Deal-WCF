using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace MDWcfServiceLibrary
{
    [DataContract]
    public class GuidBox
    {
        [DataMember]
        public Guid guid;
        [DataMember]
        public Guid guid2;
        [DataMember]
        public bool bool1;
        [DataMember]
        public string message;
    }
}