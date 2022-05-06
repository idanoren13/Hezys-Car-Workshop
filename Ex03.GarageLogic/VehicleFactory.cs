using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ex03.GarageLogic
{
    public class VehicleFactory
    {
        public enum eVehicleType
        {
            Car,
            MotorBike,
            Truck
        }

        public Vehicle CreatVehicle(eVehicleType i_Type, string i_ModelName, string i_LicenseNumber, string i_OwnersName, string i_OwnersNumber)
        {
            Vehicle newVehicle;

            switch (i_Type)
            {
                case eVehicleType.Car:
                    newVehicle = new Ex03.GarageLogic.Vehicles.Car(i_ModelName, i_LicenseNumber, 4, i_OwnersName, i_OwnersNumber);
                    break;
                case eVehicleType.MotorBike:
                    newVehicle = new Ex03.GarageLogic.Vehicles.MotorBike(i_ModelName, i_LicenseNumber, 2, i_OwnersName, i_OwnersNumber);
                    break;
                //case eVehicleType.Truck:
                //    newVehicle = new Ex03.GarageLogic.Vehicles.Truck(i_ModelName, i_LicenseNumber, 16, i_OwnersName, i_OwnersNumber);
                //    break;
                default:
                    throw new ArgumentException("Bad Vehicle Type, This type of vehicle is not recognizeble");
                    break;
            }

            return newVehicle;
        }

        public VehicleParts.Engine CreateEngine(VehicleParts.Engine.eEngineType i_EngineType, 
                                                float i_MaxEnergyCapacity, 
                                                VehicleParts.CombustionEngine.eFuelType i_Fuel = default)
        {
            VehicleParts.Engine engine;
            switch (i_EngineType)
            {
                case VehicleParts.Engine.eEngineType.Electricty:
                    engine = new VehicleParts.ElectricEngine(i_MaxEnergyCapacity);
                    break;
                case VehicleParts.Engine.eEngineType.Fuel:
                    engine = new VehicleParts.CombustionEngine(i_MaxEnergyCapacity, i_Fuel);
                    break;
                default:
                    throw new ArgumentException("Bad Engine Type, This type engine of is not recognizeble");
                    break;
            }

            return engine;
        }

        public void AddVehicleParts()
        {
            //Vehicle ctor
            //engine
            //wheels

        }
    }
}
