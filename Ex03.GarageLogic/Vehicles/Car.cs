using System;
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
            Two,
            Three,
            Four,
            Five
        }

        private eDoorsAmount m_AmountOfDoors;
        private eColor eCarColor;

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
                    throw new ValueOutOfRangeException(3, 0);
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
                    throw new ValueOutOfRangeException(3, 0);
                }
            }
        }

        public Car(string i_ModelName, string i_LicenseNumber, int i_NumberOfWheels, string i_OwnersName, string i_OwnersNumber) :
        base(i_ModelName, i_LicenseNumber, i_NumberOfWheels, i_OwnersName, i_OwnersNumber, VehicleFactory.eVehicleType.Car) { }

        public override string ToString()
        {
            StringBuilder carToString = new StringBuilder();

            carToString.Append(GeneralInfoToString());
            carToString.Append($"{Environment.NewLine}The amount of doors: {this.m_AmountOfDoors} ");
            carToString.Append($"{Environment.NewLine}The Color is: {this.eCarColor} ");

            return carToString.ToString();
        }
    }
}