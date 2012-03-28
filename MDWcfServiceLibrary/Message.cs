using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace MDWcfServiceLibrary
{
    [DataContract]
    public enum MessageType
    {
        notify,
        chat,
    }

    [DataContract]
    public class Message
    {
        [DataMember]
        Guid playerSendingMessage;
        [DataMember]
        bool serviceSendingMessage = false;
        [DataMember]
        Object[] objectsInMessage;
        [DataMember]
        MessageType messageType;
        [DataMember]
        Guid[] playersRecievingMessage;

        [DataMember]
        Guid thisMessageGuid;
        [DataMember]
        Guid messageRespondingToGuid;

        public Message(Guid thisMessageGuidP, Guid messageRespondingToP, MessageType messageTypeP, Object[] messageObjectsP, bool serviceIsSenderP, Guid playerSendingP, Guid[] playersRecievingP)
        {
            //Type of message
            this.messageType = messageTypeP;
            //Array containing objects in the message
            objectsInMessage = messageObjectsP;
            //boolean is true if the service is the sender of the message. false if a player is sending the message
            serviceSendingMessage = serviceIsSenderP;
            //Guid of player sending message if a player is sending message
            playerSendingMessage = playerSendingP;
            //Array containing Guids of players recieving the message
            playersRecievingMessage = playersRecievingP;
            //Guid for this message
            thisMessageGuid = thisMessageGuidP;
            //Guid of a message this message is responding to
            messageRespondingToGuid = messageRespondingToP;
        }
    }
}