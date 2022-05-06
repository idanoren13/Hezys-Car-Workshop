using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ex03.GarageLogic; //todo: remove
using System.Reflection;

namespace Ex03.ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            //test
            VehicleFactory factory = new VehicleFactory();
            Vehicle newVehicle;
            PropertyInfo[] propertyInfo;
            int n = 2;

            ConsoleHandler.printEnum<VehicleFactory.eVehicleType>();

            VehicleParts.Engine engine = new VehicleParts.CombustionEngine(60, VehicleParts.CombustionEngine.eFuelType.Octan95);

            newVehicle = factory.CreatVehicle(VehicleFactory.eVehicleType.Car, "Toyota", "7653550", "Idan", "0546446798");
            propertyInfo = newVehicle.GetType().GetProperties();
            //ConsoleHandler.NewVehilceMessage(propertyInfo);
            //Console.WriteLine(propertyInfo[n]);
            //propertyInfo[n].SetValue(newVehicle, engine, null);

            engine = new VehicleParts.CombustionEngine(35, VehicleParts.CombustionEngine.eFuelType.Octan98);
        }
    }
}
