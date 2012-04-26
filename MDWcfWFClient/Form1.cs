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
        RequestHanderMonopolyDeal requestHandler;
        RequestHanderMonopolyDeal requestHandlerMD;
        int playerIDNum = -1;
        List<MonopolyDealServiceReference.Card> playersSelectedToPayWithCards = new List<MonopolyDealServiceReference.Card>();
        List<MonopolyDealServiceReference.Card> playersPlayedCards = new List<MonopolyDealServiceReference.Card>();
        System.Windows.Forms.Timer timerForLobby = new System.Windows.Forms.Timer();
        System.Windows.Forms.Timer timerForPollingState = new System.Windows.Forms.Timer();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Capture the UI synchronization context
            _uiSyncContext = SynchronizationContext.Current;
            //rqh
            //requestHandler = new RequestHandler(_uiSyncContext, this);
            requestHandlerMD = new RequestHanderMonopolyDeal(_uiSyncContext, this);
            requestHandler = requestHandlerMD;
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
            //requestHandler.connect();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //requestHandler.startGame();
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
            //Player0
            int numPlayers = pfm.playerModels.Count();
            //Draw playpile
            listBoxDiscardPile.DataSource = pfm.playpile.playPile;
            listBoxDiscardPile.DisplayMember = "description";
            switch (numPlayers)
            {
                case 0:
                    break;
                case 1:
                    drawPlayer0(pfm);
                    playersPlayedCards = new List<MonopolyDealServiceReference.Card>();
                    playersSelectedToPayWithCards = new List<MonopolyDealServiceReference.Card>();
                    bindSelectPlayersPropertyCardSetsPropertyCards(pfm);
                    bindPlayerPayDebt(pfm);
                    break;
                case 2:
                    drawPlayer0(pfm);
                    drawPlayer1(pfm);
                    playersPlayedCards = new List<MonopolyDealServiceReference.Card>();
                    playersSelectedToPayWithCards = new List<MonopolyDealServiceReference.Card>();
                    bindSelectPlayersPropertyCardSetsPropertyCards(pfm);
                    bindPlayerPayDebt(pfm);
                    break;
                case 3:
                    drawPlayer0(pfm);
                    drawPlayer1(pfm);
                    drawPlayer2(pfm);
                    playersPlayedCards = new List<MonopolyDealServiceReference.Card>();
                    playersSelectedToPayWithCards = new List<MonopolyDealServiceReference.Card>();
                    bindSelectPlayersPropertyCardSetsPropertyCards(pfm);
                    bindPlayerPayDebt(pfm);
                    break;
                case 4:

                    drawPlayer0(pfm);
                    drawPlayer1(pfm);
                    drawPlayer2(pfm);
                    drawPlayer3(pfm);
                    playersPlayedCards = new List<MonopolyDealServiceReference.Card>();
                    playersSelectedToPayWithCards = new List<MonopolyDealServiceReference.Card>();
                    bindSelectPlayersPropertyCardSetsPropertyCards(pfm);
                    bindPlayerPayDebt(pfm);
                    break;
                case 5:
                    drawPlayer0(pfm);
                    drawPlayer1(pfm);
                    drawPlayer2(pfm);
                    drawPlayer3(pfm);
                    drawPlayer4(pfm);
                    playersPlayedCards = new List<MonopolyDealServiceReference.Card>();
                    playersSelectedToPayWithCards = new List<MonopolyDealServiceReference.Card>();
                    bindSelectPlayersPropertyCardSetsPropertyCards(pfm);
                    bindPlayerPayDebt(pfm);
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
            //listBox1.ValueMember = "cardID";
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
            //listBoxPlayer0Hand.ValueMember = "cardID";

            listBoxPlayer0Bank.DataSource = requestHandler.CurrentPlayFieldModel.playerModels.ElementAt(0).bank.cardsInBank;
            listBoxPlayer0Bank.DisplayMember = "description";
            //listBoxPlayer0Bank.ValueMember = "cardID";

            listBoxPSetsP0.DataSource = requestHandler.CurrentPlayFieldModel.playerModels.ElementAt(0).propertySets.playersPropertySets;
            listBoxPSetsP0.DisplayMember = "propertySetColor";
            //listBoxPSetsP0.ValueMember = "id";
        }

        public void bindPlayer1(MonopolyDealServiceReference.PlayFieldModel pfm)
        {
            listBoxPlayer1Hand.DataSource = requestHandler.CurrentPlayFieldModel.playerModels.ElementAt(1).hand.cardsInHand;
            listBoxPlayer1Hand.DisplayMember = "description";
            //listBoxPlayer1Hand.ValueMember = "cardID";

            listBoxPlayer1Bank.DataSource = requestHandler.CurrentPlayFieldModel.playerModels.ElementAt(1).bank.cardsInBank;
            listBoxPlayer1Bank.DisplayMember = "description";
            //listBoxPlayer1Bank.ValueMember = "cardID";

            listBoxPSetsP1.DataSource = requestHandler.CurrentPlayFieldModel.playerModels.ElementAt(1).propertySets.playersPropertySets;
            listBoxPSetsP1.DisplayMember = "propertySetColor";
        }

        public void bindPlayer2(MonopolyDealServiceReference.PlayFieldModel pfm)
        {
            listBoxPlayer2Hand.DataSource = requestHandler.CurrentPlayFieldModel.playerModels.ElementAt(2).hand.cardsInHand;
            listBoxPlayer2Hand.DisplayMember = "description";
            //listBoxPlayer2Hand.ValueMember = "cardID";

            listBoxPlayer2Bank.DataSource = requestHandler.CurrentPlayFieldModel.playerModels.ElementAt(2).bank.cardsInBank;
            listBoxPlayer2Bank.DisplayMember = "description";
            //listBoxPlayer2Bank.ValueMember = "cardID";

            listBoxPSetsP2.DataSource = requestHandler.CurrentPlayFieldModel.playerModels.ElementAt(2).propertySets.playersPropertySets;
            listBoxPSetsP2.DisplayMember = "propertySetColor";
        }

        public void bindPlayer3(MonopolyDealServiceReference.PlayFieldModel pfm)
        {
            listBoxPlayer3Hand.DataSource = requestHandler.CurrentPlayFieldModel.playerModels.ElementAt(3).hand.cardsInHand;
            listBoxPlayer3Hand.DisplayMember = "description";
            //listBoxPlayer3Hand.ValueMember = "cardID";

            listBoxPlayer3Bank.DataSource = requestHandler.CurrentPlayFieldModel.playerModels.ElementAt(3).bank.cardsInBank;
            listBoxPlayer3Bank.DisplayMember = "description";
            //listBoxPlayer3Bank.ValueMember = "cardID";

            listBoxPSetsP3.DataSource = requestHandler.CurrentPlayFieldModel.playerModels.ElementAt(3).propertySets.playersPropertySets;
            listBoxPSetsP3.DisplayMember = "propertySetColor";
        }

        public void bindPlayer4(MonopolyDealServiceReference.PlayFieldModel pfm)
        {
            listBoxPlayer4Hand.DataSource = requestHandler.CurrentPlayFieldModel.playerModels.ElementAt(4).hand.cardsInHand;
            listBoxPlayer4Hand.DisplayMember = "description";
            // listBoxPlayer4Hand.ValueMember = "cardID";

            listBoxPlayer4Bank.DataSource = requestHandler.CurrentPlayFieldModel.playerModels.ElementAt(4).bank.cardsInBank;
            listBoxPlayer4Bank.DisplayMember = "description";
            //listBoxPlayer4Bank.ValueMember = "cardID";

            listBoxPSetsP4.DataSource = requestHandler.CurrentPlayFieldModel.playerModels.ElementAt(4).propertySets.playersPropertySets;
            listBoxPSetsP4.DisplayMember = "propertySetColor";
        }

        public void drawPlayer0(MonopolyDealServiceReference.PlayFieldModel pfm)
        {
            bindPlayer0(pfm);
            if (requestHandler.thisClientGuid.CompareTo(pfm.playerModels[0].guid) == 0)
            {
                //This is the player on this client
                BindListBox(0);
                updateAllowAbleActions(pfm.playerModels[0]);
                playerIDNum = 0;
            }
        }

        public void drawPlayer1(MonopolyDealServiceReference.PlayFieldModel pfm)
        {
            bindPlayer1(pfm);
            if (requestHandler.thisClientGuid.CompareTo(pfm.playerModels[1].guid) == 0)
            {
                BindListBox(1);
                updateAllowAbleActions(pfm.playerModels[1]);
                playerIDNum = 1;
            }
        }

        public void drawPlayer2(MonopolyDealServiceReference.PlayFieldModel pfm)
        {
            bindPlayer2(pfm);
            if (requestHandler.thisClientGuid.CompareTo(pfm.playerModels[2].guid) == 0)
            {
                BindListBox(2);
                updateAllowAbleActions(pfm.playerModels[2]);
                playerIDNum = 2;
            }
        }

        public void drawPlayer3(MonopolyDealServiceReference.PlayFieldModel pfm)
        {
            bindPlayer3(pfm);
            if (requestHandler.thisClientGuid.CompareTo(pfm.playerModels[3].guid) == 0)
            {
                BindListBox(3);
                updateAllowAbleActions(pfm.playerModels[3]);
                playerIDNum = 3;
            }
        }

        public void drawPlayer4(MonopolyDealServiceReference.PlayFieldModel pfm)
        {
            bindPlayer4(pfm);
            if (requestHandler.thisClientGuid.CompareTo(pfm.playerModels[4].guid) == 0)
            {
                BindListBox(4);
                updateAllowAbleActions(pfm.playerModels[4]);
                playerIDNum = 4;
            }
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
            //Update State
            buttonPollMD_Click(null, null);
        }

        private void buttonBankCard_Click(object sender, EventArgs e)
        {
            int cardIDOfCardToBank = -1;
            if (listBox1.Items.Count != 0)
            {
                cardIDOfCardToBank = (int)((MonopolyDealServiceReference.Card)listBox1.SelectedValue).cardID;
                requestHandler.bankCard(cardIDOfCardToBank);
            }
            else
            {
                buttonBankCard.Enabled = false;
            }
            //Update State
            buttonPollMD_Click(null, null);
        }

        public void updateAllowAbleActions(MonopolyDealServiceReference.PlayerModel pm)
        {
            buttonDraw2.Enabled = false;
            buttonBankCard.Enabled = false;
            buttonDraw5OnTurnStart.Enabled = false;
            buttonPlayPropNewSetFromHand.Enabled = false;
            buttonDiscard1.Enabled = false;
            buttonEndTurn.Enabled = false;
            buttonAdd.Enabled = false;
            buttonRemove.Enabled = false;
            buttonPay.Enabled = false;
            if (!(pm.actionsCurrentlyAllowed == null || pm.actionsCurrentlyAllowed.Length == 0))
            {
                foreach (MonopolyDealServiceReference.TurnActionTypes tAT in pm.actionsCurrentlyAllowed)
                {
                    if (tAT.CompareTo(MonopolyDealServiceReference.TurnActionTypes.drawTwoCardsAtStartOfTurn) == 0)
                    {
                        buttonDraw2.Enabled = true;
                        timerForPollingState.Stop();
                    }
                    else if (tAT.CompareTo(MonopolyDealServiceReference.TurnActionTypes.BankActionCard) == 0)
                    {
                        buttonBankCard.Enabled = true;
                        timerForPollingState.Stop();
                    }
                    else if (tAT.CompareTo(MonopolyDealServiceReference.TurnActionTypes.PlayPropertyCard_New_Set) == 0)
                    {
                        buttonPlayPropNewSetFromHand.Enabled = true;
                        timerForPollingState.Stop();
                    }
                    else if (tAT.CompareTo(MonopolyDealServiceReference.TurnActionTypes.drawFiveCardsAtStartOfTurn) == 0)
                    {
                        buttonDraw5OnTurnStart.Enabled = true;
                        timerForPollingState.Stop();
                    }
                    else if (tAT.CompareTo(MonopolyDealServiceReference.TurnActionTypes.Discard_1_Card) == 0 || tAT.CompareTo(MonopolyDealServiceReference.TurnActionTypes.Discard_2_Cards) == 0 || tAT.CompareTo(MonopolyDealServiceReference.TurnActionTypes.Discard_3_Cards) == 0 || tAT.CompareTo(MonopolyDealServiceReference.TurnActionTypes.Discard_4_Cards) == 0 || tAT.CompareTo(MonopolyDealServiceReference.TurnActionTypes.Discard_5_Cards) == 0)
                    {
                        buttonDiscard1.Enabled = true;
                        timerForPollingState.Stop();
                    }
                    else if (tAT.CompareTo(MonopolyDealServiceReference.TurnActionTypes.EndTurn) == 0)
                    {
                        buttonEndTurn.Enabled = true;
                        timerForPollingState.Stop();
                    }
                    else if (tAT.CompareTo(MonopolyDealServiceReference.TurnActionTypes.PayDebt) == 0)
                    {
                        buttonAdd.Enabled = true;
                        buttonRemove.Enabled = true;
                        buttonPay.Enabled = true;
                        timerForPollingState.Stop();
                        MessageBox.Show("You are in debt! Pay Debt or Just Say No!");
                    }
                }
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            if (requestHandler.checkHasGameStarted())
            {
                buttonPollMD.Enabled = true;
                buttonPoll.Enabled = true;
                button2.Enabled = false;
                timerForLobby.Stop();
                MessageBox.Show("Game Started");

                timerForPollingState.Interval = 5000;//5 second polling
                timerForPollingState.Tick += new EventHandler(buttonPollMD_Click);
                timerForPollingState.Start();
                buttonPollMD_Click(null, null);
            }
            //Update Lobbies state
            button9_Click(null, null);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int cardIDOfPropertyToPlay = -1;
            cardIDOfPropertyToPlay = (int)((MonopolyDealServiceReference.Card)listBox1.SelectedValue).cardID;
            bool isOrientedUp = true;
            requestHandler.playPropertyToNewSet(cardIDOfPropertyToPlay, isOrientedUp);
            //Update State
            buttonPollMD_Click(null, null);
        }

        private void buttonEndTurn_Click(object sender, EventArgs e)
        {
            requestHandler.endTurn();
            //Update State
            buttonPollMD_Click(null, null);
            timerForPollingState.Start();
        }

        private void buttonSelectOption_Click(object sender, EventArgs e)
        {
        }

        private void buttonDraw5OnTurnStart_Click(object sender, EventArgs e)
        {
            requestHandler.drawFiveAtTurnStart();
            //Update State
            buttonPollMD_Click(null, null);
        }

        private void listBoxPSetsP0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxPSetsP0.SelectedValue != null && listBoxPSetsP0.DataSource != null)
            {
                listBoxPSetSelectedP0.DataSource = ((MonopolyDealServiceReference.PropertyCardSet)listBoxPSetsP0.SelectedValue).properties.ToArray<MonopolyDealServiceReference.Card>();
                listBoxPSetSelectedP0.DisplayMember = "description";
                listBoxPSetSelectedP0.ValueMember = "cardID";
            }
        }

        private void listBoxPSetsP1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxPSetsP1.SelectedValue != null && listBoxPSetsP1.DataSource != null)
            {
                listBoxPSetSelectedP1.DataSource = ((MonopolyDealServiceReference.PropertyCardSet)listBoxPSetsP1.SelectedValue).properties.ToArray<MonopolyDealServiceReference.Card>();
                listBoxPSetSelectedP1.DisplayMember = "description";
                listBoxPSetSelectedP1.ValueMember = "cardID";
            }
        }

        private void listBoxPSetsP2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxPSetsP2.SelectedValue != null && listBoxPSetsP0.DataSource != null)
            {
                listBoxPSetSelectedP2.DataSource = ((MonopolyDealServiceReference.PropertyCardSet)listBoxPSetsP2.SelectedValue).properties.ToArray<MonopolyDealServiceReference.Card>();
                listBoxPSetSelectedP2.DisplayMember = "description";
                listBoxPSetSelectedP2.ValueMember = "cardID";
            }
        }

        private void bindSelectPlayersPropertyCardSetsPropertyCards(MonopolyDealServiceReference.PlayFieldModel pfm)
        {
            if (pfm.playerModels != null)
            {
                listBoxPlayers.DataSource = pfm.playerModels;
                listBoxPlayers.DisplayMember = "name";
                if (listBoxPlayers.SelectedValue != null)
                {
                    listBoxSelectedPlayersPropertySets.DataSource = ((MonopolyDealServiceReference.PlayerModel)listBoxPlayers.SelectedValue).propertySets.playersPropertySets;
                    listBoxSelectedPlayersPropertySets.DisplayMember = "propertySetColor";
                    if (listBoxSelectedPlayersPropertySets.SelectedValue != null)
                    {
                        listBoxSelectedPlayersSelectedSetPropertyCards.DataSource = ((MonopolyDealServiceReference.PropertyCardSet)listBoxSelectedPlayersPropertySets.SelectedValue).properties.ToArray<MonopolyDealServiceReference.Card>();
                        listBoxSelectedPlayersSelectedSetPropertyCards.DisplayMember = "description";
                        listBoxSelectedPlayersSelectedSetPropertyCards.ValueMember = "cardID";
                    }
                }
            }
        }

        private void bindPlayerPayDebt(MonopolyDealServiceReference.PlayFieldModel pfm)
        {
            int playersTotalValue = getAllPlayersPlayedCards(pfm, playersPlayedCards);
            listBoxAllPlayersPlayedCards.DataSource = playersPlayedCards;
            listBoxAllPlayersPlayedCards.DisplayMember = "description";
            listBoxCardsToPayWith.DataSource = playersSelectedToPayWithCards;
            listBoxCardsToPayWith.DisplayMember = "description";
        }

        /// <summary>
        /// Gets all of the clients property, house, hotel and banked cards and puts them in a list passed to the method.
        /// Returns the total value of the players played cards
        /// </summary>
        /// <param name="pfm">The current game state</param>
        /// <param name="playersPlayedCards">List to place cards in.</param>
        /// <returns>Total value of all of a players played cards</returns>
        public int getAllPlayersPlayedCards(MonopolyDealServiceReference.PlayFieldModel pfm, List<MonopolyDealServiceReference.Card> playersPlayedCards)
        {
            int totalValue = 0;
            foreach (MonopolyDealServiceReference.PlayerModel player in pfm.playerModels)
            {
                if (requestHandler.thisClientGuid.CompareTo(player.guid) == 0)
                {
                    foreach (MonopolyDealServiceReference.Card c in player.bank.cardsInBank)
                    {
                        playersPlayedCards.Add(c);
                        totalValue += c.cardValue;
                    }
                    foreach (MonopolyDealServiceReference.PropertyCardSet ps in player.propertySets.playersPropertySets)
                    {
                        foreach (MonopolyDealServiceReference.Card property in ps.properties)
                        {
                            playersPlayedCards.Add(property);
                            totalValue += property.cardValue;
                        }
                        if (ps.hasHouse)
                        {
                            playersPlayedCards.Add(ps.house);
                            totalValue += ps.house.cardValue;
                            if (ps.hasHotel)
                            {
                                playersPlayedCards.Add(ps.hotel);
                                totalValue += ps.hotel.cardValue;
                            }
                        }
                    }
                }
            }
            return totalValue;
        }

        private void button1_Click_2(object sender, EventArgs e)
        {
            requestHandler.discard1Card((int)((MonopolyDealServiceReference.Card)listBox1.SelectedValue).cardID);
            //Update State
            buttonPollMD_Click(null, null);
        }

        private void listBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void listBoxPlayers_SelectedIndexChanged(object sender, EventArgs e)
        {
            bindSelectPlayersPropertyCardSetsPropertyCards(requestHandler.CurrentPlayFieldModel);
        }

        private void listBoxSelectedPlayersPropertySets_SelectedIndexChanged(object sender, EventArgs e)
        {
            bindSelectPlayersPropertyCardSetsPropertyCards(requestHandler.CurrentPlayFieldModel);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            MonopolyDealServiceReference.Card card = (MonopolyDealServiceReference.Card)listBox1.SelectedValue;
            MonopolyDealServiceReference.ActionCard actionCard = card as MonopolyDealServiceReference.ActionCard;
            MonopolyDealServiceReference.PlayerModel playerTargeted = (MonopolyDealServiceReference.PlayerModel)listBoxPlayers.SelectedItem;
            if (actionCard != null)
            {
                if (actionCard.actionType.CompareTo(MonopolyDealServiceReference.ActionCardAction.PassGo) == 0)
                {
                    //card is a pass go card
                    MessageBox.Show("Playing A Pass Go Action Card");
                    bool result = requestHandler.passGo(card.cardID);
                }
                else if (actionCard.actionType.CompareTo(MonopolyDealServiceReference.ActionCardAction.DebtCollector) == 0)
                {
                    MessageBox.Show("Playing a Debt Collector Card");
                    //card is a Debt Collector card
                    if (playerTargeted.guid.CompareTo(requestHandlerMD.thisClientGuid) == 0)
                    {
                        MessageBox.Show("You can not play a Debt Collector card against yourself. Action canceled");
                    }
                    else
                    {
                        MessageBox.Show("Playing A Debt Collector Action Card against " + playerTargeted.name);
                        bool result = requestHandler.debtCollector(card.cardID, playerTargeted.guid);
                        if (result)
                        {
                            MessageBox.Show("Action Performed");
                        }
                        else
                        {
                            MessageBox.Show("Action not performed");
                        }
                    }
                }
                else if (actionCard.actionType.CompareTo(MonopolyDealServiceReference.ActionCardAction.ItsMyBirthday) == 0)
                {
                    MessageBox.Show("Playing a It's My Birthday Card");
                    bool result = requestHandler.itsMyBirthday(card.cardID);
                    if (result)
                    {
                        MessageBox.Show("Action Performed");
                    }
                    else
                    {
                        MessageBox.Show("Action not performed");
                    }
                }
            }
            //Update State
            buttonPollMD_Click(null, null);
        }

        private void buttonConnectToService_Click(object sender, EventArgs e)
        {
            requestHandlerMD.connectToLobby(this.textBoxPlayerName.Text);
            //Update Lobbies state
            button9_Click(null, null);
            timerForLobby.Tick += new EventHandler(button2_Click_1);
            timerForLobby.Interval = 5000;//5 second updates
            timerForLobby.Start();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            requestHandlerMD.iAmReady();
            //Update Lobbies state
            button9_Click(null, null);
            // Hook up the Elapsed event for the timer.

            timerForLobby.Start();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            requestHandlerMD.iAmNotReady();
            //Update Lobbies state
            button9_Click(null, null);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            requestHandlerMD.updateLobbies();
        }

        public void displayLobbiesState(MonopolyDealServiceReference.GameLobby[] gameLobbies)
        {
            if (gameLobbies.Length >= 1)
            {
                listBoxGameLobbies.DataSource = gameLobbies;
                listBoxGameLobbies.DisplayMember = "description";
            }
            else
            {
                List<String> empty = new List<string>();
                empty.Add("Create new game");
                listBoxGameLobbies.DataSource = empty;
            }
        }

        public void updateLobby(Object lobby)
        {
            if (lobby is List<String>)
            {
                //Connect to new lobby
            }
            else
            {
                MonopolyDealServiceReference.GameLobby gl = lobby as MonopolyDealServiceReference.GameLobby;
                listBoxPlayersInLobby.DataSource = gl.clientsConnectedToGame;
                listBoxPlayersInLobby.DisplayMember = "description";
            }
        }

        private void listBoxGameLobbies_SelectedIndexChanged(object sender, EventArgs e)
        {
            MonopolyDealServiceReference.GameLobby gl = listBoxGameLobbies.SelectedValue as MonopolyDealServiceReference.GameLobby;
            if (gl != null)
            {
                updateLobby(gl);
            }
        }

        private void button1_Click_3(object sender, EventArgs e)
        {
            //Connect to new lobby
            requestHandlerMD.connectToNewLobby();
            //Update Lobbies state
            button9_Click(null, null);
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            if (listBoxGameLobbies.SelectedValue is String)
            {
                //Connect to new lobby
                requestHandlerMD.connectToNewLobby();
                //Update Lobbies state
                button9_Click(null, null);
            }
            else
            {
                MonopolyDealServiceReference.GameLobby gl = listBoxGameLobbies.SelectedValue as MonopolyDealServiceReference.GameLobby;
                requestHandlerMD.connectToExistingLobby(gl.guid);
                //Update Lobbies state
                button9_Click(null, null);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            requestHandlerMD.leaveGameLobby();
            //Update Lobbies state
            button9_Click(null, null);
        }

        private void buttonPollMD_Click(object sender, EventArgs e)
        {
            requestHandlerMD.pollState();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            MonopolyDealServiceReference.PropertyCardSet pcs = null;
            MonopolyDealServiceReference.PropertyCard pc = null;
            if (playerIDNum == 0)
            {
                pc = listBoxPlayer0Hand.SelectedValue as MonopolyDealServiceReference.PropertyCard;
                pcs = listBoxPSetsP0.SelectedValue as MonopolyDealServiceReference.PropertyCardSet;
            }
            else if (playerIDNum == 1)
            {
                pc = listBoxPlayer1Hand.SelectedValue as MonopolyDealServiceReference.PropertyCard;
                pcs = listBoxPSetsP1.SelectedValue as MonopolyDealServiceReference.PropertyCardSet;
            }
            else if (playerIDNum == 2)
            {
                pc = listBoxPlayer2Hand.SelectedValue as MonopolyDealServiceReference.PropertyCard;
                pcs = listBoxPSetsP2.SelectedValue as MonopolyDealServiceReference.PropertyCardSet;
            }
            else if (playerIDNum == 3)
            {
                pc = listBoxPlayer3Hand.SelectedValue as MonopolyDealServiceReference.PropertyCard;
                pcs = listBoxPSetsP3.SelectedValue as MonopolyDealServiceReference.PropertyCardSet;
            }
            else if (playerIDNum == 4)
            {
                pc = listBoxPlayer4Hand.SelectedValue as MonopolyDealServiceReference.PropertyCard;
                pcs = listBoxPSetsP4.SelectedValue as MonopolyDealServiceReference.PropertyCardSet;
            }
            if (pc != null && pcs != null)
            {
                requestHandlerMD.playPropertyToSelectedSet(pc, pcs);
            }
            //Update State
            buttonPollMD_Click(null, null);
        }

        private void buttonRearrange_Click(object sender, EventArgs e)
        {
        }

        private void button10_Click(object sender, EventArgs e)
        {
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            MonopolyDealServiceReference.Card card = listBoxAllPlayersPlayedCards.SelectedItem as MonopolyDealServiceReference.Card;
            if (card != null)
            {
                playersPlayedCards.Remove(card);
                playersSelectedToPayWithCards.Add(card);
                updateCardsToPayWithTotalValue(calculateTotalValueOfCardsInList(playersSelectedToPayWithCards));
                CurrencyManager cmPlayed = (CurrencyManager)BindingContext[this.playersPlayedCards];
                cmPlayed.Refresh();
                CurrencyManager cmToPay = (CurrencyManager)BindingContext[this.playersSelectedToPayWithCards];
                cmToPay.Refresh();
                //bindPlayerPayDebt(requestHandlerMD.CurrentPlayFieldModel);
            }
        }

        private void updateCardsToPayWithTotalValue(int total)
        {
            labelSelectedCardsToPayValue.Text = "Total Value of Selected Cards: $" + total;
        }

        private int calculateTotalValueOfCardsInList(List<MonopolyDealServiceReference.Card> listOfCards)
        {
            int totalValue = 0;
            foreach (MonopolyDealServiceReference.Card c in listOfCards)
            {
                totalValue += c.cardValue;
            }
            return totalValue;
        }

        private void buttonRemove_Click(object sender, EventArgs e)
        {
            MonopolyDealServiceReference.Card card = listBoxCardsToPayWith.SelectedItem as MonopolyDealServiceReference.Card;
            if (card != null)
            {
                playersSelectedToPayWithCards.Remove(card);
                playersPlayedCards.Add(card);
                updateCardsToPayWithTotalValue(calculateTotalValueOfCardsInList(playersSelectedToPayWithCards));
                //bindPlayerPayDebt(requestHandlerMD.CurrentPlayFieldModel);
                CurrencyManager cmPlayed = (CurrencyManager)BindingContext[this.playersPlayedCards];
                cmPlayed.Refresh();
                CurrencyManager cmToPay = (CurrencyManager)BindingContext[this.playersSelectedToPayWithCards];
                cmToPay.Refresh();
            }
        }

        private void buttonPay_Click(object sender, EventArgs e)
        {
            requestHandlerMD.payDebt(playersSelectedToPayWithCards);
            bindPlayerPayDebt(requestHandlerMD.CurrentPlayFieldModel);
            playersPlayedCards = new List<MonopolyDealServiceReference.Card>();
            playersSelectedToPayWithCards = new List<MonopolyDealServiceReference.Card>();
            CurrencyManager cmPlayed = (CurrencyManager)BindingContext[this.playersPlayedCards];
            cmPlayed.Refresh();
            CurrencyManager cmToPay = (CurrencyManager)BindingContext[this.playersSelectedToPayWithCards];
            cmToPay.Refresh();
            //Update State
            buttonPollMD_Click(null, null);
        }

        private void listBoxAllPlayersPlayedCards_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void listBoxCardsToPayWith_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void buttonJustSayNo_Click(object sender, EventArgs e)
        {
            int cardIDOfCardToUse = -1;
            if (listBox1.Items.Count != 0)
            {
                MonopolyDealServiceReference.ActionCard card = listBox1.SelectedValue as MonopolyDealServiceReference.ActionCard;
                if (card != null)
                {
                    cardIDOfCardToUse = card.cardID;
                    if (card.actionType.CompareTo(MonopolyDealServiceReference.ActionCardAction.JustSayNo) == 0)
                    {
                        requestHandlerMD.justSayNo(cardIDOfCardToUse);
                    }
                }
            }
            else
            {
                buttonJustSayNo.Enabled = false;
            }
            //Update State
            buttonPollMD_Click(null, null);
        }
    }
}