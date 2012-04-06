using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MDWcfServiceLibrary
{
    interface ILobby
    {
        Guid connectToLobby(string name);

        GameLobbyStatus getGameLobbyStatus(Guid gameLobbyGuidP);

        List<GameLobby> getListOfAllGameLobbys();

        bool joinExistingGameLobby(Guid gameLobbyGuidP, Guid clientGuidP);

        Guid joinNewGameLobby(Guid clientGuidP);

        bool exitGameLobby(Guid clientGuidP);

        bool setLobbyClientReady(Guid gameLobbyGuidP, Guid clientGuidP, bool readyP);

        bool checkIfGameStarted(Guid gameLobbyGuidP);
    }
}