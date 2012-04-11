using System;
using System.Collections.Generic;

namespace MDWcfServiceLibrary.Monopoly_Deal_Game
{
    interface IMonopolyDeal_GameStateManager
    {
        bool bankCard(int playedCardID, Guid playerGuid, Guid serverGuid, Guid playfieldModelInstanceGuid);

        Card checkIfCardInHand(Card card, PlayerModel pm);

        PlayFieldModel copyPlayFieldModel(PlayFieldModel currentPlayFieldModel);

        bool doAction(Guid gameGuid, Guid playerGuid, Guid gameStateActionShouldBeAppliedOnGuid, TurnActionTypes actionType);

        void drawFiveCards(PlayerModel player);

        void drawTwoCardsAtTurnStart(PlayerModel player);

        void endTurn(PlayerModel player);

        PlayFieldModel getCurrentPlayFieldModel();

        PlayFieldModel getCurrentState();

        PlayerModel getPlayerByGuid(Guid player, PlayFieldModel pfm);

        PlayerModel getPlayerModel(Guid playerGuid, Guid gameGuid, Guid currentPlayFieldModelGuid);

        PlayFieldModel getPlayFieldModelByGuid(Guid playFieldModelGuid);

        bool haveAllPlayersAcknowledgedCurrentState(Guid gameServiceInstanceGuid, Guid currentStateGuidP, List<PlayerModel> playersInGame);

        bool isActionAllowedForPlayer(TurnActionTypes turnActionToDo, Guid playerGuid, PlayFieldModel currentState);

        bool playPropertyCardToNewSet(Guid gameGuid, bool isOrientedUp, Guid playerGuid, Guid gameStateActionShouldBeAppliedOnGuid, TurnActionTypes actionType, int propertyCardID);
    }
}