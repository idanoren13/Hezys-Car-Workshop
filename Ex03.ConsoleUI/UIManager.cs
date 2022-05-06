using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Ex03.GarageLogic;


namespace Ex03.ConsoleUI
{
    public class UIManager
    {
        private Garage m_Garage;

        private Vehicle m_NewVehicle;

        public UIManager()
        {
            m_Garage = new Garage();
        }

        public enum test
        {
            one,
            two,
            three,
            four
        }


        public bool getInputForAction(Garage.eGarageOptions i_GetCurrentOptions) // todo more functionality
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

        private void insertNewVehicle()
        {
            VehicleFactory.eVehicleType vehicleType;
            string model, licenseNumber, ownersName, ownersNumber;

            ConsoleHandler.printEnum<VehicleFactory.eVehicleType>();
            vehicleType = (VehicleFactory.eVehicleType)readEnumFromConsole();
            Console.WriteLine("Please enter Your name: ");
            ownersName = Console.ReadLine();
            Console.WriteLine("Please enter Your number: ");
            ownersNumber = Console.ReadLine();
            Console.WriteLine("Please enter Your {} model: ", vehicleType.ToString());
            model = Console.ReadLine();
            Console.WriteLine("Please enter Your license Number: ");
            licenseNumber = Console.ReadLine();

            m_NewVehicle = m_Garage.R_Factory.CreatVehicle(vehicleType, model, licenseNumber, ownersName, ownersNumber);

            if (m_NewVehicle.Engine == null)
            {
                selectAndInitilizeEngine();
            }
        }
        
        private int readEnumFromConsole()
        {
            int readEnum;
            int maxEnumValue;

            if (!int.TryParse(Console.ReadLine(), out readEnum))
            {
                maxEnumValue = Enum.GetValues(typeof(VehicleFactory.eVehicleType)).Length;
                throw new ValueOutOfRangeException(maxEnumValue, 0);
            }

            return readEnum;
        }

        private void selectAndInitilizeEngine()
        {
            VehicleParts.Engine.eEngineType engineType;

            ConsoleHandler.printEnum<VehicleParts.Engine.eEngineType>();
            engineType = (VehicleParts.Engine.eEngineType)readEnumFromConsole();            
        }

        public void SetValueForUniqueProperty(PropertyInfo i_UniquePropertyInfo, Vehicle i_NewVehicle, string i_NewPropertyValue)
        {
            i_UniquePropertyInfo.SetValue(i_NewVehicle, i_NewVehicle.SelfParser(i_UniquePropertyInfo, i_NewPropertyValue), null);
        }
    }
}
