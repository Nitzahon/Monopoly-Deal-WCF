using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace MDWcfServiceLibrary
{
    internal class MoveInfo
    {
        public PlayerModel playerWhoseTurnItIs;

        public PlayerModel playerMakingMove;

        public List<PlayerModel> playersAffectedByMove;

        TurnActionTypes moveBeingMade;

        Guid guidOfActionCardBeingUsed;

        //Pass Go does not need any extra info

        //Banking Cards do not need extra information

        //Drawing Cards do not need extra information

        //Ending turn and discarding cards

        List<Guid> listOfGuidOfCardsPlayerIsDiscardingAtTheEndOfTheirTurn;

        //Adknowledgements do not need extra information

        //Sly Deal
        Guid guidOfCardToBeSlyDealed;
        Guid guidOfSetCardToBeSlyDealedIsIn;
        Guid guidOfPlayerWhoIsBeingSlyDealed;

        //Forced Deal
        Guid guidOfCardToBeForcedDealed;
        Guid guidOfSetCardToBeForcedDealedIsIn;
        Guid guidOfPlayerWhoIsBeingForcedDealed;
        //Forced Deal given up card
        Guid guidOfCardToBeGivenUpInForcedDeal;
        Guid guidOfSetCardGivenUpInForcedDealIsIn;

        //Deal Breaker
        Guid guidOfFullSetToBeDealBreakered;
        Guid guidOfPlayerWhoIsBeingDealBreakered;

        //Debt Collector
        Guid guidOfPlayerBeingDebtCollected;

        //It's My Birthday
        List<Guid> guidsOfPlayersWhoHaveToPayForBirthday;

        //MultiColoured Rent
        Guid guidOfSetToCollectRentOnAgainstOnePlayer;
        Guid guidOfPlayerToPayRent;

        //Standard2Colour Rent
        Guid guidOfSetToCollectRentOn;
        List<Guid> guidsOfPlayersWhoHaveToPayRent;

        //Double The Rent
        bool isDoubleTheRentCardBeingUsed;
        Guid guidOfDoubleTheRentCardBeingUsed;

        //House
        Guid guidFullSetToAddHouseTo;

        //House
        Guid guidFullSetWithHouseToAddHotelTo;

        //Move Property in set and set orientation
        Guid guidOfSetPropertyToMoveIsIn;
        Guid guidOfPropertyToMove;
        bool isPropertyToMoveOrientedUp;
        bool addPropertyToMoveToExistingSet;
        Guid guidOfExistingSetToMovePropertyTo;

        //Play property from hand to a set
        Guid guidOfPropertyToPlay;
        bool isPropertyToPlayOrientedUp;
        bool addPropertyToPlayToExistingSet;
        Guid guidOfExistingSetToPlayPropertyTo;
    }
}