using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ex03.GarageLogic
{
    public class DuplicateKeysException : ArgumentException
    {
        string r_Messege;
        
        public DuplicateKeysException(string s): base(s) 
        {
            r_Messege = s; 
        }

        public override string ToString()
        {
            return r_Messege;
        }
    }
}
