using System;
using System.Text;

namespace Ex03.GarageLogic.Vehicles
{
    public class MotorBike : Vehicle
    {
        public enum eLicenseType
        {
            A,
            A1,
            B1,
            BB
        }

        private const int k_MaxEngineVolume = 10000;
        private int m_EngineVolume;
        private eLicenseType m_LicenseType;

        public MotorBike(string i_ModelName, string i_LicenseNumber, int i_NumberOfWheels, string i_OwnersName, string i_OwnersNumber) :
        base(i_ModelName, i_LicenseNumber, i_NumberOfWheels, i_OwnersName, i_OwnersNumber, VehicleFactory.eVehicleType.MotorBike) { }

        public int EngineVolume 
        { 
            get => m_EngineVolume;
            set => m_EngineVolume = value;
        }

        public eLicenseType LicenseType 
        { 
            get => m_LicenseType; 
            set => m_LicenseType = value; 
        }

        public override string ToString()
        {
            StringBuilder motorbikeToString = new StringBuilder();

            motorbikeToString.Append(GeneralInfoToString());
            motorbikeToString.Append($"{Environment.NewLine}The License Type is: {this.m_LicenseType} ");
            motorbikeToString.Append($"{Environment.NewLine}The Engine Volume: {this.m_EngineVolume} ");

            return motorbikeToString.ToString();
        }
    }
}
