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

        protected float m_CurrentEnergy;
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
            return m_CurrentEnergy / m_MaxEnergyCapacity * 100;
        }

        public void SetCurrentEnergyByPercentage(float i_Percentage)
        {
            m_CurrentEnergy = (i_Percentage / 100) * m_MaxEnergyCapacity;
        }

        protected void AddEnergy(float i_AddedEnergy)
        {
            if (m_CurrentEnergy + i_AddedEnergy > m_MaxEnergyCapacity ||
                m_CurrentEnergy + i_AddedEnergy < 0)
            {
                throw new ValueOutOfRangeException(m_MaxEnergyCapacity, 0);
            }
            else
            {
                m_CurrentEnergy += i_AddedEnergy;
                m_CurrentEnergy = m_MaxEnergyCapacity;
            }
        }
    }
}
