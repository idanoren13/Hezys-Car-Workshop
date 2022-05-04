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
            Three,
            Four,
            Five
        }

        private eDoorsAmount m_AmountOfDoors;
        private eColor eCarColor;

        public Car() : base("mazda" ,"bivas" , "04055555" , eVehicleType.Car) { } // todo maybe no need for all this shiit to ctor?

        public override object SelfParser(PropertyInfo i_PropertyToBeParsed, object valueToBeParsed)
        {
            throw new NotImplementedException();
        }

        public override Type GetEnumProperty(string i_EnumPropertyID)
        {
            Type specificType;
            if (i_EnumPropertyID == "Color")
            {
                specificType = typeof(eColor);
            }
            else if (i_EnumPropertyID == "DoorsAmount")
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
