using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MDWcfServiceLibrary
{
    internal class LobbyClient
    {
        private Guid guid;
        private Guid gameModelGuid;
        private bool readyToStart;
        private String name = "Player";

        public LobbyClient(Guid guidP)
        {
            this.guid = guidP;
            readyToStart = false;
            name = name + " " + getGuid().ToString();
        }

        public LobbyClient(Guid guidP, String nameP)
        {
            this.guid = guidP;
            readyToStart = false;
            name = nameP;
        }

        public Guid getLobbyClientGuid()
        {
            return guid;
        }

        public bool assignClientToGame(Guid gameGuidP)
        {
            if (gameModelGuid == null || gameModelGuid.CompareTo(new Guid()) == 0)
            {
                gameModelGuid = gameGuidP;
                return true;
            }
            else
            {
                //LobbyClient Allready assigned to a game
                return false;
            }
        }

        public bool disassignClientFromGame(Guid gameGuidP)
        {
            if (gameModelGuid.CompareTo(gameGuidP) == 0)
            {
                //Removing client from correct game
                //set guid to zero guid
                gameModelGuid = new Guid();
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
            gameModelGuid = new Guid();
            //Player can't be ready as no longer assigned to a game lobby
            setIfReadyToStart(false);
            return true;
        }

        public Guid getGuidOfGameAssignedTo()
        {
            return gameModelGuid;
        }

        public void setIfReadyToStart(bool readyP)
        {
            readyToStart = readyP;
        }

        public bool getIfReadyToStart()
        {
            return readyToStart;
        }

        public Guid getGuid()
        {
            return guid;
        }
    }
}