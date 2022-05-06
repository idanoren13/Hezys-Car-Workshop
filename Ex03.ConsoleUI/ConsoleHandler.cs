using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using Ex03.GarageLogic; 
using System.Reflection;

namespace Ex03.ConsoleUI
{
    static class ConsoleHandler // todo static? yes
    {
        public static void InvalidInputMessage()
        {
            Console.WriteLine("Invalid Input, enter again");
        }

        public static void EnumsConsoleMessage(Type i_MatchingTypeEnum)
        {
            int i = 0;
            StringBuilder messageToScreen = new StringBuilder();

            foreach (Enum enumType in Enum.GetValues(i_MatchingTypeEnum))
            {
                messageToScreen.Append(string.Format("Press {0} to pick a {1}{2}", i, enumType.ToString() ,Environment.NewLine));
                ++i;
            }

            Console.WriteLine(messageToScreen);
        }

        public static void NewVehilceMessage(PropertyInfo[] i_VehicleProperties)
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
