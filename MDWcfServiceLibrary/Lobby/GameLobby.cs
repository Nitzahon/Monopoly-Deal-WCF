using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace MDWcfServiceLibrary
{
    [DataContract]
    public enum GameLobbyStatus
    {
        [EnumMember]
        Empty,
        [EnumMember]
        Not_Enough_Players_To_Start,
        [EnumMember]
        Enough_Players_To_Start,
        [EnumMember]
        Full,
        [EnumMember]
        Game_In_Progress,
        [EnumMember]
        Game_Ended
    }

    internal class GameLobby
    {
        private List<LobbyClient> clientsConnectedToGame = new List<LobbyClient>();
        private Guid guid;
        private String gameLobbyName;
        private int id;
        private GameLobbyStatus status;

        #region static

        public static int nextLobbyID = 0;

        public static int getNewLobbyID()
        {
            int id = nextLobbyID;
            nextLobbyID++;
            return id;
        }

        #endregion static

        #region Constructors

        /// <summary>
        /// Creates an new LobbyGame with an initial client
        /// </summary>
        /// <param name="firstClient"></param>
        /// <param name="thisGameLobbyGuidP"></param>
        public GameLobby(LobbyClient firstClient, Guid thisGameLobbyGuidP)
        {
            this.guid = thisGameLobbyGuidP;
            id = getNewLobbyID();
            setLobbyName();
            setStatus(GameLobbyStatus.Empty);
            if (addClientToGame(firstClient))
            {
                setStatus(GameLobbyStatus.Not_Enough_Players_To_Start);
            }
        }

        public GameLobby(Guid guid)
        {
            this.guid = guid;
            id = getNewLobbyID();
            setLobbyName();
            setStatus(GameLobbyStatus.Empty);
        }

        #endregion Constructors

        #region Setters

        private void setStatus(GameLobbyStatus s)
        {
            status = s;
        }

        private void setLobbyName()
        {
            gameLobbyName = "Game Lobby: " + getLobbyID();
        }

        #endregion Setters

        #region Getters

        public int getLobbyID()
        {
            return id;
        }

        public string getLobbyName()
        {
            return gameLobbyName;
        }

        public List<LobbyClient> getListOfClients()
        {
            return clientsConnectedToGame;
        }

        public Guid getGameLobbyGuid()
        {
            return guid;
        }

        public GameLobbyStatus getStatus()
        {
            return status;
        }

        #endregion Getters

        #region Public_GameLobby_State_Changing_Methods

        /// <summary>
        /// Adds client to game
        /// </summary>
        /// <param name="client"></param>
        /// <returns>Returns true if client added. False if client is not added
        /// </returns>

        public bool addClientToGame(LobbyClient client)
        {
            if ((status.CompareTo(GameLobbyStatus.Full) == 0) ||
                (status.CompareTo(GameLobbyStatus.Game_In_Progress) == 0))
            {
                //Can't add client to this game
                return false;
            }
            else
            {
                clientsConnectedToGame.Add(client);
                //client added
                return true;
            }
        }

        public bool removeClientFromGame(LobbyClient client)
        {
            if ((status.CompareTo(GameLobbyStatus.Game_In_Progress) == 0))
            {
                //Can't remove client from this game
                return false;
            }
            else
            {
                if (clientsConnectedToGame.Remove(client))
                {
                    // client was in game and has been removed
                    return true;
                }
                else
                {
                    //client not removed
                    return false;
                }
            }
        }

        #endregion Public_GameLobby_State_Changing_Methods

        internal bool setClientReady(LobbyClient lc, bool readyP)
        {
            //Safety Check
            foreach (LobbyClient client in getListOfClients())
            {
                if (client.getLobbyClientGuid().CompareTo(lc.getLobbyClientGuid()) == 0)
                {
                    client.setIfReadyToStart(readyP);
                    return true;
                }
            }
            return false;
        }
    }
}