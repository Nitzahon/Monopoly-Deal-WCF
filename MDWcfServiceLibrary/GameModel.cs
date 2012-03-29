using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace MDWcfServiceLibrary
{
    internal class GameModel
    {
        MessageManager messageManager;
        public List<PlayFieldModel> gameStates = new List<PlayFieldModel>();
        PlayFieldModel initialState;
        public PlayFieldModel currentState;
        PlayPile initialPlayPile;
        public List<PlayerModel> players;
        public List<Guid> playerIdLookup = new List<Guid>();
        int currentPlayerTurn;
        int FIRST_PLAYER = 0;
        bool gameOver = false;

        public Guid gameModelGuid;

        //Options
        int NUMBER_OF_DECKS = 1;
        int NEW_TURN_NUMBER_OF_CARDS_PLAYABLE = 3;

        public GameModel(List<PlayerModel> playersP, MessageManager messageManagerP)
        {
            //give this a guid
            gameModelGuid = Guid.NewGuid();
            //Assign Players to the game
            players = playersP;
            //Create idLookup
            foreach (PlayerModel pm in players)
            {
                playerIdLookup.Add(pm.guid);
            }
            initialState = createInitialState(players);
            gameStates.Add(initialState);
            messageManager = messageManagerP;
            //State added to gameStates list, notify all players, wait for responses
        }

        private PlayFieldModel createInitialState(List<PlayerModel> players)
        {
            //guid for initial state
            Guid playFieldModelGuid = PlayFieldModel.generateplayFieldModelGuid();
            //no cards to be shown as played in playpile
            List<Card> emptyTopPlayPile = new List<Card>();
            //set player 0 to be first to play
            Guid firstPlayerGuid = players.ElementAt(FIRST_PLAYER).guid;
            currentPlayerTurn = FIRST_PLAYER;
            //no players can be affected by actioncards as none have been played
            List<Guid> noPlayersAffectedByActionCard = new List<Guid>();
            //No actions have been taken
            TurnActionModel noActionsPlayed = new TurnActionModel();
            //create empty playpile
            initialPlayPile = new PlayPile();
            //fill  new drawpile
            DrawPile initialDrawPile = generateInitialDrawPile(initialPlayPile);
            //Deal players thier first five cards
            dealPlayersInitialFiveCards(players, initialDrawPile);
            //It is the start of a players turn
            bool turnStart = true;
            //put it all into the intial state
            PlayFieldModel state = new PlayFieldModel(playFieldModelGuid, players, emptyTopPlayPile, firstPlayerGuid, noPlayersAffectedByActionCard,
                noActionsPlayed, initialDrawPile, initialPlayPile, NEW_TURN_NUMBER_OF_CARDS_PLAYABLE, turnStart);
            //stateCreated
            currentState = state;
            return state;
        }

        private DrawPile generateInitialDrawPile(PlayPile pp)
        {
            //ShuffleDeck
            Deck deck = new Deck(NUMBER_OF_DECKS);
            DrawPile dp = new DrawPile(deck.getDeck(), pp);
            return dp;
        }

        private void dealPlayersInitialFiveCards(List<PlayerModel> players, DrawPile drawPile)
        {
            //PreCondition drawPile is Full and each player.hand contains no Cards
            //PostCondition each player.hand contains 5 Cards
            for (int i = 0; i < 5; i++)
            {
                foreach (PlayerModel player in players)
                {
                    Card drawnCard = drawPile.drawcard();
                    player.hand.addCardToHand(drawnCard);
                }
            }
        }
    }
}