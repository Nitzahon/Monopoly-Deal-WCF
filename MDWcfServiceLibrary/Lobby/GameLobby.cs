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
        overFull,
        [EnumMember]
        Game_In_Progress,
        [EnumMember]
        Game_Ended
    }

    [DataContract]
    public class GameLobby
    {
        [DataMember]
        public List<LobbyClient> clientsConnectedToGame = new List<LobbyClient>();
        [DataMember]
        public Guid guid;
        [DataMember]
        public String gameLobbyName;
        [DataMember]
        public int id;
        [DataMember]
        public GameLobbyStatus status;
        [DataMember]
        public String description;
        //private MonopolyDeal monopolyDealGame;

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
            //monopolyDealGame = monopolyDealGameP;
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

        internal void setStatus(GameLobbyStatus s)
        {
            status = s;
            setDescription();
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

        private void numberOfClientsInGameLobbyChanged()
        {
            if (clientsConnectedToGame.Count > MonopolyDeal.getMaxPlayers())
            {
                setStatus(GameLobbyStatus.overFull);
                //To many players to start game
            }
            else if (clientsConnectedToGame.Count == MonopolyDeal.getMaxPlayers())
            {
                setStatus(GameLobbyStatus.Full);
            }
            else if (clientsConnectedToGame.Count == 0)
            {
                setStatus(GameLobbyStatus.Empty);
            }
            else if (clientsConnectedToGame.Count == 1)
            {
                setStatus(GameLobbyStatus.Not_Enough_Players_To_Start);
            }
            else if (clientsConnectedToGame.Count >= MonopolyDeal.getMinPlayers())
            {
                setStatus(GameLobbyStatus.Enough_Players_To_Start);
            }
        }

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
                client.assignClientToGameLobby(getGameLobbyGuid());
                numberOfClientsInGameLobbyChanged();
                //client added
                return true;
            }
        }

        public bool removeClientFromGame(LobbyClient client)
        {
            if ((status.CompareTo(GameLobbyStatus.Game_In_Progress) == 0))
            {
                //Can't remove client from this game game in progress
                return false;
            }
            else
            {
                if (clientsConnectedToGame.Remove(client) && client.disassignClientFromGame())
                {
                    // client was in game and has been removed
                    numberOfClientsInGameLobbyChanged();
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

        public bool areAllClientsReady()
        {
            foreach (LobbyClient client in getListOfClients())
            {
                if (!(client.getIfReadyToStart()))
                {
                    //A client is not ready
                    return false;
                }
            }
            //All clients are ready
            return true;
        }

        public void setDescription()
        {
            description = gameLobbyName + " Status:" + status.ToString();
        }
    }
}