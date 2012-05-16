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
        RequestHanderMonopolyDeal request;

        public MovePropertyToNewSetForm(MonopolyDealServiceReference.PlayFieldModel pfm, Guid thisClientGuid, RequestHanderMonopolyDeal rqmd)
        {
            request = rqmd;
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
            //new up
            MonopolyDealServiceReference.PropertyCard pickedPropertyToMove = listBoxPickProperty.SelectedValue as MonopolyDealServiceReference.PropertyCard;
            MonopolyDealServiceReference.PropertyCardSet newPropertySet = null;
            MonopolyDealServiceReference.PropertyCardSet oldPropertySet = listBoxPickOriginalSetToRemoveCardFrom.SelectedValue as MonopolyDealServiceReference.PropertyCardSet;
            bool isCardUp = true;
            bool playToExistingSet = false;
            if (pickedPropertyToMove != null)
            {
                if (newPropertySet == null)
                {
                    if (oldPropertySet != null)
                    {
                        if (request.moveProperty(pickedPropertyToMove.cardID, oldPropertySet.guid, newPropertySet.guid, isCardUp, playToExistingSet))
                        {
                            MessageBox.Show("Property Moved");
                        }
                    }
                    else
                    {
                        MessageBox.Show("No old property set picked");
                    }
                }
                else
                {
                    MessageBox.Show("No new Property set picked");
                }
            }
            else
            {
                MessageBox.Show("No Property picked");
            }
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

        private void buttonMoveSelected_Click(object sender, EventArgs e)
        {
            //selected up
            MonopolyDealServiceReference.PropertyCard pickedPropertyToMove = listBoxPickProperty.SelectedItem as MonopolyDealServiceReference.PropertyCard;
            MonopolyDealServiceReference.PropertyCardSet newPropertySet = listBoxSetToMovePropertyTo.SelectedItem as MonopolyDealServiceReference.PropertyCardSet;
            MonopolyDealServiceReference.PropertyCardSet oldPropertySet = listBoxPickOriginalSetToRemoveCardFrom.SelectedItem as MonopolyDealServiceReference.PropertyCardSet;
            bool isCardUp = true;
            bool playToExistingSet = true;
            if (pickedPropertyToMove != null)
            {
                if (newPropertySet != null)
                {
                    if (oldPropertySet != null)
                    {
                        if (request.moveProperty(pickedPropertyToMove.cardID, oldPropertySet.guid, newPropertySet.guid, isCardUp, playToExistingSet))
                        {
                            MessageBox.Show("Property Moved");
                        }
                    }
                    else
                    {
                        MessageBox.Show("No old property set picked");
                    }
                }
                else
                {
                    MessageBox.Show("No new Property set picked");
                }
            }
            else
            {
                MessageBox.Show("No Property picked");
            }
        }

        private void buttonMoveToSelectedSetDown_Click(object sender, EventArgs e)
        {
            //selected down
            MonopolyDealServiceReference.PropertyCard pickedPropertyToMove = listBoxPickProperty.SelectedItem as MonopolyDealServiceReference.PropertyCard;
            MonopolyDealServiceReference.PropertyCardSet newPropertySet = listBoxSetToMovePropertyTo.SelectedItem as MonopolyDealServiceReference.PropertyCardSet;
            MonopolyDealServiceReference.PropertyCardSet oldPropertySet = listBoxPickOriginalSetToRemoveCardFrom.SelectedItem as MonopolyDealServiceReference.PropertyCardSet;
            bool isCardUp = false;
            bool playToExistingSet = true;
            if (pickedPropertyToMove != null)
            {
                if (newPropertySet != null)
                {
                    if (oldPropertySet != null)
                    {
                        if (request.moveProperty(pickedPropertyToMove.cardID, oldPropertySet.guid, newPropertySet.guid, isCardUp, playToExistingSet))
                        {
                            MessageBox.Show("Property Moved");
                        }
                    }
                    else
                    {
                        MessageBox.Show("No old property set picked");
                    }
                }
                else
                {
                    MessageBox.Show("No new Property set picked");
                }
            }
            else
            {
                MessageBox.Show("No Property picked");
            }
        }

        private void buttonMoveNewSetDown_Click(object sender, EventArgs e)
        {
            //new down
            MonopolyDealServiceReference.PropertyCard pickedPropertyToMove = listBoxPickProperty.SelectedValue as MonopolyDealServiceReference.PropertyCard;
            MonopolyDealServiceReference.PropertyCardSet newPropertySet = null;
            MonopolyDealServiceReference.PropertyCardSet oldPropertySet = listBoxPickOriginalSetToRemoveCardFrom.SelectedValue as MonopolyDealServiceReference.PropertyCardSet;
            bool isCardUp = false;
            bool playToExistingSet = false;
            if (pickedPropertyToMove != null)
            {
                if (newPropertySet == null)
                {
                    if (oldPropertySet != null)
                    {
                        if (request.moveProperty(pickedPropertyToMove.cardID, oldPropertySet.guid, newPropertySet.guid, isCardUp, playToExistingSet))
                        {
                            MessageBox.Show("Property Moved");
                        }
                    }
                    else
                    {
                        MessageBox.Show("No old property set picked");
                    }
                }
                else
                {
                    MessageBox.Show("No new Property set picked");
                }
            }
            else
            {
                MessageBox.Show("No Property picked");
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Move property cancelled");
        }

        private void MovePropertyToNewSetForm_Load(object sender, EventArgs e)
        {
        }
    }
}