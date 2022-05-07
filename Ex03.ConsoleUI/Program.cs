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
            UIManager ui = new UIManager();

            ui.InsertNewVehicle();

            //test
            //VehicleFactory factory = new VehicleFactory();
            //Vehicle newVehicle;
            //PropertyInfo[] propertiesInfo;
            //int n = 2;

            //ConsoleHandler.PrintEnum<VehicleFactory.eVehicleType>();

            //VehicleParts.Engine engine = new VehicleParts.CombustionEngine();

            //newVehicle = factory.CreatVehicle(VehicleFactory.eVehicleType.Car, "Toyota", "7653550", "Idan", "0546446798");
            

            //propertiesInfo = newVehicle.GetType().GetProperties();
            //ConsoleHandler.NewVehilceMessage(propertyInfo);
            //Console.WriteLine(propertyInfo[n]);
            //propertyInfo[n].SetValue(newVehicle, engine, null);

            //MethodInfo[] methods = newVehicle.GetType().GetMethods();
            //foreach (MethodInfo method in methods)
            //{
            //    if (method.ReturnType is Vehicle)
            //    {
            //        int b = 10;
            //    }
            //}

            //engine = new VehicleParts.CombustionEngine();
        }
    }
}
