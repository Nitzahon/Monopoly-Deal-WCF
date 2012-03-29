using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace MDWcfServiceLibrary
{
    [DataContract]
    public enum PropertyColor
    {
        Brown,
        LightBlue,
        Pink,
        Orange,
        Red,
        Yellow,
        Green,
        Blue,
        Station,
        Utilities,
        Wild_LightBlue_Brown,
        Wild_LightBlue_Station,
        Wild_Pink_Orange,
        Wild_Red_Yellow,
        Wild_Blue_Green,
        Wild_Green_Station,
        Wild_Station_Utility,
        Wild_MultiColored,
    }

    [DataContract]
    public class PropertyCard : Card
    {
        public void setCardUp(PropertyColor newUpColor)
        {
            if (propertyColors[0] == newUpColor)
            {
                isCardUp = true;
            }
            else
            {
                isCardUp = false;
            }
        }

        public PropertyCard(PropertyColor color, String name, int fullsetSize, int oneCardVal, int twoCardVal, int threeCardVal, int fourCardVal, int fiveCardVal, int bankVal)
            : base(name, "rent", bankVal, CardType.Property)
        {
            //Standard Property card
            isWild = false;
            isMultiWild = false;
            isCardUp = true;
            propertyColors = new List<PropertyColor>();
            this.currentPropertyColor = color;
            this.currentColorSetCompleteSize = fullsetSize;
            this.oneCardInSetRentValue = oneCardVal;
            this.twoCardInSetRentValue = twoCardVal;
            this.threeCardInSetRentValue = threeCardVal;
            this.fourCardInSetRentValue = fourCardVal;
            this.fiveCardInSetRentValue = fiveCardVal;
        }

        public PropertyCard(PropertyColor colorUp, PropertyColor colorDown, String nameUp, int fullsetSizeUp, int oneCardValUp, int twoCardValUp, int threeCardValUp, int fourCardValUp, int fiveCardValUp,
            String nameDown, int fullsetSizeDown, int oneCardValDown, int twoCardValDown, int threeCardValDown, int fourCardValDown, int fiveCardValDown, int bankVal)
            : base(nameUp, "Use either way up", bankVal, CardType.WildProperty)
        {
            //Two Color Wild Property Card
            isWild = true;
            isMultiWild = false;
            isCardUp = true;
            propertyColors = new List<PropertyColor>();
            this.currentPropertyColor = colorUp;
            this.currentColorSetCompleteSize = fullsetSizeUp;
            this.oneCardInSetRentValue = oneCardValUp;
            this.twoCardInSetRentValue = twoCardValUp;
            this.threeCardInSetRentValue = threeCardValUp;
            this.fourCardInSetRentValue = fourCardValUp;
            this.fiveCardInSetRentValue = fiveCardValUp;

            this.propertyColors.Add(colorUp);
            this.propertyColors.Add(colorDown);

            this.upSetSize = fullsetSizeUp;
            this.oneCardInSetRentValueUp = oneCardValUp;
            this.twoCardInSetRentValueUp = twoCardValUp;
            this.threeCardInSetRentValueUp = threeCardValUp;
            this.fourCardInSetRentValueUp = fourCardValUp;
            this.fiveCardInSetRentValueUp = fiveCardValUp;

            this.downSetSize = fullsetSizeDown;
            this.oneCardInSetRentValueDown = oneCardValDown;
            this.twoCardInSetRentValueDown = twoCardValDown;
            this.threeCardInSetRentValueDown = threeCardValDown;
            this.fourCardInSetRentValueDown = fourCardValDown;
            this.fiveCardInSetRentValueDown = fiveCardValDown;
        }

        public PropertyCard(PropertyColor color)
            : base("PROPERTY WILD CARD", "This card can be used as part of any property set. This card has no monetary value.", 0, CardType.WildProperty)
        {
            //MultiColor Wild PropertyCard
            isCardUp = true;
            isWild = true;
            isMultiWild = true;
            propertyColors = new List<PropertyColor>();
            propertyColors.Add(PropertyColor.Wild_MultiColored);
            this.currentPropertyColor = PropertyColor.Wild_MultiColored;
        }

        public PropertyColor getPropertyColor()
        {
            if (isCardUp)
            {
                return propertyColors[0];
            }
            else
            {
                //-1 as picking last color
                return propertyColors[propertyColors.Count - 1];
            }
        }

        public void setPropertyColor(bool isUp)
        {
            isCardUp = isUp;
        }

        [DataMember]
        public PropertyColor currentPropertyColor;
        //List of Colors the property may take, Up Orientation Color first then Down Orientation Colour
        [DataMember]
        public List<PropertyColor> propertyColors;
        [DataMember]
        public bool isWild;
        [DataMember]
        public bool isMultiWild;
        [DataMember]
        public bool isCardUp;
        [DataMember]
        public int currentColorSetCompleteSize;
        [DataMember]
        public int oneCardInSetRentValue;
        [DataMember]
        public int twoCardInSetRentValue;
        [DataMember]
        public int threeCardInSetRentValue;
        [DataMember]
        public int fourCardInSetRentValue;
        [DataMember]
        public int fiveCardInSetRentValue;
        [DataMember]
        public int upSetSize;
        [DataMember]
        public int oneCardInSetRentValueUp;
        [DataMember]
        public int twoCardInSetRentValueUp;
        [DataMember]
        public int threeCardInSetRentValueUp;
        [DataMember]
        public int fourCardInSetRentValueUp;
        [DataMember]
        public int fiveCardInSetRentValueUp;
        [DataMember]
        public int downSetSize;
        [DataMember]
        public int oneCardInSetRentValueDown;
        [DataMember]
        public int twoCardInSetRentValueDown;
        [DataMember]
        public int threeCardInSetRentValueDown;
        [DataMember]
        public int fourCardInSetRentValueDown;
        [DataMember]
        public int fiveCardInSetRentValueDown;
    }
}