using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace MDWcfServiceLibrary
{
    /* Monopoly Deal Service
     * Client calls operations on the service to play the game
     *
     *
     *
     */
    //

    [ServiceContract(
    SessionMode = SessionMode.Required,
    CallbackContract = typeof(IMonopolyDealCallback))] //Duplex mode specifing callback Contract
    public interface IMonopolyDeal
    {
        [OperationContract]
        string GetData(int value);

        [OperationContract]
        CompositeType GetDataUsingDataContract(CompositeType composite);

        // TODO: Add your service operations here
        //connect returns an int with the players id
        [OperationContract(IsOneWay = true)]
        void connect(string name);

        [OperationContract(IsOneWay = true)]
        void startGame(int id);

        [OperationContract(IsOneWay = true)]
        void chatToAll(String chat);

        //Used for callbacks
        [OperationContract(IsOneWay = true)]
        void testOperation(int id);
    }

    // Use a data contract as illustrated in the sample below to add composite types to service operations
    [DataContract]
    public class CompositeType
    {
        bool boolValue = true;
        string stringValue = "Hello ";

        [DataMember]
        public bool BoolValue
        {
            get { return boolValue; }
            set { boolValue = value; }
        }

        [DataMember]
        public string StringValue
        {
            get { return stringValue; }
            set { stringValue = value; }
        }
    }
}