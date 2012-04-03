using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace MDWcfServiceLibrary
{
    [DataContract]
    public class Acknowledgement
    {
        [DataMember]
        Guid player; //Guid of player Acknowledging that they have recieved the gameStateinstance for the gameServiceInstance and their Guids
        [DataMember]
        Guid gameServiceInstance;
        [DataMember]
        Guid gameStateInstance;

        public bool equal(Guid gp, Guid gService, Guid gState)
        {
            if ((player.CompareTo(gp) == 0) && (gameServiceInstance.CompareTo(gService) == 0) && (gameStateInstance.CompareTo(gState) == 0))
            {
                return true;
            }
            return false;
        }
    }
}