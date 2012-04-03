using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace MDWcfServiceLibrary
{
    [DataContract]
    public class PropertySetInfo
    {
        //Contains Rent Value Information
        [DataMember]
        private PropertyColor propertyColor;
        [DataMember]
        private int numberOfCardsInFullSet;
        [DataMember]
        private int onePropertyCardRentValue;
        [DataMember]
        private int twoPropertCardRentValue;
        [DataMember]
        private int threePropertyCardRentValue;
        [DataMember]
        private int fourPropertyCardRentValue;
        [DataMember]
        private int houseValue = 3;
        [DataMember]
        private int hotelValue = 4;

        public void setPropertyInfo(PropertyColor propertyColor, int numberOfCardsInFullSet, int onePropertyCardRentValue,
            int twoPropertCardRentValue, int threePropertyCardRentValue, int fourPropertyCardRentValue)
        {
            this.propertyColor = propertyColor;
            this.numberOfCardsInFullSet = numberOfCardsInFullSet;
            this.onePropertyCardRentValue = onePropertyCardRentValue;
            this.twoPropertCardRentValue = twoPropertCardRentValue;
            this.threePropertyCardRentValue = threePropertyCardRentValue;
            this.fourPropertyCardRentValue = fourPropertyCardRentValue;
        }

        public PropertySetInfo(PropertyColor pc)
        {
            int notAbleToBeFullSet = 100;
            switch (pc)
            {
                case PropertyColor.Blue:
                    setPropertyInfo(PropertyColor.Blue, 2, 3, 8, 8, 8);
                    break;
                case PropertyColor.Brown:
                    setPropertyInfo(PropertyColor.Brown, 2, 1, 2, 2, 2);
                    break;
                case PropertyColor.Green:
                    setPropertyInfo(PropertyColor.Green, 3, 2, 4, 7, 7);
                    break;
                case PropertyColor.LightBlue:
                    setPropertyInfo(PropertyColor.LightBlue, 3, 1, 2, 3, 3);
                    break;
                case PropertyColor.Orange:
                    setPropertyInfo(PropertyColor.Orange, 3, 1, 3, 5, 5);
                    break;
                case PropertyColor.Pink:
                    setPropertyInfo(PropertyColor.Pink, 3, 1, 2, 4, 4);
                    break;
                case PropertyColor.Red:
                    setPropertyInfo(PropertyColor.Red, 3, 2, 3, 6, 6);
                    break;
                case PropertyColor.Station:
                    setPropertyInfo(PropertyColor.Station, 4, 1, 2, 3, 4);
                    break;
                case PropertyColor.Utilities:
                    setPropertyInfo(PropertyColor.Utilities, 2, 1, 2, 2, 2);
                    break;
                case PropertyColor.Wild_MultiColored:
                    setPropertyInfo(PropertyColor.Wild_MultiColored, notAbleToBeFullSet, 0, 0, 0, 0);
                    break;
            }
        }

        public bool getIsFullSet(LinkedList<PropertyCard> propertyCards)
        {
            return (propertyCards.Count == numberOfCardsInFullSet);
        }

        public int getRentValue(PropertyColor propertyColor, int numberOfProperties, bool hasHouse, bool hasHotel)
        {
            int rentValue = 0;
            switch (numberOfProperties)
            {
                case 1:
                    rentValue = onePropertyCardRentValue;
                    break;
                case 2:
                    rentValue = twoPropertCardRentValue;
                    if (numberOfProperties == numberOfCardsInFullSet && propertyColor != PropertyColor.Utilities && hasHouse)
                    {
                        rentValue += houseValue;
                        if (hasHotel)
                        {
                            rentValue += hotelValue;
                        }
                    }
                    break;
                case 3:
                    rentValue = threePropertyCardRentValue;
                    if (numberOfProperties == numberOfCardsInFullSet && hasHouse)
                    {
                        rentValue += houseValue;
                        if (hasHotel)
                        {
                            rentValue += hotelValue;
                        }
                    }
                    break;
                case 4:
                    rentValue = fourPropertyCardRentValue;
                    break;
                default:
                    rentValue = 0;
                    break;
            }
            return rentValue;
        }
    }
}