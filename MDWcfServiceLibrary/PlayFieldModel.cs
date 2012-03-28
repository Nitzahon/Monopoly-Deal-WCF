using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace MDWcfServiceLibrary
{
    [DataContract]
    internal class PlayFieldModel
    {
        /// <summary>
        /// Models the playField at any given instance of the game
        /// </summary>
        [DataMember]
        List<PlayerModel> playerModels;
        [DataMember]
        List<Card> topCardsOnPlaypile;
        [DataMember]
        Guid guidOfPlayerWhosTurnItIs;
        [DataMember]
        int numberOfTurnsRemainingForPlayerWhosTurnItIs;
        [DataMember]
        List<Guid> playersAffectedByActionCardGuids;
        [DataMember]
        Guid thisPlayFieldModelInstanceGuid;
        [DataMember]
        TurnActionModel lastActionPlayed;

        //Service side only fields
        Deck deck;
        PlayPile playpile;
        //Guid generator
        public static List<Guid> playFieldModelGuids = new List<Guid>();

        //Static Methods
        private static Guid generateplayFieldModelGuid()
        {
            Guid newGuid = new Guid();
            while (true)
            {
                bool existsAllready = false;
                foreach (Guid existingGuid in playFieldModelGuids)
                {
                    if (existingGuid.CompareTo(newGuid) == 0)
                    {
                        //guid exists
                        existsAllready = true;
                        break;
                    }
                }
                if (!existsAllready)
                {
                    return newGuid;
                }
                else
                {
                    newGuid = new Guid();
                }
            }
        }

        public PlayFieldModel(Guid thisInstanceGuid, List<PlayerModel> playerModelsP, List<Card> topCardsPlayPileP, Guid guidOfPlayerWhosTurnItIsP,
            List<Guid> playersAffectedByActionCardGuidsP, TurnActionModel lastActionP, Deck currentDeckState, PlayPile currentPlayPileState, int numberOfTurnsRemainingForPlayerP)
        {
            //The guid of this instance of PlayFieldModel
            thisPlayFieldModelInstanceGuid = thisInstanceGuid;
            //The Players
            playerModels = playerModelsP;
            //The last 4 played cards
            topCardsOnPlaypile = topCardsPlayPileP;
            //The guid of the player whos turn it currently is
            guidOfPlayerWhosTurnItIs = guidOfPlayerWhosTurnItIsP;
            //A list of players who are affected by an action card that have just been played
            playersAffectedByActionCardGuids = playersAffectedByActionCardGuidsP;
            //The TurnActionModel of the last action
            lastActionPlayed = lastActionP;
            //The current State of the deck
            deck = currentDeckState;
            //The current State of the playpile
            playpile = currentPlayPileState;
            //The maximun number of cards that the player whos turn it is can play before their turn is over
            numberOfTurnsRemainingForPlayerWhosTurnItIs = numberOfTurnsRemainingForPlayerP;
        }
    }
}