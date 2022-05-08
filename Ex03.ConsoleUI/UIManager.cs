﻿using System;
using System.Collections.Generic;
using System.Reflection;
using Ex03.GarageLogic;
using Ex03.GarageLogic.VehicleParts;

namespace Ex03.ConsoleUI
{
    public class UIManager
    {
        private const int r_MaxParams = 2;
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
                    Console.Clear();
                    ConsoleHandler.PrintMainMenu();
                    garageOptions = (Garage.eGarageOptions)ConsoleHandler.ReadEnumFromConsole(typeof(Garage.eGarageOptions));
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
                catch (ValueOutOfRangeException voofe)
                {
                    Console.WriteLine(voofe.ToString());
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
                    ConsoleHandler.DisplayReturnMenuMessage();
                }
            }
        }

        private void garageInterfaceManager(Garage.eGarageOptions i_GetCurrentOptions)
        {
            Console.Clear();
            switch (i_GetCurrentOptions)
            {
                case Garage.eGarageOptions.InsertVehicle:
                    insertNewVehicle();
                    break;
                case Garage.eGarageOptions.DisplayListOfLicensedVehicle:
                    displayListOfLicensedVehicles();
                    break;
                case Garage.eGarageOptions.FillTirePressure:
                    addTyrePressureFromInput();
                    break;
                case Garage.eGarageOptions.FillGas:
                    fillGas();
                    break;
                case Garage.eGarageOptions.ChangeVehicleState:
                    changeVehicleStatus();
                    break;
                case Garage.eGarageOptions.ChargeElectricVehicle:
                    ChargeElectricVehicle();
                    break;
                case Garage.eGarageOptions.ExtendedInformationOfSelectedVehicle:
                    printSpacificCarInfo();
                    break;
                case Garage.eGarageOptions.EndService:
                    m_IsGarageOpen = false;
                    break;
                default:
                    throw new FormatException("Invalid choice");
            } 
        }

        private void changeVehicleStatus()
        {
            string licenseNumber;

            Console.WriteLine("Please enter license number:");
            licenseNumber = Console.ReadLine();
            if (m_Garage.CheckIfVehicleExists(licenseNumber))
            {
                Console.WriteLine("select status:");
                ConsoleHandler.PrintEnum<Vehicle.eVehicleStatus>();
                m_Garage.GetVehicle(licenseNumber).Status = 
                    (Vehicle.eVehicleStatus)ConsoleHandler.ReadIntFromConsole(
                        0,
                        Enum.GetValues(typeof(Vehicle.eVehicleStatus)).Length - 1);
                ConsoleHandler.OperationSuccededMessage();
            }
            else
            {
                throw new KeyNotFoundException($"Vehicle with license number: {licenseNumber} is not in the garage");
            }
        }

        private void insertNewVehicle()
        {
            try
            {
                VehicleFactory.eVehicleType vehicleType;
                string model, licenseNumber, ownersName, ownersNumber;

                ConsoleHandler.PrintEnum<VehicleFactory.eVehicleType>();
                vehicleType = (VehicleFactory.eVehicleType)ConsoleHandler.ReadEnumFromConsole(typeof(VehicleFactory.eVehicleType));
                ConsoleHandler.GetBasicInfoFromConsole(vehicleType, out model, out licenseNumber, out ownersName, out ownersNumber);
                if (m_Garage.CheckIfVehicleExists(licenseNumber))
                {
                    throw new KeysCollisionException($"Error: {licenseNumber} Already exist! ");
                }

                m_NewVehicle = m_Garage.Factory.CreatVehicle(vehicleType, model, licenseNumber, ownersName, ownersNumber);
                if (m_NewVehicle.Engine == null)
                {
                    getEngineTypeInput();
                }

                m_NewVehicle.Engine.SetCurrentEnergyByPercentage(ConsoleHandler.GetEnergyPercentage(m_NewVehicle.Engine.EngineType)); // TODO return unused float??
                setNewVehicleWheels();
                initUniqueVehicleProperties();
                m_Garage.AddVehicle(m_NewVehicle, licenseNumber);
                ConsoleHandler.OperationSuccededMessage();
            }
            catch (KeysCollisionException dke)
            {
                Console.WriteLine(dke.ToString());
                m_Garage.GetVehicle(m_NewVehicle.LicenseNumber).Status = Vehicle.eVehicleStatus.InProcess;
                ConsoleHandler.DisplayReturnMenuMessage();
            }
        }

        private void displayListOfLicensedVehicles()
        {
            int choosenStatus, userInput;
            Dictionary<string, Vehicle>.KeyCollection PlatesList;
            string plates;

            Console.WriteLine("select dispaly option:");
            userInput = ConsoleHandler.Choose1Or0("plates filtered by our service current status option", "show all plates option");
            if (userInput == 0)
            {
                PlatesList = m_Garage.GetPlatesList();
                plates = m_Garage.PlatesToString(PlatesList);
                if (plates == null)
                {
                    plates = "~empty list of plates~";
                }

                Console.WriteLine(plates);
            }
            else
            {
                Console.WriteLine("choose status to filter the list with");
                ConsoleHandler.PrintEnum<Vehicle.eVehicleStatus>();
                choosenStatus = ConsoleHandler.ReadIntFromConsole(0, Enum.GetValues(typeof(Vehicle.eVehicleStatus)).Length);
                PlatesList = m_Garage.GetSortedListFilterdByStatus((Vehicle.eVehicleStatus)choosenStatus);
                plates = m_Garage.PlatesToString(PlatesList);
                if (plates == null)
                {
                    plates = "~empty list of plates~";
                }

                Console.WriteLine(plates);
            }
        
            ConsoleHandler.DisplayReturnMenuMessage();
        }

        private void fillGas()
        {
            string licenseNumber, amountToFill;
            CombustionEngine.eFuelType fuelType;

            Console.WriteLine("Please enter the license number.");
            licenseNumber = Console.ReadLine();
            if (!m_Garage.CheckIfVehicleExists(licenseNumber))
            {
                throw new KeyNotFoundException($"Vehicle with license number: {licenseNumber} is not in the garage");
            }
            else
            {
                m_Garage.CheckIfEngineIsCombustion(licenseNumber);
                Console.WriteLine("Please select fuel type:");
                ConsoleHandler.PrintEnum<CombustionEngine.eFuelType>();
                fuelType = (CombustionEngine.eFuelType)ConsoleHandler.ReadEnumFromConsole(typeof(CombustionEngine.eFuelType));
                Console.WriteLine(m_Garage.GetVehicle(licenseNumber).Engine.ToString());
                Console.WriteLine("Please select the amount of fuel to add in percenge <0 - 100>%");
                amountToFill = Console.ReadLine();
                m_Garage.AddFuel(licenseNumber, fuelType, float.Parse(amountToFill));
                ConsoleHandler.OperationSuccededMessage();
            }
        }

        private void ChargeElectricVehicle()
        {
            string licenseNumber, energyToAdd;

            Console.WriteLine("Please enter the license number.");
            licenseNumber = Console.ReadLine();
            if (!m_Garage.CheckIfVehicleExists(licenseNumber))
            {
                throw new KeyNotFoundException($"Vehicle with license number: {licenseNumber} is not in the garage");
            }
            else
            {
                m_Garage.CheckIfEngineIsElectric(licenseNumber);
                Console.WriteLine(m_Garage.GetVehicle(licenseNumber).Engine.ToString());
                Console.WriteLine("Please select the amount of energy to charge percenge <0 - 100>%");
                energyToAdd = Console.ReadLine();
                m_Garage.Charge(licenseNumber, float.Parse(energyToAdd));
                ConsoleHandler.OperationSuccededMessage();
            }
        }

        private void printSpacificCarInfo()
        {
            Vehicle vehicleToDisplay;

            Console.WriteLine($"Please enter the license number you wish .");
            vehicleToDisplay = m_Garage.GetVehicle(Console.ReadLine());
            displayAllVehicleInfo(vehicleToDisplay);
            ConsoleHandler.DisplayReturnMenuMessage();
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
            engineType = (Engine.eEngineType)ConsoleHandler.ReadEnumFromConsole(typeof(Engine.eEngineType));
            m_NewVehicle.Engine = m_Garage.Factory.CreateEngine(engineType, m_NewVehicle.VehicleType);
        }

        private void setNewVehicleWheels()
        {
            int userChoice;
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

            userChoice = ConsoleHandler.Choose1Or0("All tyres at once", "Each tyre individual");
            if (userChoice == 0)
            {
                fillTyresAirPressureFromInput(licenseNumber);
            }
            else
            {
                m_Garage.FillTyresMaxAirPressure(licenseNumber);
            }

            ConsoleHandler.OperationSuccededMessage();
            ConsoleHandler.DisplayReturnMenuMessage();
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
            catch (ValueOutOfRangeException voore)
            {
                Console.WriteLine("can not add pressure over the max pressure!");
                Console.WriteLine(voore.ToString());
            }

            ++i_CurrentIndex;

            return i_CurrentIndex;
        }

        private void fillTyresAirPressureFromInput(string i_LicenseNumber)
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
            bool isValueOutOfRangeExceptionOccured = false;

            foreach (MethodInfo method in m_NewVehicle.UniqueMethods)
            {
                if (method.GetParameters()[0].ParameterType.IsEnum)
                {
                    ConsoleHandler.EnumsConsoleMessage(method.GetParameters()[0].ParameterType);
                    getParameterForMethod[0] = ConsoleHandler.ReadEnumFromConsole(method.GetParameters()[0].ParameterType);
                    method.Invoke(m_NewVehicle, getParameterForMethod);
                }
                else
                {
                    Console.WriteLine(
                        string.Format(
                            "Please enter {0} as {1}:",
                            ConsoleHandler.PrintCamelCase(method.Name.Remove(0, 4)),
                            method.GetParameters()[0].ParameterType.Name));
                    do
                    {
                        try
                        {
                            getParameterForMethod[0] = dynamicTryParse(method);
                            method.Invoke(m_NewVehicle, getParameterForMethod);
                            isValueOutOfRangeExceptionOccured = false;
                        } 
                        catch (TargetInvocationException tie)
                        {
                            Console.WriteLine(tie.InnerException.ToString());
                            isValueOutOfRangeExceptionOccured = true;
                        }
                        catch (ValueOutOfRangeException voore)
                        {
                            Console.WriteLine(voore.ToString());
                            isValueOutOfRangeExceptionOccured = true;
                        }
                        catch (ArgumentNullException ane)
                        {
                            Console.WriteLine(ane.Message);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                        }
                    } while (isValueOutOfRangeExceptionOccured);
                }
            }
        }

        private object dynamicTryParse(MethodInfo i_Method)
        {
            Type typeToBeParsed;
            MethodInfo dynamicTryPrase;
            object[] dynamicTryPraseParameters = new object[r_MaxParams];
            Type[] tryParseSignature;
            object isParsed;

            dynamicTryPraseParameters[1] = null;
            typeToBeParsed = i_Method.GetParameters()[0].ParameterType;
            tryParseSignature = new Type[]
            {
                typeof(string), typeToBeParsed.MakeByRefType()
            };

            dynamicTryPrase = i_Method.GetParameters()[0].ParameterType.GetMethod("TryParse", tryParseSignature);
            if (dynamicTryPrase == null)
            {
                throw new ArgumentNullException(string.Format(
                    "Error: No TryParse Method is found in {0}",
                    i_Method.GetParameters()[0].ParameterType));
            }

            dynamicTryPraseParameters[0] = Console.ReadLine();
            isParsed = dynamicTryPrase.Invoke(typeToBeParsed, dynamicTryPraseParameters);
            while (!Convert.ToBoolean(isParsed))
            {
                Console.WriteLine(string.Format("invalid input please enter {0}", typeToBeParsed.Name));
                dynamicTryPraseParameters[0] = Console.ReadLine();
                isParsed = dynamicTryPrase.Invoke(typeToBeParsed, dynamicTryPraseParameters);
            }

            return dynamicTryPraseParameters[1];
        }
    }
}
