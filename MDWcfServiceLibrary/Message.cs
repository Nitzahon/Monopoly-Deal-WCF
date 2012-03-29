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
        fieldupdate,//Server notifying of field updated
        actionTaken,//player notifing they took action on their turn
        pollForFieldUpdate
    }

    [DataContract]
    public class Message
    {
        [DataMember]
        public Guid playerSendingMessage
        {
            get { return playerSendingMessage; }
            set { playerSendingMessage = value; }
        }

        [DataMember]
        public bool serviceSendingMessage
        {
            get { return serviceSendingMessage; }
            set { serviceSendingMessage = value; }
        }

        [DataMember]
        public Object[] objectsInMessage
        {
            get { return objectsInMessage; }
            set { objectsInMessage = value; }
        }

        [DataMember]
        public MessageType messageType
        {
            get { return messageType; }
            set { messageType = value; }
        }

        [DataMember]
        public Guid[] playersRecievingMessage
        {
            get { return playersRecievingMessage; }
            set { playersRecievingMessage = value; }
        }

        [DataMember]
        public System.Type type
        {
            get { return type; }
            set { type = value; }
        }

        [DataMember]
        public Guid thisMessageGuid
        {
            get { return thisMessageGuid; }
            set { thisMessageGuid = value; }
        }

        [DataMember]
        public Guid serverGuid
        {
            get { return serverGuid; }
            set { thisMessageGuid = value; }
        }

        [DataMember]
        public Guid messageRespondingToGuid
        {
            get { return messageRespondingToGuid; }
            set { messageRespondingToGuid = value; }
        }

        public Message(Guid thisMessageGuidP, Guid messageRespondingToP, MessageType messageTypeP, Object[] messageObjectsP, bool serviceIsSenderP, Guid playerSendingP, Guid serverGameGuidP, Guid[] playersRecievingP)
        {
            //Type of message
            messageType = messageTypeP;
            //type = typeP;
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
            //Guid of the game on the server this is being sent to
        }
    }
}