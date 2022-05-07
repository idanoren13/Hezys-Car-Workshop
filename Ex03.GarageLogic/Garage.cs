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

        public VehicleFactory Factory
        {
            get => r_Factory;
        }

        public eGarageOptions Options 
        {
            get => m_Options;
        }

        public void AddVehicle(Vehicle i_Vehicle, string i_LicenseNumber)
        {
            r_GarageVehicles.Add(i_LicenseNumber, i_Vehicle);
        }

        public void FillTyresAirPressure(string i_LicenseNumber)
        {
            bool isValueFatched = r_GarageVehicles.TryGetValue(i_LicenseNumber, out Vehicle currentVehicle);
            if (isValueFatched)
            {
                foreach (VehicleParts.Wheel wheel in r_GarageVehicles[i_LicenseNumber].Wheels)
                {
                    wheel.FillMaxPressure();
                }
            }
            else
            {
                throw new FormatException("Error:Could Not fined Vehicle");
            }
        }

        public Vehicle.eVehicleStatus GetVehicleStatus(string i_PlatesList)
        {
            r_GarageVehicles.TryGetValue(i_PlatesList, out Vehicle currentVehicle); // todo use exception in catch ?
            return currentVehicle.Status;
        }

        //public void SetValueOfEnumProperty(PropertyInfo i_EnumPropertyInfo, Vehicle i_NewVehicle, string i_NewEnumValue)
        //{
        //    i_EnumPropertyInfo.SetValue(i_NewVehicle, i_NewVehicle.SelfParser(i_EnumPropertyInfo, i_NewEnumValue), null);
        //}

        public const string k_NotIntError = "Error: non Integer number entered!";

        public void CheckIfVehicleExists(string i_Input)
        {
            int parsedInt;

            if (!int.TryParse(i_Input, out parsedInt))
            {
                throw new FormatException(k_NotIntError);
            }

            if (r_GarageVehicles.ContainsKey(i_Input) == true)
            {
                throw new ArgumentException($"Error: {i_Input} Already exist! ");
            }
        }

        //TODO: bad method not console write line in logic
        private int setPressureFromInput(string i_LicenseNumber, int i_CurrentIndex, VehicleParts.Wheel i_Wheel)
        {
            bool isNumeric;
            int parsedInteger;
            string addedAirPressure;

            Console.WriteLine($"wheel number:{i_CurrentIndex + 1} out of {r_GarageVehicles[i_LicenseNumber].NumberOfWheels} Enter the pressure amount to add: {Environment.NewLine}");
            Console.WriteLine($"< 0 - {i_Wheel.MaxAirPressure} > {Environment.NewLine}");
            addedAirPressure = Console.ReadLine();
            isNumeric = int.TryParse(addedAirPressure, out parsedInteger);
            if (isNumeric)
            {
                i_Wheel.AddAir(parsedInteger);
            }
            else
            {
                throw new FormatException(Garage.k_NotIntError);
            }

            ++i_CurrentIndex;

            return i_CurrentIndex;
        }

        public void FillTyresMaxAirPressure(string i_LicenseNumber)
        {
            bool isValueFatched = r_GarageVehicles.TryGetValue(i_LicenseNumber, out Vehicle currentVehicle);
            int i = 0;

            if (isValueFatched)
            {
                foreach (VehicleParts.Wheel wheel in r_GarageVehicles[i_LicenseNumber].Wheels)
                {
                    i = setPressureFromInput(i_LicenseNumber, i, wheel);
                }
            }
            else
            {
                throw new FormatException("Error:Could Not fined Vehicle");
            }
        }
    }
}
