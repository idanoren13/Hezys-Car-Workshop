using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ex03.GarageLogic.VehicleParts
{
    public class CombustionEngine : Engine
    {
        public enum eFuelType
        {
            Soler,
            Octan95,
            Octan96,
            Octan98
        }

        private eFuelType m_FuelType;

        public CombustionEngine() : base(eEngineType.Fuel) { }

        public float CurrentFuelAmount
        {
            get => m_CurrentEnergy;
            set => m_CurrentEnergy = value;
        }

        public float MaxTankCapacity
        {
            get => m_MaxEnergyCapacity;
            set => m_MaxEnergyCapacity = value;
        }

        public eFuelType FuelType
        {
            get => m_FuelType;
            set => m_FuelType = value;
        }

        public void AddFuel(float i_AmountOfFuelToAdd, eFuelType i_FuelType)
        {
            float percentegeAmountToAdd = (i_AmountOfFuelToAdd / 100) * m_MaxEnergyCapacity;

            if (m_CurrentEnergy + percentegeAmountToAdd > m_MaxEnergyCapacity || i_AmountOfFuelToAdd < 0)
            {
                throw new ValueOutOfRangeException(m_MaxEnergyCapacity, 0);
            }
            else
            {
                m_CurrentEnergy += percentegeAmountToAdd;
            }
        }

        public void SetCurrentFuelAmountByPercentage(float i_Percentage)
        {
            SetCurrentEnergyByPercentage(i_Percentage);
        }

        public override string ToString()
        {
            StringBuilder ElectricEngineToString = new StringBuilder();
            float fuelPercentage = GetEnergyPercentage();

            ElectricEngineToString.Append($"Fuel left: {fuelPercentage}% Tank.");
            ElectricEngineToString.Append($"{Environment.NewLine}Full tank is: {this.m_MaxEnergyCapacity} litres.");

            return ElectricEngineToString.ToString();
        }
    }
}
