using System;
using System.Collections.Generic;
using System.Linq;
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

    }
}
