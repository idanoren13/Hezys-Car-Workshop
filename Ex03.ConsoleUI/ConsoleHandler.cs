using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Threading;
using Ex03.GarageLogic;
using Ex03.GarageLogic.VehicleParts;

namespace Ex03.ConsoleUI
{
    public static class ConsoleHandler
    {
        public const string k_NegativeInput = "Error: negative phone number!";

        public static void InvalidInputMessage()
        {
            Console.WriteLine("Invalid Input, enter again");
        }

        public static void OperationSuccededMessage()
        {
            Console.WriteLine("Operation Succeded!");
            Thread.Sleep(700);
        }

        public static void EnumsConsoleMessage(Type i_MatchingTypeEnum)
        {
            int i = 0;
            StringBuilder messageToScreen = new StringBuilder();

            messageToScreen.Append(string.Format("to select {0}:{1}", PrintEnumName(i_MatchingTypeEnum), Environment.NewLine));
            foreach (Enum enumType in Enum.GetValues(i_MatchingTypeEnum))
            {
                messageToScreen.Append(string.Format("Press {0} to pick a {1}{2}", i, enumType.ToString(), Environment.NewLine));
                ++i;
            }

            messageToScreen.Remove(messageToScreen.Length - 1, 1);
            Console.WriteLine(messageToScreen);
        }

        public static string PrintEnumName(Type i_EnumType)
        {
            string shortendEnumName = i_EnumType.Name.Remove(0, 1);

            shortendEnumName = PrintCamelCase(shortendEnumName);

            return shortendEnumName;
        }

        public static string PrintCamelCase(string i_PropertyName)
        {
            for (int i = 0; i < i_PropertyName.Length - 1; i++)
            {
                if (char.IsLower(i_PropertyName[i]) && char.IsUpper(i_PropertyName[i + 1]))
                {
                    i_PropertyName = i_PropertyName.Insert(i + 1, " ");
                }
            }

            return i_PropertyName;
        }

        public static void UniquePropertiesVehilceMessage(List<PropertyInfo> i_VehicleProperties)
        {
            int i = 0;
            StringBuilder messageToScreen = new StringBuilder();

            foreach (PropertyInfo property in i_VehicleProperties)
            {
                messageToScreen.Append(string.Format("Press {0} to initilize {1}{2}", i, property.Name, Environment.NewLine));
                i++;
            }

            Console.WriteLine(messageToScreen);
        }

        public static void GetBasicInfoFromConsole(
            VehicleFactory.eVehicleType i_VehicleType, 
            out string o_Model,
            out string o_LicenseNumber, 
            out string o_OwnersName, 
            out string o_OwnersNumber)
        {
            o_LicenseNumber = null;
            o_OwnersNumber = null;
            Console.WriteLine(string.Format("Please enter your name: "));
            o_OwnersName = Console.ReadLine();
            getAndCheckPhoneNumberInput(ref o_OwnersNumber);
            Console.WriteLine(string.Format("Please enter your {0} model: ", i_VehicleType.ToString()));
            o_Model = Console.ReadLine();
            getAndCheckLicenseNumberInput(ref o_LicenseNumber);    
        }

        private static void getAndCheckPhoneNumberInput(ref string io_Input)
        {
            Console.WriteLine(string.Format("Please enter your phone number: "));
            io_Input = Console.ReadLine();
            while (!checkValidNumberInString(io_Input, k_NegativeInput))
            {
                io_Input = Console.ReadLine();
            }
        }

        private static void getAndCheckLicenseNumberInput(ref string io_Input)
        {
            Console.WriteLine(string.Format("Please enter your license number: "));
            io_Input = Console.ReadLine();
            while (!checkValidNumberInString(io_Input, k_NegativeInput))
            {
                io_Input = Console.ReadLine();
            }
        }

        private static bool checkValidNumberInString(string i_Input, string i_NegativeMessege)
        {
            bool isValid = true;
            if (!long.TryParse(i_Input, out long parsedInteger))
            {
                isValid = false;
                Console.WriteLine("Error: failed parse to a natural number");
            }
            else if (parsedInteger < 0)
            {
                isValid = false;
                Console.WriteLine(i_NegativeMessege);
            }

            return isValid;
        }

        public static float GetEnergyPercentage(Engine.eEngineType i_EngineType)
        {
            float energyPercentage;

            Console.WriteLine(string.Format("Please enter your current {0} percentage <0 - 100> ", i_EngineType == Engine.eEngineType.Fuel ? "fuel tank" : "battery"));
            while (!float.TryParse(Console.ReadLine(), out energyPercentage) || (energyPercentage > 100 || energyPercentage < 0))
            {
                Console.WriteLine("Invalid input please enter a value between 0 to 100"); 
            }

            return energyPercentage;
        }
        
        public static void PrintEnum<genericEnum>()
        {
            genericEnum enumType = default;
            Type type = enumType.GetType();

            EnumsConsoleMessage(type);
        }

        public static void PrintMainMenu()
        {
            StringBuilder messageToScreen = new StringBuilder();

            messageToScreen.Append(string.Format("Welcome to I&D garage!{0}", Environment.NewLine));
            messageToScreen.Append(string.Format("Menu options : {0}", Environment.NewLine));
            messageToScreen.Append(string.Format("Press 1 to insert a new vehicle{0}", Environment.NewLine));
            messageToScreen.Append(string.Format("Press 2 to display a list of the licensed vehicles within our garage.{0}", Environment.NewLine));
            messageToScreen.Append(string.Format("Press 3 to fill a selected vehicle tyres.{0}", Environment.NewLine));
            messageToScreen.Append(string.Format("Press 4 to fill gas to a vehicle with combustion based Engine. {0}", Environment.NewLine));
            messageToScreen.Append(string.Format("Press 5 to change the state of your vehicle .{0}", Environment.NewLine));
            messageToScreen.Append(string.Format("Press 6 to super charge a vehicle with electric based engine.  {0}", Environment.NewLine));
            messageToScreen.Append(string.Format("Press 7 to get an extended information on a vehicle. {0}", Environment.NewLine));
            messageToScreen.Append(string.Format("Press 0 to exit from the garage. {0}", Environment.NewLine));
            messageToScreen.Append(string.Format("Please enter your choice:", Environment.NewLine));

            Console.WriteLine(messageToScreen);
        }

        public static int Choose1Or0(string i_Messege1, string i_Messege0)
        {
            int userChoice;
            Console.WriteLine("Press 0 to set {2}{1}Press 1 to set {0}", i_Messege1, Environment.NewLine, i_Messege0);
            while (!int.TryParse(Console.ReadLine(), out userChoice) || (userChoice != 1 && userChoice != 0))
            {
                Console.WriteLine("Invalid input press 1/0");
            }

            return userChoice;
        }

        public static int ReadEnumFromConsole(Type i_EnumType)
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

        public static int ReadIntFromConsole(int i_MinValue, int i_MaxValue)
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

        public static float readFloatFromConsole(float i_MinValue, float i_MaxValue)
        {
            float parseFloat = default;
            bool exceptionFlag = false;

            while (!exceptionFlag)
            {
                try
                {
                    if (!float.TryParse(Console.ReadLine(), out parseFloat))
                    {
                        throw new FormatException("Error: failed parsing to float.");
                    }
                    else if (parseFloat < i_MinValue || parseFloat > i_MaxValue)
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

            return parseFloat;
        }

        public static void DisplayReturnMenuMessage()
        {
            Console.WriteLine("Press any key to return the main menu");
            Console.ReadKey();
        }
    }
}
