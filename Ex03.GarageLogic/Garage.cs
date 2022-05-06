using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Ex03.GarageLogic
{
    public class Garage
    {
        public enum eGarageOptions
        {
            InsertVehicle = 1,
            ChangeVehicleState,
            FillTirePressure,
            FillGasMotor,
            FillElectricMotor,
        }

        //todo
        private readonly Dictionary<string, Vehicle> r_GarageVehicles;
        private readonly VehicleFactory r_Factory;
        private readonly eGarageOptions m_Options;
        const int k_NumberOfAvailableMethodsInGarage = 0;
        const string k_ParsingToIntErrorFlag = "0";
        const float k_MaxPercentage = 100;
        const float k_MinPercentage = 0;
        //const int k_MaxNumberOfStatuses = 0; 
        //const int k_MinNumberOfStatuses = 0;

        public Garage()
        {
            r_GarageVehicles = new Dictionary<string, Vehicle>();
            r_Factory = new VehicleFactory();
        }

        public Dictionary<string, Vehicle>.KeyCollection GetPlatesList()
        {
            return r_GarageVehicles.Keys;
        }

        public Dictionary<string, Vehicle>.KeyCollection PlatesList
        {
            get => r_GarageVehicles.Keys;
        }

        public VehicleFactory R_Factory => r_Factory;

        public eGarageOptions Options 
        {
            get => m_Options;
        }

        public void AddVehicle(Vehicle i_Vehicle, string i_LicenseNumber)
        {
            r_GarageVehicles.Add(i_LicenseNumber, i_Vehicle);
        }

        void FillTyresAirPressure()
        {
            //get numbers of wheels
            for (int i = 0; i < 3; i++)
            {
                //RefillAir();
            }
        }

        public Vehicle.eVehicleStatus GetVehicleStatus(string i_PlatesList)
        {
            r_GarageVehicles.TryGetValue(i_PlatesList, out Vehicle currentVehicle); // todo use exception in catch ?
            return currentVehicle.Status;
        }

        public void SetValueOfEnumProperty(PropertyInfo i_EnumPropertyInfo, Vehicle i_NewVehicle, string i_NewEnumValue)
        {
            i_EnumPropertyInfo.SetValue(i_NewVehicle, i_NewVehicle.SelfParser(i_EnumPropertyInfo, i_NewEnumValue), null);
        }

        //public float GetAmountOfEnergy
        //public float GetMaxAmountOfEnergy


        //mathods

    }
}
