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
            textBoxLog.Text = description + Environment.NewLine + textBoxLog.Text;
        }

        //UI

        //UI
        public void showMessage(string msg)
        {
            updateTextBoxLog(msg);
            //MessageBox.Show(msg);
        }

        //UI
        private void button1_Click(object sender, EventArgs e)
        {
            requestHandler.connect();
        }

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

        public void drawField(MonopolyDealServiceReference.PlayFieldModel pfm)
        {
            //textBoxHand1.Text = "";
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

        /*
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
        */

        public void setGuid(Guid id)
        {
            requestHandler.thisClientGuid = id;
        }

        #region drawplayers

        private void BindListBox(int playerIndex)
        {
            listBox1.DataSource = requestHandler.CurrentPlayFieldModel.playerModels.ElementAt(playerIndex).hand.cardsInHand;
            listBox1.DisplayMember = "description";
            //this.listBox1.DisplayMember = "Text";
            listBox1.ValueMember = "cardID";
            //comboBox1.DataSource = dataSet1.Tables["Suppliers"];
            //comboBox1.DisplayMember = "ProductName";
        }

        public void popluateHandListBox(MonopolyDealServiceReference.Card card)
        {
            //MDWcfServiceLibrary.Card c = new MDWcfServiceLibrary.Card(card.cardName, card.cardText, card.cardValue, cardTypeConvert(card));
            //listBox1.Items.Add(card.description);
        }

        public void bindPlayer0(MonopolyDealServiceReference.PlayFieldModel pfm)
        {
            listBoxPlayer0Hand.DataSource = requestHandler.CurrentPlayFieldModel.playerModels.ElementAt(0).hand.cardsInHand;
            listBoxPlayer0Hand.DisplayMember = "description";
            listBoxPlayer0Hand.ValueMember = "cardID";

            listBoxPlayer0Bank.DataSource = requestHandler.CurrentPlayFieldModel.playerModels.ElementAt(0).bank.cardsInBank;
            listBoxPlayer0Bank.DisplayMember = "description";
            listBoxPlayer0Bank.ValueMember = "cardID";

            listBoxPSetsP0.DataSource = requestHandler.CurrentPlayFieldModel.playerModels.ElementAt(0).propertySets.playersPropertySets;
            listBoxPSetsP0.DisplayMember = "propertySetColor";
            listBoxPSetsP0.ValueMember = "id";

            //listBoxPSetsP0.DataSource = requestHandler.CurrentPlayFieldModel.playerModels.ElementAt(0).propertySets.playersPropertySets;
            //listBoxPSetsP0.DisplayMember = "propertySetColor";
            //listBoxPSetsP0.ValueMember = "id";
        }

        public void bindPlayer1(MonopolyDealServiceReference.PlayFieldModel pfm)
        {
            listBoxPlayer1Hand.DataSource = requestHandler.CurrentPlayFieldModel.playerModels.ElementAt(1).hand.cardsInHand;
            listBoxPlayer1Hand.DisplayMember = "description";
            listBoxPlayer1Hand.ValueMember = "cardID";

            listBoxPlayer1Bank.DataSource = requestHandler.CurrentPlayFieldModel.playerModels.ElementAt(1).bank.cardsInBank;
            listBoxPlayer1Bank.DisplayMember = "description";
            listBoxPlayer1Bank.ValueMember = "cardID";

            //listBoxPlayer1PropertySets.DataSource = requestHandler.CurrentPlayFieldModel.playerModels.ElementAt(1).propertySets.playersPropertySets;
            //listBoxPlayer1PropertySets.DisplayMember = "description";
            //listBoxPlayer1PropertySets.ValueMember = "cardID";
        }

        public void bindPlayer2(MonopolyDealServiceReference.PlayFieldModel pfm)
        {
            listBoxPlayer2Hand.DataSource = requestHandler.CurrentPlayFieldModel.playerModels.ElementAt(2).hand.cardsInHand;
            listBoxPlayer2Hand.DisplayMember = "description";
            listBoxPlayer2Hand.ValueMember = "cardID";

            listBoxPlayer2Bank.DataSource = requestHandler.CurrentPlayFieldModel.playerModels.ElementAt(2).bank.cardsInBank;
            listBoxPlayer2Bank.DisplayMember = "description";
            listBoxPlayer2Bank.ValueMember = "cardID";

            //listBoxPlayer2PropertySets.DataSource = requestHandler.CurrentPlayFieldModel.playerModels.ElementAt(2).propertySets.playersPropertySets;
            //listBoxPlayer2PropertySets.DisplayMember = "description";
            //listBoxPlayer2PropertySets.ValueMember = "cardID";
        }

        public void bindPlayer3(MonopolyDealServiceReference.PlayFieldModel pfm)
        {
            listBoxPlayer3Hand.DataSource = requestHandler.CurrentPlayFieldModel.playerModels.ElementAt(3).hand.cardsInHand;
            listBoxPlayer3Hand.DisplayMember = "description";
            listBoxPlayer3Hand.ValueMember = "cardID";

            listBoxPlayer3Bank.DataSource = requestHandler.CurrentPlayFieldModel.playerModels.ElementAt(3).bank.cardsInBank;
            listBoxPlayer3Bank.DisplayMember = "description";
            listBoxPlayer3Bank.ValueMember = "cardID";

            //listBoxPlayer0PropertySets.DataSource = requestHandler.CurrentPlayFieldModel.playerModels.ElementAt(0).propertySets.playersPropertySets;
            //listBoxPlayer0PropertySets.DisplayMember = "description";
            //listBoxPlayer0PropertySets.ValueMember = "cardID";
        }

        public void bindPlayer4(MonopolyDealServiceReference.PlayFieldModel pfm)
        {
            listBoxPlayer4Hand.DataSource = requestHandler.CurrentPlayFieldModel.playerModels.ElementAt(4).hand.cardsInHand;
            listBoxPlayer4Hand.DisplayMember = "description";
            listBoxPlayer4Hand.ValueMember = "cardID";

            listBoxPlayer4Bank.DataSource = requestHandler.CurrentPlayFieldModel.playerModels.ElementAt(4).bank.cardsInBank;
            listBoxPlayer4Bank.DisplayMember = "description";
            listBoxPlayer4Bank.ValueMember = "cardID";

            //listBoxPlayer4PropertySets.DataSource = requestHandler.CurrentPlayFieldModel.playerModels.ElementAt(4).propertySets.playersPropertySets;
            //listBoxPlayer4PropertySets.DisplayMember = "description";
            //listBoxPlayer4PropertySets.ValueMember = "cardID";
        }

        public void drawPlayer0(MonopolyDealServiceReference.PlayFieldModel pfm)
        {
            bindPlayer0(pfm);
            if (requestHandler.thisClientGuid.CompareTo(pfm.playerModels[0].guid) == 0)
            {
                //This is the player on this client
                BindListBox(0);
                updateAllowAbleActions(pfm.playerModels[0]);
            }
            /*
            textBoxHand1.Text = "";
            textBoxBank1.Text = "";
            textBoxProp1.Text = "";

            foreach (MonopolyDealServiceReference.Card card in pfm.playerModels[0].hand.cardsInHand)
            {
                textBoxHand1.Text = textBoxHand1.Text + "ID:" + card.cardID + " " + card.cardName + " $" + card.cardValue + Environment.NewLine;

                if (requestHandler.thisClientGuid.CompareTo(pfm.playerModels[0].guid) == 0)
                {
                    updateTextBoxLog(card.description);
                    popluateHandListBox(card);
                    BindListBox(0);
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
             * */
        }

        public void drawPlayer1(MonopolyDealServiceReference.PlayFieldModel pfm)
        {
            bindPlayer1(pfm);
            if (requestHandler.thisClientGuid.CompareTo(pfm.playerModels[1].guid) == 0)
            {
                BindListBox(1);
                updateAllowAbleActions(pfm.playerModels[1]);
            }
            /*
            textBoxHand2.Text = "";
            textBoxBank2.Text = "";
            textBoxProp2.Text = "";
            foreach (MonopolyDealServiceReference.Card card in pfm.playerModels[1].hand.cardsInHand)
            {
                textBoxHand2.Text = textBoxHand2.Text + "ID:" + card.cardID + " " + card.cardName + " $" + card.cardValue + Environment.NewLine;
                if (requestHandler.thisClientGuid.CompareTo(pfm.playerModels[1].guid) == 0)
                {
                    updateTextBoxLog(card.description);
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
             * */
        }

        public void drawPlayer2(MonopolyDealServiceReference.PlayFieldModel pfm)
        {
            bindPlayer2(pfm);
            if (requestHandler.thisClientGuid.CompareTo(pfm.playerModels[2].guid) == 0)
            {
                BindListBox(2);
                updateAllowAbleActions(pfm.playerModels[2]);
            }
            /*
            textBoxHand3.Text = "";
            textBoxBank3.Text = "";
            textBoxProp3.Text = "";
            foreach (MonopolyDealServiceReference.Card card in pfm.playerModels[2].hand.cardsInHand)
            {
                textBoxHand3.Text = textBoxHand3.Text + "ID:" + card.cardID + " " + card.cardName + " $" + card.cardValue + Environment.NewLine;
                if (requestHandler.thisClientGuid.CompareTo(pfm.playerModels[2].guid) == 0)
                {
                    updateTextBoxLog(card.description);
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
             * */
        }

        public void drawPlayer3(MonopolyDealServiceReference.PlayFieldModel pfm)
        {
            bindPlayer3(pfm);
            if (requestHandler.thisClientGuid.CompareTo(pfm.playerModels[3].guid) == 0)
            {
                BindListBox(3);
                updateAllowAbleActions(pfm.playerModels[3]);
            }
            /*
            textBoxHand4.Text = "";
            textBoxBank4.Text = "";
            textBoxProp4.Text = "";
            foreach (MonopolyDealServiceReference.Card card in pfm.playerModels[3].hand.cardsInHand)
            {
                textBoxHand4.Text = textBoxHand4.Text + "ID:" + card.cardID + " " + card.cardName + " $" + card.cardValue + Environment.NewLine;
                if (requestHandler.thisClientGuid.CompareTo(pfm.playerModels[3].guid) == 0)
                {
                    updateTextBoxLog(card.description);
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
             * */
        }

        public void drawPlayer4(MonopolyDealServiceReference.PlayFieldModel pfm)
        {
            bindPlayer4(pfm);
            if (requestHandler.thisClientGuid.CompareTo(pfm.playerModels[4].guid) == 0)
            {
                BindListBox(4);
                updateAllowAbleActions(pfm.playerModels[4]);
            }
            /*
            textBoxHand5.Text = "";
            textBoxBank5.Text = "";
            textBoxProp5.Text = "";
            foreach (MonopolyDealServiceReference.Card card in pfm.playerModels[4].hand.cardsInHand)
            {
                textBoxHand5.Text = textBoxHand5.Text + "ID:" + card.cardID + " " + card.cardName + " $" + card.cardValue + Environment.NewLine;
                if (requestHandler.thisClientGuid.CompareTo(pfm.playerModels[4].guid) == 0)
                {
                    updateTextBoxLog(card.description);
                    popluateHandListBox(card);
                }
            }
            foreach (MonopolyDealServiceReference.Card card in pfm.playerModels[4].bank.cardsInBank)
            {
                textBoxBank5.Text = textBoxBank5.Text + "ID:" + card.cardID + " " + card.cardName + " $" + card.cardValue + Environment.NewLine;
            }
             *
            foreach (MonopolyDealServiceReference.PropertyCardSet cardSet in pfm.playerModels[4].propertySets.playersPropertySets)
            {
                foreach (MonopolyDealServiceReference.Card card in cardSet.properties)
                {
                    textBoxProp5.Text = textBoxProp5.Text + "ID:" + card.cardID + " " + card.cardName + " $" + card.cardValue + Environment.NewLine;
                }
            }
             * */
        }

        #endregion drawplayers

        private void buttonPoll_Click(object sender, EventArgs e)
        {
            requestHandler.pollState();
            //Clears hand
            //listBox1.Items.Clear();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void buttonDraw2_Click(object sender, EventArgs e)
        {
            requestHandler.drawTwoAtTurnStart();
        }

        private void buttonBankCard_Click(object sender, EventArgs e)
        {
            int cardIDOfCardToBank = -1;
            if (listBox1.Items.Count != 0)
            {
                cardIDOfCardToBank = (int)listBox1.SelectedValue;
                requestHandler.bankCard(cardIDOfCardToBank);
            }
            else
            {
                buttonBankCard.Enabled = false;
            }
            //Replace with card picker
            //MonopolyDealServiceReference.Card card = requestHandler.getPlayerModelByGuid(requestHandler.thisClientGuid).hand.cardsInHand.ElementAt(cardIDOfCardToBank);
            //requestHandler.bankCard(card.cardID);
        }

        public void updateAllowAbleActions(MonopolyDealServiceReference.PlayerModel pm)
        {
            buttonDraw2.Enabled = false;
            buttonBankCard.Enabled = false;
            if (!(pm.actionsCurrentlyAllowed == null || pm.actionsCurrentlyAllowed.Length == 0))
            {
                foreach (MonopolyDealServiceReference.TurnActionTypes tAT in pm.actionsCurrentlyAllowed)
                {
                    if (tAT.CompareTo(MonopolyDealServiceReference.TurnActionTypes.drawTwoCardsAtStartOfTurn) == 0)
                    {
                        buttonDraw2.Enabled = true;
                    }
                    else if (tAT.CompareTo(MonopolyDealServiceReference.TurnActionTypes.BankActionCard) == 0)
                    {
                        buttonBankCard.Enabled = true;
                    }
                }
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            if (requestHandler.checkHasGameStarted())
            {
                buttonPoll.Enabled = true;
                button2.Enabled = false;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int cardIDOfPropertyToPlay = -1;
            cardIDOfPropertyToPlay = (int)listBox1.SelectedValue;
            requestHandler.playPropertyToNewSet(cardIDOfPropertyToPlay);
        }

        private void buttonEndTurn_Click(object sender, EventArgs e)
        {
            requestHandler.endTurn();
        }

        private void buttonSelectOption_Click(object sender, EventArgs e)
        {
        }

        private void buttonDraw5OnTurnStart_Click(object sender, EventArgs e)
        {
            requestHandler.drawFiveAtTurnStart();
        }
    }
}