using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ex03.GarageLogic
{
    public class ValueOutOfRangeException : Exception
    {
        private readonly float m_MaxValue;
        private readonly float m_MinValue;

        public ValueOutOfRangeException(float i_MaxValue, float i_MinValue)
        {
            m_MaxValue = i_MaxValue;
            m_MinValue = i_MinValue;
        }

        public float MaxValue
        { 
            get => m_MaxValue; 
        }

        public float MinValue
        {
            get => m_MinValue;
        }

        public override string ToString()
        {
            return string.Format("Error: invalid value the inserted value is out of range minimum:{0} maxsimum:{1}", m_MinValue, m_MaxValue);
        }
    }
}
