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

        public PlayerModel(/*IMonopolyDealCallback ICallBackP,*/ String nameP)
        {
            //Set ID
            guid = generatePlayerGuid();
            //Set Callback interface
            // ICallBack = ICallBackP;
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

        #endregion Constructors

        #region Methods

        #endregion Methods

        internal PlayerModel clone()
        {
            throw new NotImplementedException();
        }
    }
}