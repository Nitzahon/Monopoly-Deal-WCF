using System;
using System.Collections.Generic;

namespace MDWcfServiceLibrary
{
    interface IMonopolyDeal_GameStateManager
    {
        bool bankCard(int playedCardID, Guid playerGuid, Guid serverGuid, Guid playfieldModelInstanceGuid);

        Card checkIfCardInHand(Card card, PlayerModel pm);

        PlayFieldModel copyPlayFieldModel(PlayFieldModel currentPlayFieldModel);

        BoolResponseBox drawTwoCardsAtTurnStart(Guid player, Guid stateGuid);

        BoolResponseBox drawFiveCards(Guid player, Guid stateGuid);

        BoolResponseBox endTurn(Guid player, Guid stateGuid);

        PlayFieldModel getCurrentPlayFieldModel();

        PlayFieldModel getCurrentState();

        PlayerModel getPlayerByGuid(Guid player, PlayFieldModel pfm);

        PlayerModel getPlayerModel(Guid playerGuid, Guid gameGuid, Guid currentPlayFieldModelGuid);

        PlayFieldModel getPlayFieldModelByGuid(Guid playFieldModelGuid);

        bool haveAllPlayersAcknowledgedCurrentState(Guid gameServiceInstanceGuid, Guid currentStateGuidP, List<PlayerModel> playersInGame);

        bool isActionAllowedForPlayer(TurnActionTypes turnActionToDo, Guid playerGuid, PlayFieldModel currentState);

        bool playPropertyCardToNewSet(Guid gameGuid, bool isOrientedUp, Guid playerGuid, Guid gameStateActionShouldBeAppliedOnGuid, TurnActionTypes actionType, int propertyCardID);

        bool discard(int cardsToDiscardID, Guid playerGuid, Guid serverGuid, Guid playfieldModelInstanceGuid);

        bool playActionCardPassGo(int passGoCardID, Guid serverGuid, Guid playerGuid, Guid playfieldModelInstanceGuid, TurnActionTypes turnActionTypes);

        bool playPropertyCardToExistingSet(Card playedCard, PropertyCardSet setToPlayPropertyTo, Guid gameLobbyGuid, Guid playerGuid, Guid playfieldModelInstanceGuid);

        bool playDebtCollector(int debtCollectorCardID, Guid targetedPlayerGuid, Guid playerGuid, Guid gameLobbyGuid, Guid playfieldModelInstanceGuid);

        bool payDebt(List<int> idOfCardsToPayWith, Guid playerGuid, Guid gameLobbyGuid, Guid playfieldModelInstanceGuid);

        bool playActionCardItsMyBirthday(int myBirthdayCardID, Guid playerGuid, Guid gameLobbyGuid, Guid playfieldModelInstanceGuid);

        bool playActionCardJustSayNo(int playedCard, Guid playerGuid, Guid gameLobbyGuid, Guid playfieldModelInstanceGuid);

        BoolResponseBox movePropertyCard(int propertyCardToMoveID, bool isCardUp, bool moveToNewEmptySet, Guid oldSetGuid, Guid setToPlayPropertyToGuid, Guid playerGuid, Guid gameLobbyGuid, Guid playfieldModelInstanceGuid);
    }
}