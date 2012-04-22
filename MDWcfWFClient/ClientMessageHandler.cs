using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace MDWcfWFClient
{
    internal class ClientMessageHandler
    {
        SynchronizationContext uiSync;
        MonopolyDealServiceReference.MonopolyDealClient monopolyDealService;
        Form1 mainForm;
        ClientInfo clientInfo;
        Guid clientGuid;
        int clientID;
        public MonopolyDealServiceReference.PlayFieldModel playFieldModel;

        public ClientMessageHandler(SynchronizationContext uiSyncP, Form1 form1P, Guid guid)
        {
            uiSync = uiSyncP;
            mainForm = form1P;
            clientGuid = guid;
        }

        /*
        public void processMessage(MonopolyDealServiceReference.Message message)
        {
            //INCOMPLETE
            if (message is MonopolyDealServiceReference.FieldUpdateMessage)
            {
                MonopolyDealServiceReference.FieldUpdateMessage messageFM = (MonopolyDealServiceReference.FieldUpdateMessage)message;
                displayField(messageFM.fieldModel);
                savefield(messageFM.fieldModel);
            }
        }
        */

        public void savefield(MonopolyDealServiceReference.PlayFieldModel pfm)
        {
            this.playFieldModel = pfm;
        }

        public void checkIfClientTurn(MonopolyDealServiceReference.PlayFieldModel pfm)
        {
            //MonopolyDealServiceReference.TurnActionModel tam = pfm.
        }

        public void displayField(MonopolyDealServiceReference.PlayFieldModel pfm)
        {
            displayHandsBanksPropertySets(pfm);
            //Check if you can take an action

            //Display anyActions taken last turn
            //Display the whose turn it is
            //prevent players whose turn is is not from doing anything but watch
            //Present the player whose turn it is with the actions they can take this turn
        }

        private void displayHandsBanksPropertySets(MonopolyDealServiceReference.PlayFieldModel pfm)
        {
            // The UI thread won't be handling the callback, but it is the only one allowed to update the controls.
            // So, we will dispatch the UI update back to the UI sync context.

            //Create a SendOrPostCallback delegate with an anon method which recieves an Object called state and runs code in the SychronisationContext it is marshalled to using Post
            SendOrPostCallback callback =
                delegate(object state)
                {
                    mainForm.drawField((MonopolyDealServiceReference.PlayFieldModel)state);
                    //state is a string object example
                    //mainForm.updateTextBoxLog(state.ToString());
                };
            //Post takes a delagate and a State object and runs the delegate(State object) in the context Post is called on.
            uiSync.Post(callback, pfm);
        }
    }
}