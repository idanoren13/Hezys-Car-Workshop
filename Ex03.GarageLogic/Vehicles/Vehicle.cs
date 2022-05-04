using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Generic;

using System.Collections.ObjectModel;
using System.Reflection;
using static Ex03.GarageLogic.VehicleParts; // todo ?

namespace Ex03.GarageLogic
{
    public abstract class Vehicle
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

        public enum eVehicleStatus
        {
            InProcess,
            Fixed,
            Done
        }

        private Engine m_Engine;
        private Collection<Wheel> m_Wheels;
        public eNumberOfWheels r_NumberOfWheels; // public ? todo
        private eVehicleStatus m_Status;
        private eVehicleType type;
        private string m_ModelName;         //readOnly ?
        private string m_OwnersName;        //readOnly ?
        private string m_OwnersPhoneNumber; //readOnly ?     

        /// <summary>
        /// Vehicle c'tor
        /// internal for future dll's and extensibility option 
        /// </summary>
        internal Vehicle(string i_ModelName, string i_OwnersName, string i_OwnersPhoneNumber, eVehicleType i_VehicleType)
        {
            m_Status = eVehicleStatus.InProcess; // todo
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

        //todo internal or public ,U? u? 
        /// <summary>
        /// this method imposes parsing responsebilty 
        /// To all those who inherit from me Vehicle
        /// </summary>
        public abstract object SelfParser(PropertyInfo i_PropertyToBeParsed, object valueToBeParsed);

        public abstract Type GetEnumProperty(string i_PropertyName); //<Enter number option, pick-able object

        public eVehicleStatus Status
        {
            get => m_Status;
            set => m_Status = value;
        }

        public string Model
        {
            get => m_ModelName;
            set => m_ModelName = value;
        }

 
  

        public class MotorBike
        {
            private int m_EngineSize;

            enum eLicenseType
            {
                A,
                A1,
                B1,
                BB
            }


        }

        public class Lorry
        {
            private float m_CargoVolume;
            private bool m_IsContainingFrozenContents;

        }
    }
}
