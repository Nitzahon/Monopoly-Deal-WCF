using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace MDWcfServiceLibrary
{
    [DataContract]
    [KnownType(typeof(DrawOnTurnStart))]
    public class TurnActionModel
    {
        [DataMember]
        public enum TurnActionTypes
        {
            [EnumMember]
            gameStarted,
            [EnumMember]
            turnStarted,
            [EnumMember]
            drawTwoCardsAtStartOfTurn,
            [EnumMember]
            EndTurn,
            [EnumMember]
            SwitchAroundPlayedProperties,
            [EnumMember]
            PlayCard,
            [EnumMember]
            PlayPropertyCard,
            [EnumMember]
            BankMoneyCard,
            [EnumMember]
            PlayActionCard,
            [EnumMember]
            BankActionCard,
            [EnumMember]
            PlayJustSayNo,
            [EnumMember]
            AdknowlegeRecievedCurrentState//used if no moves playable
        }

        [DataMember]
        int numberOfCardPlaysRemaining;
        [DataMember]
        TurnActionTypes turnActionType;
        [DataMember]
        List<Guid> playerGuids;
        [DataMember]
        Guid serverGuid;
        [DataMember]
        Guid currentPlayFieldModelGuid;
        [DataMember]
        Guid thisTurnactionGuid;
        [DataMember]
        List<TurnActionTypes> typesOfActionListedPlayersCanTake;

        public TurnActionModel(List<Guid> playerGuidsP, Guid serverGuidP, Guid currentPlayFieldModelGuidP, Guid guidOfThisTurnAction,
            List<TurnActionTypes> typesOfActionsAllowedForPlayersListed)
        {
            //GameStateManager generates a TurnActionModel for each PlayFieldModel
            //All players recieve the field and check if the turnActionModel has their guid
            //if it has their guid it is their turn
            //The player on their turn fills a subclass of TurnActionModel for the move they want to make using the information in this TurnActionModel
            //The player then sends that back to the service and GameStateManager does the move
            playerGuids = playerGuidsP;//List of players who can use the actions listed
            serverGuid = serverGuidP;
            currentPlayFieldModelGuid = currentPlayFieldModelGuidP;
            thisTurnactionGuid = guidOfThisTurnAction;//server side generated
            typesOfActionListedPlayersCanTake = typesOfActionsAllowedForPlayersListed;
        }
    }
}