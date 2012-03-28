using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace MDWcfServiceLibrary
{
    [DataContract]
    internal class PlayerModel
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
        PlayerHand hand;
        [DataMember]
        PlayerBank bank;
        [DataMember]
        PlayerPropertySets propertySets;
        [DataMember]
        Guid guid;
        [DataMember]
        int id;
        [DataMember]
        bool isThisPlayersTurn;

        //StaticFields
        public static List<Guid> playerGuids = new List<Guid>();

        #endregion Fields

        #region Static Methods

        //Static Methods
        private static Guid generatePlayerGuid()
        {
            Guid newPlayerGuid = new Guid();
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
                    newPlayerGuid = new Guid();
                }
            }
        }

        #endregion Static Methods

        #region Constructors

        public PlayerModel()
        {
        }

        #endregion Constructors

        #region Methods

        #endregion Methods
    }
}