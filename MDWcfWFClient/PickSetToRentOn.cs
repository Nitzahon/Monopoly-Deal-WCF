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
    public partial class PickSetToRentOn : Form
    {
        RequestHanderMonopolyDeal request;
        MonopolyDealServiceReference.Card RentCard;
        Guid targetedPlayer;

        public PickSetToRentOn(MonopolyDealServiceReference.PlayFieldModel pfm, Guid thisClientGuid, RequestHanderMonopolyDeal rqmd, MonopolyDealServiceReference.Card card, Guid targetedPlayerWildRent)
        {
            request = rqmd;
            this.RentCard = card;
            targetedPlayer = targetedPlayerWildRent;
            InitializeComponent();
            displayOriginalSet(pfm, thisClientGuid);
            displayCardsInHand(pfm);
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {
        }

        private void PickSetToRentOn_Load(object sender, EventArgs e)
        {
        }

        private void displayOriginalSet(MonopolyDealServiceReference.PlayFieldModel pfm, Guid thisClientGuid)
        {
            //Get correct player
            MonopolyDealServiceReference.PlayerModel playerClient = null;
            foreach (MonopolyDealServiceReference.PlayerModel p in pfm.playerModels)
            {
                if (p.guid.CompareTo(thisClientGuid) == 0)
                {
                    playerClient = p;
                    break;
                }
            }
            if (playerClient != null)
            {
                this.listBoxSet.DataSource = playerClient.propertySets.playersPropertySets;
                this.listBoxSet.DisplayMember = "propertySetColor";
            }
        }

        private void displayCardsInHand(MonopolyDealServiceReference.PlayFieldModel pfm)
        {
            foreach (MonopolyDealServiceReference.PlayerModel p in this.request.CurrentPlayFieldModel.playerModels)
            {
                if (p.guid.CompareTo(request.thisClientGuid) == 0)
                {
                    listBoxCardsInHand.DataSource = p.hand.cardsInHand;
                    listBoxCardsInHand.DisplayMember = "description";
                }
            }
        }

        private void listBoxSet_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxSet.SelectedValue != null && listBoxSet.DataSource != null)
            {
                this.listBoxCardsInSet.DataSource = ((MonopolyDealServiceReference.PropertyCardSet)listBoxSet.SelectedValue).properties;
                listBoxCardsInSet.DisplayMember = "description";
                listBoxCardsInSet.ValueMember = "cardID";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //use rent no double the rent
            MonopolyDealServiceReference.PropertyCardSet selectedSet = listBoxSet.SelectedItem as MonopolyDealServiceReference.PropertyCardSet;
            MonopolyDealServiceReference.Card doubleTheRentCard = null;
            int doubleTheRentCardID = -1;
            if (doubleTheRentCard != null)
            {
                doubleTheRentCardID = doubleTheRentCard.cardID;
            }
            bool usingDoubleTheRent = false;
            bool isWildRentCard = true;
            if (RentCard is MonopolyDealServiceReference.RentStandard)
            {
                isWildRentCard = false;
            }
            request.useRentCard(RentCard.cardID, selectedSet.guid, usingDoubleTheRent, doubleTheRentCardID, isWildRentCard, targetedPlayer);
        }

        private void buttonRentDouble_Click(object sender, EventArgs e)
        {
            MonopolyDealServiceReference.PropertyCardSet selectedSet = listBoxSet.SelectedItem as MonopolyDealServiceReference.PropertyCardSet;
            MonopolyDealServiceReference.ActionCard doubleTheRentCard = listBoxCardsInHand.SelectedItem as MonopolyDealServiceReference.ActionCard;
            int doubleTheRentCardID = -1;
            if (doubleTheRentCard != null && doubleTheRentCard.actionType.CompareTo(MonopolyDealServiceReference.ActionCardAction.DoubleTheRent) == 0)
            {
                doubleTheRentCardID = doubleTheRentCard.cardID;
            }
            else
            {
                MessageBox.Show("No card Selected or is not a double the rent card");
                return;
            }
            bool usingDoubleTheRent = true;
            bool isWildRentCard = true;
            if (RentCard is MonopolyDealServiceReference.RentStandard)
            {
                isWildRentCard = false;
            }
            request.useRentCard(RentCard.cardID, selectedSet.guid, usingDoubleTheRent, doubleTheRentCardID, isWildRentCard, targetedPlayer);
        }
    }
}