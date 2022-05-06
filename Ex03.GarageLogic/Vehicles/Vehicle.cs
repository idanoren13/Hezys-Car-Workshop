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
        public enum eVehicleStatus
        {
            InProcess,
            Fixed,
            Done
        }

        protected Engine m_Engine;
        protected Wheel[] m_Wheels;
        protected readonly int r_NumberOfWheels; 
        protected eVehicleStatus m_Status;
        private readonly string r_ModelName;
        private readonly string r_LicenseNumber;
        private readonly string r_OwnersName;
        private readonly string r_OwnersNumber;

        /// <summary>
        /// Vehicle c'tor
        /// internal for future dll's and extensibility option 
        /// </summary>
        internal Vehicle(string i_ModelName, string i_LicenseNumber, int i_NumberOfWheels, string i_OwnersName, string i_OwnersNumber)
        {
            r_ModelName = i_ModelName;
            r_LicenseNumber = i_LicenseNumber;
            r_NumberOfWheels = i_NumberOfWheels;
            m_Wheels = new Wheel[i_NumberOfWheels];
            m_Status = eVehicleStatus.InProcess;
            r_OwnersName = i_OwnersName;
            r_OwnersNumber = i_OwnersNumber;
        }

        public Engine Engine
        {
            get => m_Engine;
            set => m_Engine = value;
        }

        public Wheel[] Wheels
        {
            get => m_Wheels;
            set => m_Wheels = value;
        }

        public int NumberOfWheels
        {
            get => r_NumberOfWheels;
        }

        public eVehicleStatus Status
        {
            get => m_Status;
            set => m_Status = value;
        }

        public string LicenseNumber
        {
            get => r_LicenseNumber;
        }

        public string Model
        {
            get => r_ModelName;
        }

        public string OwnersName
        { 
            get => r_OwnersName; 
        }

        public string OwnersNumber 
        { 
            get => r_OwnersNumber; 
        }

        //todo internal or public ,U? u? 
        /// <summary>
        /// this method imposes parsing responsebilty 
        /// To all those who inherit from me Vehicle
        /// </summary>
        public abstract object SelfParser(PropertyInfo i_PropertyToBeParsed, object valueToBeParsed);

        public abstract Type GetEnumProperty(string i_PropertyName);

    }
}
