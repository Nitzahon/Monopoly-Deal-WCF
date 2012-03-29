using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace MDWcfServiceLibrary
{
    [DataContract]
    public class FieldUpdateMessage : Message
    {
        [DataMember]
        PlayFieldModel fieldModel;
        [DataMember]
        Guid referenceThisGuidToRespond;

        public FieldUpdateMessage(Guid guidOfMessageThisMessageIsRespondingTo, Guid guidOfThisMessage, Guid serverGuid, Guid[] playersRecieving, PlayFieldModel playFieldModelP)
            : base(guidOfThisMessage, guidOfMessageThisMessageIsRespondingTo, MessageType.fieldupdate, null, true, serverGuid, serverGuid, playersRecieving)
        {
            //Message used for notifying all players that the field has been updated
            fieldModel = playFieldModelP;
            referenceThisGuidToRespond = guidOfThisMessage;

            //Clients of players whose turn it is not should adknowledge they recieved this message and request again for an update of the playing field
            //Client of player whose turn it is should reply with an action
        }
    }
}