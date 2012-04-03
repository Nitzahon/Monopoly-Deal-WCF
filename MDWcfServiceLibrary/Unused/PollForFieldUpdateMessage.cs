using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace MDWcfServiceLibrary
{
    [DataContract]
    public class PollForFieldUpdateMessage : Message
    {
        [DataMember]
        PlayFieldModel fieldModel;
        [DataMember]
        Guid fieldModelGuid;
        [DataMember]
        Guid referenceThisGuidToRespond;
        [DataMember]
        TurnActionModel actionsToTake;

        public PollForFieldUpdateMessage(Guid playerSendingGuidP, Guid guidOfMessageThisMessageIsRespondingTo, Guid guidOfThisMessage,
            Guid serverGameGuidP, PlayFieldModel playFieldModelP, Guid playFieldModelGuidP, TurnActionModel turnActionModel)
            : base(guidOfThisMessage, guidOfMessageThisMessageIsRespondingTo, MessageType.pollForFieldUpdate, null, false,
            playerSendingGuidP, serverGameGuidP, null)
        {
            //Message used for requesting updates to the games state represented by PlayFieldModel
            fieldModel = playFieldModelP;

            //Guid to reference in response to this message
            referenceThisGuidToRespond = guidOfThisMessage;

            //Guid of the turn the action was taken on
            fieldModelGuid = playFieldModelGuidP;
            //Actions taken by client sending this message
            actionsToTake = turnActionModel;

            //Clients of players whose turn it is not should adknowledge they recieved this message and request again for an update of the playing field
            //Client of player whose turn it is should reply with an action
        }
    }
}