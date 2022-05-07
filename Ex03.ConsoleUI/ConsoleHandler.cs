using System;
using System.Collections.Generic;
using System.Text;
using Ex03.GarageLogic;
using System.Reflection;

namespace Ex03.ConsoleUI
{
    public static class ConsoleHandler
    {
        public static void InvalidInputMessage()
        {
            Console.WriteLine("Invalid Input, enter again");
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

        //TODO: bad method you ignored checkers, now checkers here this method checks logics right after input move to mngre
        public static void GetBasicInfoFromConsole(VehicleFactory.eVehicleType i_VehicleType, out string o_Model, out string o_LicenseNumber, out string o_OwnersName, out string o_OwnersNumber)
        {
            Console.WriteLine(string.Format("Please enter your name: "));
            o_OwnersName = Console.ReadLine();
            Console.WriteLine(string.Format("Please enter your phone number: "));
            o_OwnersNumber = Console.ReadLine();
            UIManager.CheckPhoneNumberInput(o_OwnersNumber);
            Console.WriteLine(string.Format("Please enter your {0} model: ", i_VehicleType.ToString()));
            o_Model = Console.ReadLine();
            Console.WriteLine(string.Format("Please enter your license number: "));
            o_LicenseNumber = Console.ReadLine();
            UIManager.CheckLicenseNumberInput(o_LicenseNumber);
        }

        public static float GetEnergyPercentage(VehicleParts.Engine.eEngineType i_EngineType)
        {
            float energyPercentage;
            //todo 0 or 1 is not precentege idk what u mean
            Console.WriteLine(string.Format("Please enter your {0} percentage", i_EngineType == VehicleParts.Engine.eEngineType.Fuel ? "fuel tank" : "battery"));
            while (!float.TryParse(Console.ReadLine(), out energyPercentage) || (energyPercentage > 1 || energyPercentage < 0))
            {
                Console.WriteLine("Invalid input please enter a value between 0 to 1"); // todo use InvalidInputMessage() or use 0 or 1 generic func for all cases
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
            messageToScreen.Append(string.Format("Press 1 to insert a new vehicle", Environment.NewLine));
            messageToScreen.Append(string.Format("Press 2 to display a list of the licensed vehicles within our garage.{0}", Environment.NewLine));
            messageToScreen.Append(string.Format("Press 3 to fill a selected vehicle tyres.{0}", Environment.NewLine));
            messageToScreen.Append(string.Format("Press 4 to fill gas to a vehicle with combustion based Engine. {0}", Environment.NewLine));
            messageToScreen.Append(string.Format("Press 5 to change the state of your vehicle .{0}", Environment.NewLine));
            messageToScreen.Append(string.Format("Press 6 to super charge a vehicle with electric based engine.  {0}", Environment.NewLine));
            messageToScreen.Append(string.Format("Press 7 to get an extended information on a vehicle. {0}", Environment.NewLine));
            messageToScreen.Append(string.Format("Press 8 to exit from the garage. {0}", Environment.NewLine));
            messageToScreen.Append(string.Format("Please enter your choice:", Environment.NewLine));

            Console.WriteLine(messageToScreen);
        }
    }
}
