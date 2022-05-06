using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
            BB //The one and Only!
        }

        private int m_EngineVolume;
        private eLicenseType m_LicenseType;


        public MotorBike(string i_ModelName, string i_LicenseNumber, int i_NumberOfWheels, string i_OwnersName, string i_OwnersNumber) :
           base(i_ModelName, i_LicenseNumber, i_NumberOfWheels, i_OwnersName, i_OwnersNumber, VehicleFactory.eVehicleType.MotorBike)
        { }

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

        public override Type GetSelfPropertyType(string i_PropertyName)
        {
            Type specificType;

            if (i_PropertyName == "LicenseType")
            {
                specificType = typeof(eLicenseType);
            }
            else if (i_PropertyName == "EngineVolume")
            {
                specificType = typeof(int);
            }
            else
            {
                throw new ArgumentException("BadEnumType:No_such_property"); //TODO ?
            }

            return specificType;
        }

        public override object SelfParser(PropertyInfo i_PropertyToBeParsed, object valueToBeParsed)
        {
            throw new NotImplementedException();
        }
    }
}
