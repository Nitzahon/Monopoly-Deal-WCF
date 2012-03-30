using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace MDWcfWFClient
{
    public partial class Form1 : Form, IClientUI
    {
        //Callback
        private SynchronizationContext _uiSyncContext = null;
        MonopolyDealServiceReference.MonopolyDealClient monopolyDealService = null;
        //
        RequestHandler requestHandler;
        Guid thisPlayerGuid;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Capture the UI synchronization context
            _uiSyncContext = SynchronizationContext.Current;
            //rqh
            requestHandler = new RequestHandler(_uiSyncContext, this);
        }

        /*
        //IMonopolyDealCallback implementation
        public void testOperationReturn2(string name)
        {
            // The UI thread won't be handling the callback, but it is the only one allowed to update the controls.
            // So, we will dispatch the UI update back to the UI sync context.
            SendOrPostCallback callback =
                delegate(object state)
                { this.showMessage(state.ToString()); };

            _uiSyncContext.Post(callback, name);
        }

        //WCF Callback
        public void addToLog(string description)
        {
            // The UI thread won't be handling the callback, but it is the only one allowed to update the controls.
            // So, we will dispatch the UI update back to the UI sync context.

            //Create a SendOrPostCallback delegate with an anon method which recieves an Object called state and runs code in the SychronisationContext it is marshalled to using Post
            SendOrPostCallback callback =
                delegate(object state)
                {
                    this.updateTextBoxLog(state.ToString());
                };
            //Post takes a delagate and a State object and runs the delegate(State object) in the context Post is called on.
            _uiSyncContext.Post(callback, description);
        }
        */

        //UI Thread
        public void updateTextBoxLog(String description)
        {
            textBoxLog.Text = textBoxLog.Text + Environment.NewLine + description;
        }

        //UI
        public void recieveID(Guid id)
        {
            thisPlayerGuid = id;
        }

        //UI
        public void showMessage(string msg)
        {
            MessageBox.Show(msg);
        }

        //UI
        private void button1_Click(object sender, EventArgs e)
        {
            requestHandler.connect();
            /*
            //Opens Connection to service
            try
            {
                // The client callback interface must be hosted for the server to invoke the callback
                // Open a connection to the Monopoly Deal service via the proxy
                monopolyDealService = new MonopolyDealServiceReference.MonopolyDealClient(new InstanceContext(this), "TcpBinding");
                monopolyDealService.Open();
                //End

                //Connect to service with player name
                monopolyDealService.connect(textBoxPlayerName.Text);

                //Disable Connect button
                buttonConnect.Enabled = false;
            }
            catch (Exception r)
            {
                MessageBox.Show(r.ToString());
                buttonConnect.Enabled = true;
            }
             * */
        }

        /*
        public void testOperationReturn()
        {
            MessageBox.Show("Connected");
        }
        */

        private void button2_Click(object sender, EventArgs e)
        {
            requestHandler.startGame();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }

        private void buttonDisconnect_Click(object sender, EventArgs e)
        {
        }

        /*
        public void recieveChat(string description)
        {
            // The UI thread won't be handling the callback, but it is the only one allowed to update the controls.
            // So, we will dispatch the UI update back to the UI sync context.
            SendOrPostCallback callback =
                delegate(object state)
                { this.updateChatTextBox(state.ToString()); };

            _uiSyncContext.Post(callback, description);
        }
        */

        //UI Thread
        internal void updateChatTextBox(String description)
        {
            textBoxLog.Text = textBoxLog.Text + Environment.NewLine + description;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
        }

        private void buttonChat_Click(object sender, EventArgs e)
        {
        }

        //public void displayThisClientHand(MonopolyDealServiceReference. player)
        // {
        //player
        //throw private new NotImplementedException();

        // }

        public void drawField(MonopolyDealServiceReference.PlayFieldModel pfm)
        {
            textBoxHand1.Text = "";
            //Player0
            int numPlayers = pfm.playerModels.Count();

            switch (numPlayers)
            {
                case 0:
                    break;
                case 1:
                    drawPlayer0(pfm);
                    break;
                case 2:
                    drawPlayer0(pfm);
                    drawPlayer1(pfm);
                    break;
                case 3:
                    drawPlayer0(pfm);
                    drawPlayer1(pfm);
                    drawPlayer2(pfm);
                    break;
                case 4:

                    drawPlayer0(pfm);
                    drawPlayer1(pfm);
                    drawPlayer2(pfm);
                    drawPlayer3(pfm);
                    break;
                case 5:
                    drawPlayer0(pfm);
                    drawPlayer1(pfm);
                    drawPlayer2(pfm);
                    drawPlayer3(pfm);
                    drawPlayer4(pfm);
                    break;
            }
        }

        public MDWcfServiceLibrary.CardType cardTypeConvert(MonopolyDealServiceReference.Card card)
        {
            if (card.cardType.ToString() == MDWcfServiceLibrary.CardType.Action.ToString())
            {
                return MDWcfServiceLibrary.CardType.Action;
            }
            else if (card.cardType.ToString() == MDWcfServiceLibrary.CardType.Money.ToString())
            {
                return MDWcfServiceLibrary.CardType.Money;
            }
            else if (card.cardType.ToString() == MDWcfServiceLibrary.CardType.Property.ToString())
            {
                return MDWcfServiceLibrary.CardType.Property;
            }
            return MDWcfServiceLibrary.CardType.WildProperty;
        }

        public void popluateHandListBox(MonopolyDealServiceReference.Card card)
        {
            MDWcfServiceLibrary.Card c = new MDWcfServiceLibrary.Card(card.cardName, card.cardText, card.cardValue, cardTypeConvert(card));
            listBox1.Items.Add(card.description);
        }

        public void drawPlayer0(MonopolyDealServiceReference.PlayFieldModel pfm)
        {
            textBoxHand1.Text = "";
            textBoxBank1.Text = "";
            textBoxProp1.Text = "";

            foreach (MonopolyDealServiceReference.Card card in pfm.playerModels[0].hand.cardsInHand)
            {
                textBoxHand1.Text = textBoxHand1.Text + "ID:" + card.cardID + " " + card.cardName + " $" + card.cardValue + Environment.NewLine;

                if (thisPlayerGuid.CompareTo(pfm.playerModels[0].guid) == 0)
                {
                    updateTextBoxLog(card.ToString());
                    popluateHandListBox(card);
                }
            }
            foreach (MonopolyDealServiceReference.Card card in pfm.playerModels[0].bank.cardsInBank)
            {
                textBoxBank1.Text = textBoxBank1.Text + "ID:" + card.cardID + " " + card.cardName + " $" + card.cardValue + Environment.NewLine;
            }
            foreach (MonopolyDealServiceReference.PropertyCardSet cardSet in pfm.playerModels[0].propertySets.playersPropertySets)
            {
                foreach (MonopolyDealServiceReference.Card card in cardSet.properties)
                {
                    textBoxProp1.Text = textBoxProp1.Text + "ID:" + card.cardID + " " + card.cardName + " $" + card.cardValue + Environment.NewLine;
                }
            }
        }

        public void setGuid(Guid id)
        {
            thisPlayerGuid = id;
        }

        public void drawPlayer1(MonopolyDealServiceReference.PlayFieldModel pfm)
        {
            textBoxHand2.Text = "";
            textBoxBank2.Text = "";
            textBoxProp2.Text = "";
            foreach (MonopolyDealServiceReference.Card card in pfm.playerModels[1].hand.cardsInHand)
            {
                textBoxHand2.Text = textBoxHand2.Text + "ID:" + card.cardID + " " + card.cardName + " $" + card.cardValue + Environment.NewLine;
                if (thisPlayerGuid.CompareTo(pfm.playerModels[1].guid) == 0)
                {
                    updateTextBoxLog(card.ToString());
                    popluateHandListBox(card);
                }
            }
            foreach (MonopolyDealServiceReference.Card card in pfm.playerModels[1].bank.cardsInBank)
            {
                textBoxBank2.Text = textBoxBank2.Text + "ID:" + card.cardID + " " + card.cardName + " $" + card.cardValue + Environment.NewLine;
            }
            foreach (MonopolyDealServiceReference.PropertyCardSet cardSet in pfm.playerModels[1].propertySets.playersPropertySets)
            {
                foreach (MonopolyDealServiceReference.Card card in cardSet.properties)
                {
                    textBoxProp2.Text = textBoxProp2.Text + "ID:" + card.cardID + " " + card.cardName + " $" + card.cardValue + Environment.NewLine;
                }
            }
        }

        public void drawPlayer2(MonopolyDealServiceReference.PlayFieldModel pfm)
        {
            textBoxHand3.Text = "";
            textBoxBank3.Text = "";
            textBoxProp3.Text = "";
            foreach (MonopolyDealServiceReference.Card card in pfm.playerModels[2].hand.cardsInHand)
            {
                textBoxHand3.Text = textBoxHand3.Text + "ID:" + card.cardID + " " + card.cardName + " $" + card.cardValue + Environment.NewLine;
                if (thisPlayerGuid.CompareTo(pfm.playerModels[2].guid) == 0)
                {
                    updateTextBoxLog(card.ToString());
                    popluateHandListBox(card);
                }
            }
            foreach (MonopolyDealServiceReference.Card card in pfm.playerModels[2].bank.cardsInBank)
            {
                textBoxBank3.Text = textBoxBank3.Text + "ID:" + card.cardID + " " + card.cardName + " $" + card.cardValue + Environment.NewLine;
            }
            foreach (MonopolyDealServiceReference.PropertyCardSet cardSet in pfm.playerModels[2].propertySets.playersPropertySets)
            {
                foreach (MonopolyDealServiceReference.Card card in cardSet.properties)
                {
                    textBoxProp3.Text = textBoxProp3.Text + "ID:" + card.cardID + " " + card.cardName + " $" + card.cardValue + Environment.NewLine;
                }
            }
        }

        public void drawPlayer3(MonopolyDealServiceReference.PlayFieldModel pfm)
        {
            textBoxHand4.Text = "";
            textBoxBank4.Text = "";
            textBoxProp4.Text = "";
            foreach (MonopolyDealServiceReference.Card card in pfm.playerModels[3].hand.cardsInHand)
            {
                textBoxHand4.Text = textBoxHand4.Text + "ID:" + card.cardID + " " + card.cardName + " $" + card.cardValue + Environment.NewLine;
                if (thisPlayerGuid.CompareTo(pfm.playerModels[3].guid) == 0)
                {
                    updateTextBoxLog(card.ToString());
                    popluateHandListBox(card);
                }
            }
            foreach (MonopolyDealServiceReference.Card card in pfm.playerModels[3].bank.cardsInBank)
            {
                textBoxBank4.Text = textBoxBank4.Text + "ID:" + card.cardID + " " + card.cardName + " $" + card.cardValue + Environment.NewLine;
            }
            foreach (MonopolyDealServiceReference.PropertyCardSet cardSet in pfm.playerModels[3].propertySets.playersPropertySets)
            {
                foreach (MonopolyDealServiceReference.Card card in cardSet.properties)
                {
                    textBoxProp4.Text = textBoxProp4.Text + "ID:" + card.cardID + " " + card.cardName + " $" + card.cardValue + Environment.NewLine;
                }
            }
        }

        public void drawPlayer4(MonopolyDealServiceReference.PlayFieldModel pfm)
        {
            textBoxHand5.Text = "";
            textBoxBank5.Text = "";
            textBoxProp5.Text = "";
            foreach (MonopolyDealServiceReference.Card card in pfm.playerModels[4].hand.cardsInHand)
            {
                textBoxHand5.Text = textBoxHand5.Text + "ID:" + card.cardID + " " + card.cardName + " $" + card.cardValue + Environment.NewLine;
                if (thisPlayerGuid.CompareTo(pfm.playerModels[4].guid) == 0)
                {
                    updateTextBoxLog(card.ToString());
                    popluateHandListBox(card);
                }
            }
            foreach (MonopolyDealServiceReference.Card card in pfm.playerModels[4].bank.cardsInBank)
            {
                textBoxBank5.Text = textBoxBank5.Text + "ID:" + card.cardID + " " + card.cardName + " $" + card.cardValue + Environment.NewLine;
            }
            foreach (MonopolyDealServiceReference.PropertyCardSet cardSet in pfm.playerModels[4].propertySets.playersPropertySets)
            {
                foreach (MonopolyDealServiceReference.Card card in cardSet.properties)
                {
                    textBoxProp5.Text = textBoxProp5.Text + "ID:" + card.cardID + " " + card.cardName + " $" + card.cardValue + Environment.NewLine;
                }
            }
        }

        private void buttonPoll_Click(object sender, EventArgs e)
        {
            requestHandler.pollState();
            //Clears hand
            listBox1.Items.Clear();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
    }
}