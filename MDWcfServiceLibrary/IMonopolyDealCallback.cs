using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace MDWcfServiceLibrary
{
    [ServiceContract]
    public interface IMonopolyDealCallback
    {
        //Implemented on client side

        //Server Notifications
        [OperationContract(IsOneWay = true)]
        void testOperationReturn();

        [OperationContract(IsOneWay = true)]
        void testOperationReturn2(string name);

        [OperationContract(IsOneWay = true)]
        void recieveGuid(Guid id);

        [OperationContract(IsOneWay = true)]
        void addToLog(String description);

        [OperationContract(IsOneWay = true)]
        void recieveChat(String description);

        [OperationContract(IsOneWay = true)]
        void recieveMessage(Message message);

        //Requests from server

        //Request Player Name as String
        [OperationContract(IsOneWay = true)] //string
        void getName();

        [OperationContract(IsOneWay = true)] //int
        void displayLookAtPlayedCardsOptions(Player currentPlayer, List<Player> players);

        [OperationContract(IsOneWay = true)]
        void displayBankedCards(Player player);

        [OperationContract(IsOneWay = true)]
        void displayPlayedProperties(Player player);

        [OperationContract(IsOneWay = true)]
        void displayLast3PlayedActionCards();

        [OperationContract(IsOneWay = true)]
        void displayCardsPlayedThisTurn();

        [OperationContract(IsOneWay = true)]
        void displayNumberOfCardsInPlayersHand();

        //Display all cards in players hand
        [OperationContract(IsOneWay = true)]
        void displayPlayerHand(Player player);

        [OperationContract(IsOneWay = true)]
        void displayCard(List<Card> cards);

        //Gives text describing action taken against you and asks if you wish to play a just say no card
        [OperationContract(IsOneWay = true)] //bool
        void askIfUsingJustSayNo(String text);

        [OperationContract(IsOneWay = true)]
        void displayListOfPlayersWithId();

        [OperationContract(IsOneWay = true)]//int
        void askWhoToDebtCollect();

        [OperationContract(IsOneWay = true)]//int
        void askWhoToRent();

        [OperationContract(IsOneWay = true)]//PropertyCardSet
        void askWhichSetToDealBreak();

        //display all sets which can be dealbreaked then ask player which one to dealbreak or cancel

        [OperationContract(IsOneWay = true)]
        void notifyTurnStarted();

        [OperationContract(IsOneWay = true)]
        void notifyOtherPlayerTurnStarted(Player p);

        /*Method for a player to make their moves on their turn.
         * Allows players to look at:
         * Players own hand
         * The number of cards in other players hands
         * the cards in any players bank
         * All players property sets
         * the last three action cards played to the play pile
        */

        [OperationContract(IsOneWay = true)]//turnoptions
        void playTurnAction();

        [OperationContract(IsOneWay = true)] //propertycardset
        void askWhatSetToAddHouseTo(Player p, Card c);

        [OperationContract(IsOneWay = true)] //propertycardset
        void askWhatSetToAddHotelTo(Player p, Card c);
    }
}