using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace MDWcfServiceLibrary
{
    [DataContract]
    public class PlayerModel
    {
        /// <summary>
        /// PlayerModel class models a single player in the game
        ///
        /// Chad Currie 28/03/2012
        /// </summary>
        ///

        #region Fields

        //Fields
        [DataMember]
        public PlayerHand hand;

        [DataMember]
        public PlayerBank bank;

        [DataMember]
        public PlayerPropertySets propertySets;

        [DataMember]
        public Guid guid;

        [DataMember]
        public int id;
        [DataMember]
        public bool isThisPlayersTurn;

        [DataMember]
        public String name;

        [DataMember]
        public List<TurnActionTypes> actionsCurrentlyAllowed;

        //[DataMember]
        //public IMonopolyDealCallback ICallBack;

        [DataMember]
        public bool isReadyToStartGame;

        [DataMember]
        public bool owesAnotherPlayer = false;
        [DataMember]
        public int amountOwedToAnotherPlayer = 0;

        [DataMember]
        //List<TurnActionTypes> actionsAllowableAtCurrentState;

        //StaticFields
        public static List<Guid> playerGuids = new List<Guid>();

        #endregion Fields

        #region Static Methods

        //Static Methods
        private static Guid generatePlayerGuid()
        {
            Guid newPlayerGuid = Guid.NewGuid();
            while (true)
            {
                bool existsAllready = false;
                foreach (Guid existingGuid in playerGuids)
                {
                    if (existingGuid.CompareTo(newPlayerGuid) == 0)
                    {
                        //guid exists
                        existsAllready = true;
                        break;
                    }
                }
                if (!existsAllready)
                {
                    return newPlayerGuid;
                }
                else
                {
                    newPlayerGuid = Guid.NewGuid();
                }
            }
        }

        #endregion Static Methods

        #region Constructors

        public PlayerModel(String nameP)
        {
            //Set ID
            guid = generatePlayerGuid();
            //Set name
            name = nameP;
            //Give emptyhand
            hand = new PlayerHand(guid, new List<Card>());
            //Give emptybank
            bank = new PlayerBank(guid, new List<Card>());
            //give emptypropertysets
            propertySets = new PlayerPropertySets(guid, new List<PropertyCardSet>());
            //Set not players turn
            isThisPlayersTurn = false;
            //notready to start game
            isReadyToStartGame = false;
        }

        //Clone Constructors
        public PlayerModel(PlayerModel player, PlayFieldModel state)
        {
            this.hand = new PlayerHand(player.hand, state);
            this.bank = new PlayerBank(player.bank, state);
            this.propertySets = new PlayerPropertySets(player.propertySets, state);
            this.guid = player.guid;
            this.id = player.id;
            this.isThisPlayersTurn = player.isThisPlayersTurn;
            this.name = player.name;
            this.actionsCurrentlyAllowed = player.actionsCurrentlyAllowed.cloneListTurnActionTypes();
            this.isReadyToStartGame = player.isReadyToStartGame;
            this.owesAnotherPlayer = player.owesAnotherPlayer;
            this.amountOwedToAnotherPlayer = player.amountOwedToAnotherPlayer;
        }

        #endregion Constructors

        #region Methods

        #endregion Methods

        internal PlayerModel clone(PlayFieldModel state)
        {
            return new PlayerModel(this, state);
        }

        internal PropertyCard removePropertyCardFromPlayersPropertySets(PropertyCard propertyCardToRemove)
        {
            foreach (PropertyCardSet ps in propertySets.playersPropertySets)
            {
                if (ps.removeProperty(propertyCardToRemove))
                {
                    //Card removed, remove any houses and hotels and place them in players bank
                    Card hotel = ps.removeHotel();
                    if (hotel != null)
                    {
                        bank.addCardToBank(hotel);
                    }
                    Card house = ps.removeHouse();
                    if (hotel != null)
                    {
                        bank.addCardToBank(house);
                    }
                    if (ps.properties.Count == 0)
                    {
                        propertySets.removeEmptySet(ps);
                    }
                    return propertyCardToRemove;
                }
            }
            //Card not removed
            return null;
        }

        internal PropertyCard removePropertyCardFromPlayersPropertySet(PropertyCard propertyCardToRemove, Guid propertySetToRemoveCardFromGuid)
        {
            foreach (PropertyCardSet ps in propertySets.playersPropertySets)
            {
                if (ps.guid.CompareTo(propertySetToRemoveCardFromGuid) == 0 && ps.removeProperty(propertyCardToRemove))
                {
                    //Card removed, remove any houses and hotels and place them in players bank
                    Card hotel = ps.removeHotel();
                    if (hotel != null)
                    {
                        bank.addCardToBank(hotel);
                    }
                    Card house = ps.removeHouse();
                    if (hotel != null)
                    {
                        bank.addCardToBank(house);
                    }
                    if (ps.properties.Count == 0)
                    {
                        propertySets.removeEmptySet(ps);
                    }
                    return propertyCardToRemove;
                }
            }
            //Card not removed
            return null;
        }
    }
}