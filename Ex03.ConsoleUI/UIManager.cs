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

        public void InsertNewVehicle()
        {
            VehicleFactory.eVehicleType vehicleType;
            string model, licenseNumber, ownersName, ownersNumber;

            ConsoleHandler.PrintEnum<VehicleFactory.eVehicleType>();
            vehicleType = (VehicleFactory.eVehicleType)readEnumFromConsole(typeof(VehicleFactory.eVehicleType));
            ConsoleHandler.GetBasicInfoFromConsole(vehicleType, out model, out licenseNumber, out ownersName, out ownersNumber);
            m_Garage.CheckIfVehicleExists(licenseNumber);
            m_NewVehicle = m_Garage.Factory.CreatVehicle(vehicleType, model, licenseNumber, ownersName, ownersNumber);
            getEngineTypeInput();
            ConsoleHandler.GetEnergyPercentage(m_NewVehicle.Engine.EngineType);
            setNewVehicleWheels();
            initUniqueVehicleProperties();

            //VehicleFactory factory = new VehicleFactory();
            //Vehicle newVehicle;
            //newVehicle = factory.CreatVehicle(VehicleFactory.eVehicleType.Car, "Toyota", "123", "Idan", "0546446798");
            //m_Garage.AddVehicle(newVehicle, "123");
            //addTyrePressureFromInput();
        }

        private int readEnumFromConsole(Type i_EnumType)
        {
            int parseEnum = int.MaxValue;
            int maxEnumValue = Enum.GetValues(i_EnumType).Length - 1;
            bool exceptionFlag = false;

            while (!exceptionFlag)
            {
                try
                {
                    if (!int.TryParse(Console.ReadLine(), out parseEnum))
                    {
                        throw new FormatException(Garage.k_NotIntError);
                    }
                    else if (parseEnum < 0 || parseEnum > maxEnumValue)
                    {
                        throw new ValueOutOfRangeException(maxEnumValue, 0);
                    }

                    exceptionFlag = true;
                }
                catch (FormatException fe)
                {
                    Console.WriteLine(fe.Message);
                }
                catch (ValueOutOfRangeException vofre)
                {
                    Console.WriteLine(vofre.ToString());
                }
            }

            return parseEnum;
        }

        public int readIntFromConsole(Type i_Type, int i_MinValue, int i_MaxValue)
        {
            int parseInt = default;
            bool exceptionFlag = false;

            while (!exceptionFlag)
            {
                try
                {
                    if (!int.TryParse(Console.ReadLine(), out parseInt))
                    {
                        throw new FormatException(Garage.k_NotIntError);
                    }
                    else if (parseInt < i_MinValue || parseInt > i_MaxValue)
                    {
                        throw new ValueOutOfRangeException(i_MaxValue, i_MinValue);
                    }

                    exceptionFlag = true;
                }
                catch (FormatException fe)
                {
                    Console.WriteLine(fe.Message);
                }
                catch (ValueOutOfRangeException vofre)
                {
                    Console.WriteLine(vofre.ToString());
                }
            }

            return parseInt;
        }



        private void getEngineTypeInput()
        {
            VehicleParts.Engine.eEngineType engineType;

            ConsoleHandler.PrintEnum<VehicleParts.Engine.eEngineType>();
            engineType = (VehicleParts.Engine.eEngineType)readEnumFromConsole(typeof(VehicleParts.Engine.eEngineType));
            m_NewVehicle.Engine = m_Garage.Factory.CreateEngine(engineType, m_NewVehicle.VehicleType);
        }

        private void setNewVehicleWheels()
        {
            int userChoice = -1;
            VehicleParts.Wheel tempWheel;

            Console.WriteLine("Press 1 to set all tyres at once.{0}Press 0 to set each tyre individuality.{1}", Environment.NewLine, Environment.NewLine);
            while (!int.TryParse(Console.ReadLine(), out userChoice) || (userChoice != 1 && userChoice != 0))
            {
                Console.WriteLine("Invalid input press 1/0");
            }

            if (userChoice == 1)
            {
                tempWheel = setSingleWheel(m_NewVehicle.Wheels[0].MaxAirPressure);
                foreach (VehicleParts.Wheel wheel in m_NewVehicle.Wheels)
                {
                    wheel.CurrentAirPressure = tempWheel.CurrentAirPressure;
                    wheel.ManufacturerName = tempWheel.ManufacturerName;
                }
            }
            else
            {
                foreach (VehicleParts.Wheel wheel in m_NewVehicle.Wheels)
                {
                    tempWheel = setSingleWheel(m_NewVehicle.Wheels[0].MaxAirPressure);
                    wheel.CurrentAirPressure = tempWheel.CurrentAirPressure;
                    wheel.ManufacturerName = tempWheel.ManufacturerName;
                }
            }
        }

        private void addTyrePressureFromInput()
        {
            int userChoice;

            Console.WriteLine("Press 0 to set each tyre individuality.{0}Press 1 to set all tyres at once", Environment.NewLine);
            while (!int.TryParse(Console.ReadLine(), out userChoice) || (userChoice != 1 && userChoice != 0)) // todo
            {
                Console.WriteLine("Invalid input press 1/0");
            }

            if (userChoice == 0)
            {
                FillTyresAirPressureFromInput("123");//todo
            }
            else //userChoice == 1
            {
                Console.WriteLine($"Please enter the license number.{Environment.NewLine}");
                m_Garage.FillTyresMaxAirPressure(userChoice.ToString());
                Console.WriteLine($"success fillilng pressure. {Environment.NewLine}");
            }
        }

        public int SetPressureFromInput(string i_LicenseNumber, ref int i_CurrentIndex, VehicleParts.Wheel i_Wheel)
        {
            string addedAirPressure;

            Console.WriteLine($"Wheel number: {i_CurrentIndex + 1} out of: {m_Garage.GetNumberOfWheels(i_LicenseNumber)}. Please Enter the pressure amount to add:");
            Console.WriteLine($"<0 - {i_Wheel.MaxAirPressure}>");
            try
            {
                addedAirPressure = Console.ReadLine();
                m_Garage.CheckPressureInputAndAdd(addedAirPressure, i_Wheel);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error:{e.Message} Please try again:");
                throw; //Todo do while ?
            }

            ++i_CurrentIndex;

            return i_CurrentIndex;
        }

        public void FillTyresAirPressureFromInput(string i_LicenseNumber)
        {
            int i = 0;
            Vehicle currentVehicle = m_Garage.GetVehicle(i_LicenseNumber); //todo check i_LicenseNumber ?

            foreach (VehicleParts.Wheel wheel in currentVehicle.Wheels)
            {
                SetPressureFromInput(i_LicenseNumber, ref i, wheel);
            }
        }

        private VehicleParts.Wheel setSingleWheel(float i_MaxAirPressure)
        {
            VehicleParts.Wheel newWheel = new VehicleParts.Wheel();
            float currentAirPressure = -1;

            Console.WriteLine("Please enter the wheel's manufacturer name:");
            newWheel.ManufacturerName = Console.ReadLine();
            Console.WriteLine("Please enter the wheel's current psi name:"); // todo english dfuq is "Pounds per Square Inch name"?
            while (!float.TryParse(Console.ReadLine(), out currentAirPressure) ||
                  (currentAirPressure < 0 && currentAirPressure > i_MaxAirPressure))
            {
                Console.WriteLine("invalid input");
            }

            newWheel.CurrentAirPressure = currentAirPressure;

            return newWheel;
        }

        public void CheckValidMenuChoice(string i_Input)
        {
            if (!int.TryParse(i_Input, out int parsedInteger))
            {
                throw new FormatException(Garage.k_NotIntError);
            }

            if (!Enum.IsDefined(typeof(VehicleFactory.eVehicleType), parsedInteger))
            {
                throw new ValueOutOfRangeException(2, 0);
            }
        }

        public static void CheckLicenseNumberInput(string i_Input)
        {
            if (!int.TryParse(i_Input, out int parsedInteger))
            {
                throw new FormatException(Garage.k_NotIntError);
            }

            if (parsedInteger <= 0)
            {
                throw new ArgumentException("Error: negative license number!");
            }
        }

        public static void CheckPhoneNumberInput(string i_Input)
        {
            if (!int.TryParse(i_Input, out int parsedInteger))
            {
                throw new FormatException(Garage.k_NotIntError);
            }

            if (parsedInteger <= 0)
            {
                throw new ArgumentException("Error: negative phone number!");
            }
        }

        private void initUniqueVehicleProperties() //TODO fix get doors starts from 2 in the menu its starts from 0. add to user choice or smthn
        {
            object[] getParameterForMethod = new object[1];
            Type type;
            //Type[] valTypes = new Type[] { typeof(float), typeof(int), typeof(bool) };
            MethodInfo dynamicTryPrase;
            object[] dynamicTryPraseParameters = new object[2];

            foreach (MethodInfo method in m_NewVehicle.UniqueMethods)
            {
                if (method.GetParameters()[0].ParameterType.IsEnum)
                {
                    ConsoleHandler.EnumsConsoleMessage(method.GetParameters()[0].ParameterType);
                    getParameterForMethod[0] = readEnumFromConsole(method.GetParameters()[0].ParameterType);
                    method.Invoke(m_NewVehicle, getParameterForMethod);
                }
                else
                { // still in Progerss dont touch!
                    Console.WriteLine(string.Format("please enter {0} as {1}:{2}",
                        ConsoleHandler.PrintCamelCase(method.Name.Remove(0, 4)),
                        method.GetParameters()[0].ParameterType.Name, Environment.NewLine));
                    getParameterForMethod[0] = Console.ReadLine();
                    type = method.GetParameters()[0].ParameterType;

                    dynamicTryPrase = method.GetParameters()[0].ParameterType.GetMethod("TryPrase");

                    dynamicTryPraseParameters[0] = Console.ReadLine();

                    dynamicTryPrase.Invoke(type, dynamicTryPraseParameters);
                }
            }
        }
    }
}
