using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace MDWcfServiceLibrary
{
    [DataContract]
    public enum Statephase
    {
        [EnumMember]
        Game_Started,
        [EnumMember]
        Turn_Started_Draw_2_Cards,
        [EnumMember]
        Turn_Started_Draw_5_Cards,

        [EnumMember]
        Turn_Started_Cards_Drawn_0_Cards_Played,//Card not played yet by player
        [EnumMember]
        Turn_Started_Cards_Drawn_0_Cards_Played_Ask_Just_Say_No,//Action Card played by player ask other player if they want to use a just say no
        [EnumMember]
        Turn_Started_Cards_Drawn_0_Cards_Played_Just_Say_No_Used_By_Oppostion_Ask_Player_On_Turn_Just_Say_No,//Action Card played by player, just say no used by oppostition player, ask player on turn if use just say no

        [EnumMember]
        Turn_Started_Cards_Drawn_1_Cards_Played,
        [EnumMember]
        Turn_Started_Cards_Drawn_1_Cards_Played_Ask_Just_Say_No,
        [EnumMember]
        Turn_Started_Cards_Drawn_1_Cards_Played_Just_Say_No_Used_By_Oppostion_Ask_Player_On_Turn_Just_Say_No,

        [EnumMember]
        Turn_Started_Cards_Drawn_2_Cards_Played,
        [EnumMember]
        Turn_Started_Cards_Drawn_2_Cards_Played_Ask_Just_Say_No,
        [EnumMember]
        Turn_Started_Cards_Drawn_2_Cards_Played_Just_Say_No_Used_By_Oppostion_Ask_Player_On_Turn_Just_Say_No,

        [EnumMember]
        Turn_Started_Cards_Drawn_3_Cards_Played_Swap_Properties_Or_End_Turn_Only,
        [EnumMember]
        Turn_Ended_7_Or_Less_Cards_In_Hand_Setup_NextPlayer,
        [EnumMember]
        Turn_Ended_8_Or_More_Cards_In_Hand_Discard_Cards,
        [EnumMember]
        Turn_Ended_8_Cards_In_Hand_Discard_1_Card,
        [EnumMember]
        Turn_Ended_9_Cards_In_Hand_Discard_2_Cards,
        [EnumMember]
        Turn_Ended_10_Cards_In_Hand_Discard_3_Cards,
        [EnumMember]
        Turn_Ended_11_Cards_In_Hand_Discard_4_Cards,
        [EnumMember]
        Turn_Ended_12_Cards_In_Hand_Discard_5_Cards,
    }

    [DataContract]
    public class PlayFieldModel
    {
        /// <summary>
        /// Models the playField at any given instance of the game
        /// </summary>
        [DataMember]
        public List<PlayerModel> playerModels;
        [DataMember]
        public List<Card> topCardsOnPlaypile;
        [DataMember]
        public Guid guidOfPlayerWhosTurnItIs;
        [DataMember]
        public int numberOfTurnsRemainingForPlayerWhosTurnItIs;
        [DataMember]
        public List<Guid> playersAffectedByActionCardGuids;
        [DataMember]
        public Guid thisPlayFieldModelInstanceGuid;
        [DataMember]
        public TurnActionModel lastActionPlayed;
        [DataMember]
        public bool startOfATurn;
        [DataMember]
        public TurnActionModel currentTurnActionModel;
        [DataMember]
        public Statephase currentPhase;
        //Service side only fields
        public DrawPile drawPile;
        public PlayPile playpile;
        //Guid generator
        public static List<Guid> playFieldModelGuids = new List<Guid>();

        //Static Methods
        public static Guid generateplayFieldModelGuid()
        {
            Guid newGuid = Guid.NewGuid();
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
                    newGuid = Guid.NewGuid();
                }
            }
        }

        public PlayFieldModel(Guid thisInstanceGuid, List<PlayerModel> playerModelsP, List<Card> topCardsPlayPileP, Guid guidOfPlayerWhosTurnItIsP,
            List<Guid> playersAffectedByActionCardGuidsP, TurnActionModel lastActionP, TurnActionModel nextActionP, DrawPile currentDrawPileState, PlayPile currentPlayPileState, int numberOfTurnsRemainingForPlayerP, bool startOfATurnP, Statephase phaseP)
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
            drawPile = currentDrawPileState;
            //The current State of the playpile
            playpile = currentPlayPileState;
            //The maximun number of cards that the player whos turn it is can play before their turn is over
            numberOfTurnsRemainingForPlayerWhosTurnItIs = numberOfTurnsRemainingForPlayerP;
            //This is the first move of a turn so the player whose turn it is should draw two cards
            startOfATurn = startOfATurnP;
            currentTurnActionModel = nextActionP;
            lastActionPlayed = lastActionP;
            //Current Phase
            currentPhase = phaseP;
        }
    }
}