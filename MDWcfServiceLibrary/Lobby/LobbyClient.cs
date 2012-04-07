using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace MDWcfServiceLibrary
{
    [DataContract]
    public class LobbyClient
    {
        [DataMember]
        private Guid guid;
        [DataMember]
        private Guid gameLobbyGuid;
        [DataMember]
        private bool readyToStart;
        [DataMember]
        private String name = "Player";
        [DataMember]
        private String description = "Player";

        public LobbyClient(Guid guidP)
        {
            this.guid = guidP;
            readyToStart = false;
            name = name + " " + getGuid().ToString();
            setDescription();
        }

        public LobbyClient(Guid guidP, String nameP)
        {
            this.guid = guidP;
            readyToStart = false;
            name = nameP;
            setDescription();
        }

        public Guid getLobbyClientGuid()
        {
            return guid;
        }

        public bool assignClientToGameLobby(Guid gameLobbyGuidP)
        {
            if (gameLobbyGuid == null || gameLobbyGuid.CompareTo(new Guid()) == 0)
            {
                gameLobbyGuid = gameLobbyGuidP;
                return true;
            }
            else
            {
                //LobbyClient Allready assigned to a game
                return false;
            }
        }

        public bool disassignClientFromGame(Guid gameLobbyGuidP)
        {
            if (gameLobbyGuid.CompareTo(gameLobbyGuidP) == 0)
            {
                //Removing client from correct game
                //set guid to zero guid
                gameLobbyGuid = new Guid();
                //Player can't be ready as no longer assigned to a game lobby
                setIfReadyToStart(false);
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool disassignClientFromGame()
        {
            gameLobbyGuid = new Guid();
            //Player can't be ready as no longer assigned to a game lobby
            setIfReadyToStart(false);
            return true;
        }

        public Guid getGuidOfGameLobbyAssignedTo()
        {
            return gameLobbyGuid;
        }

        public void setIfReadyToStart(bool readyP)
        {
            readyToStart = readyP;
            setDescription();
        }

        public bool getIfReadyToStart()
        {
            return readyToStart;
        }

        public Guid getGuid()
        {
            return guid;
        }

        internal string getName()
        {
            return name;
        }

        private void setDescription()
        {
            if (readyToStart)
            {
                description = name + " Ready";
            }
            else
            {
                description = name + " Not Ready";
            }
        }
    }
}