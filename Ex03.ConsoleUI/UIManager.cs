using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Ex03.GarageLogic;
using Ex03.GarageLogic.VehicleParts;

namespace Ex03.ConsoleUI
{
    public class UIManager
    {
        private readonly Garage m_Garage;
        private Vehicle m_NewVehicle;
        private bool m_IsGarageOpen;
        public UIManager()
        {
            m_Garage = new Garage();
        }

        public void OpenGarage()
        {
            bool hasExceptionOccured = false;

            Garage.eGarageOptions garageOptions;
            m_IsGarageOpen = true;
            while (m_IsGarageOpen)
            {
                try
                {
                    System.Console.Clear();
                    ConsoleHandler.PrintMainMenu();
                    garageOptions = (Garage.eGarageOptions)ConsoleHandler.readEnumFromConsole(typeof(Garage.eGarageOptions));
                    garageInterfaceManager(garageOptions);
                }
                catch (FormatException fe)
                {
                    Console.WriteLine(fe.Message);
                    hasExceptionOccured = true;
                }
                catch (KeyNotFoundException knfe)
                {
                    Console.WriteLine(knfe.Message);
                    hasExceptionOccured = true;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    hasExceptionOccured = true;
                }

                if (hasExceptionOccured)
                {
                    hasExceptionOccured = false;
                    ConsoleHandler.HaltScreenUntilUserInput();
                }
            }
        }

        public void garageInterfaceManager(Garage.eGarageOptions i_GetCurrentOptions)
        {
            System.Console.Clear();
            switch (i_GetCurrentOptions)
            {
                case Garage.eGarageOptions.InsertVehicle:
                    InsertNewVehicle();
                    break;
                case Garage.eGarageOptions.DisplayListOfLicensedVehicle:
                    DisplayListOfLicensedVehicles();
                    break;
                case Garage.eGarageOptions.FillTirePressure:
                    addTyrePressureFromInput();
                    break;
                case Garage.eGarageOptions.FillGasMotor: // TODO
                    break;
                case Garage.eGarageOptions.ChangeVehicleState: // TODO
                    break;
                case Garage.eGarageOptions.FillElectricMotor: // TODO
                    break;
                case Garage.eGarageOptions.ExtendedInformationOfSelectedVehicle:
                    printSpacificCarInfo();
                    break;
                case Garage.eGarageOptions.CloseTheBasta:
                    m_IsGarageOpen = false;
                    break;
                default:
                    throw new FormatException("Invalid choice");
            } 
        }

        private void DisplayListOfLicensedVehicles()
        {
            int displayListByStatus;
            
            //displayListByStatus = ConsoleHandler.Choose1Or0("diplay by vehicle status", "display by license number");
            Console.WriteLine("select dispaly option:");
            ConsoleHandler.PrintEnum<Vehicle.eVehicleStatus>();
            Console.WriteLine(string.Format("press{0} to display all vehicles", Enum.GetValues(typeof(Vehicle.eVehicleStatus)).Length));
            displayListByStatus = ConsoleHandler.readIntFromConsole(0, Enum.GetValues(typeof(Vehicle.eVehicleStatus)).Length);
            if (displayListByStatus == Enum.GetValues(typeof(Vehicle.eVehicleStatus)).Length)
            {
                Console.WriteLine(m_Garage.GetPlatesList().ToString());
            }
        }

        public void InsertNewVehicle()
        {
            try
            {
                VehicleFactory.eVehicleType vehicleType;
                string model, licenseNumber, ownersName, ownersNumber;

                ConsoleHandler.PrintEnum<VehicleFactory.eVehicleType>();
                vehicleType = (VehicleFactory.eVehicleType)ConsoleHandler.readEnumFromConsole(typeof(VehicleFactory.eVehicleType));
                ConsoleHandler.GetBasicInfoFromConsole(vehicleType, out model, out licenseNumber, out ownersName, out ownersNumber);
                if (m_Garage.CheckIfVehicleExists(licenseNumber))
                {
                    throw new DuplicateKeysException($"Error: {licenseNumber} Already exist! ");
                }
                m_NewVehicle = m_Garage.Factory.CreatVehicle(vehicleType, model, licenseNumber, ownersName, ownersNumber);
                if (m_NewVehicle.Engine == null)
                {
                    getEngineTypeInput();
                }

                m_NewVehicle.Engine.SetCurrentEnergyByPercentage( ConsoleHandler.GetEnergyPercentage(m_NewVehicle.Engine.EngineType)); // TODO return unused float??
                setNewVehicleWheels();
                initUniqueVehicleProperties();
                m_Garage.AddVehicle(m_NewVehicle, licenseNumber);
            }
            catch (DuplicateKeysException dke)
            {
                Console.WriteLine(dke.ToString());
                m_Garage.GetVehicle(m_NewVehicle.LicenseNumber).Status = Vehicle.eVehicleStatus.InProcess;
                ConsoleHandler.HaltScreenUntilUserInput();
            }
        }

        private void printSpacificCarInfo()
        {
            Vehicle vehicleToDisplay;

            Console.WriteLine($"Please enter the license number you wish .");
            vehicleToDisplay = m_Garage.GetVehicle(Console.ReadLine());
            displayAllVehicleInfo(vehicleToDisplay);
            ConsoleHandler.HaltScreenUntilUserInput();
        }

        private void displayAllVehicleInfo(Vehicle i_VehicleToDisplay)
        {
            Console.WriteLine(i_VehicleToDisplay.ToString());
            Console.WriteLine(i_VehicleToDisplay.ToStringAllWheels());
            Console.WriteLine(i_VehicleToDisplay.Engine.ToString());
        }

        private void getEngineTypeInput()
        {
            Engine.eEngineType engineType;

            ConsoleHandler.PrintEnum<Engine.eEngineType>();
            engineType = (Engine.eEngineType)ConsoleHandler.readEnumFromConsole(typeof(Engine.eEngineType));
            m_NewVehicle.Engine = m_Garage.Factory.CreateEngine(engineType, m_NewVehicle.VehicleType);
        }

        private void setNewVehicleWheels()
        {
            int userChoice = -1;
            Wheel tempWheel;

            userChoice = ConsoleHandler.Choose1Or0("all tyres at once", "each tyre individual");
            if (userChoice == 1)
            {
                tempWheel = setSingleWheel(m_NewVehicle.Wheels[0].MaxAirPressure);
                foreach (Wheel wheel in m_NewVehicle.Wheels)
                {
                    wheel.CurrentAirPressure = tempWheel.CurrentAirPressure;
                    wheel.ManufacturerName = tempWheel.ManufacturerName;
                }
            }
            else
            {
                foreach (Wheel wheel in m_NewVehicle.Wheels)
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
            string licenseNumber;

            Console.WriteLine("Please enter the license number.");
            licenseNumber = Console.ReadLine();
            if (!m_Garage.CheckIfVehicleExists(licenseNumber))
            {
                throw new KeyNotFoundException($"Vehicle with license number: {licenseNumber} is not in the garage");
            } 
            userChoice = ConsoleHandler.Choose1Or0("all tyres at once", "each tyre individual");
            if (userChoice == 0)
            {
                FillTyresAirPressureFromInput(licenseNumber);
            }
            else 
            {
                m_Garage.FillTyresMaxAirPressure(licenseNumber);
                Console.WriteLine($"success fillilng pressure.");
                ConsoleHandler.HaltScreenUntilUserInput();
            }
        }

        public int SetPressureFromInput(string i_LicenseNumber, ref int i_CurrentIndex, Wheel i_Wheel)
        {
            string addedAirPressure;

            Console.WriteLine(string.Format(
                "Wheel number: {0} out of: {1}.{2}Current Air Pressure:{3}{4}Please Enter the pressure amount to add:", 
                i_CurrentIndex + 1, 
                m_Garage.GetNumberOfWheels(i_LicenseNumber), 
                Environment.NewLine,
                i_Wheel.CurrentAirPressure,
                Environment.NewLine));
            Console.WriteLine($"<0 - {i_Wheel.MaxAirPressure}>");
            try
            {
                addedAirPressure = Console.ReadLine();
                m_Garage.CheckPressureInputAndAdd(addedAirPressure, i_Wheel);
            }
            catch (ValueOutOfRangeException vore)
            {
                Console.WriteLine($"Error:{vore.Message} Please try again:");
                ConsoleHandler.HaltScreenUntilUserInput();
            }

            ++i_CurrentIndex;

            return i_CurrentIndex;
        }

        public void FillTyresAirPressureFromInput(string i_LicenseNumber)
        {
            int i = 0;
            Vehicle currentVehicle = m_Garage.GetVehicle(i_LicenseNumber);

            foreach (Wheel wheel in currentVehicle.Wheels)
            {
                SetPressureFromInput(i_LicenseNumber, ref i, wheel);
            }
        }

        private Wheel setSingleWheel(float i_MaxAirPressure)
        {
            Wheel newWheel = new Wheel();

            Console.WriteLine("Please enter the wheel's manufacturer name:");
            newWheel.ManufacturerName = Console.ReadLine();
            Console.WriteLine("Please enter the wheel's current psi:");
            newWheel.CurrentAirPressure = ConsoleHandler.readFloatFromConsole(0, i_MaxAirPressure);

            return newWheel;
        }

        private void initUniqueVehicleProperties() 
        {
            object[] getParameterForMethod = new object[1];

            foreach (MethodInfo method in m_NewVehicle.UniqueMethods)
            {
                if (method.GetParameters()[0].ParameterType.IsEnum)
                {
                    ConsoleHandler.EnumsConsoleMessage(method.GetParameters()[0].ParameterType);
                    getParameterForMethod[0] = ConsoleHandler.readEnumFromConsole(method.GetParameters()[0].ParameterType);
                    method.Invoke(m_NewVehicle, getParameterForMethod);
                }
                else
                {
                    Console.WriteLine(
                        string.Format(
                            "please enter {0} as {1}:",
                            ConsoleHandler.PrintCamelCase(method.Name.Remove(0, 4)),
                            method.GetParameters()[0].ParameterType.Name));
                    try
                    {
                        method.Invoke(m_NewVehicle, new[] { dynamicTryParse(method) });
                    }
                    catch (ArgumentNullException ane)
                    {
                        Console.WriteLine(ane);
                    }
                    catch (Exception)
                    {
                    }
                }
            }
        }

        private object dynamicTryParse(MethodInfo method)
        {
            Type type;
            MethodInfo dynamicTryPrase;
            object[] dynamicTryPraseParameters = new object[2];
            Type[] tryParseSignature;
            object isParsed = false;

            dynamicTryPraseParameters[1] = null;
            type = method.GetParameters()[0].ParameterType;
            tryParseSignature = new[] { typeof(string), type.MakeByRefType() };

            dynamicTryPrase = method.GetParameters()[0].ParameterType.GetMethod("TryParse", tryParseSignature);
            if (dynamicTryPrase == null)
            {
                throw new ArgumentNullException(string.Format(
                    "Error: No TryParse Method is found in {0}",
                    method.GetParameters()[0].ParameterType));
            }

            dynamicTryPraseParameters[0] = Console.ReadLine();
            isParsed = dynamicTryPrase.Invoke(type, dynamicTryPraseParameters);
            while (!Convert.ToBoolean(isParsed))
            {
                Console.WriteLine(string.Format("invalid input please enter {0}", type.Name));
                dynamicTryPraseParameters[0] = Console.ReadLine();
                isParsed = dynamicTryPrase.Invoke(type, dynamicTryPraseParameters);
            }

            return dynamicTryPraseParameters[1];
        }
    }
}
