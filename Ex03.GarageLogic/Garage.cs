using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ex03.GarageLogic
{
    public class Garage
    {
        public enum eGarageOptions
        {
            EndService,
            InsertVehicle,
            DisplayListOfLicensedVehicle,
            FillTirePressure,
            FillGas,
            ChangeVehicleState,
            ChargeElectricVehicle,
            ExtendedInformationOfSelectedVehicle,
        }

        public const string k_NotIntError = "Error: non Integer number entered!";
        private readonly Dictionary<string, Vehicle> r_GarageVehicles;
        private readonly VehicleFactory r_Factory;

        public Garage()
        {
            r_GarageVehicles = new Dictionary<string, Vehicle>();
            r_Factory = new VehicleFactory();
        }

        public Dictionary<string, Vehicle>.KeyCollection PlatesList
        {
            get => r_GarageVehicles.Keys;
        }

        public VehicleFactory Factory
        {
            get => r_Factory;
        }

        public Vehicle GetVehicle(string i_LicenseNumber)
        {
            if (!r_GarageVehicles.TryGetValue(i_LicenseNumber, out Vehicle currentVehicle))
            {
                throw new KeyNotFoundException($"Error:Could Not find {i_LicenseNumber} Vehicle");
            }

            return r_GarageVehicles[i_LicenseNumber];
        }

        public void AddVehicle(Vehicle i_Vehicle, string i_LicenseNumber)
        {
            r_GarageVehicles.Add(i_LicenseNumber, i_Vehicle);
        }

        public string PlatesToString(Dictionary<string, Vehicle>.KeyCollection i_Plates)
        {
            StringBuilder plates = new StringBuilder();
            string outString = null;

            foreach (string plate in i_Plates)
            {
                plates.Append($"{plate}{Environment.NewLine}");
            }

            if (i_Plates.Count > 0)
            {
                outString = plates.ToString();
            }

            return outString;
        }

        public Dictionary<string, Vehicle>.KeyCollection GetPlatesList()
        {
            if (r_GarageVehicles.Count() == 0)
            {
                throw new KeyNotFoundException($"Error: the garage is empty!");
            }

            return r_GarageVehicles.Keys;
        }

        public Dictionary<string, Vehicle>.KeyCollection GetSortedListFilterdByStatus(Vehicle.eVehicleStatus i_Status)
        {
            var filteredList = new Dictionary<string, Vehicle>();
            foreach (var vehicle in r_GarageVehicles.Values)
            {
                if (i_Status == vehicle.Status)
                {
                    filteredList.Add(vehicle.LicenseNumber, vehicle);
                }
            }

            return filteredList.Keys;
        }

        public void FillTyresMaxAirPressure(string i_LicenseNumber)
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
                throw new KeyNotFoundException($"Error:Could Not find {i_LicenseNumber} Vehicle");
            }
        }

        public void AddFuel(string i_LicenseNumber, VehicleParts.CombustionEngine.eFuelType i_FuelType, float i_AmountToFill)
        {
            r_GarageVehicles.TryGetValue(i_LicenseNumber, out Vehicle currentVehicle);
            ((VehicleParts.CombustionEngine)currentVehicle.Engine).AddFuel(i_AmountToFill, i_FuelType);
        }

        public void Charge(string i_LicenseNumber, float i_EnergyToAdd)
        {
            r_GarageVehicles.TryGetValue(i_LicenseNumber, out Vehicle currentVehicle);
            ((VehicleParts.ElectricEngine)currentVehicle.Engine).SuperCharge(i_EnergyToAdd);
        }

        public Vehicle.eVehicleStatus GetVehicleStatus(string i_LicenseNumber)
        {
            if (!r_GarageVehicles.TryGetValue(i_LicenseNumber, out Vehicle currentVehicle))
            {
                throw new KeyNotFoundException($"Error: {i_LicenseNumber} is not in the garage");
            }

            return currentVehicle.Status;
        }

        public int GetNumberOfWheels(string i_LicenseNumber)
        {
            return r_GarageVehicles[i_LicenseNumber].NumberOfWheels;
        }

        public void CheckIfEngineIsCombustion(string i_LicenseNumber)
        {
            r_GarageVehicles.TryGetValue(i_LicenseNumber, out Vehicle currentVehicle);
            if (!(currentVehicle.Engine is VehicleParts.CombustionEngine))
            {
                throw new FormatException("cant fill gas on electric engine!");
            }
        }

        public void CheckIfEngineIsElectric(string i_LicenseNumber)
        {
            r_GarageVehicles.TryGetValue(i_LicenseNumber, out Vehicle currentVehicle);
            if (!(currentVehicle.Engine is VehicleParts.ElectricEngine))
            {
                throw new FormatException("cant charge fuel engine!");
            }
        }

        public void CheckPressureInputAndAdd(string i_addedAirPressure, VehicleParts.Wheel i_Wheel)
        {
            bool isNumeric;
            int parsedInteger;

            isNumeric = int.TryParse(i_addedAirPressure, out parsedInteger);
            if (isNumeric)
            {
                i_Wheel.AddAir(parsedInteger);
            }
            else
            {
                throw new FormatException(Garage.k_NotIntError);
            }
        }

        public bool CheckIfVehicleExists(string i_Input)
        {
            if (!int.TryParse(i_Input, out int parsedInt))
            {
                throw new FormatException(k_NotIntError);
            }

            return r_GarageVehicles.ContainsKey(i_Input);
        }
    }
}
