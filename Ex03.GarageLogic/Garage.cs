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
            InsertVehicle,
            ChangeVehicleState,
            FillTirePressure,
            FillGasMotor,
            FillElectricMotor,

        }

        //todo
        Dictionary<string, Vehicle> r_VehiclesInGarage;
        VehicleFactory r_Factory;
        eGarageOptions options;
        const int k_NumberOfAvailableMethodsInGarage = 0;
        const string k_ParsingToIntErrorFlag = "0";
        const float k_MaxPercentage = 100;
        const float k_MinPercentage = 0;
        //const int k_MaxNumberOfStatuses = 0; 
        //const int k_MinNumberOfStatuses = 0;

        void FillWheelsAirPressure()
        {
            //get numbers of wheels
            for (int i = 0; i < 3; i++)
            {
                //RefillAir();
            }
        }

        //mathods

    }
}
