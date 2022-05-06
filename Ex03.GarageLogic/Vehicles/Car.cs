using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Ex03.GarageLogic.Vehicles
{
    public class Car : Vehicle
    {
        public enum eColor
        {
            Red,
            Green,
            Blue,
            White
        }

        public enum eDoorsAmount
        {
            Two = 2,
            Three = 3,
            Four = 4,
            Five = 5
        }

        private eDoorsAmount m_AmountOfDoors;
        private eColor eCarColor;

        private const int k_MaxColorVal = 3;//todo
        private const int k_MinColorVal = 0;//todo
        private const int k_MaxDoorsVal = 3;//todo
        private const int k_MinDoorsVal = 0;//todo

        public eColor Color
        {
            get => eCarColor;
            set
            {
                if (value <= eColor.White && value >= eColor.Red)
                {
                    eCarColor = value;
                }
                else
                {
                    throw new ValueOutOfRangeException(0, 0); // Todo max min ?
                }
            }
        }

        public eDoorsAmount DoorsAmount
        {
            get => m_AmountOfDoors;
            set
            {
                if (value <= eDoorsAmount.Five && value >= eDoorsAmount.Two)
                {
                    m_AmountOfDoors = value;
                }
                else
                {
                    throw new ValueOutOfRangeException(0, 0); // Todo max min ?
                }
            }
        }


        public Car(string i_ModelName, string i_LicenseNumber, int i_NumberOfWheels, string i_OwnersName, string i_OwnersNumber) :
            base(i_ModelName, i_LicenseNumber, i_NumberOfWheels, i_OwnersName, i_OwnersNumber)
        { }

        public override object SelfParser(PropertyInfo i_PropertyToParse, object i_ValueToParse)
        {
            object parsedValue = null;
            if (i_ValueToParse != null)
            {
                string strValue = i_ValueToParse as string;
                //Wheel wheel in i_Vehicle.Wheels
                if (Equals(i_PropertyToParse, this.GetType().GetProperty("Color")))
                {
                    //TODO: check valid input
                    parsedValue = Enum.Parse(typeof(eColor), strValue);
                }
                else //it's the number of doors
                {
                    parsedValue = Enum.Parse(typeof(eDoorsAmount), strValue);
                }
            }
            else // parsedValue == null -> only return the object string that represents the properties value
            {
                if (Equals(i_PropertyToParse, this.GetType().GetProperty("Color")))
                {
                    //TODO: check valid input
                    parsedValue = eCarColor.ToString();
                }
                else //it's the number of doors
                {
                    parsedValue = m_AmountOfDoors.ToString();
                }
            }
            return parsedValue;
        }

        public override Type GetSelfPropertyType(string i_PropertyName)
        {
            Type uniqeType;

            if (i_PropertyName == "Color")
            {
                uniqeType = typeof(eColor);
            }
            else if (i_PropertyName == "DoorsAmount")
            {
                uniqeType = typeof(eDoorsAmount);
            }
            else
            {
                throw new ArgumentException("BadPropertyName:No_such_property"); //TODO ?
            }

            return uniqeType;
        }

        public override Vehicle DeepClone()
        {
            Car clonedCar = base.DeepClone() as Car;
            return clonedCar;
        }
    }
}