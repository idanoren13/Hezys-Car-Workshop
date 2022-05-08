using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ex03.GarageLogic.VehicleParts
{
    public class Wheel
    {
        private float m_CurrentAirPressure;
        private string m_ManufacturerName;
        private float m_MaxAirPressure;

        public float CurrentAirPressure
        {
            get => m_CurrentAirPressure;
            set => m_CurrentAirPressure = value;
        }

        public float MaxAirPressure
        {
            get => m_MaxAirPressure;
            set => m_MaxAirPressure = value;
        }

        public string ManufacturerName
        {
            get => m_ManufacturerName;
            set => m_ManufacturerName = value;
        }

        public void AddAir(int i_AddedPressure)
        {
            if (m_CurrentAirPressure + i_AddedPressure > m_MaxAirPressure ||
                m_CurrentAirPressure + i_AddedPressure < 0)
            {
                throw new ValueOutOfRangeException(m_MaxAirPressure, 0);
            }
            else
            {
                m_CurrentAirPressure += i_AddedPressure;
                m_CurrentAirPressure = m_MaxAirPressure;
            }
        }

        public void FillMaxPressure()
        {
            m_CurrentAirPressure = m_MaxAirPressure;
        }

        public override string ToString()
        {
            StringBuilder WheelToString = new StringBuilder();

            WheelToString.Append("Wheel manufacturer: ");
            WheelToString.Append(this.m_ManufacturerName);
            WheelToString.Append($"{Environment.NewLine}Wheel Pressure: {this.CurrentAirPressure} Psi ");

            return WheelToString.ToString();
        }
    }
}
