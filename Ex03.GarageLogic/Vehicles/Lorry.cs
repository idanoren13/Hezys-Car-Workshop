using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ex03.GarageLogic.Vehicles
{
    public class Lorry : Vehicle
    {
        private bool m_IsActiveTemprtureControl;

        private float m_CargoVolume;

        public Lorry(string i_ModelName, string i_LicenseNumber, int i_NumberOfWheels, string i_OwnersName, string i_OwnersNumber) : 
            base(i_ModelName, i_LicenseNumber, i_NumberOfWheels, i_OwnersName, i_OwnersNumber, VehicleFactory.eVehicleType.Lorry)
        {
        }

        public bool IsActiveTemprtureControl 
        {
            get => m_IsActiveTemprtureControl; 
            set => m_IsActiveTemprtureControl = value; 
        }
        
        public float CargoVolume 
        {
            get => m_CargoVolume; 
            set => m_CargoVolume = value; 
        }

        public override string ToString()
        {
            StringBuilder LorryToString = new StringBuilder();

            LorryToString.Append(GeneralInfoToString());
            LorryToString.Append($"{Environment.NewLine}Cooling cargo type: {this.m_IsActiveTemprtureControl} ");
            LorryToString.Append($"{Environment.NewLine}The Engine Volume: {this.m_CargoVolume} ");

            return LorryToString.ToString();
        }
    }
}
