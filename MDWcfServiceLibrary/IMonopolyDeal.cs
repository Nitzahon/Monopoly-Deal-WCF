using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace MDWcfServiceLibrary
{
    [ServiceContract(
    SessionMode = SessionMode.Required)] //Duplex mode specifing callback Contract
    interface IMonopolyDeal
    {
        // TODO: Add your service operations here
        //connect returns an int with the players id
        [OperationContract]
        Guid connectToService(string name); //connects Player To Service returns Guid for player

        [OperationContract]
        GuidBox startGame(GuidBox guidBoxed);//sets The player as ready to start game, when all players connected to the game have called this the game starts automatically

        //returns game guid
        //returns 0 guid if error

        [OperationContract]
        PlayFieldModel pollState(GuidBox playerGuid, GuidBox gameGuid);//returns current playfield model of game for player

        //Make a moves
        [OperationContract]//Returns false if move is not valid for player at current time
        bool draw2AtStartOfTurn(GuidBox playerGuid, GuidBox serverGuid, GuidBox playfieldModelInstanceGuid, GuidBox turnActionGuid);

        [OperationContract]//Player calls this method to play card from hand to bank, returns true if move valid and played and card should be removed from players hand, false if move not valid and card stays in players hand
        Boolean playCardFromHandToBank(PlayerModel player, Card playedCard, GuidBox playerGuid, GuidBox serverGuid, GuidBox playfieldModelInstanceGuid, GuidBox turnActionGuid);

        [OperationContract]//Player calls this method to play action card on their turn
        Boolean playActionCardOnTurn(PlayerModel player, Card playedCard, PlayerModel playerTargeted, List<Card> cardsTargeted, GuidBox playerGuid, GuidBox serverGuid, GuidBox playfieldModelInstanceGuid, GuidBox turnActionGuid);

        [OperationContract]//Player calls this method to play action card on their turn
        Boolean playWildRentActionCardOnTurn(PlayerModel player, Card playedCard, PlayerModel playerTargeted, PropertyCardSet setOfPropertiesToRentOn, GuidBox playerGuid, GuidBox serverGuid, GuidBox playfieldModelInstanceGuid, GuidBox turnActionGuid);

        [OperationContract]//Player calls this method to play action card on their turn
        Boolean playStandardRentActionCardOnTurn(PlayerModel player, Card playedCard, PropertyCardSet setOfPropertiesToRentOn, GuidBox playerGuid, GuidBox serverGuid, GuidBox playfieldModelInstanceGuid, GuidBox turnActionGuid);

        [OperationContract]//Player calls this method to play just say no card against action, returns true if card valid and action canceled
        Boolean playJustSayNo(PlayerModel player, Card playedCard, GuidBox playerGuid, GuidBox serverGuid, GuidBox playfieldModelInstanceGuid, GuidBox turnActionGuid);

        [OperationContract]//Player calls this method to play property to set
        Boolean playPropertyCard(PlayerModel player, Card playedCard, PropertyCardSet setToPlayPropertyTo, GuidBox playerGuid, GuidBox serverGuid, GuidBox playfieldModelInstanceGuid, GuidBox turnActionGuid);

        [OperationContract]//Player calls this method to move a property between sets, return true if card is valid for set and is added to the set
        Boolean movePropertyCard(PlayerModel player, Card propertyCard, PropertyCardSet oldSet, PropertyCardSet setToPlayPropertyTo, GuidBox playerGuid, GuidBox serverGuid, GuidBox playfieldModelInstanceGuid, GuidBox turnActionGuid);

        [OperationContract]//Player calls this method to give cards for payment, returns true if cards sufficent for payment, false if not
        Boolean payCards(PlayerModel playerPaying, PlayerModel playerRecieving, List<Card> cards, GuidBox playerGuid, GuidBox serverGuid, GuidBox playfieldModelInstanceGuid, GuidBox turnActionGuid);

        [OperationContract]//Player calls this method to end their turn, returns true to notify turn over completed with 7 or less cards, returns false requiring cards to be discarded
        Boolean endTurn(PlayerModel player, GuidBox playerGuid, GuidBox serverGuid, GuidBox playfieldModelInstanceGuid, GuidBox turnActionGuid);

        [OperationContract]//discard  cards at end of turn
        Boolean discard(PlayerModel player, Card[] cardsToDiscard, GuidBox playerGuid, GuidBox serverGuid, GuidBox playfieldModelInstanceGuid, GuidBox turnActionGuid);

        [OperationContract]//dummy method to ensure all data contracts are available to client
        void referenceAllDataContracts(ActionCard ac, Card c, FieldUpdateMessage fum, Message msg, MoneyCard mc, PlayerBank pb, PlayerHand ph, PlayerModel pm, PlayerPropertySets pps, PlayFieldModel pfm, PlayPile pp, PollForFieldUpdateMessage pffum, PropertyCard pc, PropertyCardSet pcs, PropertySetInfo psi, RentStandard rs, TakeActionOnTurnMessage taotm, TurnActionModel tam);
    }
}