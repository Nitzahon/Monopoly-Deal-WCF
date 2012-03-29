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
        int thisPlayerID;

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
        public void recieveID(int id)
        {
            thisPlayerID = id;
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
            foreach (MonopolyDealServiceReference.Card card in pfm.playerModels[0].hand.cardsInHand)
            {
                textBoxHand1.Text = textBoxHand1.Text + "ID:" + card.cardID + " " + card.cardName + " $" + card.cardValue + Environment.NewLine;
            }
            throw new NotImplementedException();
        }

        private void buttonPoll_Click(object sender, EventArgs e)
        {
            requestHandler.pollState();
        }
    }
}