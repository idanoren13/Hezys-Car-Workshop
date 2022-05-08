using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ex03.GarageLogic.VehicleParts
{
    public class ElectricEngine : Engine
    {
        public ElectricEngine() : base(eEngineType.Electricty) { }

        public float BatteryTimeLeft
        {
            get => m_CurrentEnergy;
            set => m_CurrentEnergy = value;
        }

        public float MaxBatteryCapacity
        {
            get => m_MaxEnergyCapacity;
            set => m_MaxEnergyCapacity = value;
        }

        public void SuperCharge(float i_EnergyToAdd)
        {
            float percentegeAmountToAdd = (i_EnergyToAdd / 100) * m_MaxEnergyCapacity;

            if (m_CurrentEnergy + percentegeAmountToAdd > m_MaxEnergyCapacity ||
               i_EnergyToAdd < 0)
            {
                throw new ValueOutOfRangeException(m_MaxEnergyCapacity, 0);
            }
            else
            {
                BatteryTimeLeft = m_CurrentEnergy + percentegeAmountToAdd;
            }
        }

        public void SetCurrentBatteryByPercentage(float i_Percentage)
        {
            SetCurrentEnergyByPercentage(i_Percentage);
        }

        public override string ToString()
        {
            StringBuilder ElectricEngineToString = new StringBuilder();

            ElectricEngineToString.Append($"Battery time in hours Left: {this.m_CurrentEnergy} ");
            ElectricEngineToString.Append($"{Environment.NewLine}Max battery capacity: {this.m_MaxEnergyCapacity} ");

            return ElectricEngineToString.ToString();
        }
    }
}
