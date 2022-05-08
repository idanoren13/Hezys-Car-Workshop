using System;
using System.Collections.Generic;
using System.Reflection;

namespace Ex03.GarageLogic
{
    public class VehicleFactory
    {
        public enum eVehicleType
        {
            Car,
            MotorBike,
            Lorry
        }

        public Vehicle CreatVehicle(eVehicleType i_Type, string i_ModelName, string i_LicenseNumber, string i_OwnersName, string i_OwnersNumber)
        {
            Vehicle newVehicle;

            switch (i_Type)
            {
                case eVehicleType.Car:
                    newVehicle = new Vehicles.Car(i_ModelName, i_LicenseNumber, 4, i_OwnersName, i_OwnersNumber);
                    newVehicle.SetMaxAirPressure(29f);
                    break;
                case eVehicleType.MotorBike:
                    newVehicle = new Vehicles.MotorBike(i_ModelName, i_LicenseNumber, 2, i_OwnersName, i_OwnersNumber);
                    newVehicle.SetMaxAirPressure(31f);
                    break;
                case eVehicleType.Lorry:
                    newVehicle = new Ex03.GarageLogic.Vehicles.Lorry(i_ModelName, i_LicenseNumber, 16, i_OwnersName, i_OwnersNumber);
                    newVehicle.SetMaxAirPressure(24f);
                    newVehicle.Engine = CreateEngine(VehicleParts.Engine.eEngineType.Fuel, eVehicleType.Lorry);
                    break;
                default:
                    throw new ArgumentException("Bad Vehicle Type, This type of vehicle is not recognizeble");
            }
            
            newVehicle.UniqueMethods = InitUniqueMethods(newVehicle);
            
            return newVehicle;
        }

        public VehicleParts.Engine CreateEngine(VehicleParts.Engine.eEngineType i_EngineType, eVehicleType i_VehicleType)
        {
            VehicleParts.Engine engine;

            switch (i_EngineType)
            {
                case VehicleParts.Engine.eEngineType.Electricty:
                    engine = new VehicleParts.ElectricEngine();
                    (engine as VehicleParts.ElectricEngine).MaxBatteryCapacity = setEnergyCapacity(i_EngineType, i_VehicleType);
                    break;
                case VehicleParts.Engine.eEngineType.Fuel:
                    engine = new VehicleParts.CombustionEngine();
                    (engine as VehicleParts.CombustionEngine).MaxTankCapacity = setEnergyCapacity(i_EngineType, i_VehicleType);
                    (engine as VehicleParts.CombustionEngine).FuelType = setFuelType(i_VehicleType);
                    break;
                default:
                    throw new ArgumentException("Bad Engine Type, This type engine of is not recognizeble");
            }

            return engine;
        }

        private VehicleParts.CombustionEngine.eFuelType setFuelType(eVehicleType i_VehicleType)
        {
            VehicleParts.CombustionEngine.eFuelType fuelType;

            switch (i_VehicleType)
            {
                case eVehicleType.Car:
                    fuelType = VehicleParts.CombustionEngine.eFuelType.Octan95;
                    break;
                case eVehicleType.MotorBike:
                    fuelType = VehicleParts.CombustionEngine.eFuelType.Octan98;
                    break;
                case eVehicleType.Lorry:
                    fuelType = VehicleParts.CombustionEngine.eFuelType.Soler;
                    break;
                default:
                    throw new FormatException();
            }

            return fuelType;
        }

        private float setEnergyCapacity(VehicleParts.Engine.eEngineType i_EngineType, eVehicleType i_VehicleType)
        {
            float capacity ;

            if (i_EngineType != VehicleParts.Engine.eEngineType.Electricty)
            {
                switch (i_VehicleType)
                {
                    case eVehicleType.Car:
                        capacity = 38f;
                        break;
                    case eVehicleType.MotorBike:
                        capacity = 6.2f;
                        break;
                    case eVehicleType.Lorry:
                        capacity = 120f;
                        break;
                    default:
                        throw new FormatException();
                }
            }
            else
            {
                switch (i_VehicleType)
                {
                    case eVehicleType.Car:
                        capacity = 2.5f;
                        break;
                    case eVehicleType.MotorBike:
                        capacity = 3.3f;
                        break;
                    default:
                        throw new FormatException();
                }
            }

            return capacity;
        }

        private List<MethodInfo> InitUniqueMethods(Vehicle i_NewVehicle)
        {
            MethodInfo[] allMethods = i_NewVehicle.GetType().GetMethods();
            List<MethodInfo> uniqueMethods = new List<MethodInfo>();

            foreach (MethodInfo currentCheckedMethod in allMethods)
            {
                if (currentCheckedMethod.Name.Contains("set_") == true 
                    && typeof(Vehicle).GetMethod(currentCheckedMethod.Name) == null)
                {
                    uniqueMethods.Add(currentCheckedMethod);
                }
            }

            return uniqueMethods;
        }
    }
}
