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
    public partial class MovePropertyToNewSetForm : Form
    {
        public MovePropertyToNewSetForm(MonopolyDealServiceReference.PlayFieldModel pfm, Guid thisClientGuid)
        {
            InitializeComponent();
            displayOriginalSet(pfm, thisClientGuid);
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
                this.listBoxPickOriginalSetToRemoveCardFrom.DataSource = playerClient.propertySets.playersPropertySets;
                this.listBoxPickOriginalSetToRemoveCardFrom.DisplayMember = "propertySetColor";

                this.listBoxSetToMovePropertyTo.DataSource = playerClient.propertySets.playersPropertySets.ToArray<MonopolyDealServiceReference.PropertyCardSet>();
                this.listBoxSetToMovePropertyTo.DisplayMember = "propertySetColor";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
        }

        private void listBoxPickOriginalSetToRemoveCardFrom_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxPickOriginalSetToRemoveCardFrom.SelectedValue != null && listBoxPickOriginalSetToRemoveCardFrom.DataSource != null)
            {
                listBoxPickProperty.DataSource = ((MonopolyDealServiceReference.PropertyCardSet)listBoxPickOriginalSetToRemoveCardFrom.SelectedValue).properties.ToArray<MonopolyDealServiceReference.Card>();
                listBoxPickProperty.DisplayMember = "description";
                listBoxPickProperty.ValueMember = "cardID";
            }
        }

        private void listBoxPickProperty_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void listBoxSetToMovePropertyTo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxPickOriginalSetToRemoveCardFrom.SelectedValue != null && listBoxPickOriginalSetToRemoveCardFrom.DataSource != null)
            {
                this.listBoxCardsInSelectedSet.DataSource = ((MonopolyDealServiceReference.PropertyCardSet)listBoxPickOriginalSetToRemoveCardFrom.SelectedValue).properties.ToArray<MonopolyDealServiceReference.Card>();
                this.listBoxCardsInSelectedSet.DisplayMember = "description";
                this.listBoxCardsInSelectedSet.ValueMember = "cardID";
            }
        }

        private void listBoxCardsInSelectedSet_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
    }
}