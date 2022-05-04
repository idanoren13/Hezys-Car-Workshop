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

        public Car(string i_ModelName, string i_LicenseNumber, int i_NumberOfWheels, string i_OwnersName, string i_OwnersNumber) : 
            base(i_ModelName, i_LicenseNumber, i_NumberOfWheels, i_OwnersName, i_OwnersNumber) { }

        public override object SelfParser(PropertyInfo i_PropertyToBeParsed, object valueToBeParsed)
        {
            
            throw new NotImplementedException();
        }

        public override Type GetEnumProperty(string i_PropertyName)
        {
            Type specificType;

            if (i_PropertyName == "Color")
            {
                specificType = typeof(eColor);
            }
            else if (i_PropertyName == "DoorsAmount")
            {
                specificType = typeof(eDoorsAmount);
            }
            else
            {
                throw new ArgumentException("BadEnumType:No_such_property"); //TODO ?
            }

            return specificType;
        }
    }
}
