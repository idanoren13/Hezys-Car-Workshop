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
            get => m_CurrentEnergyCapacity;
            set => m_CurrentEnergyCapacity = value;
        }

        public float MaxBatteryCapacity
        {
            get => m_MaxEnergyCapacity;
            set => m_MaxEnergyCapacity = value;
        }

        void SuperCharge(float i_AddEnergy)
        {
            AddEnergy(i_AddEnergy);
        }

        public void SetCurrentBatteryByPercentage(float i_Percentage)
        {
            SetCurrentEnergyByPercentage(i_Percentage);
        }

        public override string ToString()
        {
            StringBuilder ElectricEngineToString = new StringBuilder();

            ElectricEngineToString.Append($"Battery time in hours Left: {this.m_CurrentEnergyCapacity} ");
            ElectricEngineToString.Append($"{Environment.NewLine}Max battery capacity: {this.m_MaxEnergyCapacity} ");

            return ElectricEngineToString.ToString();
        }
    }

}
