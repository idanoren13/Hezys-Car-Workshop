using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using static Ex03.GarageLogic.VehicleParts; // todo ?

namespace Ex03.GarageLogic
{
    public class Vehicle
    {
        public enum eNumberOfWheels
        {
            TwoWheeled = 2,
            FourWheeled = 4,
            SixteenWheeled = 16
        }

        public enum eVehicleType
        {
            Car,
            Motorcycle,
            Truck
        }

        enum eVehicleStatus //TODO
        {
            afterHezys,
            beforeHezysTreets
        }

        private Engine m_Engine;
        private Collection<Wheel> m_Wheels;
        public eNumberOfWheels r_NumberOfWheels; // public ? todo
        private eVehicleStatus m_Status;
        private eVehicleType type;
        private string m_ModelName;         //readOnly ?
        private string m_OwnersName;        //readOnly ?
        private string m_OwnersPhoneNumber; //readOnly ?     

        public Vehicle(string i_ModelName, string i_OwnersName, string i_OwnersPhoneNumber, eVehicleType i_VehicleType)
        {
            m_Status = eVehicleStatus.beforeHezysTreets; // todo
            m_ModelName = i_ModelName;
            m_OwnersName = i_OwnersName;
            m_OwnersPhoneNumber = i_OwnersPhoneNumber;

            switch (i_VehicleType) //extract todo
            {
                case eVehicleType.Car:
                    r_NumberOfWheels = eNumberOfWheels.FourWheeled;
                    break;
                case eVehicleType.Motorcycle:
                    r_NumberOfWheels = eNumberOfWheels.TwoWheeled;
                    break;
                case eVehicleType.Truck:
                    r_NumberOfWheels = eNumberOfWheels.SixteenWheeled;
                    break;
            }

            m_Wheels = new Collection<Wheel>();
            for (int i = 0; i < (int)r_NumberOfWheels; i++)
            {
                Wheel correspondWheel = new Wheel("subaro", 32);
                m_Wheels.Add(correspondWheel); // TODO 
            }

            m_Engine = new Engine();// todo
        }

        public class Car
        {
            enum eColor
            {
                Red,
                Green,
                Blue,
                White
            }

            enum eDoorsAmount
            {
                Two = 2,
                Three,
                Four,
                Five
            }

            private eDoorsAmount m_AmountOfDoors;
            private eColor eCarColor;

        }

        public class Motorike
        {
            enum eLicenseType
            {
                A,
                A1,
                B1,
                BB
            }

            private int m_EngineSize;

        }

        public class Lorry
        {
            private float m_CargoVolume;
            private bool m_IsContainingFrozenContents;

        }
    }
}
