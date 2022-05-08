using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ex03.GarageLogic.VehicleParts
{
    public abstract class Engine
    {
        public enum eEngineType
        {
            Electricty,
            Fuel,
        }

        protected float m_CurrentEnergyCapacity;
        protected float m_MaxEnergyCapacity;
        private readonly eEngineType r_EngineType;

        protected Engine(eEngineType i_EngineType)
        {
            r_EngineType = i_EngineType;
        }

        public eEngineType EngineType
        {
            get => r_EngineType;
        }

        public float GetEnergyPercentage()
        {
            return m_CurrentEnergyCapacity / m_MaxEnergyCapacity * 100;
        }

        public void SetCurrentEnergyByPercentage(float i_Percentage)
        {
            m_CurrentEnergyCapacity = (i_Percentage / 100) * m_MaxEnergyCapacity;
        }

        protected void AddEnergy(float i_AddedVolume)
        {
            if (m_CurrentEnergyCapacity + i_AddedVolume > m_MaxEnergyCapacity ||
                m_CurrentEnergyCapacity + i_AddedVolume < 0)
            {
                throw new ValueOutOfRangeException(m_MaxEnergyCapacity, 0);
            }
            else
            {
                m_CurrentEnergyCapacity += i_AddedVolume;
                m_CurrentEnergyCapacity = m_MaxEnergyCapacity;
            }
        }
    }
}
