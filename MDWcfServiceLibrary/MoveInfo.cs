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
        public Guid GuidOfGameOnService;

        public Guid GuidOfLobbyOnService;

        public Guid GuidOfStateRespondingTo; //The current state

        public Guid playerWhoseTurnItIs;

        public Guid playerMakingMove;

        public List<PlayerModel> playersAffectedByMove;

        public TurnActionTypes moveBeingMade;

        public ActionCardAction actionCardActionType;

        public Guid guidOfCardBeingUsed;

        public int idOfCardBeingUsed;

        //Pass Go does not need any extra info

        //Banking Cards do not need extra information

        //Drawing Cards do not need extra information

        //Ending turn and discarding cards

        public List<Guid> listOfGuidOfCardsPlayerIsDiscardingAtTheEndOfTheirTurn;

        //Adknowledgements do not need extra information

        //Sly Deal
        public Guid guidOfCardToBeSlyDealed;
        public Guid guidOfSetCardToBeSlyDealedIsIn;
        public Guid guidOfPlayerWhoIsBeingSlyDealed;
        public int idOfCardToBeSlyDealed;
        //Forced Deal
        public Guid guidOfCardToBeForcedDealed;
        public Guid guidOfSetCardToBeForcedDealedIsIn;
        public Guid guidOfPlayerWhoIsBeingForcedDealed;
        public int idOfCardToBeForcedDealed;
        //Forced Deal given up card
        public int idOfCardToBeGivenUpInForcedDeal;
        public Guid guidOfCardToBeGivenUpInForcedDeal;
        public Guid guidOfSetCardGivenUpInForcedDealIsIn;

        //Deal Breaker
        public Guid guidOfFullSetToBeDealBreakered;
        public Guid guidOfPlayerWhoIsBeingDealBreakered;

        //Debt Collector
        public Guid guidOfPlayerBeingDebtCollected;
        public Guid guidOfPlayerToPayDebtTo;
        //It's My Birthday
        public List<Guid> guidsOfPlayersWhoHaveToPayForBirthday;

        //MultiColoured Rent
        public Guid guidOfSetToCollectRentOnAgainstOnePlayer;
        public Guid guidOfPlayerToPayRent;

        //Standard2Colour Rent
        public Guid guidOfSetToCollectRentOn;
        public List<Guid> guidsOfPlayersWhoHaveToPayRent;

        //Double The Rent
        public bool isDoubleTheRentCardBeingUsed;
        public Guid guidOfDoubleTheRentCardBeingUsed;
        public int idOfDoubleTheRentCardBeingUsed;

        //House
        public Guid guidFullSetToAddHouseTo;

        //House
        public Guid guidFullSetWithHouseToAddHotelTo;

        //Move Property in set and set orientation
        public Guid guidOfSetPropertyToMoveIsIn;
        public Guid guidOfPropertyToMove;
        public bool isPropertyToMoveOrientedUp;
        public bool addPropertyToMoveToExistingSet;
        public Guid guidOfExistingSetToMovePropertyTo;

        //Play property from hand to a set
        public Guid guidOfPropertyToPlay;
        public bool isPropertyToPlayOrientedUp;
        public bool addPropertyToPlayToExistingSet;
        public Guid guidOfExistingSetToPlayPropertyTo;

        //Pay Debt
        public int amountOwed;
        public List<int> listOfIDsOfCardsBeingUsedToPayDebt;
    }
}