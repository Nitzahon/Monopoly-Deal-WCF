using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MDWcfServiceLibrary
{
    internal class Lobby
    {
        /// <summary>
        /// Lobby is a singleton
        /// </summary>

        private List<GameStateManager> gameStateManagers = new List<GameStateManager>();
        private List<GameModel> gameModels = new List<GameModel>();
        private List<LobbyClient> lobbyClients = new List<LobbyClient>();
        private List<GameLobby> gameLobbys = new List<GameLobby>();

        #region Contructors

        public Lobby()
        {
        }

        #endregion Contructors

        private Guid generateGuid()
        {
            return Guid.NewGuid();
        }

        #region Private_Methods

        private GameLobby setupNewGameLobby()
        {
            GameLobby gl = new GameLobby(generateGuid());
            gameLobbys.Add(gl);
            return gl;
        }

        /// <summary>
        /// Creates a new LobbyClient and assigns it to the list of lobbyClients
        /// </summary>
        /// <returns>LobbyClient instance for client
        /// </returns>
        private LobbyClient setUpNewLobbyClient()
        {
            LobbyClient client = new LobbyClient(generateGuid());
            //Add client to the list of clients
            lobbyClients.Add(client);
            return client;
        }

        #endregion Private_Methods

        #region Public_Client_Methods

        /// <summary>
        /// Client calls connect to lobby to be assigned a Guid so it can join games
        /// </summary>
        /// <returns>Guid of client to be used in calls to service
        /// </returns>
        public Guid connectToLobby()
        {
            LobbyClient newClient = setUpNewLobbyClient();
            return newClient.getLobbyClientGuid();
        }

        /// <summary>
        /// Attempts to add a Client to a new Game Lobby
        ///
        /// </summary>
        /// <param name="gameLobbyGuidP"></param>
        /// <param name="clientGuidP"></param>
        /// <returns> returns the zero Guid if unsuccessful </returns>
        public Guid joinNewGameLobby(Guid clientGuidP)
        {
            GameLobby gl = setupNewGameLobby();
            if (joinExistingGameLobby(gl.getGameLobbyGuid(), clientGuidP))
            {
                //connected
                return gl.getGameLobbyGuid();
            }
            else
            {
                return new Guid();
            }
        }

        /// <summary>
        /// Attempts to add a Client to an Existing Game Lobby
        ///
        /// </summary>
        /// <param name="gameLobbyGuidP"></param>
        /// <param name="clientGuidP"></param>
        /// <returns>returns true if </returns>
        public bool joinExistingGameLobby(Guid gameLobbyGuidP, Guid clientGuidP)
        {
            GameLobby gl = getGameLobby(gameLobbyGuidP);
            LobbyClient lc = getLobbyClientByGuid(clientGuidP);
            if (gl.addClientToGame(lc))
            {
                //Client successfully added to GameLobby
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Used by a client to set if they are ready to start a game
        /// </summary>
        /// <param name="gameLobbyGuidP">game lobby client is connected to guid</param>
        /// <param name="clientGuidP">clients guid</param>
        /// <param name="readyP">true if ready, false if not</param>
        /// <returns>returns if method was successful</returns>
        public bool setLobbyClientReady(Guid gameLobbyGuidP, Guid clientGuidP, bool readyP)
        {
            GameLobby gl = getGameLobby(gameLobbyGuidP);
            LobbyClient lc = getLobbyClientByGuid(clientGuidP);
            bool success = gl.setClientReady(lc, readyP);
            return success;
        }

        #endregion Public_Client_Methods

        #region getters

        /// <summary>
        /// Gets an existing LobbyClient from its Guid
        /// </summary>
        /// <param name="clientGuidP"></param>
        /// <returns></returns>
        private LobbyClient getLobbyClientByGuid(Guid clientGuidP)
        {
            foreach (LobbyClient gc in getListOfLobbyClients())
            {
                if (gc.getLobbyClientGuid().CompareTo(clientGuidP) == 0)
                {
                    return gc;
                }
            }
            //Client not found
            return null;
        }

        /// <summary>
        /// Gets a list of all clients connected to service
        /// </summary>
        /// <returns></returns>
        private List<LobbyClient> getListOfLobbyClients()
        {
            return lobbyClients;
        }

        /// <summary>
        /// Gets the GameLobbyStatus of a gamelobby specified by guid
        /// </summary>
        /// <param name="gameLobbyGuidP"></param>
        /// <returns></returns>
        public GameLobbyStatus getGameLobbyStatus(Guid gameLobbyGuidP)
        {
            GameLobby gl = getGameLobby(gameLobbyGuidP);
            return gl.getStatus();
        }

        /// <summary>
        /// Gets an existing GameLobby from its Guid
        /// </summary>
        /// <param name="gameLobbyGuidP"></param>
        /// <returns>Returns GameLobby instance if found. returns null if not found</returns>
        public GameLobby getGameLobby(Guid gameLobbyGuidP)
        {
            foreach (GameLobby gl in getListOfAllGameLobbys())
            {
                if (gl.getGameLobbyGuid().CompareTo(gameLobbyGuidP) == 0)
                {
                    return gl;
                }
            }
            //Lobby not found
            return null;
        }

        /// <summary>
        /// Gets a list of all GameLobbys
        /// </summary>
        /// <returns></returns>
        public List<GameLobby> getListOfAllGameLobbys()
        {
            return gameLobbys;
        }

        #endregion getters
    }
}