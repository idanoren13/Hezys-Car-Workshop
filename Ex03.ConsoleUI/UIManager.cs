using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Ex03.GarageLogic;

namespace Ex03.ConsoleUI
{
    class UIManager
    {
        private bool getInputForAction(Garage.eGarageOptions i_GetCurrentOptions) // todo more functionality
        {
            bool flag = true;
            switch (i_GetCurrentOptions)
            {
                case Garage.eGarageOptions.InsertVehicle:
                    break;
                case Garage.eGarageOptions.ChangeVehicleState:
                    break;
                case Garage.eGarageOptions.FillTirePressure:
                    break;
                case Garage.eGarageOptions.FillGasMotor:
                    break;
                case Garage.eGarageOptions.FillElectricMotor:
                    break;
                default:
                    flag = false;
                    break;
            }

            return flag;
        }

        private string handleEnumCase(Vehicle i_NewVehicle, PropertyInfo i_UniquePropertyInfo)
        {
            Type matchingTypeEnum = i_NewVehicle.GetUniqueType(i_UniquePropertyInfo.Name);
            Console.WriteLine("\nChoosing " + i_UniquePropertyInfo.Name + ":");
            getEnumConsoleMessage(matchingTypeEnum);
            //user enters enum choice
            return Console.ReadLine();
        }

     


    }
}
