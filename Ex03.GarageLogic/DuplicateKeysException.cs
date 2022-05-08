using System;

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
