using System;
using System.Collections.Generic;
using System.Reflection;
using static Ex03.GarageLogic.VehicleParts;
using System.Text;

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
        private readonly VehicleFactory.eVehicleType r_VehicleType;
        private List<MethodInfo> m_UniqueMethods;

        /// <summary>
        /// Vehicle c'tor
        /// internal for future dll's and extensibility option 
        /// </summary>
        internal Vehicle(string i_ModelName, string i_LicenseNumber, int i_NumberOfWheels, string i_OwnersName, string i_OwnersNumber, VehicleFactory.eVehicleType i_VehicleType)
        {
            r_ModelName = i_ModelName;
            r_LicenseNumber = i_LicenseNumber;
            r_NumberOfWheels = i_NumberOfWheels;
            m_Wheels = new Wheel[i_NumberOfWheels];
            for (int i = 0; i < i_NumberOfWheels; i++)
            {
                m_Wheels[i] = new Wheel();
            }

            m_Status = eVehicleStatus.InProcess;
            r_OwnersName = i_OwnersName;
            r_OwnersNumber = i_OwnersNumber;
            r_VehicleType = i_VehicleType;
            //m_UniqueProperties = new List<PropertyInfo>();
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

        public VehicleFactory.eVehicleType VehicleType
        {
            get => r_VehicleType;
        }

        public List<MethodInfo> UniqueMethods
        { 
            get => m_UniqueMethods;
            set => m_UniqueMethods = value;
        }

        public void SetMaxAirPressure(float i_MaxAirPresure)
        {
            foreach (VehicleParts.Wheel wheel in m_Wheels)
            {
                wheel.MaxAirPressure = i_MaxAirPresure;
            }
        }

        protected string generalInfoToString()
        {
            StringBuilder VehicleToString = new StringBuilder();

            VehicleToString.Append("License Number: ");
            VehicleToString.Append(this.LicenseNumber);
            VehicleToString.Append($"{Environment.NewLine}Model: ");
            VehicleToString.Append(this.Model);
            VehicleToString.Append($"{Environment.NewLine}Owners: ");
            VehicleToString.Append(this.OwnersName);
            VehicleToString.Append($"{Environment.NewLine}Status: ");
            VehicleToString.Append(this.Status);

            return VehicleToString.ToString();
        }

        public string ToStringAllWheels()
        {
            int wheelIndex = 0;
            StringBuilder WheelsToString = new StringBuilder();

            foreach (Wheel wheel in m_Wheels)
            {
                WheelsToString.Append($"{Environment.NewLine}Wheel number: {wheelIndex + 1}");
                WheelsToString.Append(wheel.ToString());
                wheelIndex++;
            }

            return WheelsToString.ToString();
        }
    }
}
