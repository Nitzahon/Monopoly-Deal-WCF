using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MDWcfServiceLibrary
{
    internal class MessageManager
    {
        //Messages to be sent
        // Queue<Message> messagesQueued = new Queue<Message>();
        //Messages sent
        //  Queue<Message> messagesDequeued = new Queue<Message>();
        //Queue<Message> sentMessages = new Queue<Message>();
        Queue<Message> recievedMessages = new Queue<Message>();
        Message currentMessage = null;

        Message currentFieldStateMessage;
        //Each time a new message created by the service is sent the game should not make
        //changes to the current field state until all clients have adknowledged they recieved the current state and the
        //client whose turn it is has responded with a TurnActionModel
        List<Guid> listOfGuidsOfClientsWhoHaveRecievedTheCurrentFieldStateMessage = new List<Guid>();
        List<Guid> listOfAllClientsInGame;
        List<PlayerModel> players;
        GameModel gameModel;

        public MessageManager(GameModel gm)
        {
            players = gm.players;
            listOfAllClientsInGame = gm.playerIdLookup;
            gameModel = gm;
        }

        private PlayerModel getPlayerModelByGuid(Guid g)
        {
            //WCFMODELSUIT
            int id = listOfAllClientsInGame.IndexOf(g);
            return players.ElementAt(id);
        }

        private bool checkIfAllClientsHaveRespondedToCurrentFieldStateMessage(Guid messageGuid)
        {
            foreach (Guid clientGuid in listOfAllClientsInGame)
            {
                if (listOfAllClientsInGame.IndexOf(clientGuid) == -1)
                {
                    //Client not responded yet to message
                    return false;
                }
            }
            //All clients have responded to the current fieldStateMessage
            return true;
        }

        public static List<Guid> usedMessageGuids = new List<Guid>();

        public static Guid generateMessageGuid()
        {
            Guid newGuid = Guid.NewGuid();
            while (true)
            {
                bool existsAllready = false;
                foreach (Guid existingGuid in usedMessageGuids)
                {
                    if (existingGuid.CompareTo(newGuid) == 0)
                    {
                        //guid exists
                        existsAllready = true;
                        break;
                    }
                }
                if (!existsAllready)
                {
                    return newGuid;
                }
                else
                {
                    newGuid = Guid.NewGuid();
                }
            }
        }

        public void recieveNewMessage(Message newMessage)
        {
            if (newMessage is PollForFieldUpdateMessage)
            {
                PollForFieldUpdateMessage message = (PollForFieldUpdateMessage)newMessage;
                respondToFieldUpdate(message);
            }
        }

        public Guid generateGuidForMessage()
        {
            return Guid.NewGuid();
        }

        public void respondToFieldUpdate(PollForFieldUpdateMessage pfm)
        {
            //Get current field and return it to requesting client
            FieldUpdateMessage fum = new FieldUpdateMessage(pfm.thisMessageGuid, generateGuidForMessage()
            , gameModel.gameModelGuid, new Guid[] { pfm.playerSendingMessage }, gameModel.currentState);
            //return via duplex channel
            getPlayerModelByGuid(fum.playerSendingMessage).ICallBack.recieveMessage(fum);
        }

        public void sendMessage()
        {
        }

        public void sendMessage(List<PlayerModel> players)
        {
        }

        private void addMessageToSendQueue(Message message)
        {
        }
    }
}