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

        public void insertNewVehicle()
        {
            VehicleFactory.eVehicleType vehicleType;
            string model, licenseNumber, ownersName, ownersNumber;

            ConsoleHandler.PrintEnum<VehicleFactory.eVehicleType>();
            vehicleType = (VehicleFactory.eVehicleType)readEnumFromConsole(typeof(VehicleFactory.eVehicleType));
            ConsoleHandler.GetBasicInfoFromConsole(vehicleType, out model, out licenseNumber, out ownersName, out ownersNumber);
            m_NewVehicle = m_Garage.Factory.CreatVehicle(vehicleType, model, licenseNumber, ownersName, ownersNumber);
            selectAndInitilizeEngine();
            ConsoleHandler.GetEnergyPercentage(m_NewVehicle.Engine.EngineType);
            setNewVehicleWheels();
            initUniqueVehicleProperties();
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
                catch(ValueOutOfRangeException vofre)
                {
                    Console.WriteLine(vofre.ToString());
                }
            }

            return parseEnum;
        }

        private void selectAndInitilizeEngine()
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

            Console.WriteLine("please select if you want to set all tyres in once press 1{0}" +
                                "set each tyre individuality press 0{1}", Environment.NewLine, Environment.NewLine);
            
            while (!int.TryParse(Console.ReadLine(),out userChoice) || (userChoice != 1 && userChoice != 0))
            {
                Console.WriteLine("please enter 1 or 0");
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

        private VehicleParts.Wheel setSingleWheel(float i_MaxAirPressure)
        {
            VehicleParts.Wheel newWheel = new VehicleParts.Wheel();
            float currentAirPressure = -1;
            Console.WriteLine("please enter the wheel's manufacturer name:");
            newWheel.ManufacturerName = Console.ReadLine();
            Console.WriteLine("please enter the wheel's current psi name:");
            while (!float.TryParse(Console.ReadLine(),out currentAirPressure) || 
                (currentAirPressure < 0 && currentAirPressure > i_MaxAirPressure))
            {
                Console.WriteLine("invalid input");
            }

            newWheel.CurrentAirPressure = currentAirPressure;

            return newWheel;
        }

        //public void SetValueForUniqueProperty(PropertyInfo i_UniquePropertyInfo, Vehicle i_NewVehicle, string i_NewPropertyValue)
        //{
        //    i_UniquePropertyInfo.SetValue(i_NewVehicle, i_NewVehicle.SelfParser(i_UniquePropertyInfo, i_NewPropertyValue), null);
        //}

        private void checkValidMenuChoice(string i_Input)
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

        private void checkLicenseNumberInput(string i_Input)
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

        private void checkPhoneNumberInput(string i_Input)
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

        private void addTyrePressureFromInput()
        {
            string userChoice = Console.ReadLine();

            Console.WriteLine($"Please enter the license number, followed by an ENTER.{Environment.NewLine}");
            m_Garage.FillTyresAirPressure(userChoice);
            Console.WriteLine($"success fillilng pressure. {Environment.NewLine}");
        }
        
        private void initUniqueVehicleProperties()
        {
            object []getParameterForMethod = new object[1];
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
                        ConsoleHandler.BeautifyName(method.Name.Remove(0,4)),
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
