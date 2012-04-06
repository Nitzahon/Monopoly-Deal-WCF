using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MDWcfServiceLibrary
{
    public interface IGame
    {
        int getMinPlayersPerGame();

        int getMaxPlayersPerGame();

        bool startNewGame(List<LobbyClient> clients);
    }
}