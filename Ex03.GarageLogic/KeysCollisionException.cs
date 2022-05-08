using System;

namespace Ex03.GarageLogic
{
    public class KeysCollisionException : Exception
    {
        private readonly string r_Messege;
        
        public KeysCollisionException(string s) : base(s) 
        {
            r_Messege = s; 
        }

        public override string ToString()
        {
            return r_Messege;
        }
    }
}
