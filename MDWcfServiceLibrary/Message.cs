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
        notify,//Notify clients of event
        sendChat,//send chat
        recieveChat,
        connect,//Connect to service
        ready,//client is ready to start game
        gameStarted,//notify clients that game has started
        playcard,
    }

    [DataContract]
    public class Message
    {
        [DataMember]
        private Guid playerSendingMessage
        {
            get { return playerSendingMessage; }
            set { playerSendingMessage = value; }
        }

        [DataMember]
        private bool serviceSendingMessage
        {
            get { return serviceSendingMessage; }
            set { serviceSendingMessage = value; }
        }

        [DataMember]
        private Object[] objectsInMessage
        {
            get { return objectsInMessage; }
            set { objectsInMessage = value; }
        }

        [DataMember]
        private MessageType messageType
        {
            get { return messageType; }
            set { messageType = value; }
        }

        [DataMember]
        private Guid[] playersRecievingMessage
        {
            get { return playersRecievingMessage; }
            set { playersRecievingMessage = value; }
        }

        [DataMember]
        private System.Type type
        {
            get { return type; }
            set { type = value; }
        }

        [DataMember]
        private Guid thisMessageGuid
        {
            get { return thisMessageGuid; }
            set { thisMessageGuid = value; }
        }

        [DataMember]
        private Guid messageRespondingToGuid
        {
            get { return messageRespondingToGuid; }
            set { messageRespondingToGuid = value; }
        }

        public Message(Guid thisMessageGuidP, Type typeP, Guid messageRespondingToP, MessageType messageTypeP, Object[] messageObjectsP, bool serviceIsSenderP, Guid playerSendingP, Guid[] playersRecievingP)
        {
            //Type of message
            messageType = messageTypeP;
            type = typeP;
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