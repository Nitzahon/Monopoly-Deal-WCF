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
        [EnumMember]
        notify,//Notify clients of event
        [EnumMember]
        sendChat,//send chat
        [EnumMember]
        recieveChat,
        [EnumMember]
        connect,//Connect to service
        [EnumMember]
        ready,//client is ready to start game
        [EnumMember]
        gameStarted,//notify clients that game has started
        [EnumMember]
        playcard,
        [EnumMember]
        fieldupdate,//Server notifying of field updated
        [EnumMember]
        actionTaken,//player notifing they took action on their turn
        [EnumMember]
        pollForFieldUpdate
    }

    [DataContract]
    [KnownType(typeof(PollForFieldUpdateMessage))]
    [KnownType(typeof(TakeActionOnTurnMessage))]
    [KnownType(typeof(FieldUpdateMessage))]
    public class Message
    {
        [DataMember]
        public Guid playerSendingMessage;

        [DataMember]
        public bool serviceSendingMessage;

        [DataMember]
        public Object[] objectsInMessage;

        [DataMember]
        public MessageType messageType;

        [DataMember]
        public Guid[] playersRecievingMessage;

        [DataMember]
        public System.Type type;

        [DataMember]
        public Guid thisMessageGuid;

        [DataMember]
        public Guid serverGuid;

        [DataMember]
        public Guid messageRespondingToGuid;

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