using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MDWcfWFClient
{
    public partial class UseActionCardForm : Form
    {
        public UseActionCardForm()
        {
            InitializeComponent();
        }

        RequestHanderMonopolyDeal request;
        MonopolyDealServiceReference.ActionCard Card;

        public UseActionCardForm(MonopolyDealServiceReference.PlayFieldModel pfm, Guid thisClientGuid, RequestHanderMonopolyDeal rqmd, MonopolyDealServiceReference.ActionCard card)
        {
            InitializeComponent();
            if (card != null)
            {
                if (card.actionType.CompareTo(MonopolyDealServiceReference.ActionCardAction.DealBreaker) == 0)
                {
                    this.Text = "Use DealBreaker Card";
                    buttonUseDealBreaker.Text = "DealBreaker";
                }
                else if (card.actionType.CompareTo(MonopolyDealServiceReference.ActionCardAction.SlyDeal) == 0)
                {
                    this.Text = "Use Sly Deal Card";
                    buttonUseDealBreaker.Text = "Sly Deal";
                    groupBox1.Text = "Pick a Card to Sly Deal";
                }
                else if (card.actionType.CompareTo(MonopolyDealServiceReference.ActionCardAction.House) == 0)
                {
                    this.Text = "Use House Card";
                    buttonUseDealBreaker.Text = "Add House";
                    groupBox1.Text = "Pick a Full Set to add a House to";
                }
                else if (card.actionType.CompareTo(MonopolyDealServiceReference.ActionCardAction.Hotel) == 0)
                {
                    this.Text = "Use Hotel Card";
                    buttonUseDealBreaker.Text = "Add Hotel";
                    groupBox1.Text = "Pick a Full Set to add a Hotel to";
                }
                else if (card.actionType.CompareTo(MonopolyDealServiceReference.ActionCardAction.ForcedDeal) == 0)
                {
                    this.Text = "Use Forced Deal Card";
                    this.Height = 567;
                    buttonUseDealBreaker.Text = "Use Forced Deal";
                    groupBox1.Text = "Pick a Card to take in a Forced Deal";
                }
            }
            request = rqmd;
            this.Card = card;

            if (pfm.playerModels != null)
            {
                listBoxPlayers.DataSource = pfm.playerModels;
                listBoxPlayers.DisplayMember = "name";
                if (listBoxPlayers.SelectedValue != null)
                {
                    listBoxSets.DataSource = ((MonopolyDealServiceReference.PlayerModel)listBoxPlayers.SelectedValue).propertySets.playersPropertySets;
                    listBoxSets.DisplayMember = "propertySetColor";
                    if (listBoxSets.SelectedValue != null)
                    {
                        listBoxCardsInSet.DataSource = ((MonopolyDealServiceReference.PropertyCardSet)listBoxSets.SelectedValue).properties.ToArray<MonopolyDealServiceReference.Card>();
                        listBoxCardsInSet.DisplayMember = "description";
                        listBoxCardsInSet.ValueMember = "cardID";
                    }
                }
            }
        }

        private void DealBreaker_Load(object sender, EventArgs e)
        {
        }

        private void bindSelectPlayersPropertyCardSetsPropertyCards(MonopolyDealServiceReference.PlayFieldModel pfm)
        {
            if (pfm.playerModels != null)
            {
                listBoxPlayers.DataSource = pfm.playerModels;
                listBoxPlayers.DisplayMember = "name";
                if (listBoxPlayers.SelectedValue != null)
                {
                    listBoxSets.DataSource = ((MonopolyDealServiceReference.PlayerModel)listBoxPlayers.SelectedValue).propertySets.playersPropertySets;
                    listBoxSets.DisplayMember = "propertySetColor";
                    if (listBoxSets.SelectedValue != null)
                    {
                        listBoxCardsInSet.DataSource = ((MonopolyDealServiceReference.PropertyCardSet)listBoxSets.SelectedValue).properties.ToArray<MonopolyDealServiceReference.Card>();
                        listBoxCardsInSet.DisplayMember = "description";
                        listBoxCardsInSet.ValueMember = "cardID";
                    }
                }
            }
        }

        private void listBoxPlayers_SelectedIndexChanged(object sender, EventArgs e)
        {
            bindSelectPlayersPropertyCardSetsPropertyCards(request.CurrentPlayFieldModel);
        }

        private void listBoxSets_SelectedIndexChanged(object sender, EventArgs e)
        {
            bindSelectPlayersPropertyCardSetsPropertyCards(request.CurrentPlayFieldModel);
        }

        private void buttonUseDealBreaker_Click(object sender, EventArgs e)
        {
            if (Card != null)
            {
                if (Card.actionType.CompareTo(MonopolyDealServiceReference.ActionCardAction.DealBreaker) == 0)
                {
                    //this.Text = "Use DealBreaker Card";
                    if (listBoxPlayers.SelectedItem != null)
                    {
                        MonopolyDealServiceReference.PlayerModel playerTargeted = (MonopolyDealServiceReference.PlayerModel)listBoxPlayers.SelectedItem;
                        if (playerTargeted.guid.CompareTo(request.thisClientGuid) == 0)
                        {
                            MessageBox.Show("Can not DealBreaker yourself");
                            return;
                        }
                        else
                        {
                            MonopolyDealServiceReference.PropertyCardSet targetedSet = listBoxSets.SelectedItem as MonopolyDealServiceReference.PropertyCardSet;
                            if (targetedSet != null)
                            {
                                request.dealBreaker(Card, playerTargeted.guid, targetedSet);
                                this.Close();
                            }
                            else
                            {
                                MessageBox.Show("No set selected");
                                return;
                            }
                        }
                    }
                }
                else if (Card.actionType.CompareTo(MonopolyDealServiceReference.ActionCardAction.SlyDeal) == 0)
                {
                    //this.Text = "Use Sly Deal Card";
                    //buttonUseDealBreaker.Text = "Sly Deal";
                    //this.Text = "Use DealBreaker Card";
                    if (listBoxPlayers.SelectedItem != null)
                    {
                        MonopolyDealServiceReference.PlayerModel playerTargeted = (MonopolyDealServiceReference.PlayerModel)listBoxPlayers.SelectedItem;
                        if (playerTargeted.guid.CompareTo(request.thisClientGuid) == 0)
                        {
                            MessageBox.Show("Can not Sly Deal yourself");
                            return;
                        }
                        else
                        {
                            MonopolyDealServiceReference.PropertyCardSet targetedSet = listBoxSets.SelectedItem as MonopolyDealServiceReference.PropertyCardSet;
                            if (targetedSet != null)
                            {
                                MonopolyDealServiceReference.Card targetedCard = listBoxCardsInSet.SelectedItem as MonopolyDealServiceReference.Card;
                                if (targetedCard != null)
                                {
                                    request.slyDeal(Card, playerTargeted.guid, targetedSet, targetedCard);
                                    this.Close();
                                }
                                else
                                {
                                    MessageBox.Show("No Card selected");
                                }
                            }
                            else
                            {
                                MessageBox.Show("No set selected");
                                return;
                            }
                        }
                    }
                }
                else if (Card.actionType.CompareTo(MonopolyDealServiceReference.ActionCardAction.House) == 0)
                {
                    //this.Text = "Use House Card";
                    //buttonUseDealBreaker.Text = "Add House";
                    if (listBoxPlayers.SelectedItem != null)
                    {
                        MonopolyDealServiceReference.PlayerModel playerTargeted = (MonopolyDealServiceReference.PlayerModel)listBoxPlayers.SelectedItem;
                        if (playerTargeted.guid.CompareTo(request.thisClientGuid) != 0)
                        {
                            MessageBox.Show("Can not add a House to another Players Sets");
                            return;
                        }
                        else
                        {
                            MonopolyDealServiceReference.PropertyCardSet targetedSet = listBoxSets.SelectedItem as MonopolyDealServiceReference.PropertyCardSet;
                            if (targetedSet != null)
                            {
                                if (targetedSet.hasHouse)
                                {
                                    MessageBox.Show("Can not add more than one house to a set");
                                    this.Close();
                                }
                                else
                                {
                                    request.house(Card, targetedSet);
                                    this.Close();
                                }
                            }
                            else
                            {
                                MessageBox.Show("No set selected");
                                return;
                            }
                        }
                    }
                }
                else if (Card.actionType.CompareTo(MonopolyDealServiceReference.ActionCardAction.Hotel) == 0)
                {
                    //this.Text = "Use Hotel Card";
                    //buttonUseDealBreaker.Text = "Add Hotel";
                    if (listBoxPlayers.SelectedItem != null)
                    {
                        MonopolyDealServiceReference.PlayerModel playerTargeted = (MonopolyDealServiceReference.PlayerModel)listBoxPlayers.SelectedItem;
                        if (playerTargeted.guid.CompareTo(request.thisClientGuid) != 0)
                        {
                            MessageBox.Show("Can not add a Hotel to another Players Sets");
                            return;
                        }
                        else
                        {
                            MonopolyDealServiceReference.PropertyCardSet targetedSet = listBoxSets.SelectedItem as MonopolyDealServiceReference.PropertyCardSet;
                            if (targetedSet != null)
                            {
                                if (targetedSet.hasHouse != true)
                                {
                                    MessageBox.Show("Can not add a Hotel to a set that does not have a House");
                                    this.Close();
                                }
                                else if (targetedSet.hasHotel)
                                {
                                    MessageBox.Show("Can not add more than one Hotel to a Set");
                                }
                                else
                                {
                                    request.hotel(Card, targetedSet);
                                    this.Close();
                                }
                            }
                            else
                            {
                                MessageBox.Show("No set selected");
                                return;
                            }
                        }
                    }
                }
                else if (Card.actionType.CompareTo(MonopolyDealServiceReference.ActionCardAction.ForcedDeal) == 0)
                {
                    this.Text = "Use Forced Deal Card";
                    buttonUseDealBreaker.Text = "Use Forced Deal";
                }
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {
        }

        private void listBoxSetCardToGiveUpIsIn_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}