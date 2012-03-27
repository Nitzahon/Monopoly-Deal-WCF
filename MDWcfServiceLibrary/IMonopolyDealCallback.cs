using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace MDWcfServiceLibrary
{
    [ServiceContract]
    interface IMonopolyDealCallback
    {
        //Implemented on client side
        [OperationContract(IsOneWay = true)]
        void testOperationReturn();

        [OperationContract(IsOneWay = true)]
        void testOperationReturn2(string name);

        [OperationContract(IsOneWay = true)]
        void recieveID(int id);

        [OperationContract(IsOneWay = true)]
        void addToLog(String description);

        [OperationContract(IsOneWay = true)]
        void recieveChat(String description);
    }
}