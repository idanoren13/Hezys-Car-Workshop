using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ex03.ConsoleUI
{
    static class ConsoleHandler // todo stati?
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
    }
}
